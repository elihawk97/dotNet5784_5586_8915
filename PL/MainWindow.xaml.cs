using BlImplementation;
using System.Windows;
using System.Windows.Controls;
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


        public MainWindow()
        {
            InitializeComponent();
            blInstance = new Bl(); // Assuming Bl is instantiated here.
            DataContext = Date; // Set DataContext for data binding
            ProductionMode = false;
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
            new Task.TaskListWindow(null).Show();
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
