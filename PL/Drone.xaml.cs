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
        int station;
        public Drone(IBL.IBL bl1)
        {

            ibl = bl1;
            drone = new IBL.BO.DroneBL();
            InitializeComponent();
            WeightCombo.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
            StationCombo.ItemsSource = ibl.ListStation();

           

           
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
            InitializeComponent();
            ibl = bl1;
            drone = new();
            WeightCombo.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
            DroneView.Items.Add(x);

            StationCombo.Visibility = Visibility.Hidden;
           
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
            StationCombo.SelectedItem = "???";
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
    }
}
