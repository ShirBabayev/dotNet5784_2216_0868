using BlApi;
using System.Windows;

namespace PL;

/// <summary>
/// Interaction logic for GantWindow.xaml
/// </summary>
public partial class GantWindow : Window
{
    private readonly IBl s_bl = Factory.Get();
    public IEnumerable<BO.Task> Tasks { get; set; }
    public GantWindow()
    {
        Tasks = s_bl.Task.ReadAll();
        InitializeComponent();
    }
}
