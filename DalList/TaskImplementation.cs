namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class TaskImplementation : ITask
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
            throw new DalDoesNotExistException($"Task with ID={id} does not exists");
        }
        DataSource.Tasks.Remove(item);
    }

    public Task? Read(int id)
    {
        return DataSource.Tasks.FirstOrDefault(task => task.Id == id);
        // return DataSource.Tasks.Find(lk => lk.Id == id);
    }

    //public List<Task> ReadAll()
    //{
    //    return DataSource.Tasks/*.ToList()*/;
    //    //return new List<Task>(DataSource.Tasks);
    //}

    public Task? Read(Func<Task, bool> filter)
    {
            return DataSource.Tasks.FirstOrDefault(filter);
    }
    public IEnumerable<Task?> ReadAll(Func<Task?, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Tasks.Select(item => item);
        else
            return DataSource.Tasks.Where(filter);
    }


    public void Update(Task item)
    {
        Task itemToUpdate = DataSource.Tasks.Find(lk => lk.Id == item.Id);
        if (itemToUpdate == null)
        {
            throw new DalDoesNotExistException($"Task with ID= {item.Id} does not exists");
        }
        DataSource.Tasks.Remove(itemToUpdate);
        DataSource.Tasks.Add(item);
    }
}
