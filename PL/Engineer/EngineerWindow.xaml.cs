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
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {

        public static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public BO.Enums.ExperienceLevel ExpLevel { get; set; } = BO.Enums.ExperienceLevel.None;

        public static readonly DependencyProperty CurrentEngineerProperty = DependencyProperty.Register(
            "CurrentEngineer",
            typeof(BO.Engineer),
            typeof(EngineerWindow),
            new PropertyMetadata(default(BO.Engineer)));

        public BO.Engineer CurrentEngineer
        {
            get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
        }


        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (EngineerId == 0)
                {
                    // Add logic
                    s_bl.Engineer.Create(CurrentEngineer);
                }
                else
                {
                    // Update logic
                    s_bl.Engineer.UpdateEngineer(EngineerId, CurrentEngineer);
                }

                // Close the window or navigate away
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private int EngineerId = 0;

        public EngineerWindow(int engineerId = 0)
        {
            InitializeComponent();
            EngineerId = engineerId;

            if (EngineerId == 0)
            {
                // Add mode: Assign a new object
                CurrentEngineer = new BO.Engineer();
            }
            else
            {
                // Update mode: Fetch the object from BL
                CurrentEngineer = s_bl.Engineer.ReadEngineer(EngineerId);
            }
        }
    }
}
