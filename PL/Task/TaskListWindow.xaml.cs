using Engineer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Task
{
    /// <summary>
    /// Interaction logic for TaskListWindow.xaml
    /// </summary>
    public partial class TaskListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public BO.Enums.ExperienceLevel ExpLevel { get; set; } = BO.Enums.ExperienceLevel.None;


        public IEnumerable<BO.Task> TaskList
        {
            get { return (IEnumerable<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }
        public static readonly DependencyProperty TaskListProperty =
        DependencyProperty.Register("TaskList",
        typeof(IEnumerable<BO.Task>),
        typeof(TaskListWindow),
        new PropertyMetadata(null)
        );

        private void cbTaskSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            // Create the filter based on the selected experience level
            Func<BO.Task, bool> filter = item => item.Level == ExpLevel;

                    // Apply the filter
                    TaskList = (ExpLevel == BO.Enums.ExperienceLevel.None || ExpLevel == BO.Enums.ExperienceLevel.All) ?
                                s_bl?.Task.ReadAll(0)! :
                                s_bl?.Task.ReadAll(filter)!;
                
        }


        public TaskListWindow()
        {

        TaskList = s_bl?.Task.ReadAll(0)!;

            InitializeComponent();
        }
    }
}
