using Simulation.Model;
using Simulation.ViewModel;

namespace Simulation;

// ReSharper disable once UnusedMember.Global
public partial class MainWindow
{
    public ModelSimulation ModelSimulation { get; set; }
    public VmSimulation VmSimulation { get; set; }

    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public MainWindow()
    {
        ModelSimulation = new ModelSimulation(_cancellationTokenSource);
        VmSimulation = new VmSimulation(ModelSimulation, _cancellationTokenSource);
        
        InitializeComponent();
        DataContext = VmSimulation;
    }
}
