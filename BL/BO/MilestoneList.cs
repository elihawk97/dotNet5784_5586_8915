
using DO;
using System.Reflection.Emit;

namespace BO; 

/// <summary>
/// Class for the List of milestones
/// </summary>
public class MilestoneList
{
    public string? Description { get; set;}
    public string? Name { get; set;}
    public DateOnly? DateCreated { get; set;}
    public TaskStatus Status { get; set; }
    public double Progress { get; set;}

    /// <summary>
    /// Over riding the default ToString method to format the data
    /// </summary>
    /// <returns>formated object as a string</returns>
    public override string ToString()
    {
        return $@"
    Name={Name},
    Description={Description},
    Date Created={DateCreated}
    Status={Status},
    Progress={Progress}
    ";
    }

}
