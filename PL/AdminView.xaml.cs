
using System.Windows;
using System.Windows.Controls;


namespace PL;

/// <summary>
/// Main Window for the Engineering Management System application.
/// This class handles the initialization of the application's main window and its events.
/// </summary>
public partial class AdminView : Window
{
    /// <summary>
    /// Static reference to the business logic layer.
    /// </summary>
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    private bool _isInitialized = false;

    /// <summary>
    /// Initializes a new instance of the MainWindow class.
    /// This constructor initializes the components necessary for the application's UI.
    /// </summary>
    public AdminView()
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
        new Task.TaskListWindow(null).Show();
    }


    private void btnSchedule_Click(object sender, RoutedEventArgs e)
    {
        // Ask user for confirmation
        MessageBoxResult result = MessageBox.Show("Do you really want to schedule tasks and enter into" +
            "production mode?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);

        // If user clicked "Yes", proceed with data initialization
        if (result == MessageBoxResult.Yes)
        {
            s_bl.Task.Scheduler();
            MessageBox.Show("Data initialization completed successfully.", "Initialization Done", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private void btnReset_Click(object sender, RoutedEventArgs e)
    {
        // Ask user for confirmation
        MessageBoxResult result = MessageBox.Show("Do you really want to reset the data?", "Confirm Reset", MessageBoxButton.YesNo, MessageBoxImage.Question);

        // If user clicked "Yes", proceed with data initialization
        if (result == MessageBoxResult.Yes)
        {
            s_bl.Task.Reset();
            s_bl.Engineer.Reset();
            MessageBox.Show("Data initialization completed successfully.", "Initialization Done", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }

    private void InitializeData_Click(object sender, RoutedEventArgs e)
    {
        if (!_isInitialized) // Check if already initialized
        {
            // Ask user for confirmation
            MessageBoxResult result = MessageBox.Show("Do you really want to Initialize the data?", "Confirm Initialization", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // If user clicked "Yes", proceed with data initialization
            if (result == MessageBoxResult.Yes)
            {
                // Assuming DalTest.Initialization.Do() is the method to reset data
                // You may need to adjust this line if the actual call is different
                DalTest.Initialization.Do();
                MessageBox.Show("Data initialization completed successfully.", "Initialization Done", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            // Set flag to true, indicating initialization is complete
            _isInitialized = true;

            // Disable the button
            (sender as Button).IsEnabled = false;
        }
    }

    private void GanttChart_Click(object sender, RoutedEventArgs e) { 
                (sender as Button).IsEnabled = false;

                new Task.GanttChart().Show(); 
            }


}