using System.ComponentModel;

namespace DO;

/// <summary>
/// Task record that describes all details of a task.
/// 
/// ID - task ID
/// nickName - the nickName for the task
/// description - description of the task
/// isMilestone - boolean whether this task is a milestone or not
/// dateCreated - the date the project was created
/// projectedStartDate - date the project is predicted to begin
/// actualStartDate - date the project started
/// deadLine - predicted end date
/// actualEndDate - actual end date of the project
/// deliverable - 
/// notes - additional notes about the project
/// engineerID - ID of the engineer working on the project
/// Difficulty - enum of level of difficulty of the project
/// </summary>
public record Task
{
    public int Id;
    string? nickName;
    string? description;
    bool isMilestone = false;
    DateTime dateCreated;
    DateTime projectedStartDate;
    DateTime? actualStartTime;
    TimeSpan? duration;
    DateTime deadLine;
    DateTime? actualEndDate;
    string? deliverables; // may need to change field type
    string? notes;
    int? engineerID;
    Enums.ExperienceLevel level;
    private string task;
    private DateTime created;
    private Enums.ExperienceLevel difficultyLevel;

    public Task()
    {
    }

    public Task(int id, string task, string? description, DateTime created, DateTime projectedStartDate, DateTime? actualStartTime, TimeSpan? duration, DateTime deadLine, DateTime? actualEndDate, string? deliverables, string? notes, int? engineerID, Enums.ExperienceLevel difficultyLevel)
    {
        Id = id;
        this.task = task;
        this.description = description;
        this.created = created;
        this.projectedStartDate = projectedStartDate;
        this.actualStartTime = actualStartTime;
        this.duration = duration;
        this.deadLine = deadLine;
        this.actualEndDate = actualEndDate;
        this.deliverables = deliverables;
        this.notes = notes;
        this.engineerID = engineerID;
    }


}


