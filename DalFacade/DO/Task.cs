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
    

    public Task()
    {
    }

    public Task(int Id, string? nickName, string description, bool isMilestone, DateTime dateCreated, DateTime projectedStartDate, DateTime? actualStartTime, int duration, DateTime dealLine, DateTime? actualEndDate, string deliverable, string notes, int engineerID)
    {
        ID = Id;
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
        this.engineerID = engineerID;
    }


}


