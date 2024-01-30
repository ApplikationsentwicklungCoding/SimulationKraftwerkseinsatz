using System.Diagnostics;
using System.Text;

namespace LibCx9020;

public class Cx9020
{
    private static readonly log4net.ILog s_log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

    public enum PlcDaemonStatus
    {
        SpsPingStarten = 0,
        SpsPingAbwarten = 1,
        SpsBeckhoff = 2
    }
    public enum PlcPingStatus
    {
        Pingen,
        Gefunden
    }

    public string? PlcAktuellerSchritt { get; set; }
    public PlcDaemonStatus PlcStatus { get; set; }
    private readonly CxDaten _cxDaten;

    private readonly byte[] _pcToPlc;
    private readonly byte[] _plcToPc;

    private readonly PlcBeckhoff _plcBeckhoff;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private TimeSpan _zyklusZeitMin;
    private TimeSpan _zyklusZeitMax;

    public Cx9020(CxDaten cxDaten, CancellationTokenSource cancellationTokenSource)
    {
        s_log.Debug("PLC Daemon gestartet");

        _cxDaten = cxDaten;
        _cancellationTokenSource = cancellationTokenSource;

        PlcStatus = PlcDaemonStatus.SpsPingStarten;
        _cancellationTokenSource = cancellationTokenSource;

        _pcToPlc = new byte[1024];
        _plcToPc = new byte[1024];

        _plcBeckhoff = new PlcBeckhoff(_pcToPlc, _plcToPc);

        s_log.Debug("SPS pingen");
        _ = Task.Run(Cx9020Task);
    }
    private void Cx9020Task()
    {
        ResetPlcInfo();
        var stopwatchZykluszeit = new Stopwatch();
        var stopwatchPingAbwarten = new Stopwatch();


        while (!_cancellationTokenSource.IsCancellationRequested)
        {
            switch (PlcStatus)
            {
                case PlcDaemonStatus.SpsPingStarten:
                    s_log.Debug("SpsPingStarten");

                    PlcAktuellerSchritt = "Ping starten - ";

                    _plcBeckhoff.PlcSuchen();

                    PlcStatus = PlcDaemonStatus.SpsPingAbwarten;
                    stopwatchPingAbwarten.Restart();
                    break;

                case PlcDaemonStatus.SpsPingAbwarten:
                    PlcAktuellerSchritt = "Ping abwarten - ";

                    if (_plcBeckhoff.PlcGefunden())
                    {
                        s_log.Debug("Beckhoff gefunden");
                        PlcAktuellerSchritt = "Beckhoff gefunden - ";
                        PlcStatus = PlcDaemonStatus.SpsBeckhoff;
                        continue;
                    }

                    if (stopwatchPingAbwarten.Elapsed > new TimeSpan(0, 0, 2))
                    {
                        stopwatchPingAbwarten.Stop();
                        PlcStatus = PlcDaemonStatus.SpsPingStarten;
                    }
                    break;

                case PlcDaemonStatus.SpsBeckhoff:
                    PlcAktuellerSchritt = "";
                    if (_plcBeckhoff.PlcBeckhoffTask())
                    {
                        s_log.Debug("Kommunikationsproblem Beckhoff");
                        PlcStatus = PlcDaemonStatus.SpsPingStarten;
                    }
                    else { DatenPcToPlcRangieren(); }
                    break;


                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (stopwatchZykluszeit.Elapsed < _zyklusZeitMin) { _zyklusZeitMin = stopwatchZykluszeit.Elapsed; }
            if (stopwatchZykluszeit.Elapsed > _zyklusZeitMax) { _zyklusZeitMax = stopwatchZykluszeit.Elapsed; }

            stopwatchZykluszeit.Restart();
            Thread.Sleep(10);
        }

        s_log.Debug("cancellationTokenSource true!");
    }
    private void DatenPcToPlcRangieren()
    {
        const int anzDi = 32;
        const int anzAi = 64;
        const int anzBefehle = 32;

        const int anzDa = 32;
        const int anzAa = 64;
        const int anzVersionsbez = 64;
        // ReSharper disable InlineTemporaryVariable
        const int anfangDi = 0;
        const int anfangAi = anzDi;
        const int anfangBefehle = anzDi + anzAi;

        const int anfangDa = 0;
        const int anfangAa = anzDa;
        const int anfangVersion = anzDa + anzAa;

        var versionsStringPlc = new byte[256];

        Buffer.BlockCopy(_cxDaten.Di, 0, _pcToPlc, anfangDi, anzDi);
        Buffer.BlockCopy(_cxDaten.Ai, 0, _pcToPlc, anfangAi, anzAi);
        Buffer.BlockCopy(_cxDaten.BefehlePlc, 0, _pcToPlc, anfangBefehle, anzBefehle);

        Buffer.BlockCopy(_plcToPc, anfangDa, _cxDaten.Da, 0, anzDa);
        Buffer.BlockCopy(_plcToPc, anfangAa, _cxDaten.Aa, 0, anzAa);
        Buffer.BlockCopy(_plcToPc, anfangVersion, versionsStringPlc, 0, anzVersionsbez);

        var textLaenge = 0;
        for (var i = 0; i < 255; i++) { if (versionsStringPlc[i] != 0) { textLaenge = i + 1; } }
        var enc = new ASCIIEncoding();
        _cxDaten.VersionsStringPlc = enc.GetString(versionsStringPlc, 0, textLaenge);
    }
    public void ResetPlcInfo()
    {
        _zyklusZeitMax = new TimeSpan();
        _zyklusZeitMin = new TimeSpan(12345, 23, 59, 59);
    }
}
