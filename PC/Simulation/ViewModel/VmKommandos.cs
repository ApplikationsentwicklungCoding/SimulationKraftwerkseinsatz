using CommunityToolkit.Mvvm.Input;

namespace Simulation.ViewModel;

public partial class VmSimulation
{
    [RelayCommand]
    private static void CheckBoxHaken(string str) => _ = str;
}
