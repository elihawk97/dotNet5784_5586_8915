using DO;

namespace BO; 


    public class Milestone
    {
    public int id { get; init; }
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





}

