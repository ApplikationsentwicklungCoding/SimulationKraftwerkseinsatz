using CommunityToolkit.Mvvm.ComponentModel;
using System.Drawing;

namespace Simulation.ViewModel;


public partial class VmSimulation
{
    [ObservableProperty] private Brush? _brushStarttaster;

    [ObservableProperty] private string? _stringPlcInfo;
    [ObservableProperty] private string? _stringKommunikationInfo;

    [ObservableProperty] private string? _stringValuePotentiometer11;
    [ObservableProperty] private string? _stringValuePotentiometer12;
    [ObservableProperty] private string? _stringValuePotentiometer13;
    [ObservableProperty] private string? _stringValuePotentiometer14;
    [ObservableProperty] private string? _stringValuePotentiometer15;
    [ObservableProperty] private string? _stringValuePotentiometer21;
    [ObservableProperty] private string? _stringValuePotentiometer22;
    [ObservableProperty] private string? _stringValuePotentiometer23;
    [ObservableProperty] private string? _stringValuePotentiometer24;
    [ObservableProperty] private string? _stringValuePotentiometer25;

    [ObservableProperty] private string? _stringStatusPotentiometer11;
    [ObservableProperty] private string? _stringStatusPotentiometer12;
    [ObservableProperty] private string? _stringStatusPotentiometer13;
    [ObservableProperty] private string? _stringStatusPotentiometer14;
    [ObservableProperty] private string? _stringStatusPotentiometer15;
    [ObservableProperty] private string? _stringStatusPotentiometer21;
    [ObservableProperty] private string? _stringStatusPotentiometer22;
    [ObservableProperty] private string? _stringStatusPotentiometer23;
    [ObservableProperty] private string? _stringStatusPotentiometer24;
    [ObservableProperty] private string? _stringStatusPotentiometer25;
}
