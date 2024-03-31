using PL.Engineer;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for ManagerWoindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
        }
        private void BtnEngineers_Click(object sender, RoutedEventArgs e)
        {
            new EngineerListWindow().Show();

        }

        private void InitializeDatabase_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult choice = MessageBox.Show("Are you sure you want to Initiate the Database?", "Datasource Initialization", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (choice == MessageBoxResult.Yes)
                BlApi.Factory.Get().InitializeDB();
            else
                MessageBox.Show("Initiation of the Database was not allowed", "Did Not Initiate", MessageBoxButton.OK, MessageBoxImage.Information);

        }
        private void ResetDatabase_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult choice = MessageBox.Show("Are you sure you want to Reset the Database?", "Worning! Reset Action", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (choice == MessageBoxResult.Yes)
                BlApi.Factory.Get().ResetDB();
            else
                MessageBox.Show("Reset of the Database was not allowed", "Did Not Reset", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void BtnTaskClick(object sender, RoutedEventArgs e)=>new TaskListWindow(true).Show();

    }
}

