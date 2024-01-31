using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DalApi;

public interface ICrud<T> where T : class
{
    int Create(T item); //Creates a new object

    //need to add a "reset" function (see the general document)
    /// <summary>
    /// Request an entity object by its identification number (id).
    /// It throws exception if there is no an object with such id
    /// </summary>
    /// <param name="id">the id of the object to read</param>
    /// <returns>an entity object if exist</returns>
    /// <exception cref="DalDoesNotExistException"/>
    T? Read(int id); //Reads object by its ID 
    T? Read(Func<T, bool> filter);
    IEnumerable<T> ReadAll(Func<T, bool>? filter = null);  //stage 1 only, Reads all objects
    void Update(T item); //Updates object
    void Delete(int id); //Deletes an object by its Id 
}
