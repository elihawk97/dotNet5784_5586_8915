using BO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Task
{
    /// <summary>
    /// Interaction logic for TaskDependencies.xaml
    /// </summary>
    public partial class AddDependenciesView : Window
    {
        public static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.Enums.ExperienceLevel ExpLevel { get; set; } = BO.Enums.ExperienceLevel.None;

        /// <summary>
        /// Dependency property for the current Task being manipulated.
        /// </summary>
        public static readonly DependencyProperty Current_TaskProperty = DependencyProperty.Register(
            "Current_Task",
            typeof(BO.Task),
            typeof(TaskWindow),
            new PropertyMetadata(default(BO.Task)));

        /// <summary>
        /// Gets or sets the current engineer being manipulated.
        /// </summary>
        public BO.Task Current_Task
        {
            get { return (BO.Task)GetValue(Current_TaskProperty); }
            set { SetValue(Current_TaskProperty, value); }
        }

        public IEnumerable<BO.TaskInList> TaskInList2
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskInList2Property); }
            set { SetValue(TaskInList2Property, value); }
        }
        public static readonly DependencyProperty TaskInList2Property =
        DependencyProperty.Register("TaskInList2",
        typeof(IEnumerable<BO.TaskInList>),
        typeof(AddDependenciesView),
        new PropertyMetadata(null)
        );


        public void Add_Dependency(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var listView = sender as ListView;
                TaskInList toAdd = listView.SelectedItem as BO.TaskInList;
                Current_Task.Dependencies.Add(toAdd);
                s_bl.Task.UpdateTask(Current_Task.Id, Current_Task);
            }
            catch (BlDoesNotExistException ex)
            {
                MessageBox.Show(
                  $"Task with ID={Current_Task.Id} does not exist!", "Not Created",
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

        public AddDependenciesView(BO.Task task)
        {
            try
            {
                Current_Task = task;
                TaskInList2 = s_bl.Task.ReadAll(0);
                InitializeComponent();
            }
            catch (BlDoesNotExistException ex)
            {
                MessageBox.Show(
                  $"Task with ID={task.Id} does not exist!", "Not Created",
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

        private void cbTaskSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Create the filter based on the selected experience level
                Func<BO.Task, bool> filter = item => item.Level == ExpLevel;
                if (Current_Task == null)
                {
                    // Apply the filter
                    TaskInList2 = (ExpLevel == BO.Enums.ExperienceLevel.None) ?
                                s_bl?.Task.ReadAll(0)! :
                                s_bl?.Task.ReadAll(filter)!;
                }
                else
                {
                    TaskInList2 = s_bl?.Task.ReadAll(0);
                }
            }
            catch (BlDoesNotExistException ex)
            {
                MessageBox.Show(
                  $"The Task list is empty.", "Not Created",
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
