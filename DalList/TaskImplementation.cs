﻿namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        DataSource.Tasks.Add(item);
        return item.Id;
    }

    public void Delete(int id)
    {
        Task item = DataSource.Tasks.Find(lk => lk.Id == id);
        if (item == null)
        {
            throw new Exception($"Task with ID={id} does not exists");
        }
        DataSource.Tasks.Remove(item);
    }

    public Task? Read(int id)
    {
        return DataSource.Tasks.Find(lk => lk.Id == id);
    }

    public List<Task> ReadAll()
    {
        return DataSource.Tasks.ToList();
    }

    public void Update(Task item)
    {
        Task itemToUpdate = DataSource.Tasks.Find(lk => lk.Id == item.Id);
        if (itemToUpdate == null)
        {
            throw new Exception($"Task with ID={item.Id} does not exists");
        }
        DataSource.Tasks.Remove(itemToUpdate);
        DataSource.Tasks.Add(item);
    }
}