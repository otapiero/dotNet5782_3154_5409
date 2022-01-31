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
using System.Text.RegularExpressions;
using BO;
using PL.MParcels;
using System.ComponentModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone : Window
    {
        BlApi.IBL ibl;
        BO.DroneBL drone;
        int cancel = 0;
        int station;
        private bool newDrone;
        BackgroundWorker worker;
        bool runSimulator = false;
        Dictionary<int, string> options = new Dictionary<int, string>(){
             {0,  "Update Model" },
             {1, "Assign a parcel to the drown" },
             {2,  "Pickedup a parcel"},
             {3, "Suplay a parcel to costumer" },
             {4,"Send the drone to Charge " },
             {5,"Release drown from charging" } };

        #region Constructors
        public Drone(BlApi.IBL bl1)
        {
            newDrone = true;
            ibl = bl1;
            drone = new BO.DroneBL();
            InitializeComponent();
            WeightCombo.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            StationCombo.ItemsSource = ibl.ListStation();
            Simulator.Visibility = Visibility.Hidden;
            MainGrid.RowDefinitions[4].Height = new GridLength(0);
            MainGrid.RowDefinitions[5].Height = new GridLength(0);
            MainGrid.RowDefinitions[6].Height = new GridLength(0);
            MainGrid.RowDefinitions[7].Height = new GridLength(0);
            MainGrid.RowDefinitions[8].Height = new GridLength(0);
            Weiht_content.Visibility = Visibility.Hidden;

        }
        public Drone(BlApi.IBL bl1, BO.DroneBL x)
        {
          
            InitializeComponent();
            worker = new();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            newDrone = false;
            ibl = bl1;
            drone = x;
            WeightCombo.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));

            MainGrid.RowDefinitions[3].Height = new GridLength(0);
            StationCombo.Visibility = Visibility.Hidden;
            Station_Label.Visibility = Visibility.Hidden;
            Weiht_content.Content = x.Weight;
            WeightCombo.Visibility = Visibility.Hidden;
            Location.Content = x.CurrentLocation.ToString();

            StatusContenetLabel.Content = x.status;
            if (x.parcel.Id == 0)
            {
                MainGrid.RowDefinitions[8].Height = new GridLength(0);
            }
            else ParcelID.Content = x.parcel.Id;
            
            BatteryText.Content = x.Battery.ToString().Length < 5 ? x.Battery.ToString() : x.Battery.ToString().Substring(0, 5);
            

            if (x.status == DroneStatuses.Available)
            {
                OptionCombo.ItemsSource = from y in options
                                          where (y.Key <= 1 || y.Key > 3) && y.Key != 5
                                          select new string(y.Value);

            }
            else if (x.status == DroneStatuses.Delivery)
            {
                OptionCombo.ItemsSource = from y in options
                                          where y.Key != 1 && y.Key != 5
                                          select new string(y.Value);
            }
            else
            {
                OptionCombo.ItemsSource = from y in options
                                          where y.Key == 0 || y.Key == 5
                                          select new string(y.Value);
            }


            DroneId.IsReadOnly = true;
            StationCombo.IsReadOnly = true;
            WeightCombo.IsReadOnly = true;
            DroneId.Text = x.Id.ToString();
            ModelText.Text = x.Model;
            StationCombo.Items.Add("???");
            WeightCombo.SelectedItem = x.Weight;

        }
        public Drone(BlApi.IBL bl1, BO.DroneBL x, int z)
        {
           
            
            InitializeComponent();
            MainGrid.RowDefinitions[2].Height = new GridLength(0);
            MainGrid.RowDefinitions[3].Height = new GridLength(0);
            MainGrid.RowDefinitions[6].Height = new GridLength(0);
            MainGrid.RowDefinitions[8].Height = new GridLength(0);
            Simulator.Visibility = Visibility.Hidden;
            Ok.Visibility = Visibility.Hidden;
            newDrone = false;
            ibl = bl1;
            drone = x;
            Location.Content = x.CurrentLocation.ToString();
            StatusContenetLabel.Content = x.status;
            if (x.parcel.Id == 0)
            {
                MainGrid.RowDefinitions[8].Height = new GridLength(0);
            }
            else ParcelID.Content = x.parcel.Id;
            BatteryText.Content = x.Battery.ToString().Length < 5 ? x.Battery.ToString() : x.Battery.ToString().Substring(0, 5);
            DroneId.IsReadOnly = true;
            ModelText.IsReadOnly = true;
            DroneId.Text = x.Id.ToString();
            ModelText.Text = x.Model;

        }
        #endregion

        #region Windows
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!runSimulator)
            {
                this.cancel = 1;
                this.Close();
            }
        }
        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.cancel == 1)
            {
                e.Cancel = false;
            }
            else e.Cancel = true;
        }
        private void ParcelID_Click(object sender, RoutedEventArgs e)
        {

            new Parcel(ibl, drone.parcel).ShowDialog();
        }
        #endregion

        #region Change Drone Detail
        private void WeightCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            drone.Weight = (WeightCategories)WeightCombo.SelectedItem;
        }

        private void StationCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var item = StationCombo.SelectedItem;
            var x = (BO.BaseStationToList)item;
            station = x.Id;
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = true;
            }
        }

        private void DroneId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
            if (e.Handled)
            {
                MessageBox.Show($"digits only\n'{e.Text}' is not a digit");
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
        private void DroneId_SelectionChanged(object sender, TextChangedEventArgs e)
        {
            drone.Id = int.Parse(DroneId.Text);
        }

        #endregion

        #region Update / Add Drone
        private void Update_Bottun(object sender, RoutedEventArgs e)
        {

            if (newDrone)
            {
                if (ModelText.Text != "")
                {
                    try
                    {
                        addNewDrone();
                    }
                    catch (Exception x)
                    {
                        MessageBoxResult mbResult = MessageBox.Show(x.ToString(), "Error Occurred", MessageBoxButton.OK, MessageBoxImage.Error);
                        switch (mbResult)
                        {
                            case MessageBoxResult.OK:
                                if (x.ToString() == "Drone Id alredy use.")
                                {
                                    ModelText.BorderBrush = ModelText.Text.Length < 1 ? Brushes.Red : Brushes.Gray;
                                }
                                else
                                {
                                    cancel = 1;
                                    this.Close();
                                }
                                DroneId.Foreground = Brushes.Red;
                                break;

                        }
                    }
                }
                else
                    MessageBox.Show("not enough information", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                try
                {

                    updateADrone();
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        private void updateADrone()
        {
            try
            {
                if (OptionCombo.SelectedValue == null)
                {

                    MessageBox.Show("Choose Action", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    switch (OptionCombo.SelectedValue)
                    {

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
                    MessageBox.Show("Done", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    DroneId.Background = Brushes.White;
                    cancel = 1;
                    this.Close();
                }
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
                if (DroneId.Text.Length < 1 || ModelText.Text.Length < 1 || WeightCombo.SelectedItem == null || StationCombo.SelectedItem == null || drone.Model.Length < 1 || drone.Id.ToString().Length < 1)
                {
                    MessageBoxResult mbResult = MessageBox.Show("Not details", "The Drone was uploud!", MessageBoxButton.OK, MessageBoxImage.Error);
                    switch (mbResult)
                    {
                        case MessageBoxResult.OK:
                            ModelText.BorderBrush = ModelText.Text.Length < 1 ? Brushes.Red : Brushes.Gray;
                            DroneId.BorderBrush = DroneId.Text.Length < 1 ? Brushes.Red : Brushes.Gray;
                            WeightCombo.BorderBrush = WeightCombo.SelectedItem == default ? Brushes.Red : Brushes.Gray;
                            StationCombo.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                            break;

                    }
                }
                else
                {
                    try
                    {
                        ibl.AddNewDrone(drone.Id, drone.Model, (int)drone.Weight, station);

                    }
                    catch(BO.IdAlredyExist x)
                    {
                        MessageBox.Show($"id {x.Id} of {x.ObjectType} already exist");
                    }
                    MessageBoxResult mbResult = MessageBox.Show("The drone was uploud!", "The Drone was uploud!", MessageBoxButton.OK, MessageBoxImage.Information);
                    switch (mbResult)
                    {
                        case MessageBoxResult.OK:
                            cancel = 1;
                            this.Close();
                            break;

                    }
                }


            }
            catch (Exception x)
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

        #endregion

        #region Start simulator
        private void Simulator_Click(object sender, RoutedEventArgs e)
        {
            if (!runSimulator)
            {
                if (worker.IsBusy != true)
                {
                    worker.RunWorkerAsync(); 
                    runSimulator = true;
                    Ok.Visibility = Visibility.Hidden;
                    MainGrid.RowDefinitions[6].Height = new GridLength(0);
                }
            }
            else
            {
                if (worker.IsBusy)
                {
                    MessageBox.Show($"Cancelling Simulator Mode, Please Wait", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    worker.CancelAsync();
                    
                }
                
            }
        }
        private bool StatusSimulator()
        {
            return worker.CancellationPending;
        }
        private void Report()
        {
            worker.ReportProgress(0);  
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ibl.startSimulator(drone.Id, StatusSimulator, Report);
            }
            catch (BO.BatteryExaption ex)
            {
                MessageBox.Show(ex.Message);
                worker.CancelAsync();
                
            }
            catch (Exception x)
            {
                if (x.Message.Length >0)
                {
                    MessageBox.Show(x.Message);
                }
                else MessageBox.Show("Error","Error");
                worker.CancelAsync();
                
            }



           
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            drone = ibl.SearchDrone(drone.Id);
            Location.Content = drone.CurrentLocation.ToString();
            StatusContenetLabel.Content = drone.status;
            if (drone.parcel.Id == 0)
            {
                MainGrid.RowDefinitions[8].Height = new GridLength(0);
            }
            else
            {
                MainGrid.RowDefinitions[8].Height = MainGrid.RowDefinitions[0].Height;
                ParcelID.Content = drone.parcel.Id;
            }

            BatteryText.Content = drone.Battery.ToString().Length < 5 ? drone.Battery.ToString() : drone.Battery.ToString().Substring(0, 5);
            DroneId.Text = drone.Id.ToString();
            ModelText.Text = drone.Model;
            WeightCombo.SelectedItem = drone.Weight;
           
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            runSimulator = false;
            Ok.Visibility = Visibility.Visible;
            MainGrid.RowDefinitions[6].Height = MainGrid.RowDefinitions[0].Height;
            Simulator.IsChecked = false;
        }
        #endregion
        
    }
}
