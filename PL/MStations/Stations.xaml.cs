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

namespace PL.MStations
{
    /// <summary>
    /// Interaction logic for Stations.xaml
    /// </summary>
    public partial class Stations : Window
    {
        //להוסיף בדיקת מילוי לפני שליחת טופס
        bool update = false;
        BlApi.IBL ibl;
        BO.BaseStation station;
        int cancel = 0;
        public Stations(BlApi.IBL bl1)
        {
            ibl = bl1;
            station = new BO.BaseStation();
            InitializeComponent();
            MainGrid.RowDefinitions[4].Height = new GridLength(0);
            MainGrid.RowDefinitions[2].Height = new GridLength(0);
        }

        public Stations(BlApi.IBL bl1, BO.BaseStation x)
        {
            ibl = bl1;
            update = true;
            InitializeComponent();
            station =x;
            StationWindows.DataContext = x;
            StationId.IsReadOnly = true;
            MainGrid.RowDefinitions[4].Height = x.dronesInCharges.Count() == 0 ? new GridLength(0) : MainGrid.RowDefinitions[0].Height;
            MainGrid.RowDefinitions[5].Height = new GridLength(0);
            MainGrid.RowDefinitions[6].Height = new GridLength(0);


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.cancel = 1;
            this.Close();
        }
      

        private void StationId_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = true;
            }
        }
      
        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = true;
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

      
        private void DronesCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = DronesCombo.SelectedItem;

            if (DronesCombo.SelectedIndex > -1)
            {
                var temp = (BO.DroneInCharge)item;
                var newDrone = new BO.DroneBL();
                newDrone = ibl.SearchDrone(temp.Id);
                new Drone(ibl, newDrone,2).ShowDialog();
                DronesCombo.SelectedItem = null;


            }
        }

        private void Map_OnClick(object sender, RoutedEventArgs e)
        {
            var smw = new ShowMapWindow(station);
            smw.ShowDialog();
        }

        private void TextBoxWithPeriod_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
            if (e.Handled)
            {
                MessageBox.Show($"digits only\n'{e.Text}' is not a digit");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            try
            {

                var name = NameText.Text;
                int.TryParse(StationId.Text, out var id);
                int.TryParse(ChargeSlot.Text, out var ch);
                if (id != new int() && name != "" && ch != new int())
                {
                    if (update)
                    {
                        ibl.UpdateStation(id, name, ch);
                    }
                    else
                    {
                        double.TryParse(TextBoxLongitude.Text, out var lon);
                        double.TryParse(TextBoxLatitude.Text, out var lat);
                        ibl.AddNewStation(id, name, lon, lat, ch);
                    }
                    MessageBox.Show("Done", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    cancel = 1;
                    this.Close();
                }
                else
                    MessageBox.Show("not enough information", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            catch (BO.IBException ex)
            {
                MessageBox.Show(ex.Message, "Station updating Error!");
                return;
            }

            catch (Exception)
            {
                MessageBox.Show("Unknown error", "Station updating Error!");
                return;
            }
               
            }

        
    }
    }

