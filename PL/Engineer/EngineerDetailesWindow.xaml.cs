using PL.Task;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerDetailesWindow.xaml
    /// </summary>
    public partial class EngineerDetailesWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        bool IsAddClick;
        public static readonly DependencyProperty EngineerProperty =
        DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerDetailesWindow), new PropertyMetadata(null));
        public BO.Engineer CurrentEngineer
        {
            get { return (BO.Engineer)GetValue(EngineerProperty); }
            set { SetValue(EngineerProperty, value); }
        }
        public EngineerDetailesWindow(int EngineerId = 0)//engineer window ctor
        {

            IsAddClick = EngineerId == 0 ? true : false;
            InitializeComponent();
            try
            {
               // MessageBox.Show(EngineerId.ToString());
                CurrentEngineer = IsAddClick ?
                                    new BO.Engineer()
                                    : s_bl?.Engineer.Read(EngineerId)!;
               // MessageBox.Show(CurrentEngineer.Email);
            }
            catch (BO.BlDoesNotExistException)
            {
                MessageBox.Show($"There is no Engineer with the id: {EngineerId}", "Engineer Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        private void TaskList_Click(object sender, RoutedEventArgs e)
        {
  
            new TaskListWindow(false, CurrentEngineer.Level,CurrentEngineer.Id).Show();
        }

        private void Task_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentEngineer.Task!=null)
            {
                new TaskWindow(false, CurrentEngineer.Task.Id,CurrentEngineer.Id).Show();
            }
            else
            {
                MessageBox.Show($"No current task", "No Current Task", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Confirmation_Click(object sender, RoutedEventArgs e)
        {
            if (s_bl.Engineer.Read(CurrentEngineer.Id)!=null)
            {
                if(Button.ContentProperty.Name  == "Enter")
                    MessageBox.Show($"No current task", "No Current Task", MessageBoxButton.OK, MessageBoxImage.Error);

                //confirmationButton.Visibility = Visibility.Collapsed;
            }
        }
    }
}
