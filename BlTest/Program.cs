using System.Xml.Serialization;
using BlApi;
using BO;
using Task = BO.Task;


namespace BlTest;
internal class Program
{
    static readonly IBl s_bl = BlApi.Factory.Get();
    static void Main(string[] args)
    {
        main_menu();
    }
    static void main_menu()
    {
        //Console.WriteLine("Would you like to create Initial data? (Y/N)");
        //string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
        //if (ans == "Y") 
        //   DalTest.Initialization.Do();
        LogicEntities choice;
        do
        {
            Console.WriteLine("choose an entity that you want to settle");
            Console.WriteLine("to EXIT press 0");
            Console.WriteLine("for Engineer press 1");
            Console.WriteLine("for Task press 2");
            choice = (LogicEntities)int.Parse(Console.ReadLine()!);
            switch (choice)
            {
                case LogicEntities.EXIT:
                    break;
                case LogicEntities.ENGINEER:
                    EngineerMenu();
                    break;
                case LogicEntities.TASK:
                    TaskMenu();
                    break;
                default:
                    return;
            }
        }
        while (choice!=LogicEntities.EXIT);
    }




    static void EngineerMenu()
    {
        printEngineerMenue();
        Actions choice = (Actions)int.Parse(Console.ReadLine()!);
        switch (choice)
        {
            case Actions.BACK_TO_MAIN: break;
            case Actions.CREATE://create
                try
                {
                    BO.Engineer newEngineer = createNewEngineer();
                    Console.WriteLine(s_bl.Engineer.Create(newEngineer));
                    Console.WriteLine("created successfully");
                }
                catch (BlAlreadyExistsException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case Actions.READ:
                readEngineer();
                break;
            case Actions.READ_ALL:
                readAllEngineer();
                break;
            case Actions.UPDATE:
                try
                {
                    updateAllEngineer();
                }
                catch (BlDoesNotExistException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case Actions.DELETE:
                try
                {
                    deleteEngineer();
                }
                catch (BlDoesNotExistException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            default:
                return;

        }
    }
    static void TaskMenu()
    {
        printTaskMenue();
        Actions choice = (Actions)int.Parse(Console.ReadLine()!);
        switch (choice)
        {
            case Actions.BACK_TO_MAIN: break;
            case Actions.CREATE:
                BO.Task newTask = createNewTask();
                Console.WriteLine(s_bl.Task.Create(newTask));
                Console.WriteLine("created successfully");
                break;
            case Actions.READ:
                readTask();
                break;
            case Actions.READ_ALL:
                readAllTask();
                break;
            case Actions.UPDATE:
                try
                {
                    updateAllTask();
                }
                catch (BlDoesNotExistException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case Actions.DELETE:
                try
                {
                    deleteTask();
                }
                catch (BlDoesNotExistException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            default:
                return;
        }
    }


    static void printEngineerMenue()
    {
        Console.WriteLine("Choose a function that you want to do for an engineer:");
        Console.WriteLine("to go Back To Main press 0");
        Console.WriteLine("to Create and add a new Engineer press 1");
        Console.WriteLine("to view an Engineer by id number press 2");
        Console.WriteLine("to view all Engineers press 3");
        Console.WriteLine("to Update an Engineer's data press 4");
        Console.WriteLine("to Delete an Engineer from the list press 5");
    }
    static void printTaskMenue()
    {
        Console.WriteLine("Choose a function that you want to do for a task:");
        Console.WriteLine("to go Back To Main press 0");
        Console.WriteLine("to Create and add a new Task press 1");
        Console.WriteLine("to view a Task by id number press 2");
        Console.WriteLine("to view all Tasks press 3");
        Console.WriteLine("to Update a Task's data press 4");
        Console.WriteLine("to Delete a Task from the list press 5");
    }
    
    static BO.Engineer createNewEngineer()
    {
        Console.WriteLine("press Id of engineer");
        int _id = int.Parse(Console.ReadLine()!);
        Console.WriteLine("press level experience of engineer");
        int _level = int.Parse(Console.ReadLine()!);
        Console.WriteLine("press cost of engineer per an hour");
        double _cost = double.Parse(Console.ReadLine()!);
        Console.WriteLine("press name of engineer");
        string? _name = Console.ReadLine();
        Console.WriteLine("press email of engineer");
        string? _email = Console.ReadLine();
        BO.Engineer newEngineer =
            new(Id: _id,
            Level: (BO.EngineerExperience)_level,
            Cost: _cost,
            Name: _name,
            Email: _email
            );
        return newEngineer;
    }
    static BO.Task createNewTask()
    {
        Console.WriteLine("press nickName of task");
        string _nickName = Console.ReadLine()!;
        Console.WriteLine("press description of task");
        string _description = Console.ReadLine()!;
        Console.WriteLine("press deliverables of task");
        string _deliverables = Console.ReadLine()!;
        Console.WriteLine("press the level of difficulty of the task");
        int _levelOfDifficulty = int.Parse(Console.ReadLine()!);
        Console.WriteLine("press remarks of task");
        string _remarks = Console.ReadLine()!;
        TimeSpan _durationOfTask = TimeSpan.Parse(Console.ReadLine()!);
        Console.WriteLine("press the date of creating the task");
        DateTime? _dateOfCreation = DateTime.Parse(Console.ReadLine()!);
        BO.Task newTask = new(
                                Id: 0,
                                NickName: _nickName,
                                Description: _description,
                                Deliverables: _deliverables,
                                LevelOfDifficulty: (EngineerExperience)_levelOfDifficulty,
                                Remarks: _remarks,
                                DurationOfTask: _durationOfTask,
                                DateOfCreation: _dateOfCreation
                             );
        return newTask;
    }

    static void readEngineer()
    {
        Console.WriteLine("press id of engineer");
        int _idOfEngineer = int.Parse(Console.ReadLine()!);
        Console.WriteLine(s_bl.Engineer.Read(_idOfEngineer));
    }
    static void readTask()
    {
        Console.WriteLine("press id of task");
        int _idOfTask = int.Parse(Console.ReadLine()!);
        Console.WriteLine(s_bl.Task.Read(_idOfTask));
    }

    static void readAllEngineer()
    {
        IEnumerable<BO.EngineerInTask> engineers = s_bl.Engineer.ReadAll();
        foreach (BO.EngineerInTask eng in engineers)
            Console.WriteLine(eng);
    }

    static void readAllTask()
    {
        IEnumerable<BO.Task> tasks = s_bl.Task.ReadAll();
        foreach (BO.Task tsk in tasks)
            Console.WriteLine(tsk);
    }


    static void updateAllEngineer()
    {
        readEngineer();//get input of identifier and print the objct
        Engineer newEngineer = createNewEngineer();
        s_bl.Engineer.Update(newEngineer);
        Console.WriteLine("updated successfully");
    }
    static void updateAllTask()
    {
        readTask();//get input of identifier and print the objct
        BO.Task newTask = createNewTask();
        s_bl.Task.Update(newTask);
        Console.WriteLine("updated successfully");
    }
    static void deleteEngineer()
    {
        Console.WriteLine("press the id of the engineer you want to delete");
        int _id = int.Parse(Console.ReadLine()!);
        s_bl.Engineer.Delete(_id);
        Console.WriteLine("deleted successfully");
    }
    static void deleteTask()
    {
        Console.WriteLine("press the id of the task you want to delete");
        int _id = int.Parse(Console.ReadLine()!);
        s_bl.Task.Delete(_id);
        Console.WriteLine("deleted successfully");
    }

}
