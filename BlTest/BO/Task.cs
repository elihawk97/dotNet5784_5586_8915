﻿
using DO;

namespace BO;

/// <summary>
/// Task class for the BO layer
/// </summary>
public class Task
{
    public int Id { get; init; }
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
    public ExperienceLevel Level { get; set; }
    public string? Notes { get; set; }
    public Milestone? Milestone { get; set; }

    /// <summary>
    /// Over riding the default ToString method to format the data
    /// </summary>
    /// <returns>formated object as a string</returns>
    public override string ToString()
    {
        return $@"
    Task
    ID={Id},
    Name={Name},
    Description={Description},
    Status={Status},
    Dependencies={(Dependencies != null ? Dependencies.ToString() : "None")},
    DateCreated={DateCreated},
    ProjectedStartDate={ProjectedStartDate},
    ActualStartDate={ActualStartDate},
    ProjectedEndDate={ProjectedEndDate},
    Deadline={DeadLine},
    ActualEndDate={ActualEndDate},
    Deliverable={Deliverable},
    EngineerT={(EngineerForTask != null ? EngineerForTask.ToString() : "None")},
    Level={Level},
    Notes={Notes},
    Milestone={(Milestone != null ? Milestone.ToString() : "None")}
    ";
    }

}
