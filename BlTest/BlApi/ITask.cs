namespace BlApi; 
public interface ITask
{

    public IEnumerable<Task> ReadAll(Func<BO.Task, bool>? filter);
    public Task ReadTask(int id);
    public void AddTask();
    public void UpdateTask(int id);
    public void DeleteTask(int id);
    public void UpdateStartDate(int id, DateTime newStartTime);
}
