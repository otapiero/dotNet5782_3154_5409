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
using IBL.BO;
namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone : Window
    {
        IBL.IBL ibl;
        IBL.BO.DroneBL drone;
        IBL.BO.DroneToList NewDrone;
        private int clickCount;
        private bool newDrone;
        int station;
        public Drone(IBL.IBL bl1)
        {
            newDrone = true;
            ibl = bl1;
            drone = new IBL.BO.DroneBL();
            InitializeComponent();
            WeightCombo.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
            StationCombo.ItemsSource = ibl.ListStation();


            Option_Label.Visibility = Visibility.Hidden;
            OptionCombo.Visibility = Visibility.Hidden;
            BatteryText.Visibility = Visibility.Hidden;
            StatusCombo.Visibility = Visibility.Hidden;
            DeliveryText.Visibility = Visibility.Hidden;
            Longitude.Visibility = Visibility.Hidden;
            Latitude.Visibility = Visibility.Hidden;

            /*  cmbActions.Visibility = Visibility.Hidden;
               btnGO.Visibility = Visibility.Hidden;

               txtBattery.IsReadOnly = true;
               lblBattery.Foreground = Brushes.Gray;
               lblLocation.Foreground = Brushes.Gray;
               txtBattery.Foreground = Brushes.Gray;
               cmbLocation.Foreground = Brushes.Gray;
            */

        }
        public Drone(IBL.IBL bl1, IBL.BO.DroneToList x)
        {
            newDrone = false;
            InitializeComponent();
            ibl = bl1;
            drone = new();
            WeightCombo.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
            DroneView.Items.Add(x);
            
            StationCombo.Visibility = Visibility.Hidden;
            List<string> OperationsPosebilities = new List<string>()
                {
                    "Update Model",
                    "Assign a parcel to the drown",
                    "Pickedup a parcel",
                    "Suplay a parcel to costumer",
                    "Send the drone to Charge ",
                    "Release drown from charging"
            };
            OptionCombo.ItemsSource = OperationsPosebilities;
            DroneId.IsReadOnly = true;
            BatteryText.IsReadOnly = true;
            StationCombo.IsReadOnly = true;
            WeightCombo.IsReadOnly = true;
            Longitude.IsReadOnly = true;
            Latitude.IsReadOnly = true;

            /*DroneId.Foreground = Brushes.Gray;
            BatteryText.Foreground = Brushes.Gray;
            lblLocation.Foreground = Brushes.Gray;
            StationCombo.Foreground = Brushes.Gray;
            lblWeight.Foreground = Brushes.Gray;
            txtID.Foreground = Brushes.Gray;
            txtBattery.Foreground = Brushes.Gray;
            txtStation.Foreground = Brushes.Gray;
            cmbWeight.Foreground = Brushes.Gray;
            cmbLocation.Foreground = Brushes.Gray;*/

            DroneId.Text = x.Id.ToString();
            ModelText.Text = x.Model;
            DeliveryText.Text = "0";
            Latitude.Text = x.CurrentLocation.Lattitude.ToString();
            Longitude.Text = x.CurrentLocation.Longitude.ToString();
            StatusCombo.SelectedItem = x.status.ToString();
            StationCombo.Items.Add("???");
            BatteryText.Text = x.Battery.ToString();
            WeightCombo.SelectedItem = x.Weight;
            MessageBox.Show(x.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WeightCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            drone.Weight = (WeightCategories)WeightCombo.SelectedItem;
           // NewDrone.Weight = (WeightCategories)WeightCombo.SelectedItem;
        }

        private void StationCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var item = StationCombo.SelectedItem;
            var x = (IBL.BO.BaseStationToList)item;
            station = x.Id;
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = true;
            }
        }

        private void DroneId_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = true;
            }
        }


        private void ModelText_SelectionChanged(object sender, RoutedEventArgs e)
        {
            drone.Model = ModelText.Text;
        }

        private void DroneId_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var number = int.Parse(DroneId.Text);
            drone.Id = number;

        }

        private void Update_Bottun(object sender, RoutedEventArgs e)
        {
           
            if (newDrone)
            {
                try
                {

                    addNewDrone();


                }
                catch (Exception)
                {
                    MessageBoxResult mbResult = MessageBox.Show("press OK to continue, else press Cancel", "Error Occurred", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                    switch (mbResult)
                    {
                        case MessageBoxResult.OK:
                            break;
                        case MessageBoxResult.Cancel:
                            this.Close();
                            break;
                    }
                }
            }
            else
            {
                try
                {
                    
                    updateADrone();
                }
                catch (Exception)
                {
                    MessageBox.Show("Errohhr", "Errohhr", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        
        private void updateADrone()
        {
            try
            {
       
                switch (OptionCombo.SelectedIndex)
                {
                    case -1:
                        MessageBox.Show("Choose Action", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
                    case 0: // update
                        ibl.UpdateDroneModel(int.Parse(DroneId.Text), ModelText.Text.ToString());
                        break;
                    case 1: // Assign a parcel to a drown
                        ibl.AssignPackageToDrone(Convert.ToInt32(DroneId.Text));
                        break;
                    case 2: // Pickup a parcel

                        ibl.CollectPackage(Convert.ToInt32(DroneId.Text));
                        MessageBox.Show("Err", "Err", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
                    case 3: // Suplay a parcel to costumer 
                        ibl.DeliverPackage(Convert.ToInt32(DroneId.Text));
                        break;
                    case 4: // Charge drown
                        ibl.SendDroneToCharge(Convert.ToInt32(DroneId.Text));
                        break;
                    case 5: // Release drown from station
                        ibl.RelesaeDroneFromCharge(Convert.ToInt32(DroneId.Text), 12);
                        break;
                }
                MessageBox.Show("Done", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                DroneId.Background = Brushes.White;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void addNewDrone()
        {
            try
            {
                ibl.AddNewDrone(drone.Id, drone.Model, (int)drone.Weight, station);
                MessageBoxResult mbResult = MessageBox.Show("The Drone was uploud!", "The Drone was uploud!", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                switch (mbResult)
                {
                    case MessageBoxResult.OK:
                        this.Close();
                        break;
                    case MessageBoxResult.Cancel:
                        this.Close();
                        break;
                }


            }
            catch (Exception)
            {
                MessageBoxResult mbResult = MessageBox.Show("press OK to continue, else press Cancel", "Error Occurred", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                switch (mbResult)
                {
                    case MessageBoxResult.OK:
                        break;
                    case MessageBoxResult.Cancel:
                        this.Close();
                        break;
                }
            }
        }
        private void BatteryText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OptionCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           

        }
    }
}
