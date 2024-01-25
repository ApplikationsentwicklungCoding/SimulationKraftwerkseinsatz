using CommunityToolkit.Mvvm.ComponentModel;
using System.Drawing;

namespace Simulation.ViewModel;


public partial class VmSimulation
{
    [ObservableProperty] private Brush? _brushStarttaster;

    [ObservableProperty] private string? _stringPotentiometer1;
    [ObservableProperty] private string? _stringPotentiometer2;
    [ObservableProperty] private string? _stringPotentiometer3;
    [ObservableProperty] private string? _stringPotentiometer4;
    [ObservableProperty] private string? _stringPotentiometer5;
    [ObservableProperty] private string? _stringPotentiometer6;
}
