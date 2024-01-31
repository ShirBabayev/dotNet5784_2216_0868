namespace DalTest;
using DalApi;
using DO;
using System;
using System.Data.Common;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml.Linq;

static class Initialization
{
    private static IDal? s_dal;
    private static readonly Random s_rand = new();


    static public void Do(/*IDal dal*/)
    {
        s_dal =/* DalApi.*/Factory.Get;/////???????????????????????????????????????????????????????????
        //s_dal = dal ?? throw new NullReferenceException("DAL object can not be null!");
        createEngineer();
        createTask();
        createDependency(s_dal.Task!.ReadAll().ToList());
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
            while (s_dal!.Engineer.Read(_id) != null);//as long the Id exists in the list, meaning that the object already exists

            EngineerExperience _level = EngineerExperience.Beginner + s_rand.Next(0, 4);
            double _cost = 250 + s_rand.Next(0, 400);
            string? _email = (_id % 2) == 0 ? _name + "@gmail.com" : _name + "@walla.co.il";

            Engineer newEngineer = new(Id: _id,
                                        Level: _level,
                                        Cost: _cost,
                                        Name: _name,
                                        Email: _email
                                        );
            s_dal.Engineer.Create(newEngineer);
        }

    }

    private static void createTask()
    {
        string[] taskNicknames =
        {
         "ProjectX", "DataMigration", "CodeRefactoring",
    "ClientMeeting", "BugFixing", "FeatureImplementation",
    "TestingAutomation", "DatabaseOptimization", "UIEnhancements",
    "SecurityAudit", "DocumentationUpdate", "ServerMaintenance",
    "UserTraining", "MobileAppIntegration", "CloudMigration",
    "NetworkConfiguration", "PerformanceAnalysis", "CodeReview",
    "SystemIntegration", "ReleaseManagement"
        };
        string[] taskDescriptions =
        {
        "Implement new features for ProjectX",
    "Migrate data to the new database",
    "Refactor and optimize existing codebase",
    "Conduct a meeting with the client to gather requirements",
    "Fix reported bugs in the application",
    "Implement a new feature requested by the client",
    "Automate testing for improved efficiency",
    "Optimize the database for better performance",
    "Enhance the user interface of the application",
    "Perform a security audit on the system",
    "Update documentation for recent changes",
    "Perform maintenance on the server infrastructure",
    "Conduct training sessions for end-users",
    "Integrate the mobile app with existing systems",
    "Migrate the application to the cloud",
    "Configure network settings for improved connectivity",
    "Analyze and optimize system performance",
    "Review code changes submitted by team members",
    "Integrate multiple systems for seamless operation",
    "Manage the release process for a new version"

        };
        string[] Remarks =
            {
            "Incomplete - Needs additional work",
    "Requires expertise - Assign to a pro",
    "Critical - Consider deleting existing code",
    "No specific remark at the moment",
    "Good enough for current requirements",
    "Revert to the previous program version",
    "Requires immediate attention - Terrible state",
    "Underestimated complexity - Needs more resources",
    "Unexpected issues - Investigate thoroughly",
    "Ready for deployment - Verify one last time",
    "Unexpected challenges - Seek assistance",
    "High priority - Address as soon as possible",
    "Revision needed - Check for logical errors",
    "Satisfactory - No major issues observed",
    "Pending review - Await feedback from team",
    "Advanced skill required - Assign to senior developer",
    "Potential for optimization - Evaluate for improvements",
    "On hold - Wait for further instructions",
    "User acceptance testing pending - Confirm with client",
    "Stable - No immediate action required"
        };
        int i = 0;
        foreach (var _nickName in taskNicknames)
        {
            DateTime _dateOfCreation = new DateTime(1999, 9, 9);
            int range = (DateTime.Today - _dateOfCreation).Days;
            string _remarks = Remarks[i];
            //DateTime _PlanedDateOfstratJob = _dateOfCreation.AddDays(s_rand.Next(range) / 5);
            //DateTime _dateOfstratJob = _PlanedDateOfstratJob.AddDays(s_rand.Next(10));
            ////DateTime _deadline = _PlanedDateOfstratJob.AddDays(s_rand.Next(10, 100));
            //DateTime _dateOfFinishing = _deadline.AddDays(s_rand.Next(-10, 0));
            TimeSpan _durationOfTask = TimeSpan.FromDays(s_rand.Next(1, 11));
            string _deliverables = " ";
            //int _engineerId = engineerList[s_rand.Next(engineerList.Count)].Id;
            //bool _mileStone = (_engineerId % 2) == 0 ? true : false;
            EngineerExperience _levelOfDifficulty = (EngineerExperience)((int)(_durationOfTask.TotalDays) / 10);
            DO.Task newTask = new(Id: 0,
                                NickName: _nickName,
                                Description: taskDescriptions[i++],
                                Deliverables: _deliverables,
                                LevelOfDifficulty: _levelOfDifficulty,
                                Remarks: _remarks,
                                DateOfCreation: _dateOfCreation,
                                DurationOfTask: _durationOfTask
                                );
            s_dal!.Task.Create(newTask);
        }
    }
    private static void createDependency(List<DO.Task> Tasks)
    {
        int? _dependentTaskId;
        int? _depententOnTaskId;
        s_dal!.Dependency.Create(new Dependency(Id: 0, Tasks[0].Id, Tasks[2]!.Id));
        s_dal!.Dependency.Create(new Dependency(Id: 0, Tasks[1].Id, Tasks[2]!.Id));

        int k = 40;
        for (int i = 2; k > 0; i++)
        {
            _dependentTaskId = Tasks.ElementAt(i)?.Id;
            for (int j = i + 1; j < Math.Min(i + 4, Tasks.Count()); j++)
            {
                _depententOnTaskId = Tasks.ElementAt(j)?.Id;
                k--;
                Dependency newDependency = new(Id: 0,
                                                DependentTaskId: _dependentTaskId,
                                                DependentOnTaskId: _depententOnTaskId
                                                );
                s_dal!.Dependency!.Create(newDependency);
            }
        }
    }
}
