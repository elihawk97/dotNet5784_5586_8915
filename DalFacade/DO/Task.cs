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
    string description;
    bool isMilestone = false;
    DateTime dateCreated;
    DateTime projectedStartDate;
    DateTime? actualStartTime;
    int duration;
    DateTime dealLine;
    DateTime? actualEndDate;
    string deliverables; // may need to change field type
    string notes;
    int engineerID;
    Enums.ExperienceLevel level;
    
    /// <summary>
    /// 
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
    public Task(string? nickName, string description, bool isMilestone, DateTime dateCreated, DateTime projectedStartDate, DateTime? actualStartTime, int duration, DateTime dealLine, DateTime? actualEndDate, string deliverable, string notes)
    {
        this.nickName = nickName;
        this.description = description;
        this.isMilestone = isMilestone;
        this.dateCreated = dateCreated;
        this.projectedStartDate = projectedStartDate;
        this.actualStartTime = actualStartTime;
        this.duration = duration;
        this.dealLine = dealLine;
        this.actualEndDate = actualEndDate;
        this.deliverables = deliverable;
        this.notes = notes;
    }


}


