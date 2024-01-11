using Dal;
using DalApi;
using DO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml.Linq;

  

namespace DalTest;
public enum Entities { EXIT=0, ENGINEER, TASK, DEPENDENCY};
public enum Actions { EXIT = 0, CREATE, READ, READ_ALL, UPDATE, DELETE };
internal class Program
{
    //private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
    //private static ITask? s_dalTask = new TaskImplementation(); //stage 1
    //private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1
     static readonly IDal s_dal = new Dal.DalList();
    static void Main(string[] args)
    {
        Initialization.Do(s_dal);
        main_menu();
    }
    static void main_menu()
    {
        Console.WriteLine("choose an entity that you want to check:");
        Console.WriteLine("to EXIT press 0");
        Console.WriteLine("for Engineer press 1");
        Console.WriteLine("for Task press 2");
        Console.WriteLine("for Dependency press 3");
        Entities choice = (Entities)int.Parse(Console.ReadLine());
        switch (choice)
        {
            case Entities.EXIT : 
                break;
            case Entities.ENGINEER:
                EngineerMenu();
                break;
            case Entities.TASK:
                TaskMenu();
                break;
            case Entities.DEPENDENCY:
                DependencyMenu();
                break;
            default:
             return;
        }
    }

    static void EngineerMenu()
    {
        printEngineerMenue();
        Actions choice = (Actions)int.Parse(Console.ReadLine());
        switch (choice)
        {
            case Actions.EXIT: break;
            case Actions.CREATE://create
                try
                {
                    Engineer newEngineer = createNewEngineer();
                    Console.WriteLine(s_dal!.Engineer.Create(newEngineer));
                    Console.WriteLine("created successfully");
                }
                catch (Exception ex)
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
                try { 
                updateAllEngineer();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case Actions.DELETE:
                try { 
                deleteEngineer();
                }
                catch (Exception ex)
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
        Actions choice = (Actions)int.Parse(Console.ReadLine());
        switch (choice)
        {
            case Actions.EXIT: break;
            case Actions.CREATE:
                try { 
                DO.Task newTask=createNewTask();
                Console.WriteLine(s_dal!.Task.Create(newTask));
                Console.WriteLine("created successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case Actions.READ:
                readTask();
                break;
            case Actions.READ_ALL:
                readAllTask();
                break;
            case Actions.UPDATE:
                try { 
                updateAllTask();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case Actions.DELETE:
                try { 
                deleteTask();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            default:
                return;
        }
    }
    static void DependencyMenu()
    {
        printDependencyMenue();
        Actions choice = (Actions)int.Parse(Console.ReadLine());
        switch (choice)
        {
            case Actions.EXIT: break;
            case Actions.CREATE:
                try { 
                DO.Dependency newDependency = createNewDependency();
                Console.WriteLine(s_dal!.Dependency.Create(newDependency));
                Console.WriteLine("created successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case Actions.READ:
                readDependency();
                break;
            case Actions.READ_ALL:
                readAllDependency();
                break;
            case Actions.UPDATE:
                try { 
                updateAllDependency();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case Actions.DELETE:
                try { 
                deleteDependency();
                }
                catch (Exception ex)
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
        Console.WriteLine("choose a function that you want to do:");
        Console.WriteLine("to EXIT press 0");
        Console.WriteLine("to create and add a new Engineer press 1");
        Console.WriteLine("to view an Engineer by id number press 2");
        Console.WriteLine("to view all Engineer objects press 3");
        Console.WriteLine("to Update an Engineer's data press 4");
        Console.WriteLine("to Delete an Engineer from the list press 5");
    }
    static void printTaskMenue()
    {
        Console.WriteLine("choose a function that you want to do:");
        Console.WriteLine("to EXIT press 0");
        Console.WriteLine("to create and add a new Task press 1");
        Console.WriteLine("to view a Task by id number press 2");
        Console.WriteLine("to view all Task objects press 3");
        Console.WriteLine("to Update a Task's data press 4");
        Console.WriteLine("to Delete a Task from the list press 5");
    }
    static void printDependencyMenue()
    {
        Console.WriteLine("choose a function that you want to do:");
        Console.WriteLine("to EXIT press 0");
        Console.WriteLine("to create and add a new Dependency press 1");
        Console.WriteLine("to view a  Dependency by id number press 2");
        Console.WriteLine("to view all Dependency objects press 3");
        Console.WriteLine("to Update a Dependency's data press 4");
        Console.WriteLine("to Delete a Dependency from the list press 5");
    }

    static Engineer createNewEngineer()
    {
        Console.WriteLine("press Id of engineer");
        int _id = int.Parse(Console.ReadLine());
        Console.WriteLine("press level experience of engineer");
        int _level = int.Parse(Console.ReadLine());
        Console.WriteLine("press cost of engineer per an hour");
        double _cost = double.Parse(Console.ReadLine());
        Console.WriteLine("press name of engineer");
        string? _name = Console.ReadLine();
        Console.WriteLine("press email of engineer");
        string? _email = Console.ReadLine();
        DO.Engineer newEngineer = new(Id: _id, (EngineerExperience)_level, _cost, _name, _email);
        return newEngineer;
    }
    static DO.Task createNewTask()
    {
        Console.WriteLine("press nickName of task");
        string _nickName = Console.ReadLine();
        Console.WriteLine("press description of task");
        string _description = Console.ReadLine();
        Console.WriteLine("press if there is a mileStone of task(0=false,1=true)");
        bool _mileStone = bool.Parse(Console.ReadLine());
        Console.WriteLine("press deliverables of task");
        string _deliverables = Console.ReadLine();
        Console.WriteLine("press the level of difficulty of the task");
        int _levelOfDifficulty = int.Parse(Console.ReadLine());
        Console.WriteLine("press the id of an engineer for the task");
        int _engineerId = int.Parse(Console.ReadLine());
        Console.WriteLine("press remarks of task");
        string _remarks = Console.ReadLine();
        Console.WriteLine("press the date of creatiing the task");
        DateTime? _dateOfCreation = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("press the planed date of strat job");
        DateTime? _planedDateOfstratJob = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("press the date of strat job");
        DateTime? _dateOfstratJob = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("press the duration of task");
        TimeSpan? _durationOfTask = TimeSpan.Parse(Console.ReadLine());
        Console.WriteLine("press the deadline of the task");
        DateTime? _deadline = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("press the date of finishing the task");
        DateTime? _dateOfFinishing = DateTime.Parse(Console.ReadLine());
        DO.Task newTask = new(Id: 0, NickName: _nickName, Description: _description, MileStone: _mileStone, Deliverables: _deliverables, LevelOfDifficulty: (EngineerExperience)_levelOfDifficulty,
            EngineerId: _engineerId, Remarks: _remarks, DateOfCreation: _dateOfCreation, PlanedDateOfstratJob: _planedDateOfstratJob, DateOfstratJob: _dateOfstratJob,
            DurationOfTask: _durationOfTask, Deadline: _deadline, DateOfFinishing: _dateOfFinishing);
        return newTask;
    }
    static DO.Dependency createNewDependency()
    {
        Console.WriteLine("press Dependent task id of dependency");
        int? _dependentTaskId = int.Parse(Console.ReadLine());
        Console.WriteLine("press dependent on taskId");
        int? _dependentOnTaskId = int.Parse(Console.ReadLine());
        DO.Dependency newDependency = new(Id: 0, DependentTaskId: _dependentTaskId, DependentOnTaskId: _dependentOnTaskId);
        return newDependency;
    }

    static void readEngineer()
    {
        Console.WriteLine("press id of engineer");
        int _idOfEngineer = int.Parse(Console.ReadLine());
        Console.WriteLine(s_dal!.Engineer.Read(_idOfEngineer));
    }
    static void readTask()
    {
        Console.WriteLine("press id of task");
        int _idOfTask = int.Parse(Console.ReadLine());
        Console.WriteLine(s_dal!.Task.Read(_idOfTask));
    }
    static void readDependency()
    {
        Console.WriteLine("press id of dependency");
        int _idOfDependency = int.Parse(Console.ReadLine());
        Console.WriteLine(s_dal!.Dependency.Read(_idOfDependency));
    }

    static void readAllEngineer()
    {
        List<DO.Engineer> engineers = s_dal!.Engineer.ReadAll();
        foreach (DO.Engineer eng in engineers)
            Console.WriteLine(eng);
    }

    static void readAllTask()
    {
        List<DO.Task> tasks = s_dal!.Task.ReadAll();
        foreach (DO.Task tsk in tasks)
            Console.WriteLine(tsk);
    }

    static void readAllDependency()
    {
        List<DO.Dependency> dependency = s_dal!.Dependency.ReadAll();
        foreach (DO.Dependency dep in dependency)
            Console.WriteLine(dep);
    }


    static void updateAllEngineer()
    {
        readEngineer();//get input of identifier and print the objct
        Engineer newEngineer = createNewEngineer();
        s_dal!.Engineer.Update(newEngineer);
        Console.WriteLine("updated successfully");
    }
    static void updateAllTask()
    {
        readTask();//get input of identifier and print the objct
        DO.Task newTask = createNewTask();
        s_dal!.Task.Update(newTask);
        Console.WriteLine("updated successfully");
    }
    static void updateAllDependency()
    {
        readDependency();//get input of identifier and print the objct
        Dependency newDependency = createNewDependency();
        s_dal!.Dependency.Update(newDependency);
        Console.WriteLine("updated successfully");
    }

    static void deleteEngineer()
    {
        Console.WriteLine("press the id of the engineer you want to delete");
        int _id = int.Parse(Console.ReadLine());
        s_dal!.Engineer.Delete(_id);
        Console.WriteLine("deleted successfully");
    }
    static void deleteTask()
    {
        Console.WriteLine("press the id of the task you want to delete");
        int _id = int.Parse(Console.ReadLine());
        s_dal!.Task.Delete(_id);
        Console.WriteLine("deleted successfully");
    }
    static void deleteDependency()
    {
        Console.WriteLine("press the id of the dependency you want to delete");
        int _id = int.Parse(Console.ReadLine());
        s_dal!.Dependency.Delete(_id);
        Console.WriteLine("deleted successfully");
    }
}
