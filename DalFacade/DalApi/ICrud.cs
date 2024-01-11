using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

public interface ICrud<T> where T : class
{
    int Create(T item); //Creates a new object

    ///need to add a "reset" function (see the general document)
    T? Read(int id); //Reads object by its ID 
    IEnumerable<T?> ReadAll(Func<T, bool>? filter = null);  //stage 1 only, Reads all objects
    void Update(T item); //Updates object
    void Delete(int id); //Deletes an object by its Id

}
