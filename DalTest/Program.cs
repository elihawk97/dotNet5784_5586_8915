using DalList;
using DalApi;
using Dal;
using DO;
using System.Runtime.InteropServices;

internal class Program
{
    public DO.Task taskInput()
    {
        Console.WriteLine("Enter data to create new task");
        string nickName = Console.ReadLine();
        string description = Console.ReadLine();
        bool isMilestone = bool.Parse(Console.ReadLine());
        DateTime dateCreated = DateTime.Parse(Console.ReadLine());
        DateTime projectedStartDate = DateTime.Parse(Console.ReadLine());
        DateTime? actualStartTime = DateTime.TryParse(Console.ReadLine()) ? DateTime.Parse(Console.ReadLine()) : null;
        int duration = int.Parse(Console.ReadLine());
        DateTime dealLine = DateTime.Parse(Console.ReadLine());
        DateTime? actualEndDate = DateTime.TryParse(Console.ReadLine()) ? DateTime.Parse(Console.ReadLine()) : null;
        string deliverables = Console.ReadLine();
        string notes = Console.ReadLine();
        int engineerID = int.Parse(Console.ReadLine());
        Enums.ExperienceLevel level = (Enums.ExperienceLevel)Enum.Parse(typeof(Enums.ExperienceLevel), Console.ReadLine());

        DO.Task newTask = new DO.Task(
            nickName,
            description,
            isMilestone,
            dateCreated,
            projectedStartDate,
            actualStartTime,
            duration,
            dealLine,
            actualEndDate,
            deliverables,
            notes
        );
        return newTask;
    }
    public void createTask()
    {
        DO.Task newTask = taskInput();
        sTask.Create(newTask);
    }

    public void readTask()
    {
        Console.WriteLine("Enter the id of the Task you wish to see");
        int id = int.Parse(Console.ReadLine());
        DO.Task toPrint = sTask.Read(id);
        Console.WriteLine(toPrint);
    }

    public void readAllTasks()
    {
        List<DO.Task> taskList = sTask.ReadAll();
        foreach (var task in taskList)
        {
            Console.WriteLine(task);
        }
    }

    public void updateTask()
    {
        Console.WriteLine("Enter the id of the Task you wish to update");
        DO.Task updateTask = taskInput();
        sTask.Update(updateTask);

    }

    public void deleteTask()
    {
        Console.WriteLine("Enter the id of the Task you wish to delete");
        int id = int.Parse(Console.ReadLine());
        sTask.Delete(id);
    }

    public void useTask()
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


    public DO.Dependency dependencyInput()
    {
        Console.WriteLine("Enter data to create new dependency");
        int dependentTask = int.Parse(Console.ReadLine());
        int dependentOnTask = int.Parse(Console.ReadLine());
        string customerEmail = Console.ReadLine();
        string address = Console.ReadLine();
        DateTime createdOn = DateTime.Parse(Console.ReadLine());
        DateTime ship = DateTime.Parse(Console.ReadLine());
        DateTime delivery = DateTime.Parse(Console.ReadLine());

        DO.Dependency newDependency = new DO.Dependency(
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

    public void createDependency()
    {
        DO.Dependency newDependency = dependencyInput();
        sDependency.Create(newDependency);
    }

    public void readDependency()
    {
        Console.WriteLine("Enter the id of the Dependency you wish to see");
        int id = int.Parse(Console.ReadLine());
        DO.Dependency toPrint = sDependency.Read(id);
        Console.WriteLine(toPrint);
    }
    public void readAllDependencies()
    {
        List<Dependency> dependencyList = sDependency.ReadAll();
        foreach (var dependency in dependencyList)
        {
            Console.WriteLine(dependency);
        }
    }
    public void updateDependency()
    {
        Console.WriteLine("Enter the id of the Dependency you wish to update");
        DO.Dependency updateDependency = dependencyInput();
        sDependency.Update(updateDependency);
    }

    public void deleteDependency()
    {
        Console.WriteLine("Enter the id of the Dependency you wish to delete");
        int id = int.Parse(Console.ReadLine());
        sDependency.Delete(id);
    }

    public void useDependency()
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

    public DO.Engineer engineerInput()
    {
        Console.WriteLine("Enter data to create a new engineer");
        string name = Console.ReadLine();
        string email = Console.ReadLine();
        double cost = double.Parse(Console.ReadLine());

        DO.Engineer newEngineer = new DO.Engineer(
            name,
            email,
            cost
        );
        return newEngineer;
    }

    public void createEngineer()
    {
        DO.Engineer newEngineer = engineerInput();
        sEngineer.Create(newEngineer);
    }

    public void readEngineer()
    {
        Console.WriteLine("Enter the id of the Engineer you wish to see");
        int id = int.Parse(Console.ReadLine());
        DO.Engineer toPrint = sEngineer.Read(id);
        Console.WriteLine(toPrint);
    }
    public void readAllEngineers()
    {
        List<Engineer> engineerList = sEngineer.ReadAll(); 
        foreach(var engineer in engineerList)
        {
            Console.WriteLine(engineer);
        }
    }


    public void updateEngineer()
    {
        Console.WriteLine("Enter the id of the Engineer you wish to update");
        DO.Engineer updateEngineer = engineerInput();
        sEngineer.Update(updateEngineer);
    }

    public void deleteEngineer()
    {
        Console.WriteLine("Enter the id of the Engineer you wish to delete");
        int id = int.Parse(Console.ReadLine());
        sEngineer.Delete(id);
    }

    public void useEngineer()
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
        private static IDependency? sDependency = new DependencyImplementation();
    private static ITask? sTask = new TaskImplementation();
    private static IEngineer? sEngineer = new EngineerImplementation();

        try{
            Initialization.Do(sDependency, sTask, sEngineer);
            int choice = 1;
            while(choice != 0){
                Console.WriteLine(@"These are the options of interfaces you may interact with:
                     1: Task
                     2: Engineer
                     3: Dependency
                    ");
            }
        }
        catch {

}
    }
}