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
        int cancel = 0;
        
        private bool newDrone;
        int station;

        Dictionary<int, string> options = new Dictionary<int, string>(){
             {0,  "Update Model" },
             {1, "Assign a parcel to the drown" },
             {2,  "Pickedup a parcel"},
             {3, "Suplay a parcel to costumer" },
             {4,"Send the drone to Charge " },
             {5,"Release drown from charging" } };

        public Drone(IBL.IBL bl1)
        {
            newDrone = true;
            ibl = bl1;
            drone = new IBL.BO.DroneBL();
            InitializeComponent();
            WeightCombo.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
            StationCombo.ItemsSource = ibl.ListStation();

            MainGrid.RowDefinitions[2].Height = new GridLength(0);

          
     
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
            

            StationCombo.Visibility = Visibility.Hidden;
            Station_Label.Visibility = Visibility.Hidden;
            Weiht_content.Content = x.Weight;
            WeightCombo.Visibility = Visibility.Hidden;
            Location.Content = x.CurrentLocation.ToString();

            StatusContenetLabel.Content = x.status;


            BatteryText.Content = x.Battery.ToString().Substring(0,5);
         
           

            if(x.status== DroneStatuses.Available)
            {
                OptionCombo.ItemsSource = from y in options
                                          where (y.Key <= 1 || y.Key > 3)&&y.Key!=5
                                          select new string(y.Value);

            }
            else if(x.status==DroneStatuses.Delivery)
            {
                OptionCombo.ItemsSource = from y in options
                                          where y.Key != 1 && y.Key!=5
                                          select new string(y.Value);
            }
            else 
            {
                OptionCombo.ItemsSource = from y in options
                                          where y.Key ==0 || y.Key == 5
                                          select new string(y.Value);
            }
            
                                     
            DroneId.IsReadOnly = true;
            
            StationCombo.IsReadOnly = true;
            WeightCombo.IsReadOnly = true;
           
            
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
            
        
            StationCombo.Items.Add("???");
            
            WeightCombo.SelectedItem = x.Weight;
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.cancel = 1;
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
           
            if (newDrone && drone.Model.Length > 0)
            {
                try
                {
                    
                    addNewDrone();


                }
                catch (Exception x)
                {
                    MessageBoxResult mbResult = MessageBox.Show(x.ToString(), "Error Occurred", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                    switch (mbResult)
                    {
                        case MessageBoxResult.OK:
                            if (x.ToString() == "Drone Id alredy use.")
                                ModelText.BorderBrush = ModelText.Text.Length < 1 ? Brushes.Red : Brushes.Gray;
                            DroneId.Foreground = Brushes.Red;
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
                catch (Exception x)
                {
                    MessageBox.Show(x.ToString(),"Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        
        private void updateADrone()
        {

            
                    
            try
            {
                switch (OptionCombo.SelectedValue)
                {
                    case -1:
                        MessageBox.Show("Choose Action", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
                    case "Update Model": // update
                        ibl.UpdateDroneModel(int.Parse(DroneId.Text), ModelText.Text.ToString());
                        break;
                    case "Assign a parcel to the drown": // Assign a parcel to a drown
                        ibl.AssignPackageToDrone(Convert.ToInt32(DroneId.Text));
                        break;
                    case "Pickedup a parcel": // Pickup a parcel
                        ibl.CollectPackage(Convert.ToInt32(DroneId.Text));
                        
                        break;
                    case "Suplay a parcel to costumer": // Suplay a parcel to costumer 
                        ibl.DeliverPackage(Convert.ToInt32(DroneId.Text));
                        break;
                    case "Send the drone to Charge ": // Charge drown
                        ibl.SendDroneToCharge(Convert.ToInt32(DroneId.Text));
                        break;
                    case "Release drown from charging": // Release drown from station
                        ibl.RelesaeDroneFromCharge(Convert.ToInt32(DroneId.Text), 12);
                        break;
                }
                MessageBoxResult mbResult = MessageBox.Show("Done", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                cancel = 1;
                this.Close();
                
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
                if (DroneId.Text.Length < 1 || ModelText.Text.Length < 1 || WeightCombo.SelectedItem == null || StationCombo.SelectedItem == null ||drone.Model.Length<1||drone.Id.ToString().Length<1)
                {
                    MessageBoxResult mbResult = MessageBox.Show("חסר לך נתונים", "The Drone was uploud!", MessageBoxButton.OK, MessageBoxImage.Error);
                    switch (mbResult)
                    {
                        case MessageBoxResult.OK:
                            ModelText.BorderBrush = ModelText.Text.Length < 1 ?  Brushes.Red : Brushes.Gray;
                            DroneId.BorderBrush = DroneId.Text.Length < 1 ? Brushes.Red : Brushes.Gray;
                            WeightCombo.BorderBrush = WeightCombo.SelectedItem == default ? Brushes.Red : Brushes.Gray;
                            StationCombo.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                            break;
                        
                    }
                }
                else
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


            }
            catch (Exception x )
            {
                MessageBoxResult mbResult = MessageBox.Show(x.ToString(), "Error Occurred", MessageBoxButton.OKCancel, MessageBoxImage.Error);
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
       

        private void OptionCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           

        }
 
        

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.cancel == 1)
            {
                e.Cancel = false;
            }
            else e.Cancel = true;
        }
    }
}
