
namespace BO;

public class Task
{
    public int Id { get; init; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public TaskStatus Status { get; set; }
    public TaskList? Dependencies { get; set; }
    public DateTime ProjectedStartDate { get; set; }
    public DateTime? ActualStartDate { get; set; }
    public DateTime Deadline { get; set; }
    public DateTime? ActualEndDate { get; set; }
    public string? Deliverable { get; set; }
    public Engineer? Engineer { get; set; }
    public ExperienceLevel Level { get; set; }

}
