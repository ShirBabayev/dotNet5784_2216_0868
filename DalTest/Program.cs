using Dal;
using DalApi;
using DO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

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













