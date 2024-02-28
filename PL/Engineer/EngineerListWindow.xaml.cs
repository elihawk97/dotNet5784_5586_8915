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

namespace Engineer
{
    /// <summary>
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>
    public partial class EngineerListWindow : Window
    {

        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public BO.Enums.ExperienceLevel ExpLevel { get; set; } = BO.Enums.ExperienceLevel.None;

        public IEnumerable<BO.Engineer> EngineerList
        {
            get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }
        public static readonly DependencyProperty EngineerListProperty =
        DependencyProperty.Register("EngineerList",
        typeof(IEnumerable<BO.Engineer>),
        typeof(EngineerListWindow),
        new PropertyMetadata(null)
        );

        private void RefreshEngineerList()
        {
            EngineerList = s_bl?.Engineer.ReadAll(null)!;
        }

        private void cbEngineerSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {


                // Create the filter based on the selected experience level
                Func<BO.Engineer, bool> filter = item => item.Level == ExpLevel;

                // Apply the filter
                EngineerList = (ExpLevel == BO.Enums.ExperienceLevel.None) ?
                            s_bl?.Engineer.ReadAll(null)! :
                            s_bl?.Engineer.ReadAll(filter)!;

            }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Instantiate the EngineerWindow in "Add" mode (not passing an ID)
            EngineerWindow engineerWindow = new EngineerWindow(); // Assuming a parameterless constructor is "Add" mode
            engineerWindow.ShowDialog(); // ShowDialog to make it modal
            RefreshEngineerList(); 
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listView = sender as ListView;
            var selectedEngineer = listView.SelectedItem as BO.Engineer;

            if (selectedEngineer != null)
            {
                // Assuming EngineerWindow has a constructor that takes an engineer's ID for update mode
                EngineerWindow engineerWindow = new EngineerWindow(selectedEngineer.Id);
                engineerWindow.ShowDialog(); // Show the window modally
            }
            RefreshEngineerList();
        }

        public EngineerListWindow()
        {
            EngineerList = s_bl?.Engineer.ReadAll(null)!;
            InitializeComponent();
        }
    }
}
