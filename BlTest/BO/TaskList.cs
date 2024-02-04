
using DO;
using System.Reflection.Emit;

namespace BO;

/// <summary>
/// List of Tasks for the BO Layer
/// </summary>
public class TaskList
{

    public int Id { get; init;}
    public string? Description { get; set; }
    public string? Name { get; set; }
    public TaskStatus Status { get; set; }

    /// <summary>
    /// Over riding the default ToString method to format the data
    /// </summary>
    /// <returns>formated object as a string</returns>
    public override string ToString()
    {
        return $@"
    ID={Id},
    Name={Name},
    Description={Description},
    Status={Status}
    ";
    }


}
