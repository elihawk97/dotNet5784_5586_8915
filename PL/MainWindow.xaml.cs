using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private void btnEngineer_Click(object sender, RoutedEventArgs e)
        {
            new Engineer.EngineerListWindow().Show();
        }

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
                MessageBox.Show("Data initialization completed successfully.", "Initialization Done", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    
    public MainWindow()
        {
            InitializeComponent();

        }
    }
}