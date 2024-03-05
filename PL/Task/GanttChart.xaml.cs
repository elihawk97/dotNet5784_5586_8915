using System;
using System.Collections.Generic;
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
    /// Interaction logic for GanttChart.xaml
    /// </summary>
    public partial class GanttChart : Window
    {

        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        private Canvas _canvas;

        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            _canvas = sender as Canvas;
            if (_canvas == null) return;

            // Now that we're sure _canvas is initialized, fetch tasks and generate the Gantt chart.
            var tasks = FetchTasks();
            GenerateGanttChart(tasks);
        }
        public IEnumerable<BO.Task> TaskList
        {
            get { return (IEnumerable<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }
        public static readonly DependencyProperty TaskListProperty =
        DependencyProperty.Register("TaskList",
        typeof(IEnumerable<BO.Task>),
        typeof(GanttChart),
        new PropertyMetadata(null)
        );

        public GanttChart()
        {
            InitializeComponent();
            var tasks = FetchTasks(); // FetchTasks() should be a method that returns a collection of your tasks
        }

        private IEnumerable<BO.Task> FetchTasks()
        {
            // Implement this method to return your tasks collection
            return s_bl.Task.ReadAll(0); // This is just an example; adjust it according to your actual method to fetch tasks
        }

        private void GenerateGanttChart(IEnumerable<BO.Task> tasks)
        {
                                              // Adjust these based on your actual data structure
            DateTime minStartDate = tasks.Min(task => task.ProjectedStartDate ?? DateTime.MaxValue);
            DateTime maxEndDate = tasks.Max(task => task.ProjectedEndDate ?? DateTime.MinValue);

            double totalDays = (maxEndDate - minStartDate).TotalDays;
            double scale = _canvas.Width / totalDays; // Assume you have a Canvas named 'canvas'

            foreach (var task in tasks)
            {
                DrawTask(task, minStartDate, scale);
               
            }
        }

        private void DrawTask(BO.Task task, DateTime minStartDate, double scale)
        {
            if (task.ProjectedStartDate == null || task.ProjectedEndDate == null) return;

            double left = ((DateTime)task.ProjectedStartDate - minStartDate).TotalDays * scale;
            double width = ((DateTime)task.ProjectedEndDate - (DateTime)task.ProjectedStartDate).TotalDays * scale;

            Rectangle rect = new Rectangle
            {
                Width = width,
                Height = 20, // Arbitrary height
                Fill = Brushes.Blue,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            Canvas.SetLeft(rect, left);
            // The vertical position can depend on the task's order or other factors
            Canvas.SetTop(rect, 20 * FetchTasks().ToList().IndexOf(task));
            _canvas.Children.Add(rect);
        }




        
    }
}
