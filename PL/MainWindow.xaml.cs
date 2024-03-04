
using System.Windows;


namespace PL;

/// <summary>
/// Main Window for the Engineering Management System application.
/// This class handles the initialization of the application's main window and its events.
/// </summary>
public partial class MainWindow : Window
{

    /// <summary>
    /// Initializes a new instance of the MainWindow class.
    /// This constructor initializes the components necessary for the application's UI.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();

    }

     /// <summary>
    /// Event handler for the "Handle Engineers" button click.
    /// Opens the Engineer List Window to manage engineers.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An RoutedEventArgs that contains the event data.</param>
    private void btnEngineer_Click(object sender, RoutedEventArgs e)
    {
        new Engineer.EngineerListWindow().Show();
    }

    /// <summary>
    /// Event handler for the "Handle Tasks" button click.
    /// Opens the Task List Window to manage tasks.
    /// This method is currently not connected in the XAML, thus commented out.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An RoutedEventArgs that contains the event data.</param>
    private void btnTask_Click(object sender, RoutedEventArgs e)
    {
        new Task.TaskListWindow().Show();
    }

    
    /// <summary>
    /// Handles click event for initializing data.
    /// Asks the user for confirmation before proceeding.
    /// </summary>
    private void InitializeData_Click(object sender, RoutedEventArgs e)
    {
        // Ask user for confirmation
        MessageBoxResult result = MessageBox.Show("Do you really want to reset the data?", "Confirm Initialization", MessageBoxButton.YesNo, MessageBoxImage.Question);

        // If user clicked "Yes", proceed with data initialization
        if (result == MessageBoxResult.Yes)
        {
            // Assuming DalTest.Initialization.Do() is the method to reset data
            // You may need to adjust this line if the actual call is different
            DalTest.Initialization.Do();
            new Task.GanttChart().Show(); 
            MessageBox.Show("Data initialization completed successfully.", "Initialization Done", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    } 
}