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


        public int EngineerId = 0;

        public BO.Engineer? CurrentEngineer { get; private set; }

        private void InitializeEngineer()
        {
            if (EngineerId == 0)
            {

                CurrentEngineer = new BO.Engineer();
            }
            else
            {
                // If EngineerId is not 0, it means we are in "Update" mode
                // Retrieve the object with the specified ID from the BL API
                // and assign it to the EngineerInfo property
                CurrentEngineer = s_bl.Engineer.ReadEngineer(EngineerId);
            }
        }

        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Call the appropriate BL API method (Add/Update) passing the CurrentEngineer object
                // Here, you should determine whether to call Add or Update based on EngineerId
                if (EngineerId == 0)
                {
                    s_bl.Engineer.Create(CurrentEngineer);
                }
                else
                {
                    s_bl.Engineer.UpdateEngineer(EngineerId, CurrentEngineer);
                }

                // Close the single-item window upon successful termination of the call
                this.Close();
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may surface during the API call
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public EngineerWindow()
        {
            InitializeComponent();
            InitializeEngineer(); 
        }
    }
}
