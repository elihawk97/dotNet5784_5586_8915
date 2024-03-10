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

    public static readonly DependencyProperty CurrentEngineerProperty = DependencyProperty.Register(
      "CurrentEngineer",
      typeof(BO.Engineer),
      typeof(EngineerWindow),
      new PropertyMetadata(default(BO.Engineer)));

    public BO.Engineer CurrentEngineer
    {
        get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
        set { SetValue(CurrentEngineerProperty, value); }
    }

    private void AssignedTaskButton_Click(object sender, RoutedEventArgs e)
    {
        // Add your logic for what happens when the Current Task button is clicked
        MessageBox.Show("Current Task button clicked!");
    }

    private void TasksButton_Click(object sender, RoutedEventArgs e)
    {
        Func<BO.Task, bool>? filterByExperienceLevel = task =>
        {
            return task.Level == CurrentEngineer.Level;
        };

        TaskListWindow taskListWindow = new TaskListWindow(null); // Assuming a parameterless constructor is "Add" mode
        taskListWindow.ShowDialog(); // ShowDialog to make it modal

        // Add your logic for what happens when the Tasks button is clicked
        MessageBox.Show("Tasks button clicked!");
    }

    public TaskTracker()
    {
        InitializeComponent();
    }
}
