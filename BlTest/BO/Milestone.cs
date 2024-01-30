using DO;

namespace BO; 


    public class Milestone
    {
    public int id { get; init; }
    public string? name;
    public string? description;
    public DateOnly? dateCreated;
    public TaskStatus status;
    public DateTime? actualStartDate;
    public DateTime? deadline;
    public DateTime? actualEndDate;
    public double progress;
    public string? comments;
    public TaskList dependencies; 
    




    }

