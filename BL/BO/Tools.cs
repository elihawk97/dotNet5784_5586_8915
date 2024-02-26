using System.Collections;
using System.Reflection;
using System.Text;

namespace BO
{
    public static class Tools
    {
        private static readonly DalApi.IDal _dal = DalApi.Factory.Get;

        /// <summary>
        /// Converts an object to a string representation, including the values of its properties.
        /// </summary>
        /// <typeparam name="T">The type of the object to convert.</typeparam>
        /// <param name="obj">The object to convert.</param>
        /// <returns>A string representation of the object, including its properties.</returns>

        public static string ToStringProperty<T>(this T obj)
        {
            StringBuilder result = new StringBuilder();
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(obj);
                if (value != null)
                {
                    if (property.PropertyType.IsGenericType &&
                        property.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    {
                        IEnumerable collection = (IEnumerable)value;
                        StringBuilder collectionBuilder = new StringBuilder();

                        foreach (var item in collection)
                        {
                            collectionBuilder.Append($"{item}, ");
                        }

                        if (collectionBuilder.Length > 0)
                            collectionBuilder.Remove(collectionBuilder.Length - 2, 2); // Remove the last comma and space

                        result.AppendLine($"{property.Name}: [{collectionBuilder}]");
                    }
                    else
                    {
                        result.AppendLine($"{property.Name}: {value}");
                    }
                }
            }

            return result.ToString();
        }

        public static IEnumerable<BO.Task> scheduler(IEnumerable<BO.Task> tasks)
        {
            List<BO.Task> taskList = tasks.ToList();
            taskList = TopologicalSort(taskList);

            // For each task in the sorted list:
            // - Calculate the latest end date among its dependencies.
            // - Set the task's actual start date to one day after the latest dependency end date, or projected start date if it has no dependencies.
            // - Calculate the task's actual end date based on its projected dates.
            // - Check if the actual end date exceeds the task's deadline or project deadline.
            // - If all checks pass, proceed; otherwise, throw an exception.
            foreach (BO.Task task in taskList)
            {
                DateTime? latestEndDate = task.Dependencies.Max(dep => _dal.Task.Read(dep.Id).ActualEndDate);
                if (latestEndDate != null)
                {
                    task.ActualStartDate = latestEndDate.Value.AddDays(1);
                    task.ActualEndDate = latestEndDate.Value.Add((TimeSpan)(task.ProjectedEndDate - task.ProjectedStartDate));
                }
                else //If it has no dependencies, we set it start and finish days to be to the ones the
                    // the admin suggested
                {
                    task.ActualStartDate = task.ProjectedStartDate;
                    task.ActualEndDate = task.ProjectedEndDate;
                }
                // Check the date is set to finish before the task's deadline and project deadline
                if(task.ActualEndDate > task.DeadLine || task.ActualEndDate > _dal.getProjectEndDate())
                {
                    throw new BlTasksCanNotBeScheduled("The given tasks can not be completed before the deadline.");
                }
            }
            foreach(BO.Task task in taskList)
            {
                Console.WriteLine(task);
            }

            return null;          
        }


        public static List<BO.Task> TopologicalSort(List<BO.Task> tasks)
        {
            List<BO.Task> sortedTasks = new List<BO.Task>();
            var dependencies = new Dictionary<int, HashSet<int>>();
            int amount = tasks.Count();
            // Populate dependencies dictionary
            foreach (var task in tasks)
            {
                if (task.Dependencies != null && task.Dependencies.Count() != 0)
                {
                    dependencies[task.Id] = new HashSet<int>(task.Dependencies.Select(dep => dep.Id));
                }
            }

            var queue = new Queue<BO.Task>(tasks.Where(task => !dependencies.ContainsKey(task.Id) || dependencies[task.Id].Count == 0));

            while (queue.Any())
            {
                var currentTask = queue.Dequeue();
                sortedTasks.Add(currentTask);
                // Remove the current task from the dependencies of other tasks
                foreach (var dep in dependencies.Values)
                {
                    dep.Remove(currentTask.Id);
                }
                // Enqueue tasks whose dependencies are satisfied
                List<BO.Task> newTasks = tasks.Where(task => dependencies.ContainsKey(task.Id) && dependencies[task.Id].Count == 0 && !sortedTasks.Any(sortedTask => sortedTask.Id == task.Id)).ToList();
                foreach (var task in newTasks)
                {
                    queue.Enqueue(task);
                    tasks.RemoveAll(curTask => curTask.Id == task.Id);
                }
            }

            // Check for cycles
            if (sortedTasks.Count != amount)
            {
                throw new Exception("Cycle detected in the task dependencies. Unable to perform topological sort.");
            }

            return sortedTasks;
        }
    }
}
