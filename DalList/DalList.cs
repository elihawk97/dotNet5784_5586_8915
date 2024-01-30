namespace Dal;

using DalApi;
using DO;

sealed internal class DalList : IDal 
{
    public static IDal Instance { get; } = new DalList();
    private DalList() { } 
    public ICrud<Task> Task =>  new TaskImplementation();

    public ICrud<Engineer> Engineer => new EngineerImplementation();

    public ICrud<Dependency> Dependency => new DependencyImplementation();
}
