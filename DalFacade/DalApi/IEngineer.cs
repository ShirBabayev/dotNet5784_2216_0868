namespace DalApi;
using DO;
public interface IEngineer
{
    int Create(Engineer item); //Creates new engineer object in DAL

    ///need to add a "reset" function (see the general document)
    Engineer? Read(int id); //Reads engineer object by its ID 
    List<Engineer> ReadAll(); //stage 1 only, Reads all engineer objects
    void Update(Engineer item); //Updates engineer object
    void Delete(int id); //Deletes an object by its Id

}
