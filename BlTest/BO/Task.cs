
namespace BO;

public class Task
{
    public int id;
    public string? name;
    public string? description;
    public TaskStatus status;
    public TaskList? dependencies;
    public DateTime projectedStartDate;
    public DateTime actualStartDate;
    public DateTime deadline;
    public DateTime actualEndDate;
    public string? deliverable;
    public Engineer? engineer;
    public ExperienceLevel level; 

}
