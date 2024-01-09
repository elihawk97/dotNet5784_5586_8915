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
    
        internal static int EngineerIdCounter = 0;
        internal static int TaskIdCounter = 0;
        internal static int dependencyIdCounter = 0;

        // Private static field for the object identifier
        private static int objectIdCounter = 0;

        // Get method for the object identifier
        internal static int GetObjectId()
        {
            return ++objectIdCounter;
        }

        // Get method for the Engineer identifier
        internal static int GetNextEngineerId()
        {
            return ++EngineerIdCounter;
        }

        // Get method for the Task identifier
        internal static int GetNextTaskId()
        {
            return ++dependencyIdCounter;
        }
        // Get method for the Dependency identifier
        internal static int GetNextDependencyId()
        {
            return ++TaskIdCounter;
        }
    }
}
