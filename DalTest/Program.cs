using DalList;
using DalApi;
using Dal;
using DO;
using System.Runtime.InteropServices;
using DalTest;
using System.ComponentModel;

internal class Program
{

    private static IDependency? sDependency = new DependencyImplementation();
    private static ITask? sTask = new TaskImplementation();
    private static IEngineer? sEngineer = new EngineerImplementation();
    public static DO.Task taskInput()
    {
        Console.WriteLine("Enter data to create new task");
        int Id = int.Parse(Console.ReadLine());
        string nickName = Console.ReadLine();
        string description = Console.ReadLine();
        bool isMilestone = bool.Parse(Console.ReadLine());
        DateTime dateCreated = DateTime.Parse(Console.ReadLine());
        DateTime projectedStartDate = DateTime.Parse(Console.ReadLine());
        DateTime? actualStartTime = DateTime.Parse(Console.ReadLine());
        System.TimeSpan duration = System.TimeSpan.Parse(Console.ReadLine());
        DateTime dealLine = DateTime.Parse(Console.ReadLine());
        DateTime? actualEndDate = DateTime.Parse(Console.ReadLine());
        string deliverables = Console.ReadLine();
        string notes = Console.ReadLine();
        int engineerID = int.Parse(Console.ReadLine());
        DO.Enums.ExperienceLevel level = (Enums.ExperienceLevel)Enum.Parse(typeof(Enums.ExperienceLevel), Console.ReadLine());


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
        sTask.Create(newTask);
    }

    public static void readTask()
    {
        Console.WriteLine("Enter the id of the Task you wish to see");
        int id = int.Parse(Console.ReadLine());
        DO.Task toPrint = sTask.Read(id);
        Console.WriteLine(toPrint);
    }

    public static void readAllTasks()
    {
        List<DO.Task> taskList = sTask.ReadAll();
        foreach (var task in taskList)
        {
            Console.WriteLine(task);
        }
    }

    public static void updateTask()
    {
        Console.WriteLine("Enter the id of the Task you wish to update");
        DO.Task updateTask = taskInput();
        sTask.Update(updateTask);

    }

    public static void deleteTask()
    {
        Console.WriteLine("Enter the id of the Task you wish to delete");
        int id = int.Parse(Console.ReadLine());
        sTask.Delete(id);
    }

    public static void useTask()
    {
        Console.WriteLine(@"Choose one of the following tasks to perfrom:
                            1: Create Task
                            2: Read Task
                            3: Update Task
                            4: Delete Task
                            5: Read all Tasks
        
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
        Console.WriteLine("Enter data to create new dependency");
        int Id = int.Parse(Console.ReadLine());
        int dependentTask = int.Parse(Console.ReadLine());
        int dependentOnTask = int.Parse(Console.ReadLine());
        string customerEmail = Console.ReadLine();
        string address = Console.ReadLine();
        DateTime createdOn = DateTime.Parse(Console.ReadLine());
        DateTime ship = DateTime.Parse(Console.ReadLine());
        DateTime delivery = DateTime.Parse(Console.ReadLine());

        DO.Dependency newDependency = new DO.Dependency(
            Id,
            dependentTask,
            dependentOnTask,
            customerEmail,
            address,
            createdOn,
            ship,
            delivery
        );
        return newDependency;
    }

    public static void createDependency()
    {
        DO.Dependency newDependency = dependencyInput();
        sDependency.Create(newDependency);
    }

    public static void readDependency()
    {
        Console.WriteLine("Enter the id of the Dependency you wish to see");
        int id = int.Parse(Console.ReadLine());
        DO.Dependency toPrint = sDependency.Read(id);
        Console.WriteLine(toPrint);
    }
    public static void readAllDependencies()
    {
        List<Dependency> dependencyList = sDependency.ReadAll();
        foreach (var dependency in dependencyList)
        {
            Console.WriteLine(dependency);
        }
    }
    public static void updateDependency()
    {
        Console.WriteLine("Enter the id of the Dependency you wish to update");
        DO.Dependency updateDependency = dependencyInput();
        sDependency.Update(updateDependency);
    }

    public static void deleteDependency()
    {
        Console.WriteLine("Enter the id of the Dependency you wish to delete");
        int id = int.Parse(Console.ReadLine());
        sDependency.Delete(id);
    }

    public static void useDependency()
    {
        Console.WriteLine(@"Choose one of the following dependencies to perform:
                        1: Create Dependency
                        2: Read Dependency
                        3: Update Dependency
                        4: Delete Dependency
                        5: Read all Dependencies
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
        Console.WriteLine("Enter data to create a new engineer");
        int Id = int.Parse(Console.ReadLine());
        string name = Console.ReadLine();
        string email = Console.ReadLine();
        double cost = double.Parse(Console.ReadLine());
        DO.Enums.ExperienceLevel level = (Enums.ExperienceLevel)Enum.Parse(typeof(Enums.ExperienceLevel), Console.ReadLine());

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
        sEngineer.Create(newEngineer);
    }

    public static void readEngineer()
    {
        Console.WriteLine("Enter the id of the Engineer you wish to see");
        int id = int.Parse(Console.ReadLine());
        DO.Engineer toPrint = sEngineer.Read(id);
        Console.WriteLine(toPrint);
    }
    public static void readAllEngineers()
    {
        List<Engineer> engineerList = sEngineer.ReadAll();
        foreach (var engineer in engineerList)
        {
            Console.WriteLine(engineer);
        }
    }


    public static void updateEngineer()
    {
        Console.WriteLine("Enter the id of the Engineer you wish to update");
        DO.Engineer updateEngineer = engineerInput();
        sEngineer.Update(updateEngineer);
    }

    public static void deleteEngineer()
    {
        Console.WriteLine("Enter the id of the Engineer you wish to delete");
        int id = int.Parse(Console.ReadLine());
        sEngineer.Delete(id);
    }

    public static void useEngineer()
    {
        Console.WriteLine(@"Choose one of the following actions to perform on an Engineer:
                        1: Create Engineer
                        2: Read Engineer
                        3: Update Engineer
                        4: Delete Engineer
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
                default:
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Request Failed.");
        }
    }



    private static void Main(string[] args)
    {
        try
        {
            Initialization.Do(sTask, sEngineer, sDependency);
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
        catch (Exception ex)
        {
            // Handle other types of exceptions
            Console.WriteLine($"Caught exception: {ex.GetType().Name}");
            Console.WriteLine($"Message: {ex.Message}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");
            // You can print or log more details based on your needs
        }
    }
}


