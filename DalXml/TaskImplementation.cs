namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Data.Common;

internal class TaskImplementation: ITask
{
    readonly string s_task_xml = "tasks";    
    public int Create(Task item)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_task_xml);//Deserialize
        int newId = XMLTools.GetAndIncreaseNextId("Config", s_task_xml);//next id number config func
        Task newItem = item with { Id = newId };
        tasks.Add(newItem);
        XMLTools.SaveListToXMLSerializer(tasks, s_task_xml);//Serialize
        return item.Id;
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_task_xml);//Deserialize
        Task item = tasks.Find(lk => lk.Id == id)
          ?? throw new DalDoesNotExistException($"Task with ID={id} does not exists");
        tasks.Remove(item);
        XMLTools.SaveListToXMLSerializer(tasks, s_task_xml);//Serialize
        throw new NotImplementedException();
    }

    public Task? Read(int id)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_task_xml);//Deserialize
        return tasks.FirstOrDefault(task => task.Id == id);
        // return tasks.Find(lk => lk.Id == id);
        throw new NotImplementedException();
    }

    public Task? Read(Func<Task, bool> filter)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_task_xml);//Deserialize
        return tasks.FirstOrDefault(filter);
        throw new NotImplementedException();
    }

    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter = null)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_task_xml);//Deserialize
        return filter == null ? tasks.Select(item => item)
                              : tasks.Where(filter);
    }
    public void Update(Task item)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_task_xml);//Deserialize
        Task itemToUpdate = tasks.Find(lk => lk.Id == item.Id)
           ?? throw new DalDoesNotExistException($"Task with ID= {item.Id} does not exists");
        tasks.Remove(itemToUpdate);
        tasks.Add(item);
        XMLTools.SaveListToXMLSerializer(tasks, s_task_xml);//Serialize
        throw new NotImplementedException();
    }
}
