using System;
using System.Collections.Generic;
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
using Task;

namespace Engineer; 

/// <summary>
/// Interaction logic for TaskTracker.xaml
/// </summary>
public partial class TaskTracker : Window
{
    public static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    private string? engineerId = null;
    int engineerIdInt;

    public string EngineerInfoString => GetEngineerInfoString();


    public static readonly DependencyProperty CurrentEngineerProperty = DependencyProperty.Register(
      "CurrentEngineer",
      typeof(BO.Engineer),
      typeof(TaskTracker),
      new PropertyMetadata(default(BO.Engineer)));

    public BO.Engineer CurrentEngineer
    {
        get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
        set { SetValue(CurrentEngineerProperty, value); }
    }

    public static readonly DependencyProperty CurrentTaskProperty = DependencyProperty.Register(
          "CurrentTask",
          typeof(BO.Task),
          typeof(TaskTracker),
          new PropertyMetadata(default(BO.Task)));

    public BO.Task CurrentTask
    {
        get { return (BO.Task)GetValue(CurrentTaskProperty); }
        set { SetValue(CurrentTaskProperty, value); }
    }

    private string GetEngineerInfoString()
    {
        return $"{CurrentEngineer?.Name} {CurrentEngineer?.Id} {CurrentEngineer?.Level}";
    }


    private void AssignedTaskButton_Click(object sender, RoutedEventArgs e)
    {
        if (CurrentTask != null)
        {
            new Task.ProductionTaskWindow(CurrentTask.Id).Show();
        }
        // Add your logic for what happens when the Current Task button is clicked
    }

    private void TasksButton_Click(object sender, RoutedEventArgs e)
    {
        if (CurrentTask == null)
        {


            TaskListWindow taskListWindow = new TaskListWindow(null, CurrentEngineer); // Assuming a parameterless constructor is "Add" mode
            taskListWindow.ShowDialog(); // ShowDialog to make it modal
        }
    }

    public TaskTracker(int id)
    {
        try
        {

            CurrentEngineer = s_bl.Engineer.ReadEngineer(id);
            CurrentTask = CurrentEngineer.Task;

            InitializeComponent();

        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}", "Read Exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        
    }
}
