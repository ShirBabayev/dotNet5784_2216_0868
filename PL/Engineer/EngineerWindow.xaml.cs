using BO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
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
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
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
        public EngineerWindow(int EngineerId = 0)//engineer window ctor
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
        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (IsAddClick)
            {
                try
                {
                    s_bl.Engineer.Create(CurrentEngineer);
                    MessageBox.Show($"New Engineer with Id: {CurrentEngineer.Id} Created Successfully!", "Creation:");
                    Close();
                }
                catch (BO.BlInvalidvalueException)
                {
                    MessageBox.Show("One Of The New-Engineer's Values Is Invalid,this Engineer is not in the system", "Invalid Detales", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (BO.BlAlreadyExistsException)
                {
                    MessageBox.Show("Ho no! There is already an Engineer with the same Id, can not add this one again ", "Engineer Exists", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                try
                {
                    s_bl.Engineer.Update(CurrentEngineer);
                    MessageBox.Show($"The Engineer with Id: {CurrentEngineer.Id}  Was Updated Successfully!", "Update:");
                    Close();
                }
                catch (BO.BlInvalidvalueException)
                {
                    MessageBox.Show("One Of The New-Engineer's Values Is Invalid,this Engineer is not in the system", "Invalid Detales", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (BO.BlDoesNotExistException)
                {
                    MessageBox.Show("Ho no! There is no Engineer with this id, can not Update. ", "Engineer Does Not Exist", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (BO.BlCantSetValue)
                {
                    MessageBox.Show("This Engineer has a current task, so you can't change his details right now, this Engineer is not Updated.", "Engineer Can't be Updated", MessageBoxButton.OK, MessageBoxImage.Error);
                }
        }
    
    }
}
