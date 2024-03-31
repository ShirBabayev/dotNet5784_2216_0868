using PL.Task;
using System;
using System.Collections.Generic;
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
        DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));
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
                CurrentEngineer = IsAddClick ?
                                    new BO.Engineer()
                                    : s_bl?.Engineer.Read(EngineerId)!;
            }
            catch (BO.BlDoesNotExistException)
            {
                MessageBox.Show($"There is no Engineer with the id: {EngineerId}", "Engineer Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        private void TaskList_Click(object sender, RoutedEventArgs e)
        {
            new TaskListWindow(false).Show();
        }
    }
}
