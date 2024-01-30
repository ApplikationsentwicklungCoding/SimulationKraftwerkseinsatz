using Newtonsoft.Json;
using System.Net.NetworkInformation;
using TwinCAT.Ads;

namespace LibCx9020;

public class PlcBeckhoff
{

    private enum BeckhoffStatus
    {
        Initialisieren = 0,
        Verbinden = 1,
        Kommunizieren = 2
    }

    private static readonly log4net.ILog s_log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

    private readonly AdsClient _adsClient;
    private readonly IpAdressenBeckhoff? _ipAdresse;
    private readonly byte[] _pcToPlcBeckhoff;
    private readonly byte[] _plcBeckhoffToPc;
    private BeckhoffStatus _status;
    private uint _handlePcToPlc;
    private uint _handlePlcToPc;
    private readonly Ping _ping = new();
    private Cx9020.PlcPingStatus _pingStatus;

    public PlcBeckhoff(byte[] pcToPlc, byte[] plcToPc)
    {
        s_log.Debug("gestartet!");

        _pcToPlcBeckhoff = pcToPlc;
        _plcBeckhoffToPc = plcToPc;
        for (var i = 0; i < _plcBeckhoffToPc.Length; i++)
        {
            _plcBeckhoffToPc[i] = 0;
        }


        try
        {
            _ipAdresse = JsonConvert.DeserializeObject<IpAdressenBeckhoff>(File.ReadAllText("IpAdressenBeckhoff.json"));
        }
        catch (Exception ex)
        {
            s_log.Debug("Datei nicht gefunden: IpAdressenBeckhoff.json" + ex);
        }

        _ping.PingCompleted += (_, args) =>
        {
            if (args.Reply == null) { return; }

            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (args.Reply.Status)
            {
                case IPStatus.Success:
                    s_log.Debug("Beckhoff SPS gefunden");
                    _pingStatus = Cx9020.PlcPingStatus.Gefunden;
                    break;

                case IPStatus.TimedOut:
                    if (_pingStatus == Cx9020.PlcPingStatus.Pingen) { _ping.SendAsync(_ipAdresse?.IpAdresse!, 2000, null); }
                    break;

                case IPStatus.DestinationHostUnreachable: break;

                default:
                    s_log.Debug($"Siemens Ping: {_ipAdresse?.IpAdresse} -> {args.Reply.Status}");
                    break;
            }
        };

        _adsClient = new AdsClient();
        _status = BeckhoffStatus.Initialisieren;
    }
    public bool PlcBeckhoffTask()
    {

        switch (_status)
        {
            case BeckhoffStatus.Initialisieren:
                s_log.Debug("ADS initialisieren");

                try
                {
                    _adsClient.Connect(_ipAdresse?.AmsNetId!, _ipAdresse!.Port);
                    _handlePcToPlc = _adsClient.CreateVariableHandle("_PcToPlc.PcToPlc");
                    _handlePlcToPc = _adsClient.CreateVariableHandle("_PlcToPc.PlcToPc");
                    _status = BeckhoffStatus.Verbinden;
                }
                catch (Exception e)
                {
                    s_log.Debug("Beckhoff Initialisieren Exception: " + e);
                    return true;
                }
                break;

            case BeckhoffStatus.Verbinden:
                if (_adsClient.IsConnected)
                {
                    s_log.Debug("ADS verbunden");
                    _status = BeckhoffStatus.Kommunizieren;
                }
                break;

            case BeckhoffStatus.Kommunizieren:
                try
                {
                    var plcToPc = (byte[]) _adsClient.ReadAny(_handlePlcToPc, typeof(byte[]), [256]);
                    for (var i = 0; i < plcToPc.Length; i++)
                    {
                        _plcBeckhoffToPc[i] = plcToPc[i];
                    }

                    _adsClient.WriteAny(_handlePcToPlc, _pcToPlcBeckhoff);
                }
                catch (Exception e)
                {
                    s_log.Debug("Beckhoff Kommunizieren Exception: " + e);
                    throw;
                }
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        return false;
    }
    public void PlcSuchen()
    {
        _pingStatus = Cx9020.PlcPingStatus.Pingen;
        _ping.SendAsync(_ipAdresse?.IpAdresse!, 1000, null);
    }
    public bool PlcGefunden() => _pingStatus == Cx9020.PlcPingStatus.Gefunden;
}
