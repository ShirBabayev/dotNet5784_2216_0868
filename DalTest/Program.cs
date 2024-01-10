namespace DalTest;

internal class Program
{
    private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
    private static ITask? s_dalTask = new TaskImplementation(); //stage 1
    private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1
    static void Main(string[] args)
    {
        Initialization.Do(s_dalEngineer, s_dalTask, s_dalDependency);
        main_menu();
        EngineerMenu();
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
                    Console.WriteLine(s_dalEngineer!.Create(newEngineer));
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
                Console.WriteLine(s_dalTask!.Create(newTask));
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
                Console.WriteLine(s_dalDependency!.Create(newDependency));
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
        Console.WriteLine(s_dalEngineer!.Read(_idOfEngineer));
    }
    static void readTask()
    {
        Console.WriteLine("press id of task");
        int _idOfTask = int.Parse(Console.ReadLine());
        Console.WriteLine(s_dalTask!.Read(_idOfTask));
    }
    static void readDependency()
    {
        Console.WriteLine("press id of dependency");
        int _idOfDependency = int.Parse(Console.ReadLine());
        Console.WriteLine(s_dalDependency!.Read(_idOfDependency));
    }

    static void readAllEngineer()
    {
        List<DO.Engineer> engineers = s_dalEngineer!.ReadAll();
        foreach (DO.Engineer eng in engineers)
            Console.WriteLine(eng);
    }

    static void readAllTask()
    {
        List<DO.Task> tasks = s_dalTask!.ReadAll();
        foreach (DO.Task tsk in tasks)
            Console.WriteLine(tsk);
    }

    static void readAllDependency()
    {
        List<DO.Dependency> dependency = s_dalDependency!.ReadAll();
        foreach (DO.Dependency dep in dependency)
            Console.WriteLine(dep);
    }


    static void updateAllEngineer()
    {
        readEngineer();//get input of identifier and print the objct
        Engineer newEngineer = createNewEngineer();
        s_dalEngineer!.Update(newEngineer);
        Console.WriteLine("updated successfully");
    }
    static void updateAllTask()
    {
        readTask();//get input of identifier and print the objct
        DO.Task newTask = createNewTask();
        s_dalTask!.Update(newTask);
        Console.WriteLine("updated successfully");
    }
    static void updateAllDependency()
    {
        readDependency();//get input of identifier and print the objct
        Dependency newDependency = createNewDependency();
        s_dalDependency!.Update(newDependency);
        Console.WriteLine("updated successfully");
    }

    static void deleteEngineer()
    {
        Console.WriteLine("press the id of the engineer you want to delete");
        int _id = int.Parse(Console.ReadLine());
        s_dalEngineer!.Delete(_id);
        Console.WriteLine("deleted successfully");
    }
    static void deleteTask()
    {
        Console.WriteLine("press the id of the task you want to delete");
        int _id = int.Parse(Console.ReadLine());
        s_dalTask!.Delete(_id);
        Console.WriteLine("deleted successfully");
    }
    static void deleteDependency()
    {
        Console.WriteLine("press the id of the dependency you want to delete");
        int _id = int.Parse(Console.ReadLine());
        s_dalDependency!.Delete(_id);
        Console.WriteLine("deleted successfully");
    }
    static void main_menu()
    {
        Console.WriteLine("choose an entity that you want to check:");
        Console.WriteLine("to EXIT press 0");
        Console.WriteLine("for Engineer press 1");
        Console.WriteLine("for Task press 2");
        Console.WriteLine("for Dependency press 3");

        int choice = int.Parse(Console.ReadLine());

        switch(choice)
        {
            case 0: break; 
            case 1:EngineerMenu();
                break;
            case 2: TaskMenu();
                break;
            case 3:
                DependencyMenu();
                break;
        }
    }



    static void EngineerMenu()
    {
        Console.WriteLine("choose a function that you want to do:");
        Console.WriteLine("to EXIT press 0");
        Console.WriteLine("to Create a new Engineer press 1");
        Console.WriteLine("to Read an Engineer press 2");
        Console.WriteLine("to Read all Engineer objects press 3");
        Console.WriteLine("to Update an Engineer's data press 4");
        Console.WriteLine("to Delete an Engineer press 5");
        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 0: break;
            case 1:
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
                Engineer newEngineer = new(Id: _id, (EngineerExperience)_level, _cost, _name, _email);
                EngineerImplementation dalEngineer = new EngineerImplementation();
                dalEngineer.Create(newEngineer);
                break;
            case 2:
                Console.WriteLine("press Id of task");
                int _id = int.Parse(Console.ReadLine());
                Console.WriteLine("press level experience of task");
                int _level = int.Parse(Console.ReadLine());
                Console.WriteLine("press cost of task per an hour");
                double _cost = double.Parse(Console.ReadLine());
                Console.WriteLine("press name of task");
                string? _name = Console.ReadLine();
                Console.WriteLine("press email of task");
                string? _email = Console.ReadLine();
                Engineer newEngineer = new(Id: _id, (EngineerExperience)_level, _cost, _name, _email);
                EngineerImplementation dalEngineer = new EngineerImplementation();
                dalEngineer.Create(newEngineer);

                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
        }
    }
    static void TaskMenu()
    {
        Console.WriteLine("choose a function that you want to do:");
        Console.WriteLine("to EXIT press 0");
        Console.WriteLine("to Create a new Task press 1");
        Console.WriteLine("to Read a Task press 2");
        Console.WriteLine("to Read all Task objects press 3");
        Console.WriteLine("to Update a Task's data press 4");
        Console.WriteLine("to Delete a Task press 5");
    }
    static void DependencyMenu()
    {
        Console.WriteLine("choose a function that you want to do:");
        Console.WriteLine("to EXIT press 0");
        Console.WriteLine("to Create a new Dependency press 1");
        Console.WriteLine("to Read a  Dependency press 2");
        Console.WriteLine("to Read all Dependency objects press 3");
        Console.WriteLine("to Update a Dependency's data press 4");
        Console.WriteLine("to Delete a Dependency press 5");
    }




    }
}

//namespace DalTest;

//using Dal;
//using DalApi;
//using DO;
//using Microsoft.VisualBasic;
//using System;
///// <summary>
///// main function
///// </summary>
//internal class Program
//{
//    /// <summary>
//    /// A method that displays a main menu for selecting actions on engineer, task and dependency
//    /// </summary>
//    /// <param name="args">Argument selection</param>
//    static void Main(string[] args)
//    {
//        Initialization.Do();

//        while (true)
//        {
//            Console.WriteLine("Hello and welcome,\r\n for engineers click 1,\r\n for tasks click 2,\r\n for dependencies click 3.\r\n");
//            string action = Console.ReadLine();
//            switch (action)
//            {
//                case "1":
//                    EngineerActions();
//                    break;

//                case "2":
//                    TaskAction();
//                    break;


//                case "3":
//                    DependencyAction();
//                    break;
//                case "4":
//                    break;

//                default:
//                    return;
//            }
//        }
//        /// <summary>
//        /// A method that displays a menu for the Engineer entity and allows the user to choose to perform actions on that entity
//        /// </summary>
//        void EngineerActions()
//        {
//            EngineerImplementation dalEngineer = new EngineerImplementation();

//            while (true)
//            {
//                Console.WriteLine("Welcome" + "\r\n " +
//                    "to exit the main menu, press 1.\r\n" +
//                    "To add a new Engineer, press 2.\r\n" +
//                    "To view an Engineer by ID number, press 3.\r\n" +
//                    "To view the list of Engineers, press 4.\r\n" +
//                    "To update an existing Engineer's data, press 5.\r\n" +
//                    "To remove an existing Engineer from the list, press 6 .");
//                string action = Console.ReadLine();

//                switch (action)
//                {
//                    case "1":
//                        return;

//                    case "2":
//                        Console.WriteLine("Please enter the name of the engineer:");
//                        string? name = Console.ReadLine();

//                        Console.WriteLine("Please enter the Level of the engineer");
//                        int level = int.Parse(Console.ReadLine());

//                        Console.WriteLine("Please enter the engineer's hourly wage:");
//                        double cost = double.Parse(Console.ReadLine());

//                        Console.WriteLine("Please enter the mail of the engineer:");
//                        string? email = Console.ReadLine();

//                        Engineer engineer = new Engineer(Id: 0, Email: email, Cost: cost, Name: name, Level: (EngineerExperience)level);
//                        dalEngineer.Create(engineer);
//                        break;
//                    case "3":
//                        Console.WriteLine("Please enter the Id of the engineer: ");
//                        int id = int.Parse(Console.ReadLine());
//                        Console.WriteLine(dalEngineer.Read(id));
//                        break;

//                    case "4":
//                        foreach (var engeen in dalEngineer.ReadAll())
//                            Console.WriteLine(engeen);
//                        break;

//                    case "5":
//                        Console.WriteLine("Please enter the Id of the engineer: ");
//                        id = int.Parse(Console.ReadLine());

//                        Console.WriteLine("Please enter the name of the engineer:");
//                        name = Console.ReadLine();

//                        Console.WriteLine("Please enter the Level of the engineer");
//                        level = int.Parse(Console.ReadLine());

//                        Console.WriteLine("Please enter the engineer's hourly wage:");
//                        cost = double.Parse(Console.ReadLine());

//                        Console.WriteLine("Please enter the Email of the engineer:");
//                        email = Console.ReadLine();

//                        engineer = new Engineer(Id: id, Email: email, Cost: cost, Level: (EngineerExperience)level, Name: name);
//                        dalEngineer.Update(engineer);
//                        break;

//                    case "6":
//                        Console.WriteLine("Please enter the Id of the engineer: ");
//                        id = int.Parse(Console.ReadLine());
//                        try
//                        {
//                            dalEngineer.Delete(id);
//                        }
//                        catch (Exception ex) { Console.WriteLine(ex.Message); }
//                        break;

//                    default:
//                        return;
//                }
//            }
//        }
//        /// <summary>
//        /// A method that displays a menu for the Task entity and allows the user to choose to perform actions on that entity
//        /// </summary>
//        void TaskAction()
//        {
//            TaskImplementation dalTask = new TaskImplementation();
//            while (true)
//            {
//                Console.WriteLine("Welcome" + "\r\n " +
//                    "to exit the main menu, press 1.\r\n" +
//                    "To add a new Task, press 2.\r\n" +
//                    "To view an Task by ID number, press 3.\r\n" +
//                    "To view the list of Tasks, press 4.\r\n" +
//                    "To update an existing Task's data, press 5.\r\n" +
//                    "To remove an existing Task from the list, press 6 .");

//                string action = Console.ReadLine();
//                switch (action)
//                {
//                    case "1":
//                        return;
//                    case "2":
//                        Console.WriteLine("Please enter the nickName of the task:");
//                        string? nickName = Console.ReadLine();
//                        Console.WriteLine("Please enter the descripition of the task:");
//                        string? description = Console.ReadLine();
//                        Console.WriteLine("Please enter the deliverables of the task:");
//                        string? deliverables = Console.ReadLine();
//                        Console.WriteLine("Please enter the mileStone of the task:");
//                        bool mileStone = bool.Parse(Console.ReadLine());
//                        Console.WriteLine("Please enter the remarks of the task:");
//                        string remarks = Console.ReadLine();
//                        Console.WriteLine("Please enter the engineers id of the task:");
//                        int engineerId = int.Parse(Console.ReadLine());
//                        Console.WriteLine("Please enter the engineers copmlexity of the task:");
//                        EngineerExperience levelOfDifficulty = (EngineerExperience)int.Parse(Console.ReadLine());
//                        Task newTask = new Task(Id: 0, NickName: nickName, Description: description, Remarks: remarks,
//                            DateOfCreation: DateTime.Now, DurationOfTask: TimeSpan.FromDays(7), Deliverables: deliverables, MileStone: mileStone,
//                            LevelOfDifficulty: levelOfDifficulty, EngineerId: engineerId);
//                        dalTask.Create(newTask);
//                        break;

//                    case "3":

//                        Console.WriteLine("Please enter the ID of the task:");
//                        int id = int.Parse(Console.ReadLine());
//                        Console.WriteLine(dalTask.Read(id));
//                        break;

//                    case "4":
//                        foreach (var tass in dalTask.ReadAll())
//                            Console.WriteLine(tass);
//                        break;

//                    case "5":
//                        Console.WriteLine("Please enter the nickName of the task:");
//                        nickName = Console.ReadLine();

//                        Console.WriteLine("Please enter the descripition of the task:");
//                        description = Console.ReadLine();

//                        Console.WriteLine("Please enter the deliverables of the task:");
//                        deliverables = Console.ReadLine();

//                        Console.WriteLine("Please enter the milestone of the task:");
//                        mileStone = bool.Parse(Console.ReadLine());

//                        Console.WriteLine("Please enter the remarks of the task:");
//                        remarks = Console.ReadLine();

//                        Console.WriteLine("Please enter the task id of the task:");
//                        int taskId = int.Parse(Console.ReadLine());

//                        Console.WriteLine("Please enter the engineers id of the task:");
//                        engineerId = int.Parse(Console.ReadLine());

//                        Console.WriteLine("Please enter the engineers copmlexity of the task:");
//                        levelOfDifficulty = (EngineerExperience)int.Parse(Console.ReadLine());

//                        newTask = new Task(Id: taskId, NickName: nickName, LevelOfDifficulty: levelOfDifficulty, Description: description, Remarks: remarks,
//                            DateOfCreation: DateTime.Now, DurationOfTask: TimeSpan.FromDays(7), Deliverables: deliverables,
//                            MileStone: mileStone, EngineerId: engineerId);
//                        dalTask.Update(newTask);
//                        break;

//                    case "6":
//                        Console.WriteLine("Please enter the ID of the task:");
//                        id = int.Parse(Console.ReadLine());
//                        try
//                        {
//                            dalTask.Delete(id);
//                        }
//                        catch (Exception ex) { Console.WriteLine(ex.Message); }
//                        break;

//                    default:
//                        return;
//                }
//            }
//        }


//        /// <summary>
//        /// A method that displays a menu for the Dependency entity and allows the user to choose to perform actions on that entity
//        /// </summary>
//        void DependencyAction()
//        {
//            DependencyImplementation dalDependent = new DependencyImplementation();
//            while (true)
//            {
//                Console.WriteLine("Welcome" + "\r\n " +
//                     "to exit the main menu, press 0.\r\n" +
//                     "To add a new Dependency, press 1.\r\n" +
//                     "To update an existing Dependency's data,  press 2.\r\n" +
//                     "To remove an existing Dependency from the list, press 3.\r\n" +
//                     "To view an Dependency by ID number, press 4.\r\n" +
//                     "To view the list of Dependencys, press 5 .");
//                string action = Console.ReadLine();

//                switch (action)
//                {
//                    case "1":
//                        Console.WriteLine("Please enter a Pending Task ID number:");
//                        int dependentTaskId = int.Parse(Console.ReadLine());
//                        Console.WriteLine("Please enter a previous assignment ID number:");
//                        int dependentOnTaskId = int.Parse(Console.ReadLine());
//                        Dependency dep = new Dependency(Id: 0, DependentTaskId: dependentTaskId, DependentOnTaskId: dependentOnTaskId);
//                        dalDependent.Create(dep);

//                        break;

//                    case "2":
//                        Console.WriteLine("Please enter the Id: ");
//                        int id = int.Parse(Console.ReadLine());
//                        Console.WriteLine("Please enter a Pending Task ID number:");
//                        dependentTaskId = int.Parse(Console.ReadLine());
//                        Console.WriteLine("Please enter a previous assignment ID number:");
//                        dependentOnTaskId = int.Parse(Console.ReadLine());
//                        Dependency depend = new Dependency(Id: id, DependentTaskId: dependentTaskId, DependentOnTaskId: dependentOnTaskId);
//                        dalDependent.Update(depend);
//                        break;

//                    case "3":

//                        Console.WriteLine("Please enter the Id:");
//                        id = int.Parse(Console.ReadLine());
//                        try
//                        {
//                            dalDependent.Delete(id);
//                        }
//                        catch (Exception ex)
//                        {
//                            Console.WriteLine(ex.Message);
//                        }
//                        break;

//                    case "4":
//                        Console.WriteLine("Please enter the Id: ");
//                        id = int.Parse(Console.ReadLine());
//                        Console.WriteLine(dalDependent.Read(id));
//                        break;

//                    case "5":
//                        foreach (var depp in dalDependent.ReadAll())
//                            Console.WriteLine(depp);
//                        break;

//                    default:
//                        return;
//                }
//            }
//        }
//    }
//}













