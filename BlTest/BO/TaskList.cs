
using DO;

namespace BO;

public class TaskList
{

    public int Id { get; init;}
    public string? Description { get; set; }
    public string? Name { get; set; }
    public TaskStatus Status { get; set; }

}
