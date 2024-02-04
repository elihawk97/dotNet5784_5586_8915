using DO;
using System.Reflection.Emit;

namespace BO; 

/// <summary>
/// Milestone for the BO Layer, defining the completion of 
/// certain tasks to be a milestone
/// </summary>
    public class Milestone
    {
    public int Id { get; init; }
    public string? Name { get; set;}
    public string? Description { get; set; }
    public DateOnly? DateCreated { get; set; }
    public TaskStatus Status { get; set; }
    public DateTime? ActualStartDate { get; set; }
    public DateTime? Deadline { get; set; }
    public DateTime? ActualEndDate { get; set; }
    public double Progress { get; set; }
    public string? Comments { get; set; }
    public TaskList? Dependencies { get; set; }

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
    Status={Status},
    Dependencies={(Dependencies != null ? Dependencies.ToString() : "None")},
    DateCreated={DateCreated},
    ActualStartDate={ActualStartDate},
    Deadline={Deadline},
    ActualEndDate={ActualEndDate},
    Comments={Comments},
    Progress={Progress}
    ";
    }



}

