using BO;
using PL.Engineer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskListWindow.xaml
    /// </summary>
    public partial class TaskListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.EngineerExperience Level { get; set; } = BO.EngineerExperience.All;

        public static readonly DependencyProperty TaskListProperty =
                DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.Task>), typeof(TaskListWindow), new PropertyMetadata(null));
        public IEnumerable<BO.Task> TaskList
        {
            get { return (IEnumerable<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        private bool _isManager {  get; set; }   
        public TaskListWindow(bool isManager)
        {
            _isManager = isManager;
            InitializeComponent();
            TaskList = s_bl?.Task.ReadAll()!;
        }
        public BO.EngineerExperience Levels { get; set; } = BO.EngineerExperience.All;
        private void ListView_DoubleClick(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListView)?.SelectedItem is BO.Task tsk)
            {
                //  MessageBox.Show(eng.EngineerId.ToString());
                new TaskWindow(_isManager, tsk.Id).ShowDialog();
                TaskList = new ObservableCollection<BO.Task>(s_bl?.Task.ReadAll()!);//in order to show the updated list
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e) { new TaskWindow(_isManager).Show(); }

        private void SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            TaskList = (Level == BO.EngineerExperience.All) ?
           s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => item.LevelOfDifficulty == (DO.EngineerExperience)Level)!;
        }
    }
}
