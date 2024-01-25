using CommunityToolkit.Mvvm.ComponentModel;
using Simulation.Model;
using System.Drawing;

namespace Simulation.ViewModel;

public partial class VmSimulation : ObservableObject
{
    private readonly ModelSimulation _modelSimulation;

    public VmSimulation(ModelSimulation modelSimulation, CancellationTokenSource cancellationTokenSource)
    {
        _modelSimulation= modelSimulation;

        BrushStarttaster = Brushes.LawnGreen;

        StringPotentiometer1 = "";
        StringPotentiometer2 = "";
        StringPotentiometer3 = "";
        StringPotentiometer4 = "";
        StringPotentiometer5 = "";
        StringPotentiometer6 = "";
        
        _ = Task.Run(() => PlcStarterTask(cancellationTokenSource.Token));
    }

    private void PlcStarterTask(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            StringPotentiometer1 = PotiWertAnzeigen(_modelSimulation.ValuePotentiometer1, _modelSimulation.StatusPotentiometer1);
            StringPotentiometer2 = PotiWertAnzeigen(_modelSimulation.ValuePotentiometer2, _modelSimulation.StatusPotentiometer2);
            StringPotentiometer3 = PotiWertAnzeigen(_modelSimulation.ValuePotentiometer3, _modelSimulation.StatusPotentiometer3);
            StringPotentiometer4 = PotiWertAnzeigen(_modelSimulation.ValuePotentiometer4, _modelSimulation.StatusPotentiometer4);
            StringPotentiometer5 = PotiWertAnzeigen(_modelSimulation.ValuePotentiometer5, _modelSimulation.StatusPotentiometer5);
            StringPotentiometer6 = PotiWertAnzeigen(_modelSimulation.ValuePotentiometer6, _modelSimulation.StatusPotentiometer6);

            Thread.Sleep(100);
        }
    }
    private static string PotiWertAnzeigen(int value, byte status)
    {
        _ = status;

        return $"{(double) value / 100}%";
    }
}
