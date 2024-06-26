﻿using BO;
using Engineer;
using PL;
using System.Security.RightsManagement;
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

        public BO.Engineer? engineer { get; set; } = null; 

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

        /// <summary>
        /// Dependency property for the current engineer being manipulated.
        /// </summary>
        public static readonly DependencyProperty CurrentTaskProperty = DependencyProperty.Register(
            "CurrentTask",
            typeof(BO.Task),
            typeof(TaskListWindow),
            new PropertyMetadata(default(BO.Task)));

        /// <summary>
        /// Gets or sets the current engineer being manipulated.
        /// </summary>
        public BO.Task CurrentTask
        {
            get { return (BO.Task)GetValue(CurrentTaskProperty); }
            set { SetValue(CurrentTaskProperty, value); }
        }


        private void cbTaskSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Create the filter based on the selected experience level
            Func<BO.Task, bool> filter = item => item.Level == ExpLevel;
            try
            {
                if (CurrentTask == null)
                {
                    // Apply the filter
                    TaskInList = (ExpLevel == BO.Enums.ExperienceLevel.None) ?
                                s_bl?.Task.ReadAll(0)! :
                                s_bl?.Task.ReadAll(filter)!;
                }
                else
                {
                    TaskInList = CurrentTask.Dependencies;
                }
            }
            catch (BlDoesNotExistException ex)
            {
                MessageBox.Show(
                  "List is empty! Add Tasks or initialize system.",
                  "Empty List",
                  MessageBoxButton.OK,
                  MessageBoxImage.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                  ex.Message,
                  "Data Query Error",
                  MessageBoxButton.OK,
                  MessageBoxImage.Information);
            }

        }

        public TaskListWindow(BO.Task? task = null, BO.Engineer? e = null )
        {


            InitializeComponent();


            if (task != null)
            {
                try
                {
                    CurrentTask = task;
                    TaskInList = task.Dependencies;
                }
                catch (BlDoesNotExistException ex)
                {
                    // Do Nothing and let the window initialize
                }
            }
            else if (e != null)
            {
                try
                {

                    Func<BO.Task, bool> filter = item => (item.Level <= e.Level && item.EngineerForTask == null);

                    engineer = e; 
                    TaskInList = s_bl?.Task.ReadAll(filter);
                }
                catch (BlDoesNotExistException ex)
                {
                    // Do Nothing and let the window initialize
                }
            }
            else
            {
                try
                {
                    TaskInList = s_bl?.Task.ReadAll(0)!;
                }
                catch (BlDoesNotExistException ex)
                {
                    // Do Nothing and let the window initialize
                }
            }

        }


        /// <summary>
        /// Handles the mouse double click event of the list view.    /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            var listView = sender as ListView;
            var selectedTask = listView.SelectedItem as BO.TaskInList;
            
            if (engineer != null)
            {
                try
                {
                    BO.Task task = s_bl.Task.ReadTask(selectedTask.Id);
                    task.EngineerForTask = engineer;
                    task.ActualStartDate = MainWindow.Date; 


                    s_bl.Task.UpdateTask(task.Id, task);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(
                 "Error Assigning Engineer To Task",
                 "Assignment Error",
                 MessageBoxButton.OK,
                 MessageBoxImage.Information);
                }
            }
          
           
            else if (selectedTask != null && MainWindow.ProductionMode == false)
            {
                // Assuming EngineerWindow has a constructor that takes an engineer's ID for update mode
                TaskWindow TaskWindow = new TaskWindow(selectedTask.Id);
                TaskWindow.ShowDialog(); // Show the window modally
            }
            else if (selectedTask != null)
            {
                ProductionTaskWindow productionTaskWindow = new ProductionTaskWindow(selectedTask.Id);
                productionTaskWindow.ShowDialog();
            }
            RefreshTaskList();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentTask == null && MainWindow.ProductionMode == false)
            {
                // Instantiate the EngineerWindow in "Add" mode (not passing an ID)
                TaskWindow taskWindow = new TaskWindow(0); // Assuming a parameterless constructor is "Add" mode
                taskWindow.ShowDialog(); // ShowDialog to make it modal
                RefreshTaskList();
            }
            else if(CurrentTask != null && MainWindow.ProductionMode == false)
            {
                AddDependenciesView dependenciesWindow = new AddDependenciesView(CurrentTask); // Assuming a parameterless constructor is "Add" mode
                dependenciesWindow.ShowDialog(); // ShowDialog to make it modal
                RefreshTaskList();
            }
            else
            {
                MessageBox.Show(
                  "Adding a Task or Dependencies is not possible in production mode!",
                  "Production Mode Restriction",
                  MessageBoxButton.OK,
                  MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Refreshes the list of engineers displayed.
        /// </summary>
        private void RefreshTaskList()
        {
            try
            {

                if (engineer != null)
                {
                    Func<BO.Task, bool> filter = item => (item.Level <= engineer.Level && item.EngineerForTask == null);
                    TaskInList = s_bl?.Task.ReadAll(filter);
                }
                else if (CurrentTask == null)
                {
                    TaskInList = s_bl?.Task.ReadAll(null)!;
                }
                else
                {
                    TaskInList = CurrentTask.Dependencies;
                }
            }
            catch (BlDoesNotExistException ex)
            {
                MessageBox.Show(
                  "List is empty! Add Tasks or initialize system.",
                  "Empty List",
                  MessageBoxButton.OK,
                  MessageBoxImage.Information);
            }
        }
    }
}
