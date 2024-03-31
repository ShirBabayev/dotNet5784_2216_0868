using DO;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace BO;

public class TaskInList
{
    public int Id {  get; init; }
    public string? NickName { get; init; }

    public string? Description {  get; init; }   
    public BO.Status Status {  get; init; }
    public override string ToString()
    {
        return "Id: " + Id +
            "\nNickname= " + NickName +
            "\nStatus= " + Status +
            //"\nEngineer's level= " + Level +
            "\n------------------------------";
    }
}
