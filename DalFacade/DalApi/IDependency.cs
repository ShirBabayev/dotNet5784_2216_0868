namespace DalApi;
using DO;
public interface IDependency
{
    int Create(Dependency item); //Creates new dependecy object in DAL

    ///need to add a "reset" function (see the general document)

    Dependency? Read(int id); //Reads dependecy object by its ID 
    List<Dependency> ReadAll(); //stage 1 only, Reads all dependecy objects
    void Update(Dependency item); //Updates dependecy object
    void Delete(int id); //Deletes an object by its Id

}
