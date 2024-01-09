using System.ComponentModel;

namespace DO;

/// <summary>
/// Represents a Task record that describes all details of a task.
/// 
/// ID - Task ID
/// _nickName - The nickname for the task
/// _description - Description of the task
/// _isMilestone - Boolean indicating whether this task is a milestone or not
/// _dateCreated - The date the project was created
/// _projectedStartDate - Date the project is predicted to begin
/// actualStartDate - Date the project started
/// _deadLine - Predicted end date
/// _actualEndDate - Actual end date of the project
/// deliverables - Deliverables associated with the task
/// _notes - Additional notes about the project
/// _engineerID - ID of the engineer working on the project
/// DifficultyLevel - Enum indicating the level of difficulty of the project
/// </summary>
public record Task
{
    public int Id;
    string? _nickName;
    string? _description;
    bool _isMilestone = false;
    DateTime _dateCreated;
    DateTime _projectedStartDate;
    DateTime? _actualStartTime;
    TimeSpan? _duration;
    DateTime _deadLine;
    DateTime? _actualEndDate;
    string? _deliverables; // may need to change field type
    string? _notes;
    int? _engineerID;
    private Enums.ExperienceLevel _difficultyLevel;

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
        this._nickName = nickName;
        this._description = description;
        this._isMilestone = false;
        this._dateCreated = dateCreated;
        this._projectedStartDate = projectedStartDate;
        this._actualStartTime = actualStartTime;
        this._duration = duration;
        this._deadLine = deadLine;
        this._actualEndDate = actualEndDate;
        this._deliverables = deliverables;
        this._notes = notes;
        this._difficultyLevel = difficultyLevel;
    }

    /// <summary>
    /// Overrides the default ToString method to provide a formatted string representation of the Task object.
    /// </summary>
    /// <returns>A formatted string representing the Task object.</returns>
    public override string ToString() => $@"
    ID={Id}, 
    NickName={_nickName}, 
    Description={_description}, 
    IsMilestone={_isMilestone}, 
    DateCreated={_dateCreated}, 
    ProjectedStartDate={_projectedStartDate}, 
    ActualStartTime={_actualStartTime}, 
    Duration={_duration}, 
    DeadLine={_deadLine}, 
    ActualEndDate={_actualEndDate}, 
    Deliverables={_deliverables}, 
    Notes={_notes}, 
    EngineerID={_engineerID}, 
    DifficultyLevel={_difficultyLevel}
    ";
}


