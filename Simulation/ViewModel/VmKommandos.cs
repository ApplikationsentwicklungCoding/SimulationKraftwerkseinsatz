using CommunityToolkit.Mvvm.Input;

namespace Simulation.ViewModel;

public partial class VmSimulation
{
    [RelayCommand]
    private void CheckBoxHaken(string str) => _ = str;
}
