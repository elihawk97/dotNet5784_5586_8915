using DalApi;
using DO;

namespace DalXml;

sealed public class DalXml : IDal
{
    public ICrud<DO.Task> Task => new TaskImplementation();
    public ICrud<Engineer> Engineer => new EngineerImplementation();
    public ICrud<Dependency> Dependency => new DependencyImplementation();
}

