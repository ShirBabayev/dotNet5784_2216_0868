using BO;
using PL.Task;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace PL;

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



    public bool IsManager
    {
        get { return (bool)GetValue(IsmanagerProp); }
        set { SetValue(IsmanagerProp, value); }
    }

    // Using a DependencyProperty as the backing store for IsManager.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IsmanagerProp =
        DependencyProperty.Register("IsManager", typeof(bool), typeof(TaskWindow));


    public bool AddMode
    {
        get { return (bool)GetValue(AddModeProp); }
        set { SetValue(AddModeProp, value); }
    }

    // Using a DependencyProperty as the backing store for IsManager.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty AddModeProp =
        DependencyProperty.Register("AddMode", typeof(bool), typeof(TaskWindow));


    public bool EnableDialog
    {
        get { return (bool)GetValue(EnableDialogProp); }
        set { SetValue(EnableDialogProp, value); }
    }

    // Using a DependencyProperty as the backing store for IsManager.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty EnableDialogProp =
        DependencyProperty.Register("EnableDialog", typeof(bool), typeof(TaskWindow));


    ObservableCollection<TaskInList> selectedTasks = new ObservableCollection<TaskInList>();

    public TaskWindow(bool isManager, int TaskId = 0)
    {
        EnableDialog = false;
        AddMode = TaskId == 0;
        IsManager = isManager;
        try
        {
            if (AddMode)
                CurrentTask = new();
            else
                CurrentTask = s_bl?.Task.Read(TaskId)!;
        }
        catch (BO.BlDoesNotExistException)
        {
            MessageBox.Show($"There is no Task with the id: {TaskId}", "Engineer Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        if (CurrentTask.DependencyList is null)
            CurrentTask.DependencyList = new List<TaskInList>();

        if (CurrentTask.EngineerOfTask is null)
            CurrentTask.EngineerOfTask = new();

        InitializeComponent();

    }


    private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        e.Handled = true;
        ScrollViewer scrollViewer = (ScrollViewer)sender;
        if (e.Delta > 0)
        {
            // Scroll up
            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - 20);
        }
        else
        {
            // Scroll down
            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset + 20);
        }

        // Mark the event as handled to prevent the default scrolling behavior
        e.Handled = true;
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        if (AddMode)
        {
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
        }
        else
        {
            try
            {
                s_bl.Task.Update(CurrentTask);
                MessageBox.Show($"The Task with Id: {CurrentTask.Id}  Was Updated Successfully!", "Update:");
                Close();
            }
            catch (BO.BlInvalidvalueException)
            {
                MessageBox.Show("One Of The New-Task's Values Is Invalid,this Task is not in the system", "Invalid Detales", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BlDoesNotExistException)
            {
                MessageBox.Show("Ho no! There is no Task with this id, can not Update. ", "Task Does Not Exist", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BlCantSetValue)
            {
                MessageBox.Show("This Task has a current task, so you can't change his details right now, this Task is not Updated.", "Task Can't be Updated", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }


    private void EditDep_Click(object sender, RoutedEventArgs e)
    {
        EnableDialog = true;
        //new MakeDependeciesWindow(CurrentTask.Id).Show();
    }

    private void ChangeDependencyCollection(object sender, MouseButtonEventArgs e)
    {
        // Add selected tasks to the task list
        if ((sender as ListView)?.SelectedItem is BO.TaskInList selectedTask)
        {
            selectedTasks.Add(selectedTask);
        }
    }
}
