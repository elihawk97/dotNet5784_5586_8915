using BlApi;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    public void AddTask()
    {
        throw new NotImplementedException();
    }

    public void DeleteTask(int id)
    {
        throw new NotImplementedException();
    }

    public System.Threading.Tasks.Task Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<System.Threading.Tasks.Task> ReadAll(Func<BO.Task, bool>? filter)
    {
        throw new NotImplementedException();
    }

    public void UpdateStartDate(int id, DateTime newStartTime)
    {
        throw new NotImplementedException();
    }

    public void UpdateTask(int id)
    {
        throw new NotImplementedException();
    }
}
