using BO;
using PL.Task;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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


    public bool _isManager { get; set; }
    public bool _isEngineer { get; set; }


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

    public IEnumerable<TaskInList> TasksList { set; get; }

    public ObservableCollection<TaskInList> DependencyTask = new ObservableCollection<TaskInList>();


    private BO.Engineer engineer { set; get; }
    public TaskWindow(bool isManager, int TaskId = 0, int engineerid = 0)
    {
        if (engineerid != 0)
        {
            engineer = s_bl.Engineer.Read(engineerid)!;
            if (engineer.Task is null)
                engineer.Task = new();
        }

        TasksList = s_bl.Task.ReadAll().ToList().Select(t => new TaskInList()
        {
            Id = t.Id,
            Description = t.Description,
            NickName = t.NickName,
            Status = t.Status,
        });

        EnableDialog = false;
        AddMode = TaskId == 0;
        _isManager = isManager;
        _isEngineer = !isManager;
        try
        {
            if (AddMode)
                CurrentTask = new() { DateOfCreation = s_bl.Clock };
            else
                CurrentTask = s_bl?.Task.Read(TaskId)!;
        }
        catch (BO.BlDoesNotExistException)
        {
            MessageBox.Show($"There is no Task with the id: {TaskId}", "Task Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        if (CurrentTask.DependencyList is null)
            CurrentTask.DependencyList = new List<TaskInList>();

        if (CurrentTask.EngineerOfTask is null)
            CurrentTask.EngineerOfTask = new();

        DependencyTask = new(CurrentTask.DependencyList);

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
                int _id = s_bl.Task.Create(CurrentTask);
                MessageBox.Show($"New Task with Id: {_id} Created Successfully!", "Creation:");
                Close();
            }

            catch (BO.BlInvalidvalueException)
            {
                if (CurrentTask.LevelOfDifficulty == null)
                    MessageBox.Show("The task's level of difficulty is not defined,try again.", "Invalid Detales", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                    MessageBox.Show("One Of The New-Task's Values Is Invalid,this Task is not in the system", "Invalid Detales", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BlAlreadyExistsException)
            {
                MessageBox.Show("Ho no! There is already a Task with the same Id, can not add this one again ", "Task Exists", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("This Task has a current Engineerr, so you can't change his details right now, this Task is not Updated.", "Task Can't be Updated", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BlIncorrectDateOrder ex)
            {
                MessageBox.Show(ex.Message, "Task Can't be Updated", MessageBoxButton.OK, MessageBoxImage.Error);

                //if (CurrentTask.PlanedDateOfstratJob == null)
                //    MessageBox.Show("The planed start date of this task is invalid", "Task Can't be Updated", MessageBoxButton.OK, MessageBoxImage.Error);
                //else
                //    MessageBox.Show("The project has already started, can't update the task", "Task Can't be Updated", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }


    private void EditDep_Click(object sender, RoutedEventArgs e)
    {
        EnableDialog = true;

    }

    private void ChangeDependencyCollection(object sender, MouseButtonEventArgs e)
    {
        // Add selected tasks to the task list
        if (sender is Label label)
        {
            if (label.Background == Brushes.LightGreen)
            {
                TaskInList t = (TaskInList)label.Content;
                
                t = DependencyTask.First(x=> x.Id == t.Id);
                DependencyTask.Remove(t);
                CurrentTask.DependencyList= DependencyTask;  
                label.Background = Brushes.Transparent;
            }
            else
            {
                TaskInList t = (TaskInList)label.Content;
                DependencyTask.Add(t);
                CurrentTask.DependencyList = DependencyTask;
                label.Background = Brushes.LightGreen;
            }
        }
    }

    private void Choose_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            engineer.Task = new()
            {
                Id = CurrentTask.Id
            };
            s_bl.Engineer.Update(engineer);
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }
}
