using BlApi;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public void AddTask()
    {
        throw new NotImplementedException();
    }

    public void DeleteTask(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Task ReadTask(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter)
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
