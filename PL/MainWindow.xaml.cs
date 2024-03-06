using System.Windows;
using System.Windows.Controls;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isInitialized = false;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void InitializeData_Click(object sender, RoutedEventArgs e)
        {
            if (!_isInitialized) // Check if already initialized
            {
                // Ask user for confirmation
                MessageBoxResult result = MessageBox.Show("Do you really want to reset the data?", "Confirm Initialization", MessageBoxButton.YesNo, MessageBoxImage.Question);

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

                new Task.GanttChart().Show(); 
            }
        }

        /// <summary>
        /// Event handler for the "Handle Engineers" button click.
        /// Opens the Engineer List Window to manage engineers.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An RoutedEventArgs that contains the event data.</param>
        private void btnAdminView_Click(object sender, RoutedEventArgs e)
        {
            new AdminView().Show();
        }

        /// <summary>
        /// Event handler for the "Handle Tasks" button click.
        /// Opens the Task List Window to manage tasks.
        /// This method is currently not connected in the XAML, thus commented out.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An RoutedEventArgs that contains the event data.</param>
        private void btnEngineerView_Click(object sender, RoutedEventArgs e)
        {
            new Task.TaskListWindow().Show();
        }
    }


}
