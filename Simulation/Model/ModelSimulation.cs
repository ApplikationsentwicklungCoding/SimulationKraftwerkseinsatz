using LibCx9020;

namespace Simulation.Model;

public class ModelSimulation
{
    private static readonly log4net.ILog s_log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

    public byte StatusPotentiometer1 { get; set; }
    public byte StatusPotentiometer2 { get; set; }
    public byte StatusPotentiometer3 { get; set; }
    public byte StatusPotentiometer4 { get; set; }
    public byte StatusPotentiometer5 { get; set; }
    public byte StatusPotentiometer6 { get; set; }


    public int ValuePotentiometer1 { get; set; }
    public int ValuePotentiometer2 { get; set; }
    public int ValuePotentiometer3 { get; set; }
    public int ValuePotentiometer4 { get; set; }
    public int ValuePotentiometer5 { get; set; }
    public int ValuePotentiometer6 { get; set; }


    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly DatenRangieren _datenRangieren;

    public ModelSimulation(CancellationTokenSource cancellationTokenSource)
    {
        s_log.Debug("gestartet!");

        _cancellationTokenSource = cancellationTokenSource;

        var cxDaten = new CxDaten();
        _ = new Cx9020(cxDaten, cancellationTokenSource);
        _datenRangieren = new DatenRangieren(this, cxDaten);

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
}
