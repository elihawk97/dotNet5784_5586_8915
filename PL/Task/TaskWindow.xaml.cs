using BlImplementation;
using BO;
using Engineer;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;


namespace Task
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        public static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        // Property to hold the list of engineers
        private ObservableCollection<BO.Engineer> _engineersList;
        public ObservableCollection<BO.Engineer> EngineersList
        {
            get { return _engineersList; }
            set
            {
                _engineersList = value;
                OnPropertyChanged();
            }
        }

        // Implement INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;

        // Method to raise PropertyChanged event
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        // Method to retrieve the list of engineers from the data source
        private void LoadEngineers()
        {
            // Assuming bl.Engineer is an instance of your business logic class for engineers
            // and ReadAll() method retrieves all engineers from the data source
            EngineersList = new ObservableCollection<BO.Engineer>(s_bl.Engineer.ReadAll(x => x != null));
        }

        public TaskWindow(int Id = 0)
        {
            InitializeComponent();
            if (Id == 0)
            {
                // Initialize the EngineersList property
                EngineersList = new ObservableCollection<BO.Engineer>();

                // Call a method to populate the EngineersList property
                LoadEngineers();
                // Add mode: Assign a new object
                CurrentTask = new BO.Task() { Id = 0};
            }
            else
            {
                // Update mode: Fetch the object from BL
                CurrentTask = s_bl.Task.ReadTask(Id);
                LoadEngineers();

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
            new Task.TaskListWindow(CurrentTask).Show();
            RefreshTask();
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
            CurrentTask = s_bl?.Task.ReadTask(CurrentTask.Id)!;
        }
    }
}
