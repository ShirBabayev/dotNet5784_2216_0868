namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int newId = DataSource.Config.NextTaskId;
        Task newItem = item with { Id = newId };
        DataSource.Tasks.Add(newItem);
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
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task item)
    {
        Task itemToUpdate = DataSource.Tasks.Find(lk => lk.Id == item.Id);
        if (itemToUpdate == null)
        {
            throw new Exception($"Task with ID= {item.Id} does not exists");
        }
        DataSource.Tasks.Remove(itemToUpdate);
        DataSource.Tasks.Add(item);
    }
}
