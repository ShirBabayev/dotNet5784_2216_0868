﻿using BlApi;
using BO;
using DalApi;

//using DalApi;
using DO;
using System.Data.Common;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace BlImplementation;
internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = Factory.Get;

    public int Create(BO.Engineer boEngineer)
    {
        if (boEngineer.Id < 0 || boEngineer.Name != "" || boEngineer.Cost < 0|| !IsValidEmail(boEngineer.Email!))
            throw new BO.BlInvalidvalueException("one of the engineer's values is invalid");
        DO.Engineer doEngineer = new DO.Engineer
            (Id:boEngineer.Id,
            Name: boEngineer.Name,
            Email: boEngineer.Email,
            Level: (DO.EngineerExperience)boEngineer.Level,
            Cost:boEngineer.Cost);
        try
        {
            int engId = _dal.Engineer.Create(doEngineer);
            return engId;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        if ((from t in _dal.Task.ReadAll()//if there is a current task for this engineer
             where IsCurrentTask(id, t.Id)||
             t.EngineerId==id&&t.DateOfFinishing!=null
             select true).FirstOrDefault()!)
            throw new BO.BlDeletionImpossible($"Engineer with ID={id} can not be deleted");
        try
        { 
            _dal.Engineer.Delete(id); 
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={id} does Not exist", ex);
        }
    }
    public bool IsValidEmail(string email)
    {
        // Regular expression pattern for validating email addresses
        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(email);
    }
    public BO.TaskInEngineer CheckTaskForEngineer(int engineerId, int taskId)
    {
        DO.Task doTask = _dal.Task.Read(taskId)!;
        DO.Engineer doEngineer = _dal.Engineer.Read(engineerId)!;
        if (doTask.EngineerId == null
            //&& doTask.DateOfFinishing < DateTime.Now
            && doTask.LevelOfDifficulty <= doEngineer.Level
        &&((from dep in _dal.Dependency.ReadAll()
                 let depOnTaskId = dep.DependentOnTaskId
                 where dep.DependentTaskId == taskId 
                 && _dal.Task.Read(depOnTaskId)!.DateOfFinishing ==null
                    select dep).FirstOrDefault() == null))
                return new BO.TaskInEngineer() { Id = taskId, NickName = doTask.NickName };
        return null!;
    }
    public IEnumerable<BO.TaskInEngineer> GetDetailedTaskForEngineer(int engineerId)
    {
        return from DO.Task doTask in _dal.Task.ReadAll()
               let IsGoodForEng = CheckTaskForEngineer(engineerId, doTask.Id)
               where IsGoodForEng != null
                    select IsGoodForEng;
    }

    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");
        DO.Task task = (from t in _dal.Task.ReadAll()
                      where IsCurrentTask(id, t.Id)
                      select t).FirstOrDefault()!;
        return new BO.Engineer()
        {
            Id= id,
            Name= doEngineer.Name,
            Email= doEngineer.Email,
            Level= (BO.EngineerExperience)doEngineer.Level,
            Cost= doEngineer.Cost,
            Task = new BO.TaskInEngineer(){Id=task.Id,NickName=task.NickName}
        };

    }

    public IEnumerable<BO.EngineerInTask> ReadAll()
    {
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                    //select new BO.EngineerInList()
                    //{
                    //    Id = doEngineer.Id,
                    //    Name = doEngineer.Name,
                    //});
                select new BO.EngineerInTask()
                {
                    EngineerId = doEngineer.Id,
                    TaskId = (from task in _dal.Task.ReadAll()
                              where IsCurrentTask(doEngineer.Id, task.Id)
                              //where task.EngineerId == doEngineer.Id
                              //&& task.DateOfstratJob != null
                              //&& task.DateOfFinishing == null
                              select task.Id).FirstOrDefault()//??throw new BO.BlDoesNotExistException($"A current task for Engineer with ID={doEngineer.Id} does Not exist")
                }) ;
    }
    public bool IsCurrentTask(int engId, int taskId)
    {
        DO.Task task = _dal.Task.Read(taskId)!;
        if (task.EngineerId == engId
            && task.DateOfstratJob != null
            && task.DateOfFinishing == null)
            return true;
        return false;
        //(from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
        //select new BO.EngineerInList()
        //{
        //    Id = doEngineer.Id,
        //    Name = doEngineer.Name,
        //});
        //return new BO.TaskInEngineer()
        //{
        //    Id = engId,
        //    TaskId = (from task in _dal.Task.ReadAll()
        //              where task.EngineerId == engId
        //              && task.DateOfstratJob != null
        //              && task.DateOfFinishing == null
        //              select task.Id).FirstOrDefault()//??throw new BO.BlDoesNotExistException($"A current task for Engineer with ID={doEngineer.Id} does Not exist")
        //});
        //return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
        //            //select new BO.EngineerInList()
        //            //{
        //            //    Id = doEngineer.Id,
        //            //    Name = doEngineer.Name,
        //            //});
        //        select new BO.EngineerInTask()
        //        {
        //            EngineerId = doEngineer.Id,
        //            TaskId = (from task in _dal.Task.ReadAll()
        //                      where task.EngineerId == doEngineer.Id
        //                      && task.DateOfstratJob != null
        //                      && task.DateOfFinishing == null
        //                      select task.Id).FirstOrDefault()//??throw new BO.BlDoesNotExistException($"A current task for Engineer with ID={doEngineer.Id} does Not exist")
        //        });
    }

    //option to convertion
    //public IEnumerable<BO.EngineerInList> ReadAll(Func<DO.Engineer, bool>? filter = null)
    //{

    //    return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll(filter)
    //            select new BO.EngineerInList()
    //            {
    //                Id = doEngineer.Id,
    //                Name = doEngineer.Name,
    //            });
    //}
    public void Update(BO.Engineer boEngineer)
    {
        if (boEngineer.Id < 0 || boEngineer.Name != "" || boEngineer.Cost < 0|| !IsValidEmail(boEngineer.Email!))
            throw new BO.BlInvalidvalueException("one of the engineer's values is invalid");
        DO.Engineer? new_doEngineer = new DO.Engineer
             (Id: boEngineer.Id,
             Name: boEngineer.Name,
             Email: boEngineer.Email,
             Level: (DO.EngineerExperience)boEngineer.Level,
             Cost: boEngineer.Cost);
        DO.Task task= (from DO.Task doTask in _dal.Task.ReadAll()
                    let IsGoodForEng = CheckTaskForEngineer(boEngineer.Id, doTask.Id)
                    where IsGoodForEng != null
                    select doTask).FirstOrDefault()!;
        if (task != null)
            _dal.Task.Update(task with{ EngineerId = boEngineer.Id});
        try
        {
            _dal.Engineer.Update(new_doEngineer);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} does Not exist", ex);
        }

    }
}