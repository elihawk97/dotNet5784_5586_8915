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
        try
        {
            List<BO.TaskInList> tasksList = FetchTasks().ToList();
            List<BO.Task> tasks = new List<BO.Task>();

            foreach (var task in tasksList)
            {
                tasks.Add(s_bl.Task.ReadTask(task.Id));
            }

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
                taskRow[0] = task.Name + " Id: " + task.Id; // Add task name in the first column

            // Ensure ProjectedStartDate is not null; otherwise, use DateTime.MaxValue
            DateTime start = (task.ActualStartDate ?? task.ProjectedStartDate ?? DateTime.MaxValue).Date;

                // Ensure ProjectedEndDate is not null; otherwise, use DateTime.MinValue
                DateTime end = (task.ProjectedEndDate ?? DateTime.MinValue).Date;

            string dependencyString = task.Dependencies != null
            ? string.Join(" ", task.Dependencies.Select(d => d.Name).Distinct())
            : string.Empty;

            for (int i = 1; i < DataTable.Columns.Count; i++)
            {
                // Parse the column name into a DateTime object
                DateTime columnDate = DateTime.ParseExact(DataTable.Columns[i].ColumnName, "MM dd yyyy", CultureInfo.InvariantCulture).Date;

                // Check if the column date is within the task's start and end dates
                if (start <= columnDate && columnDate <= end)
                {
                    // On the start date, use the dependency string if it exists; otherwise, use the task status
                    // On other dates within the range, use the task status
                    taskRow[i] = start == columnDate && !string.IsNullOrEmpty(dependencyString)
                        ? dependencyString
                        : task.Status.ToString();
                }
                else
                {
                    // If the column date is outside the task's date range, leave the cell empty
                    taskRow[i] = "";
                }
            }
            DataTable.Rows.Add(taskRow);

            }
            FooBar1.ItemsSource = DataTable.DefaultView;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                  "Please ensure system was properly initialized. Gantt Chart Creation Failed!",
                  "ReadAll Exception",
                  MessageBoxButton.OK,
                  MessageBoxImage.Warning);
        }
    }

    public GanttChart()
    {
        FetchTasks(); // FetchTasks() should be a method that returns a collection of your tasks
        InitializeComponent();
    }

    private static IEnumerable<BO.TaskInList> FetchTasks()
    {
        try
        {
            // Implement this method to return your tasks collection
            return s_bl.Task.ReadAll(null); // This is just an example; adjust it according to your actual method to fetch tasks
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                  "Please ensure system was properly initialized. ReadAll Failed!",
                  "ReadAll Exception",
                  MessageBoxButton.OK,
                  MessageBoxImage.Warning);
        }
        return null;
    }

}