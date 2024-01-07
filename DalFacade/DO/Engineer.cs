using DO;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace DO;
/// <summary>
/// Engineer Entity represents an engineer with all its props
/// </summary>
/// <param name="Id">Personal unique ID of the engineer </param>
/// <param name="name">Private Name of the engineer</param>
/// <param name="email"></param>
/// <param name="level"></param>
/// <param name="cost"></param>

public record Engineer
(
    int Id,
    int level,
    double cost,
    string? name = null,
    string? email = null
)
{
    public Engineer(int id, string name, string email, int level, double cost) : this()
    {
        this.Id = id;
        this.name = name;
        this.email = email;
        this.level = level;
        this.cost = cost;
    }

    public Engineer() : this(0,0,0) { } //empty ctor for stage 3

}