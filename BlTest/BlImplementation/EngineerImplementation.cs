using BlApi;
using BO;

namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public void AddEngineer()
    {
        throw new NotImplementedException();
    }

    public void DeleteEngineer()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool> filter)
    {
        throw new NotImplementedException();
    }

    public Engineer ReadEngineer(int Id)
    {
        throw new NotImplementedException();
    }

    public void UpdateEngineer(Engineer item)
    {
        throw new NotImplementedException();
    }
}
