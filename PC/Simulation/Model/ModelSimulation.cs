using LibCx9020;

namespace Simulation.Model;

public class ModelSimulation
{
    private static readonly log4net.ILog s_log =
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

    public byte StatusPotentiometer11 { get; set; }
    public byte StatusPotentiometer12 { get; set; }
    public byte StatusPotentiometer13 { get; set; }
    public byte StatusPotentiometer14 { get; set; }
    public byte StatusPotentiometer15 { get; set; }

    public byte StatusPotentiometer21 { get; set; }
    public byte StatusPotentiometer22 { get; set; }
    public byte StatusPotentiometer23 { get; set; }
    public byte StatusPotentiometer24 { get; set; }
    public byte StatusPotentiometer25 { get; set; }

    public int ValuePotentiometer11 { get; set; }
    public int ValuePotentiometer12 { get; set; }
    public int ValuePotentiometer13 { get; set; }
    public int ValuePotentiometer14 { get; set; }
    public int ValuePotentiometer15 { get; set; }

    public int ValuePotentiometer21 { get; set; }
    public int ValuePotentiometer22 { get; set; }
    public int ValuePotentiometer23 { get; set; }
    public int ValuePotentiometer24 { get; set; }
    public int ValuePotentiometer25 { get; set; }

    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly DatenRangieren _datenRangieren;
    private readonly Cx9020 _cx9020;
    private readonly CxDaten _cxDaten;

    public ModelSimulation(CancellationTokenSource cancellationTokenSource)
    {
        s_log.Debug("gestartet!");

        _cancellationTokenSource = cancellationTokenSource;

        _cxDaten = new CxDaten();
        _cx9020 = new Cx9020(_cxDaten, cancellationTokenSource);
        _datenRangieren = new DatenRangieren(this, _cxDaten);

        _ = Task.Run(ModelSimulationTask);
    }

    private void ModelSimulationTask()
    {
        while (!_cancellationTokenSource.IsCancellationRequested)
        {
            _datenRangieren.Rangieren();

            Thread.Sleep(100);
        }
    }

    public string GetKommunikationInfo() => _cx9020.GetPlcStatus();
    public string? GetVersionsString() => _cxDaten.VersionsStringPlc;
}
