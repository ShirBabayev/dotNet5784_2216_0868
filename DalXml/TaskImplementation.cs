﻿namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Xml.Linq;

internal class TaskImplementation: ITask
{
    readonly string s_task_xml = "tasks";
    readonly string s_data_config_xml = "data-config";
    //public DateTime? getInitDate() 
    //{
    //    XElement root = XMLTools.LoadListFromXMLElement(s_data_config_xml).Element("InitDate");
    //        if (root.Value == "")
    //            return null;
    //        return DateTime.Parse(root.Value); 
    //}
    //public DateTime? getEndDate()
    //{
    //    XElement root = XMLTools.LoadListFromXMLElement(s_data_config_xml).Element("EndDate");
    //    if (root.Value == "")
    //        return null;
    //    return DateTime.Parse(root.Value);
    //} 
    //public void setInitDate(DateTime InitDate) 
    //{
    //XElement root = XMLTools.LoadListFromXMLElement(s_data_config_xml);
    //    root.Element("InitDate")!.Value = InitDate.ToString();
    //XMLTools.SaveListToXMLElement(root, s_data_config_xml);
    //}
    //public void setEndDate(DateTime EndDate)
    //{
    //    XElement root = XMLTools.LoadListFromXMLElement(s_data_config_xml);
    //    root.Element("EndDate")!.Value = EndDate.ToString();
    //    XMLTools.SaveListToXMLElement(root, s_data_config_xml);
    //}
    public int Create(Task item)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_task_xml);//Deserialize
        int newId = XMLTools.GetAndIncreaseNextId(s_data_config_xml, "nextTaskId");//next id number config func
        Task newItem = item with { Id = newId };
        tasks.Add(newItem);
        XMLTools.SaveListToXMLSerializer(tasks, s_task_xml);//Serialize
        return newItem.Id;
    }

    public void Delete(int id)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_task_xml);//Deserialize
        Task item = tasks.Find(tsk => tsk.Id == id)
          ?? throw new DalDoesNotExistException($"Task with ID={id} does not exists");
        tasks.Remove(item);
        XMLTools.SaveListToXMLSerializer(tasks, s_task_xml);//Serialize
    }

    public Task? Read(int id)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_task_xml);//Deserialize
        return tasks.FirstOrDefault(task => task.Id == id);
        // return tasks.Find(lk => lk.Id == id);
    }

    public Task? Read(Func<Task, bool> filter)
    {
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_task_xml);//Deserialize
        return tasks.FirstOrDefault(filter);
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
        Task itemToUpdate = tasks.Find(tsk => tsk.Id == item.Id)
           ?? throw new DalDoesNotExistException($"Task with ID= {item.Id} does not exists");
        tasks.Remove(itemToUpdate);
        tasks.Add(item);
        XMLTools.SaveListToXMLSerializer(tasks, s_task_xml);//Serialize
    }
    public void Reset()
    {
        List<DO.Task> emptyList = new List<DO.Task>();
        XMLTools.SaveListToXMLSerializer(emptyList, s_task_xml);
        //XElement root = new XElement("ArrayOfTasks");
        //XMLTools.SaveListToXMLElement(root, s_task_xml);
    }

}
