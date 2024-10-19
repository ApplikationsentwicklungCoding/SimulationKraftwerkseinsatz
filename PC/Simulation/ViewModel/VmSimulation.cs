using CommunityToolkit.Mvvm.ComponentModel;
using Simulation.Model;
using System.Drawing;

namespace Simulation.ViewModel;

public partial class VmSimulation : ObservableObject
{
    private readonly ModelSimulation _modelSimulation;

    public VmSimulation(ModelSimulation modelSimulation, CancellationTokenSource cancellationTokenSource)
    {
        _modelSimulation = modelSimulation;

        BrushStarttaster = Brushes.LawnGreen;

        StringPlcInfo = "PlcInfo";
        StringKommunikationInfo = "KommunikationInfo";

        StringValuePotentiometer11 = "";
        StringValuePotentiometer12 = "";
        StringValuePotentiometer13 = "";
        StringValuePotentiometer14 = "";
        StringValuePotentiometer15 = "";
        StringValuePotentiometer21 = "";
        StringValuePotentiometer22 = "";
        StringValuePotentiometer23 = "";
        StringValuePotentiometer24 = "";
        StringValuePotentiometer25 = "";

        StringStatusPotentiometer11 = "";
        StringStatusPotentiometer12 = "";
        StringStatusPotentiometer13 = "";
        StringStatusPotentiometer14 = "";
        StringStatusPotentiometer15 = "";
        StringStatusPotentiometer21 = "";
        StringStatusPotentiometer22 = "";
        StringStatusPotentiometer23 = "";
        StringStatusPotentiometer24 = "";
        StringStatusPotentiometer25 = "";

        _ = Task.Run(() => PlcStarterTask(cancellationTokenSource.Token));
    }

    private void PlcStarterTask(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            StringValuePotentiometer11 = _modelSimulation.ValuePotentiometer11.ToString();
            StringValuePotentiometer12 = _modelSimulation.ValuePotentiometer12.ToString();
            StringValuePotentiometer13 = _modelSimulation.ValuePotentiometer13.ToString();
            StringValuePotentiometer14 = _modelSimulation.ValuePotentiometer14.ToString();
            StringValuePotentiometer15 = _modelSimulation.ValuePotentiometer15.ToString();

            StringValuePotentiometer21 = _modelSimulation.ValuePotentiometer21.ToString();
            StringValuePotentiometer22 = _modelSimulation.ValuePotentiometer22.ToString();
            StringValuePotentiometer23 = _modelSimulation.ValuePotentiometer23.ToString();
            StringValuePotentiometer24 = _modelSimulation.ValuePotentiometer24.ToString();
            StringValuePotentiometer25 = _modelSimulation.ValuePotentiometer25.ToString();

            StringStatusPotentiometer11 = $"0b{_modelSimulation.StatusPotentiometer11:b8}";
            StringStatusPotentiometer12 = $"0b{_modelSimulation.StatusPotentiometer12:b8}";
            StringStatusPotentiometer13 = $"0b{_modelSimulation.StatusPotentiometer13:b8}";
            StringStatusPotentiometer14 = $"0b{_modelSimulation.StatusPotentiometer14:b8}";
            StringStatusPotentiometer15 = $"0b{_modelSimulation.StatusPotentiometer15:b8}";

            StringStatusPotentiometer21 = $"0b{_modelSimulation.StatusPotentiometer21:b8}";
            StringStatusPotentiometer22 = $"0b{_modelSimulation.StatusPotentiometer22:b8}";
            StringStatusPotentiometer23 = $"0b{_modelSimulation.StatusPotentiometer23:b8}";
            StringStatusPotentiometer24 = $"0b{_modelSimulation.StatusPotentiometer24:b8}";
            StringStatusPotentiometer25 = $"0b{_modelSimulation.StatusPotentiometer25:b8}";

            StringKommunikationInfo = _modelSimulation.GetKommunikationInfo();
            StringPlcInfo = _modelSimulation.GetVersionsString();

            Thread.Sleep(100);
        }
    }
}
