namespace BlApi;

/// <summary>
/// Class of function to manipulate the Task list in the Bl layer 
/// </summary>
public interface ITask
{

    public IEnumerable<BO.Task> ReadAll(int engineerId);
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool> filter);
    public BO.Task ReadTask(int id);
    public BO.Task ReadTask(Func<DO.Task, int, bool> filter, int engineerId);
    public void Scheduler();
    public void CreateTask(BO.Task task);
    public void UpdateTask(int id, BO.Task boTask);
    public void DeleteTask(int id);
    public void UpdateStartDate(int id, DateTime newStartTime);
    public void Reset();
}
