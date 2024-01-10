namespace DalApi;
using DO;
public interface ITask
{
    int Create(Task item); //Creates new task object in DAL

    ///need to add a "reset" function (see the general document)

    Task? Read(int id); //Reads task object by its ID 
    List<Task> ReadAll(); //stage 1 only, Reads all task objects
    void Update(Task item); //Updates task object
    void Delete(int id); //Deletes an object by its Id

}
