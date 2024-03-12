using PL;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Converters;


namespace Task
{
    /// <summary>
    /// Interaction logic for ProductionTaskWindow.xaml
    /// </summary>
    public partial class ProductionTaskWindow : Window
    {
        public static readonly BlApi.IBl s_bl = BlApi.Factory.Get();


        // Implement INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;

        // Method to raise PropertyChanged event
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        /// <summary>
        /// Constructor for the ProductionTaskWindow
        /// Initializes the Task attribute to the Task with 
        /// the passed Id
        /// </summary>
        /// <param name="Id">Id of the Task</param>
        public ProductionTaskWindow(int Id)
        {
            try
            {
                InitializeComponent();
                // Update mode: Fetch the object from BL
                CurrentTask3 = s_bl.Task.ReadTask(Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Read Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Dependency property for the current Task being manipulated.
        /// </summary>
        public static readonly DependencyProperty CurrentTask3Property = DependencyProperty.Register(
            "CurrentTask3",
            typeof(BO.Task),
            typeof(ProductionTaskWindow),
            new PropertyMetadata(default(BO.Task)));

        /// <summary>
        /// Gets or sets the current engineer being manipulated.
        /// </summary>
        public BO.Task CurrentTask3
        {
            get { return (BO.Task)GetValue(CurrentTask3Property); }
            set { SetValue(CurrentTask3Property, value); }
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
                if (CurrentTask3.Id == 0)
                {
                    CurrentTask3.DateCreated = MainWindow.Date;
                    s_bl.Task.CreateTask(CurrentTask3);
                }
                else
                {
                    s_bl.Task.UpdateTask(CurrentTask3.Id, CurrentTask3);
                }


                // Close the window or navigate away
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Update Exception",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Mark_As_Done(object sender, RoutedEventArgs e)
        {
            try
            {
                 if(CurrentTask3.ActualEndDate == null)
                {
                    CurrentTask3.ActualEndDate = MainWindow.Date;
                    s_bl.Task.UpdateTask(CurrentTask3.Id, CurrentTask3);
                }
                else
                {
                    MessageBox.Show("Task Already Finished. Can not assign new end date.", "Task Date", MessageBoxButton.OK, MessageBoxImage.Error);
                }


                // Close the window or navigate away
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Update Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        public void View_Dependencies(object sender, RoutedEventArgs e)
        {
            new Task.TaskListWindow(CurrentTask3).Show();
            RefreshTask();
        }

        public void addEngineer(object sender, RoutedEventArgs e)
        {
            new Engineer.EngineerListWindow(CurrentTask3).Show();
            RefreshTask();
        }

        /// <summary>
        /// Refreshes the list of engineers displayed.
        /// </summary>
        private void RefreshTask()
        {
            try
            {
                CurrentTask3 = s_bl?.Task.ReadTask(CurrentTask3.Id)!;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Update Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
