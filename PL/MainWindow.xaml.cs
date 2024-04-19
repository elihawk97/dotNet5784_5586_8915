using BlImplementation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Task;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Bl blInstance;
        public static DateTime Date = DateTime.Now;
        // Property to get or set the value of ProductionMode
        public static bool ProductionMode = false;
        /// <summary>
        /// Dependency property for the current Task being manipulated.
        /// </summary>
        public static readonly DependencyProperty ProductionModeProperty = DependencyProperty.Register(
            "ProductionMode",
            typeof(bool),
            typeof(MainWindow),
            new PropertyMetadata(default(bool)));
        public static bool _isInitialized = false;
        private DispatcherTimer timer;
        private bool isStopping = false; // Flag to signal timer stop


        public MainWindow()
        {
            InitializeComponent();
            blInstance = new Bl(); // Assuming Bl is instantiated here.
            DataContext = Date; // Set DataContext for data binding
            ProductionMode = false;

            // Initialize and start the timer (adjust interval as needed)
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            lock (Bl.clockLock) // Lock using the static object
            {
                blInstance.ClockForwardHour();
            }
            DataContext = blInstance.Clock; // Notify UI of data change
            Date = blInstance.Clock.Date;
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            isStopping = true; // Set flag to signal timer stop

            // Wait for timer to stop using Dispatcher.Invoke
            Dispatcher.Invoke(() =>
            {
                timer.Stop();
            });

            base.OnClosing(e);
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

            if (ProductionMode == true)
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
            else
            {
                MessageBox.Show("You are still in Planning Mode", "System has not been initialized yet", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            lock (Bl.clockLock)
            { // Lock using the static object
                blInstance.ClockForward(); // Adjust the time forward by 1 day
            }
            Date = blInstance.Clock.Date;
            DataContext = blInstance.Clock;
        }

        private void ButtonBackward_Click(object sender, RoutedEventArgs e)
        {
            lock (Bl.clockLock)
            { // Lock using the static object
                blInstance.ClockBackward();
            }
            // Adjust the time backward by 1 day
            Date = blInstance.Clock.Date;
            DataContext = blInstance.Clock;
        }

        private void Reset_Clock(object sender, RoutedEventArgs e)
        {
            lock (Bl.clockLock)
            { // Lock using the static object
                blInstance.Reset_Time(); // Adjust the time backward by 1 day
            }
            DataContext = blInstance.Clock;
        }
    }
}
