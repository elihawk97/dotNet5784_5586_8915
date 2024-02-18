namespace BlApi; 
public interface ITask
{

    public IEnumerable<BO.Task> ReadAll(int engineerId);
    public BO.Task ReadTask(int id);
    public void CreateTask(BO.Task task);
    public void UpdateTask(int id, BO.Task boTask);
    public void DeleteTask(int id);
    public void UpdateStartDate(int id, DateTime newStartTime);
    public void Reset();
}
