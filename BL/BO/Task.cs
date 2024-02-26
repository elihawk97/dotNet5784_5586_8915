using DO;
namespace BO;

/// <summary>
/// Task class for the BO layer
/// </summary>
public class Task
{    public int Id { get; init; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public TaskStatus Status { get; set; }
    public List<TaskInList> Dependencies { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? ProjectedStartDate { get; set; }
    public DateTime? ActualStartDate { get; set; }
    public DateTime? ProjectedEndDate { get; set; }
    public DateTime? DeadLine { get; set; }
    public DateTime? ActualEndDate { get; set; }
    public string? Deliverable { get; set; }
    public Engineer? EngineerForTask { get; set; }
    public Enums.ExperienceLevel Level { get; set; }
    public string? Notes { get; set; }
    public Milestone? Milestone { get; set; }

    /// <summary>
    /// Default constructor
    /// </summary>
    public Task() { }
    public Task( string? name, string? description, List<BO.TaskInList> tasksDependencies, DateTime? dateCreated, DateTime? projectedStartDate, DateTime? actualStartDate, DateTime? deadLine, DateTime? actualEndDate, string? deliverable, string? notes, Enums.ExperienceLevel experienceLevel)
    {
        Id = 0;
        Name = name;
        this.Level = (Enums.ExperienceLevel)experienceLevel;
        Dependencies = tasksDependencies;
        Description = description;
        DateCreated = dateCreated;
        ProjectedStartDate = projectedStartDate;
        ActualStartDate = actualStartDate;
        DeadLine = deadLine;
        ActualEndDate = actualEndDate;
        Deliverable = deliverable;
        Notes = notes;
    }


    /// <summary>
    /// Over riding the default ToString method to format the data
    /// </summary>
    /// <returns>formated object as a string</returns>
    public override string ToString()
    {
        string dependenciesString = Dependencies != null ? string.Join(", ", Dependencies.Select(dep => dep.Id.ToString())) : "None";

        return $@"
    Task
    ID={Id},
    Name={Name},
    Description={Description},
    Status={Status},
    Dependencies={dependenciesString},
    DateCreated={DateCreated},
    ProjectedStartDate={ProjectedStartDate},
    ActualStartDate={ActualStartDate},
    ProjectedEndDate={ProjectedEndDate},
    Deadline={DeadLine},
    ActualEndDate={ActualEndDate},
    Deliverable={Deliverable},
    EngineerTask={(EngineerForTask != null ? EngineerForTask.Id : "None")},
    Level={Level},
    Notes={Notes},
    Milestone={(Milestone != null ? Milestone.ToString() : "None")}
    ";
    }

}
