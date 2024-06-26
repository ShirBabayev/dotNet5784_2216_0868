﻿using BlApi;
using BO;
using DalApi;
using DO;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace BlImplementation;

internal class TaskImplementation : BlApi.ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public void StartProject(DateTime InitDate) => _dal.InitDate = InitDate;

    public void EndProject(DateTime EndDate) => _dal.EndDate = EndDate;


    /************* CRUD functions ************/
    public int Create(BO.Task boTask)
    {
        if (boTask.Id < 0 || boTask.NickName == null || boTask.NickName == "")
            throw new BO.BlInvalidvalueException("one of the task's values is invalid");
        if (boTask.LevelOfDifficulty == null)
            throw new BO.BlInvalidvalueException("The value of LevelOfDifficulty is Null");
        DO.Task doTask = new DO.Task
           (Id: boTask.Id,
            NickName: boTask.NickName,
            Description: boTask.Description ?? "",
            Deliverables: boTask.Deliverables ?? "",
            LevelOfDifficulty: (DO.EngineerExperience)boTask.LevelOfDifficulty!,
            Remarks: boTask.Remarks,
            DurationOfTask: boTask.DurationOfTask,
            DateOfCreation: _dal.Clock);

        try
        {
            int taskId = _dal.Task.Create(doTask);
            foreach (BO.TaskInList task in boTask.DependencyList!)
                _dal.Dependency.Create(new Dependency()
                { DependentTaskId = doTask.Id, DependentOnTaskId = task.Id });
            return taskId;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={boTask.Id} already exists", ex);
        }

    }

    public void Delete(int id)
    {
        if ((from dep in _dal.Dependency.ReadAll()
             let depOnTaskId = dep.DependentOnTaskId
             where depOnTaskId == id
             select dep).FirstOrDefault() != null)
            throw new BO.BlDeletionImpossible($"There are dependent tasks on Task with ID={id}");
        if (_dal.Task.Read(id) != null)//task exists
            try
            {
                _dal.Task.Delete(id);
            }
            catch (DO.DalDeletionImpossible ex)
            {
                throw new BO.BlDeletionImpossible($"Task with ID={id} does Not exist", ex);
            }
    }

    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        return ConvertTaskFromDOToBO(doTask);
    }

    public IEnumerable<BO.Task> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        return (from DO.Task doTask in _dal.Task.ReadAll(filter)
                select ConvertTaskFromDOToBO(doTask));
    }

    public void Update(BO.Task boTask)
    {
        DO.Task new_doTask = _dal.Task.Read(boTask.Id)
            ?? throw new BO.BlDoesNotExistException($"task with id= {boTask.Id} does not exist");
        if (_dal.InitDate < _dal.Clock && (new_doTask.NickName != boTask.NickName
           || new_doTask.Description != boTask.Description || new_doTask.Deliverables != boTask.Deliverables 
           || new_doTask.LevelOfDifficulty != (DO.EngineerExperience)boTask.LevelOfDifficulty!
           || new_doTask.Remarks != boTask.Remarks || new_doTask.DurationOfTask != boTask.DurationOfTask
           || new_doTask.PlanedDateOfstratJob != boTask.PlanedDateOfstratJob))
                throw new BlIncorrectDateOrder("can't change task's info after starting project");
        if (boTask.Id <= 0 || boTask.NickName == "" || boTask.NickName is null)
            throw new BO.BlInvalidvalueException("one of the task's values is invalid");
        new_doTask = new_doTask with
        {
            NickName = boTask.NickName,
            Description = boTask.Description ?? "",
            Deliverables = boTask.Deliverables,
            LevelOfDifficulty = (DO.EngineerExperience)boTask.LevelOfDifficulty!,
            Remarks = boTask.Remarks,
            EngineerId = (new_doTask.EngineerId == null || new_doTask.EngineerId != null && boTask.EngineerId == new_doTask.EngineerId)
            ? boTask.EngineerId
            : throw new BO.BlCantSetValue($"task with id {boTask.Id} already has engineer"),
            DurationOfTask = boTask.DurationOfTask,
            DateOfstratJob = CanAddOrUpdateDateOfTask(boTask.Id, boTask.DateOfstratJob)
                            ? boTask.DateOfstratJob
                            : throw new BlIncorrectDateOrder("The task's date of start job is invalid"),
            DateOfFinishing = boTask.DateOfFinishing,
            PlanedDateOfstratJob = CanAddOrUpdateDateOfTask(boTask.Id, boTask.PlanedDateOfstratJob)
                                ? boTask.PlanedDateOfstratJob
                                : throw new BO.BlIncorrectDateOrder($"The planed date of start job of task with id= {boTask.Id} is not valid"),
        };
        try
        {
            _dal.Task.Update(new_doTask);
            IEnumerable<DO.Dependency> dependencies = _dal.Dependency.ReadAll(x => x.DependentTaskId == boTask.Id);//all the depending tasks
            if (boTask.DependencyList is null)
                boTask.DependencyList = new List<TaskInList>();//initialize as an empty list
                                                             
            IEnumerable<BO.TaskInList> lst = boTask.DependencyList.Where(_new => !dependencies.Any(old => old.DependentOnTaskId == _new.Id));
             lst.ToList().ForEach(dep => _dal.Dependency.Create(new DO.Dependency()
             {
                 DependentTaskId = boTask.Id,
                 DependentOnTaskId = dep.Id//the dapending task
             }));//doesn't enter to the the CREATE function

            //boTask.DependencyList.Where(_new => !dependencies.Any(old => old.DependentOnTaskId == _new.Id))
            //    .ToList().ForEach(dep => _dal.Dependency.Create(new DO.Dependency()
            //    {
            //        DependentTaskId = boTask.Id,
            //        DependentOnTaskId = dep.Id//the dapending task
            //    }));//doesn't enter to the the CREATE function
            IEnumerable<DO.Dependency> lst1 = dependencies.Where(old => !boTask.DependencyList.Any(_new => _new.Id == old.DependentOnTaskId));
                lst1.ToList().ForEach(dep => _dal.Dependency.Delete(dep.Id));//doesn't enter to the the DELETE function

        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={boTask.Id} does Not exist", ex);
        }
    }


    /************ Tools  ************/

    public BO.Task ConvertTaskFromDOToBO(DO.Task doTask)
    {
        BO.EngineerInList? engOfTask = null;
        if (doTask.EngineerId is not null)
        {
            DO.Engineer doEngineer = _dal.Engineer.Read((int)doTask.EngineerId!)
                       ?? throw new BlDoesNotExistException($"engineer with id= {doTask.EngineerId} does not exist");
            engOfTask = new BO.EngineerInList()
            {
                Id = doEngineer.Id,
                Name = doEngineer.Name
            };
        }
        return new BO.Task()
        {
            Id = doTask.Id,
            NickName = doTask.NickName,
            Description = doTask.Description,
            Deliverables = doTask.Deliverables,
            Status = CheckStatus(doTask),
            LevelOfDifficulty = (BO.EngineerExperience)doTask.LevelOfDifficulty!,
            EngineerId = doTask.EngineerId,
            EngineerOfTask = engOfTask,
            Remarks = doTask.Remarks,
            DateOfCreation = doTask.DateOfCreation,
            PlanedDateOfstratJob = doTask.PlanedDateOfstratJob,
            DateOfstratJob = doTask.DateOfstratJob,
            DateOfFinishing = doTask.DateOfFinishing,
            DurationOfTask = doTask.DurationOfTask,
            EstimatedFinishingDate = EstimatedFinishingDate(doTask.Id),
            DependencyList = from DO.Dependency dep in _dal.Dependency.ReadAll(dep => dep.DependentTaskId == doTask.Id)
                             select TaskInListConvertor(_dal.Task.Read(dep.DependentOnTaskId)!)
        };
    }

    TaskInList TaskInListConvertor(DO.Task task)
    {
        return new BO.TaskInList()
        {
            Id = task.Id,
            NickName = task.NickName,
            Description = task.Description,
            Status = CheckStatus(task)
        };
    }

    private bool CanAddOrUpdateDateOfTask(int taskId, DateTime? taskDate)
    {
        if (taskDate == null) { return true; }//if there is no date the tere is nothing to calculate and this is ok
        if (taskDate < _dal.InitDate)//if the date is sooner then the projects starting date
            return false;
        IEnumerable<DO.Dependency> depenList = from dep in _dal.Dependency.ReadAll() where dep.DependentTaskId == taskId select dep;//all the dependencies where this task is the dependent task
        var v = (from DO.Dependency dep in depenList
                 let depTask = _dal.Task.Read(dep.DependentOnTaskId)!//the depending task
                 where depTask.PlanedDateOfstratJob == null&& depTask.DateOfstratJob==null || EstimatedFinishingDate(depTask.Id) > taskDate
                 select dep).FirstOrDefault();//if there is even one depending task that its times are null
        if (v != null)
           return false;
        //v = (from DO.Dependency dep in depenList
        //     let depTask = _dal.Task.Read(dep.DependentOnTaskId)!//the depending task
        //     where EstimatedFinishingDate(depTask.Id) > taskDate
        //     //where depTask.DateOfFinishing + depTask.DurationOfTask > taskDate//??
        //     select dep).FirstOrDefault();
        //if (v != null)
        //    return false;
        return true;
    }

    private DateTime? EstimatedFinishingDate(int id)
    {
        DO.Task doTask = _dal.Task.Read(id)!;
        if (doTask.PlanedDateOfstratJob is null) return null;
        if (doTask.DateOfstratJob is null) return doTask.PlanedDateOfstratJob + doTask.DurationOfTask;

        DateTime? starting = doTask.PlanedDateOfstratJob > doTask.DateOfstratJob
                                ? doTask.PlanedDateOfstratJob : doTask.DateOfstratJob;
        return starting.Value + doTask.DurationOfTask;
    }

    private BO.Status CheckStatus(DO.Task doTask)
    {
        if (doTask.PlanedDateOfstratJob != null && doTask.DateOfstratJob == null)
            return BO.Status.Schedueled;
        if (doTask.DateOfstratJob != null && doTask.DateOfFinishing == null)//started to work and didn't finish
            return BO.Status.OnTrack;
        if (doTask.DateOfFinishing < _dal.Clock)
            return BO.Status.Done;
        //doTask.PlanedDateOfstratJob==null
        return BO.Status.Unschedueled;
    }

    //private BO.TaskInEngineer GetDetailedEngineerForTask(int EngineerId, int TaskId)
    //{
    //    throw new Exception();
    //}
}