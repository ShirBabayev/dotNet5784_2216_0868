using BO;
using DO;
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
using System.Xml.Linq;

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>
   
    public partial class EngineerListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.EngineerExperience Level { get; set; } = BO.EngineerExperience.All;

        public static readonly DependencyProperty EngineerListProperty =
                DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.EngineerInTask>), typeof(EngineerListWindow), new PropertyMetadata(null));
        public IEnumerable<BO.EngineerInTask> EngineerList
        {
            get { return (IEnumerable<BO.EngineerInTask>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }
        public EngineerListWindow()
        {
            InitializeComponent();
            EngineerList = s_bl?.Engineer.ReadAll()!;
        }
        public BO.EngineerExperience Levels { get; set; } = BO.EngineerExperience.All;

        private void SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            EngineerList = (Level == BO.EngineerExperience.All) ?
            s_bl?.Engineer.ReadAll()! : s_bl?.Engineer.ReadAll(item => item.Level == (DO.EngineerExperience)Level)!;
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            new EngineerWindow().ShowDialog();
            EngineerList = new ObservableCollection<BO.EngineerInTask>(s_bl?.Engineer.ReadAll()!);//in order to show the updated list
        }
        private void ListView_DoubleClick(object sender, SelectionChangedEventArgs e)
        {
            
            if ((sender as ListView)?.SelectedItem is BO.EngineerInTask eng)
            {
              //  MessageBox.Show(eng.EngineerId.ToString());
                new EngineerWindow(eng.EngineerId).ShowDialog();
                EngineerList = new ObservableCollection<BO.EngineerInTask>(s_bl?.Engineer.ReadAll()!);//in order to show the updated list
            }
                //new EngineerWindow().Show();
        }
    }
}
