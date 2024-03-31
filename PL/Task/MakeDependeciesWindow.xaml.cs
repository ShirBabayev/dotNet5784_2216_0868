using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    /// Interaction logic for MakeDependeciesWindow.xaml
    /// </summary>
    public partial class MakeDependeciesWindow : Window
    {      
        ObservableCollection<TaskInList> selectedTasks = new ObservableCollection<TaskInList>();
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        static int currentTaskId = 0;
        public BO.EngineerExperience Level { get; set; } = BO.EngineerExperience.All;

        public static readonly DependencyProperty TaskListProperty =
                DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.Task>), typeof(MakeDependeciesWindow), new PropertyMetadata(null));
        public IEnumerable<BO.Task> TaskList
        {
            get { return (IEnumerable<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        public MakeDependeciesWindow(int Id)
        {
            currentTaskId = Id;
           // IEnumerable<TaskInList>? selectedTasks = new IEnumerable<TaskInList>();
            InitializeComponent();
            TaskList = s_bl?.Task.ReadAll(t=>t.Id!= currentTaskId)!;//all the tasks except the current task

        }
       // IEnumerable<TaskInList>? DependencyList = s_bl?.Task.Read(currentTaskId)!.DependencyList;
        public BO.EngineerExperience Levels { get; set; } = BO.EngineerExperience.All;

        private void AddSelectedTasks_Click(object sender, RoutedEventArgs e)
        {       
            // Add selected tasks to the task list
            if ((sender as ListView)?.SelectedItem is BO.TaskInList selectedTask)
            {
                selectedTasks.Add(selectedTask);
                
            }
           
        }
        

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //foreach (var task in selectedTasks) { if(DependencyList.find(task)==null) DependencyList.insert(task);else DependencyList.erase(task) }
        }
    }
}
