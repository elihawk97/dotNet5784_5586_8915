using DalApi;
using DO;
using System.Diagnostics;

namespace DalXml;

sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public ICrud<DO.Task> Task => new TaskImplementation();
    public ICrud<Engineer> Engineer => new EngineerImplementation();
    public ICrud<Dependency> Dependency => new DependencyImplementation();
}

