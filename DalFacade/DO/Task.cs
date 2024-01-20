using System.ComponentModel;

namespace DO;

/// <summary>
/// Represents a Task record that describes all details of a task.
/// 
/// Id - Task ID
/// NickName - The nickname for the task
/// Description - Description of the task
/// IsMilestone - Boolean indicating whether this task is a milestone or not
/// DateCreated - The date the project was created
/// ProjectedStartDate - Date the project is predicted to begin
/// actualStartDate - Date the project started
/// DeadLine - Predicted end date
/// ActualEndDate - Actual end date of the project
/// deliverables - Deliverables associated with the task
/// Notes - Additional notes about the project
/// EngineerID - ID of the engineer working on the project
/// DifficultyLevel - Enum indicating the level of difficulty of the project
/// </summary>
public record Task
{
    public int Id;
    string? NickName;
    string? Description;
    bool IsMilestone = false;
    DateTime DateCreated;
    public DateTime ProjectedStartDate;
    DateTime? ActualStartTime;
    TimeSpan? Duration;
    public DateTime DeadLine;
    DateTime? ActualEndDate;
    string? Deliverables; // may need to change field type
    string? Notes;
    int? EngineerID;
    private Enums.ExperienceLevel DifficultyLevel;
    public bool IsActive = true;

    /// <summary>
    /// Initializes a new instance of the Task class.
    /// </summary>
    public Task()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nickName"></param>
    /// <param name="description"></param>
    /// <param name="isMilestone"></param>
    /// <param name="dateCreated"></param>
    /// <param name="projectedStartDate"></param>
    /// <param name="actualStartTime"></param>
    /// <param name="duration"></param>
    /// <param name="dealLine"></param>
    /// <param name="actualEndDate"></param>
    /// <param name="deliverable"></param>
    /// <param name="notes"></param>
    /// <param name="engineerID"></param>
    public Task(int id, string nickName, string? description, DateTime dateCreated, DateTime projectedStartDate, DateTime? actualStartTime, TimeSpan? duration, DateTime deadLine, DateTime? actualEndDate, string? deliverables, string? notes, int? engineerID, Enums.ExperienceLevel difficultyLevel)
    {
        this.Id = Id;
        this.NickName = nickName;
        this.Description = description;
        this.IsMilestone = false;
        this.DateCreated = dateCreated;
        this.ProjectedStartDate = projectedStartDate;
        this.ActualStartTime = actualStartTime;
        this.Duration = duration;
        this.DeadLine = deadLine;
        this.ActualEndDate = actualEndDate;
        this.Deliverables = deliverables;
        this.Notes = notes;
        this.DifficultyLevel = difficultyLevel;
    }

    /// <summary>
    /// Overrides the default ToString method to provide a formatted string representation of the Task object.
    /// </summary>
    /// <returns>A formatted string representing the Task object.</returns>
    public override string ToString() => $@"
    ID={Id}, 
    NickName={NickName}, 
    Description={Description}, 
    IsMilestone={IsMilestone}, 
    DateCreated={DateCreated}, 
    ProjectedStartDate={ProjectedStartDate}, 
    ActualStartTime={ActualStartTime}, 
    Duration={Duration}, 
    DeadLine={DeadLine}, 
    ActualEndDate={ActualEndDate}, 
    Deliverables={Deliverables}, 
    Notes={Notes}, 
    EngineerID={EngineerID}, 
    DifficultyLevel={DifficultyLevel}
    ";
}

 

