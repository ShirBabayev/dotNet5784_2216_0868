namespace DalTest;
using DalApi;
using DO;
using System.Data.Common;
using System.Security.Cryptography;

static class Initialization
{
    private static IEngineer? s_dalEngineer; //stage 1
    private static ITask? s_dalTask; //stage 1
    private static IDependency? s_dalDependency; //stage 1
    private static readonly Random s_rand = new/*Random*/();



    static public void Do(IEngineer? s_dalEngineer, ITask? s_dalTask, IDependency? s_dalDependency)
    {
        createEngineer();
        createTask();
        createDependency();
        //s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        //s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        //s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
    }
    private static void createEngineer()
    {
        string[] engineerNames =
        {
        "Dani_Levi", "Eli_Amar", "Yair_Cohen",
        "Ariela_Levin", "Dina_Klein", "Shira_Israelof"
        };


        foreach (var _name in engineerNames)
        {
            int _id;
            do
                _id = s_rand.Next(100000000, 399999999);
            while (s_dalEngineer!.Read(_id) != null);

            DifficultyLevel _level = DifficultyLevel.Beginner + s_rand.Next(0, 4);
            double _cost = 250 + s_rand.Next(0, 400);
            string? _email = (_id % 2) == 0 ? _name + "@gmail.com" : _name + "@walla.co.il";

            Engineer newEngineer = new(_id, _level, _cost, _name, _email);

            s_dalEngineer!.Create(newEngineer);
        }

    }

    private static void createTask()
    {
        List<Engineer?> engineers = s_dalEngineer!.ReadAll();
        string[] taskNicknames =
        {
        "distuction", "lakes", "big_baby",
        "houses", "car4U", "zero232","programs"
        };
        string[] taskDescriptions =
        {
        "do that", "bring theme to the right place", "open a center",
        "build 3 houses", "finish instruction", "throw everything","program the main advertizing program"
        };
        int i = 1;

        foreach (var _nickName in taskNicknames)
        {
            int _id;

            _id = s_rand.Next(10000, 99999);
            bool _mileStone = (_id % 2) == 0 ? true : false;
            DateTime _dateOfCreation = new DateTime(1999, 9, 9);
            int range = (DateTime.Today - _dateOfCreation).Days;
            DateTime _PlanedDateOfstratJob = _dateOfCreation.AddDays(s_rand.Next(range) / 5);
            DateTime _dateOfstratJob = _PlanedDateOfstratJob.AddDays(s_rand.Next(10));
            DateTime _deadline = _PlanedDateOfstratJob.AddDays(s_rand.Next(10, 100));
            DateTime _dateOfFinishing = _deadline.AddDays(s_rand.Next(-10, 0));
            TimeSpan _durationOfTask = _deadline - _PlanedDateOfstratJob;
            string _deliverables = null;
            int _engineerId = engineers[s_rand.Next(engineers.Count)].Id;
            DifficultyLevel _levelOfDifficulty = (DifficultyLevel)((int)(_durationOfTask.TotalDays) / 10);
            Task newTask = new(_id, _nickName, taskDescriptions[i++], _mileStone, _deliverables, _levelOfDifficulty,
            _engineerId, _dateOfCreation, _PlanedDateOfstratJob, _dateOfstratJob, _durationOfTask,
                _deadline, _dateOfFinishing);

            s_dalTask!.Create(newTask);

        }

    }
    private static void createDependency()
    {
        int _id = 0;
        for (int i = 0; i < 6; i++)
        {

            List<DO.Task?> tasks = s_dalTask!.ReadAll()!;
            int _formerTaskId;
            int _dependentTaskId;
            for (int j = 0; j < 40; j++)
            {
                int index = s_rand.Next(5, 20);
                _dependentTaskId = tasks[index]!.Id;

                if (index < 11 && index > 5)
                    _formerTaskId = tasks[s_rand.Next(0, 6)]!.Id;
                else
                {
                    if (index > 10 && index < 15)
                        _formerTaskId = tasks[s_rand.Next(6, 11)]!.Id;
                    else
                    {
                        _formerTaskId = tasks[s_rand.Next(11, 16)]!.Id;
                    }
                }
                s_dalDependency!.Create(new Dependency(_id, dependentTaskId: _dependentTaskId, formerTaskId: _formerTaskId));
            }

        }
    }
   

}

    



