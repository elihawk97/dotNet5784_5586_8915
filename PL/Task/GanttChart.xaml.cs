using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Task; 

/// <summary>
/// Interaction logic for GanttChart.xaml
/// </summary>
public partial class GanttChart : Window
{

    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    
        
    private DataTable DataTable { get; set; }
    public DataView DataView { get; set; }

    public IEnumerable<BO.Task> TaskList
    {
        get { return (IEnumerable<BO.Task>)GetValue(TaskListProperty); }
        set { SetValue(TaskListProperty, value); }
    }
    public static readonly DependencyProperty TaskListProperty =
    DependencyProperty.Register("TaskList",
    typeof(IEnumerable<BO.Task>),
    typeof(GanttChart),
    new PropertyMetadata(null)
    );

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        s_bl.Task.Scheduler(); 
        List<BO.Task> tasks = FetchTasks().ToList();
        DataTable = new DataTable();

        DateTime minStartDate = tasks.Min(task => task.ProjectedStartDate ?? DateTime.MaxValue);
        DateTime maxEndDate = tasks.Max(task => task.ProjectedEndDate ?? DateTime.MinValue);

        double diffResult = (maxEndDate - minStartDate).TotalDays;

        DataTable.Columns.Add("Tasks & Dependencies");

        for (int i = 0; i <= diffResult; i += 1)
        {
            DataTable.Columns.Add(minStartDate.AddDays(i).ToString("MM dd yyyy"));
        }

        DataRow dr = DataTable.NewRow();

        foreach (var task in tasks)
        {
            DataRow taskRow = DataTable.NewRow();
            taskRow[0] = task.Name; // Add task name in the first column
            

            // Ensure ProjectedStartDate is not null; otherwise, use DateTime.MaxValue
            DateTime start = task.ProjectedStartDate ?? DateTime.MaxValue;
           
            // Ensure ProjectedEndDate is not null; otherwise, use DateTime.MinValue
            DateTime end = task.ProjectedEndDate ?? DateTime.MinValue;


            for (int i = 1; i < DataTable.Columns.Count; i++)
            {
                
                DateTime columnDate = DateTime.ParseExact(DataTable.Columns[i].ColumnName, "MM dd yyyy", CultureInfo.InvariantCulture);
                if (start <= columnDate && columnDate <= end)
                {
                    taskRow[i] = "1"; // Mark this cell as part of the task's duration
                } else
                {
                    taskRow[i] = "";
                }
            }

            DataTable.Rows.Add(taskRow);

            foreach (var dependency in task.Dependencies)
            {
                // Check if the dependency row already exists
                bool dependencyExists = DataTable.AsEnumerable().Any(row => row.Field<string>(0) == $"Dependency: {dependency.Id}");

                if (!dependencyExists)
                {
                    DataRow dependencyRow = DataTable.NewRow();
                    dependencyRow[0] = $"Dependency: {dependency.Id}"; // Add dependency under the task
                    DataTable.Rows.Add(dependencyRow);
                }
            }

            DataTable.Rows.Add(DataTable.NewRow());
        }
        FooBar1.ItemsSource = DataTable.DefaultView;
    }
  
    public GanttChart()
    {
        FetchTasks(); // FetchTasks() should be a method that returns a collection of your tasks
        InitializeComponent();
    }

    private static IEnumerable<BO.Task> FetchTasks()
    {
        // Implement this method to return your tasks collection
        return s_bl.Task.ReadAll(0); // This is just an example; adjust it according to your actual method to fetch tasks
    }

}
