using DalApi;
using Dal;
using DO;
using System.Runtime.InteropServices;
using DalTest;
using System.ComponentModel;
internal class Program
{
    
    public static DO.Task taskInput()
    {


        Console.WriteLine("Enter data to create new task");
        Console.WriteLine("Enter task ID:");
        int Id = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter task nickname:");
        string nickName = Console.ReadLine();

        Console.WriteLine("Enter task description:");
        string description = Console.ReadLine();

        Console.WriteLine("Is it a milestone? (True/False):");
        bool isMilestone = bool.Parse(Console.ReadLine());

        Console.WriteLine("Enter date created (MM/dd/yyyy HH:mm:ss):");
        DateTime dateCreated = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Enter projected start date (MM/dd/yyyy HH:mm:ss):");
        DateTime projectedStartDate = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Enter actual start time (optional, leave blank if not applicable - MM/dd/yyyy HH:mm:ss):");
        DateTime? actualStartTime = string.IsNullOrEmpty(Console.ReadLine()) ? (DateTime?)null : DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Enter task duration (HH:mm:ss):");
        TimeSpan duration = TimeSpan.Parse(Console.ReadLine());

        Console.WriteLine("Enter deadline (MM/dd/yyyy HH:mm:ss):");
        DateTime dealLine = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Enter actual end date (optional, leave blank if not applicable - MM/dd/yyyy HH:mm:ss):");
        DateTime? actualEndDate = string.IsNullOrEmpty(Console.ReadLine()) ? (DateTime?)null : DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Enter deliverables:");
        string deliverables = Console.ReadLine();

        Console.WriteLine("Enter additional notes:");
        string notes = Console.ReadLine();

        Console.WriteLine("Enter engineer ID:");
        int engineerID = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter experience level (Junior/Mid/Senior):");
        DO.Enums.ExperienceLevel level = (DO.Enums.ExperienceLevel)Enum.Parse(typeof(DO.Enums.ExperienceLevel), Console.ReadLine());


        DO.Task newTask = new DO.Task(
            Id,
            nickName,
            description,
            dateCreated,
            projectedStartDate,
            actualStartTime,
            duration,
            dealLine,
            actualEndDate,
            deliverables,
            notes,
            engineerID,
            level
        );
        return newTask;
    }
    public static void createTask()
    {
        DO.Task newTask = taskInput();
        s_dal!.Task.Create(newTask);
    }

    public static void readTask()
    {
        Console.WriteLine("Enter the id of the Task you wish to see");
        int id = int.Parse(Console.ReadLine());
        try
        {
            DO.Task toPrint = s_dal!.Task.Read(id);
            Console.WriteLine(toPrint);
        }
        catch (DalDoesNotExistException ex)
        {
            Console.WriteLine(ex);
        }
    }

    public static void readAllTasks()
    {
        try
        {
            IEnumerable<DO.Task> taskList = s_dal!.Task.ReadAll();
            foreach (var task in taskList)
            {
                Console.WriteLine(task);
            }
        }
        catch (DalDoesNotExistException ex)
        {
            Console.WriteLine(ex);
        }

    }

        public static void updateTask()
    {
        try
        {
            Console.WriteLine("Enter the id of the Task you wish to update");
            DO.Task updateTask = taskInput();
            s_dal!.Task.Update(updateTask);
        }
        catch (DalDoesNotExistException ex)
        {
            Console.WriteLine(ex);
        }

    }

    public static void deleteTask()
    {
        try
        {
            Console.WriteLine("Enter the id of the Task you wish to delete");
            int id = int.Parse(Console.ReadLine());
            s_dal!.Task.Delete(id);
        }
        catch (DalDoesNotExistException ex)
        {
            Console.WriteLine(ex);
        }
    }

    public static void resetTasks()
    {
        s_dal!.Task.Reset();
    }

    public static void useTask()
    {
        Console.WriteLine(@"Choose one of the following tasks to perfrom:
                            0: Exit
                            1: Create Task
                            2: Read Task
                            3: Update Task
                            4: Delete Task
                            5: Read all Tasks
                            6: Reset
                            Any Other number to go back
        
        ");
        string inputString = Console.ReadLine();
        int parsedInt;
        bool success = int.TryParse(inputString, out parsedInt);
        if (success)
        {
            switch (parsedInt)
            {
                case 1:
                    createTask();
                    break;
                case 2:
                    readTask();
                    break;
                case 3:
                    updateTask();
                    break;
                case 4:
                    deleteTask();
                    break;
                case 5:
                    readAllTasks();
                    break;
                case 6:
                    resetTasks();
                    break;
                default:
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Request Failed.");
        }
    }


    public static DO.Dependency dependencyInput()
    {
        Console.WriteLine("Enter data to create a new dependency:");
        Console.WriteLine("Enter dependency ID:");
        int Id = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter dependent task ID:");
        int dependentTask = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter dependent on task ID:");
        int dependentOnTask = int.Parse(Console.ReadLine());


        DO.Dependency newDependency = new DO.Dependency(
            Id,
            dependentTask,
            dependentOnTask
        );
        return newDependency;
    }

    public static void createDependency()
    {
        DO.Dependency newDependency = dependencyInput();
        s_dal!.Dependency.Create(newDependency);
    }

    public static void readDependency()
    {
        try
        {
            Console.WriteLine("Enter the id of the Dependency you wish to see");
            int id = int.Parse(Console.ReadLine());
            DO.Dependency toPrint = s_dal!.Dependency.Read(id);
            Console.WriteLine(toPrint);
        }
        catch (DalDoesNotExistException ex)
        {
            Console.WriteLine(ex);
        }
    }
    public static void readAllDependencies()
    {
        try
        {
            IEnumerable<Dependency> dependencyList = s_dal!.Dependency.ReadAll();
            foreach (var dependency in dependencyList)
            {
                Console.WriteLine(dependency);
            }
        }
        catch (DalDoesNotExistException ex)
        {
            Console.WriteLine(ex);
        }
    }
    public static void updateDependency()
    {
        try
        {
            Console.WriteLine("Enter the id of the Dependency you wish to update");
            DO.Dependency updateDependency = dependencyInput();
            s_dal!.Dependency.Update(updateDependency);
        }
        catch (DalDoesNotExistException ex)
        {
            Console.WriteLine(ex);
        }
    }

    public static void deleteDependency()
    {
        try
        {
            Console.WriteLine("Enter the id of the Dependency you wish to delete");
            int id = int.Parse(Console.ReadLine());
            s_dal!.Dependency.Delete(id);
        }
        catch (DalDoesNotExistException ex)
        {
            Console.WriteLine(ex);
        }
    }
    public static void resetDependency()
    {
        s_dal!.Dependency.Reset();
    }

    public static void useDependency()
    {
        Console.WriteLine(@"Choose one of the following dependencies to perform:
                        1: Create Dependency
                        2: Read Dependency
                        3: Update Dependency
                        4: Delete Dependency
                        5: Read all Dependencies
                        6: Reset all Dependencies
    ");
        string inputString = Console.ReadLine();
        int parsedInt;
        bool success = int.TryParse(inputString, out parsedInt);
        if (success)
        {
            switch (parsedInt)
            {
                case 1:
                    createDependency();
                    break;
                case 2:
                    readDependency();
                    break;
                case 3:
                    updateDependency();
                    break;
                case 4:
                    deleteDependency();
                    break;
                case 5:
                    readAllDependencies();
                    break;
                case 6:
                    resetDependency();
                    break;
                default:
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Request Failed.");
        }
    }

    public static DO.Engineer engineerInput()
    {
        Console.WriteLine("Enter data to create a new engineer:");
        Console.WriteLine("Enter engineer ID:");
        int Id = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter engineer name:");
        string name = Console.ReadLine();

        Console.WriteLine("Enter engineer email:");
        string email = Console.ReadLine();

        Console.WriteLine("Enter engineer cost:");
        double cost = double.Parse(Console.ReadLine());

        Console.WriteLine("Enter engineer experience level (Junior/Mid/Senior):");
        DO.Enums.ExperienceLevel level = (DO.Enums.ExperienceLevel)Enum.Parse(typeof(DO.Enums.ExperienceLevel), Console.ReadLine());

        DO.Engineer newEngineer = new DO.Engineer(
            Id,
            name,
            email,
            level,
            cost
        );
        return newEngineer;
    }
    public static void createEngineer()
    {
        DO.Engineer newEngineer = engineerInput();
        s_dal!.Engineer.Create(newEngineer);
    }

    public static void readEngineer()
    {
        try
        {
            Console.WriteLine("Enter the id of the Engineer you wish to see");
            int id = int.Parse(Console.ReadLine());
            DO.Engineer toPrint = s_dal!.Engineer.Read(id);
            Console.WriteLine(toPrint);
        }
        catch (DalDoesNotExistException ex)
        {
            Console.WriteLine(ex);
        }
    }
    public static void readAllEngineers()
    {
        try
        {
            IEnumerable<Engineer> engineerList = s_dal!.Engineer.ReadAll();
            foreach (var engineer in engineerList)
            {
                Console.WriteLine(engineer);
            }
        }
        catch (DalDoesNotExistException ex)
        {
            Console.WriteLine(ex);
        }
    }

    public static void updateEngineer()
    {
        try
        {
            Console.WriteLine("Enter the id of the Engineer you wish to update");
            DO.Engineer updateEngineer = engineerInput();
            s_dal!.Engineer.Update(updateEngineer);
        }
        catch (DalDoesNotExistException ex)
        {
            Console.WriteLine(ex);
        }
    }

    public static void deleteEngineer()
    {
        try
        {
            Console.WriteLine("Enter the id of the Engineer you wish to delete");
            int id = int.Parse(Console.ReadLine());
            s_dal!.Engineer.Delete(id);
        }
        catch (DalDoesNotExistException ex)
        {
            Console.WriteLine(ex);
        }
    }
    public static void resetEngineer()
    {
        s_dal!.Engineer.Reset();
    }
    public static void useEngineer()
    {
        Console.WriteLine(@"Choose one of the following actions to perform on an Engineer:
                        1: Create Engineer
                        2: Read Engineer
                        3: Update Engineer
                        4: Delete Engineer
                        5: Read All Engineers
                        6: Reset Engineers List
    ");
        string inputString = Console.ReadLine();
        int parsedInt;
        bool success = int.TryParse(inputString, out parsedInt);
        if (success)
        {
            switch (parsedInt)
            {
                case 1:
                    createEngineer();
                    break;
                case 2:
                    readEngineer();
                    break;
                case 3:
                    updateEngineer();
                    break;
                case 4:
                    deleteEngineer();
                    break;
                case 5:
                    readAllEngineers();
                    break;
                case 6:
                    resetEngineer();
                    break;
                default:
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Request Failed.");
        }
    }

    static readonly IDal s_dal = new DalList(); //stage 2

    static void Main(string[] args)
    {

            Initialization.Do(s_dal);
            int choice = 1;
            while (choice != 0)
            {
                Console.WriteLine(@"These are the options of interfaces you may interact with:
                         1: Task
                         2: Engineer
                         3: Dependency
                        ");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        useTask();
                        break;
                    case 2:
                        useEngineer();
                        break;
                    case 3:
                        useDependency();
                        break;
                    default:
                        choice = 0;
                        break;
                }
            }

    }
}


