using DO;
using System.Reflection.Emit;
using System.Xml.Linq;


namespace DO;
/// <summary>
/// Engineer Entity - represents an engineer with all its props
/// </summary>
/// <param name="Id">Personal unique ID of the engineer </param>
/// <param name="Name">Private Name of the engineer</param>
/// <param name="Email">the Email account of the engineer</param>
/// <param name="Level"> the Difficulty Level that the engineer is defind with</param>
/// <param name="Cost"> the price in wich the engineer gets per an hour </param>
public enum EngineerExperience
{ Beginner, AdvancedBeginner, Intermediate, Advanced, Expert }

public record Engineer
(
    int Id,
    EngineerExperience Level,
    double Cost,
    string? Name = null,
    string? Email = null
)
{
    public Engineer() : this(0,0,0) { } //empty ctor for stage 3

}