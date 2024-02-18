using Dal;
using DalApi;
using DO;
using System.Data.Common;
using System.Diagnostics;

namespace Dal;

sealed internal class DalXml : IDal
{
    public static IDal Instance = GetInstance();
    public ICrud<DO.Task> Task => new TaskImplementation();
    public ICrud<Engineer> Engineer => new EngineerImplementation();
    public ICrud<Dependency> Dependency => new DependencyImplementation();

    private DalXml() { }


    /// <summary>
    /// This will only initiailze the "Instance" of IDal if this function is called, since
    /// it is only initialized from within this function. 
    /// The if-null check ensures that is will only be initizalized once.
    /// The lock(typeof) uses a built-in C# mutex to ensure that only one thread
    /// can enter the code that initalizes the instance at a time, and if another was already there,
    /// the second if-null wont allow it to initalize the instance again.
    /// </summary>
    /// <returns>The instance of IDal</returns>
    public static IDal GetInstance()
    {
        // Use a double-checked locking idiom to ensure that the instance is only created once.
        if (Instance == null)
        {
            // Use a lazy initializer to delay the creation of the instance until it is needed.
            lock (typeof(DalXml))
            {
                if (Instance == null)
                {
                    Instance = new DalXml();
                }
            }
        }

        return Instance;
    }

    public void SetProjectEndDate(DateTime? endDate)
    {
        Config.ProjectEndDate = endDate; 
        XMLTools.SetProjectDates(endDate, "EndDate");
    }

    public void SetProjectStartDate(DateTime? startDate)
    {
        Config.ProjectStartDate = startDate; 
        XMLTools.SetProjectDates(startDate, "StartDate");
    }

    public DateTime? getProjectStartDate()
    {
        return Config.ProjectStartDate;
    }

    public DateTime? getProjectEndDate()
    {
        return Config.ProjectEndDate;
    }

    
}