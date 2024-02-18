using DO;
namespace Dal;

internal static class DataSource
{
    private static readonly Random random = new Random();

    // Internal lists for data entities
    internal static List<DO.Task> Tasks = new List<DO.Task>();
    internal static List<Engineer> Engineers = new List<Engineer>();
    internal static List<Dependency> Dependencies = new List<Dependency>();


    internal static class Config
    {
        // Internal static fields for auto-incremental identifier fields

        //Engineer
        internal const int startEngineerId = 0;
        private static int nextEngineerId = startEngineerId;
        internal static int NextEngineerId { get => nextEngineerId++; }

        //Task 
        internal const int startTaskId = 0;
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId {get => nextTaskId++;}

        //Dependency
        internal const int startDependencyId = 0;
        private static int nextDependencyId = startDependencyId;
        internal static int NextDependencyId { get => nextDependencyId++; }

        internal static DateTime? StartDate { get;  set; } = DateTime.Now.AddDays(-(365 + 365));
        internal static DateTime? EndDate { get;  set; } = DateTime.Now.AddDays(365 + 365);
    }

        


}
