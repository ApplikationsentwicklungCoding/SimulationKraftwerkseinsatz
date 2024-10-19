using Simulation.Model;
using Simulation.ViewModel;

namespace Simulation;

// ReSharper disable once UnusedMember.Global
public partial class MainWindow
{
    private ModelSimulation ModelSimulation { get; }
    private VmSimulation VmSimulation { get; }

    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public MainWindow()
    {
        ModelSimulation = new ModelSimulation(_cancellationTokenSource);
        VmSimulation = new VmSimulation(ModelSimulation, _cancellationTokenSource);

        InitializeComponent();
        DataContext = VmSimulation;
    }
}
