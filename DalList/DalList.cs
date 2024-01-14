namespace Dal;

using DalApi;
using DO;

sealed public class DalList : IDal
{
    public ICrud<Task> Task =>  new TaskImplementation();

    public ICrud<Engineer> Engineer => new EngineerImplementation();

    public ICrud<Dependency> Dependency => new DependencyImplementation();
}
