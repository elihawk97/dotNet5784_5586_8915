using BlImplementation;
using System.Windows;
using System.Windows.Controls;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Bl blInstance;
        public DateTime Date = DateTime.Now;

        public MainWindow()
        {
            InitializeComponent();
            blInstance = new Bl(); // Assuming Bl is instantiated here.
            DataContext = Date; // Set DataContext for data binding
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


            // Prompt the user for an ID when the window loads
            string Id = Microsoft.VisualBasic.Interaction.InputBox("Please enter your Engineer ID:", "Engineer ID", "", -1, -1);
            int engineerIdInt = 0; 

            // Assuming input validation and conversion is handled as necessary
            if (!string.IsNullOrWhiteSpace(Id) && int.TryParse(Id, out engineerIdInt))
            {

             

                // Here, you would implement or call your logic to retrieve the engineer's information based on the ID
                // For demonstration, just showing the ID in a message box
                MessageBox.Show($"You entered ID: {Id}", "ID Entered");

                // Example: Set the CurrentEngineer based on the input ID
                // This is where you would retrieve the engineer details using the input ID
                // CurrentEngineer = YourMethodToGetEngineerById(input);

                // Update UI or do further processing here based on the CurrentEngineer details
            }
            else
            {
                MessageBox.Show("No ID entered. Please enter a valid Engineer ID to proceed.", "No ID Entered", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            new Engineer.TaskTracker(engineerIdInt).Show();
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            blInstance.ClockForward(); // Adjust the time forward by 1 day
            DataContext = blInstance.Clock;
        }

        private void ButtonBackward_Click(object sender, RoutedEventArgs e)
        {
            blInstance.ClockBackward(); // Adjust the time backward by 1 day
            DataContext = blInstance.Clock;
        }

        private void Reset_Clock(object sender, RoutedEventArgs e)
        {
            blInstance.Reset_Time(); // Adjust the time backward by 1 day
            DataContext = blInstance.Clock;
        }
    }


}
