﻿using DO;
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
        internal const int startEngineerId = 0;
        private static int nextEngineerId = startEngineerId;
        internal static int NextEngineerId { get => nextEngineerId++; }


        internal const int startTaskId = 0;
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId { get => nextTaskId++; }

        internal const int startDependencyId = 0;
        private static int nextDependencyId = startDependencyId;
        internal static int NextDependencyId { get => nextDependencyId++; }

        internal static DateTime StartDate = DateTime.Now.AddDays(-(365 + 365));
        internal static DateTime EndDate = DateTime.Now.AddDays(365 + 365);


    }



       
       
       


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

        internal static DateTime getStartDate()
        {
            return StartDate;
        }
        internal static DateTime getEndDate()
        {
            return EndDate;
        }

        internal static void setStartDate(DateTime startDate)
        {
            StartDate = startDate;
        }
        internal static void setEndDate(DateTime endDate)
        {
            EndDate = endDate;
        }
    }
}
