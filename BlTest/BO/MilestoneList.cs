
using DO;

namespace BO; 

public class MilestoneList
{
    public string? Description { get; set;}
    public string? Name { get; set;}
    public DateOnly? DateCreated { get; set;}
    public TaskStatus Status { get; set; }
    public double Progress { get; set;}
}
