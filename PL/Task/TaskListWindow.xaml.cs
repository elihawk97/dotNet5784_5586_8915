using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Task
{
    /// <summary>
    /// Interaction logic for TaskListWindow.xaml
    /// </summary>
    public partial class TaskListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public BO.Enums.ExperienceLevel ExpLevel { get; set; } = BO.Enums.ExperienceLevel.None;


        public IEnumerable<BO.TaskInList> TaskInList
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskInListProperty); }
            set { SetValue(TaskInListProperty, value); }
        }
        public static readonly DependencyProperty TaskInListProperty =
        DependencyProperty.Register("TaskInList",
        typeof(IEnumerable<BO.TaskInList>),
        typeof(TaskListWindow),
        new PropertyMetadata(null)
        );

        private void cbTaskSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            // Create the filter based on the selected experience level
            Func<BO.Task, bool> filter = item => item.Level == ExpLevel;

                    // Apply the filter
                    TaskInList = (ExpLevel == BO.Enums.ExperienceLevel.None) ?
                                s_bl?.Task.ReadAll(0)! :
                                s_bl?.Task.ReadAll(filter)!;
                
        }


        public TaskListWindow(BO.Task? task)
        {
            if (task == null)
            {
                TaskInList = s_bl?.Task.ReadAll(0)!;
            }
            else
            {
                TaskInList = task.Dependencies;
            }

            InitializeComponent();
        }

        /// <summary>
        /// Handles the mouse double click event of the list view.    /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listView = sender as ListView;
            var selectedTask = listView.SelectedItem as BO.TaskInList;

            if (selectedTask != null)
            {
                // Assuming EngineerWindow has a constructor that takes an engineer's ID for update mode
                TaskWindow TaskWindow = new TaskWindow(selectedTask.Id);
                TaskWindow.ShowDialog(); // Show the window modally
            }
            //RefreshTaskList();
        }

        /// <summary>
        /// Refreshes the list of engineers displayed.
        /// </summary>
        private void RefreshTaskList()
        {
            TaskInList = s_bl?.Task.ReadAll(null)!;
        }
    }
}
