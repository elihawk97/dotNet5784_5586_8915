using BO;
using PL;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Converters;


namespace Task
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        public static readonly BlApi.IBl s_bl = BlApi.Factory.Get();


        // Implement INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;

        // Method to raise PropertyChanged event
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




        public TaskWindow(int Id)
        {
            try
            {
                InitializeComponent();
                if (Id == 0)
                {

                    // Add mode: Assign a new object
                    CurrentTask = new BO.Task() { Id = 0 };
                }
                else
                {
                    // Update mode: Fetch the object from BL
                    CurrentTask = s_bl.Task.ReadTask(Id);
                }
            }
            catch (BlDoesNotExistException ex)
            {
                MessageBox.Show(
                  $"Task with ID={CurrentTask.Id} does not exist!", "Not Created",
                  MessageBoxButton.OK,
                  MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                "Internal Server Error 504. Please try again later.",
                "Query Error",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Dependency property for the current Task being manipulated.
        /// </summary>
        public static readonly DependencyProperty CurrentTaskProperty = DependencyProperty.Register(
            "CurrentTask",
            typeof(BO.Task),
            typeof(TaskWindow),
            new PropertyMetadata(default(BO.Task)));

        /// <summary>
        /// Gets or sets the current engineer being manipulated.
        /// </summary>
        public BO.Task CurrentTask
        {
            get { return (BO.Task)GetValue(CurrentTaskProperty); }
            set { SetValue(CurrentTaskProperty, value); }
        }

        /// <summary>
        /// Handles the click event of the add/update button.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurrentTask.Id == 0)
                {
                    CurrentTask.DateCreated = MainWindow.Date;
                    s_bl.Task.CreateTask(CurrentTask);
                }
                else {
                    s_bl.Task.UpdateTask(CurrentTask.Id, CurrentTask);
                }


                // Close the window or navigate away
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void View_Dependencies(object sender, RoutedEventArgs e)
        {
            try
            {
                new Task.TaskListWindow(CurrentTask).Show();
                RefreshTask();
            }
            catch(BlDoesNotExistException ex)
            {
                MessageBox.Show(
                  "You must first create the task before adding dependencies!",
                  "Not Created",
                  MessageBoxButton.OK,
                  MessageBoxImage.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                "Internal Server Error 504. Please try again later.",
                "Query Error",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
            }
        }

        public void addEngineer(object sender, RoutedEventArgs e) {
            new Engineer.EngineerListWindow(CurrentTask).Show();
            RefreshTask();
        }

        /// <summary>
        /// Refreshes the list of engineers displayed.
        /// </summary>
        private void RefreshTask()
        {
            try
            {
                CurrentTask = s_bl?.Task.ReadTask(CurrentTask.Id)!;
            }
            catch (BlDoesNotExistException ex)
            {
                MessageBox.Show(
                  $"Task with ID={CurrentTask.Id} does not exist!",
                  "Not Created",
                  MessageBoxButton.OK,
                  MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                "Internal Server Error 504. Please try again later.",
                "Query Error",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
            }
        }
    }
}
