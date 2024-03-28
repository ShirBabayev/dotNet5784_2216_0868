using PL.Engineer;
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
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        //bool IsAddClick;
        public static readonly DependencyProperty TaskProperty =
        DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));
        public BO.Task CurrentTask
        {
            get { return (BO.Task)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }
        public TaskWindow(int TaskId = 0)
        {
            //IsAddClick = TaskId == 0 ? true : false;

            InitializeComponent();
            try
            {
                //CurrentTask = IsAddClick ?
                CurrentTask = s_bl?.Task.Read(TaskId)!;
            }
            catch (BO.BlDoesNotExistException)
            {
                MessageBox.Show($"There is no Task with the id: {TaskId}", "Engineer Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            //if (IsAddClick)
            //{
                try
                {
                    s_bl.Task.Create(CurrentTask);
                    MessageBox.Show($"New Task with Id: {CurrentTask.Id} Created Successfully!", "Creation:");
                    Close();
                }
                catch (BO.BlInvalidvalueException)
                {
                    MessageBox.Show("One Of The New-Task's Values Is Invalid,this Engineer is not in the system", "Invalid Detales", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (BO.BlAlreadyExistsException)
                {
                    MessageBox.Show("Ho no! There is already an Task with the same Id, can not add this one again ", "Task Exists", MessageBoxButton.OK, MessageBoxImage.Error);
                }
        //    }
        //    else
        //        try
        //        {
        //            s_bl.Task.Update(CurrentTask);
        //            MessageBox.Show($"The Task with Id: {CurrentTask.Id}  Was Updated Successfully!", "Update:");
        //            Close();
        //        }
        //        catch (BO.BlInvalidvalueException)
        //        {
        //            MessageBox.Show("One Of The New-Task's Values Is Invalid,this Task is not in the system", "Invalid Detales", MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //        catch (BO.BlDoesNotExistException)
        //        {
        //            MessageBox.Show("Ho no! There is no Task with this id, can not Update. ", "Task Does Not Exist", MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //        catch (BO.BlCantSetValue)
        //        {
        //            MessageBox.Show("This Task has a current task, so you can't change his details right now, this Task is not Updated.", "Task Can't be Updated", MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        }
    }
}
