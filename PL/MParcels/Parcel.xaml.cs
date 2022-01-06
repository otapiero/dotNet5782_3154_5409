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

namespace PL.MParcels
{
    /// <summary>
    /// Interaction logic for Parcel.xaml
    /// </summary>
    public partial class Parcel : Window
    {
        BlApi.IBL ibl;
        BO.ParcelBl parcel;
        int cancel = 0;
        bool update = false;
        public Parcel(BlApi.IBL bl1)
        {
            ibl = bl1;
            parcel = new BO.ParcelBl();
            InitializeComponent();
            MainGrid.RowDefinitions[5].Height = new GridLength(0);
            MainGrid.RowDefinitions[6].Height = new GridLength(0);
            MainGrid.RowDefinitions[7].Height = new GridLength(0);
            MainGrid.RowDefinitions[8].Height = new GridLength(0);
            MainGrid.RowDefinitions[9].Height = new GridLength(0);
            MainGrid.RowDefinitions[10].Height = new GridLength(0);
            Sender.Visibility = Visibility.Hidden;
            Getter.Visibility = Visibility.Hidden;
            Weight.Visibility = Visibility.Hidden;
            Priorites.Visibility = Visibility.Hidden;
            SenderCombo.DataContext = ibl.ListCustomer();
            GetterCombo.DataContext = ibl.ListCustomer();
            WeightCombo.DataContext = Enum.GetValues(typeof(BO.WeightCategories));
            PrioritiesCombo.DataContext = Enum.GetValues(typeof(BO.Priorities));
        }

        public Parcel(BlApi.IBL bl1, BO.ParcelBl x)
        {
            ibl = bl1;
            InitializeComponent();
            
            parcel = x;
            update = true;
            ParcelWindows.DataContext = x;
            if (x.drone.Id == 0)
            {
                MainGrid.RowDefinitions[5].Height = new GridLength(0);
            }
            else Drone.Content = x.drone.Id;
            Sender.Content = x.Sender.Name;
            Getter.Content = x.Getter.Name;
            Id.IsReadOnly = true;
            SenderCombo.Visibility = Visibility.Hidden;
            GetterCombo.Visibility = Visibility.Hidden;
            WeightCombo.Visibility = Visibility.Hidden;
            PrioritiesCombo.Visibility = Visibility.Hidden;
            
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.cancel = 1;
            this.Close();
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
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.cancel == 1)
            {
                e.Cancel = false;
            }
            else e.Cancel = true;
        }

        private void Drone_Click(object sender, RoutedEventArgs e)
        {
            var temp = (BO.DroneInParcel)parcel.drone;
            var newDrone = new BO.DroneBL();
            newDrone = ibl.SearchDrone(temp.Id);
            new Drone(ibl, newDrone).ShowDialog();
        }

        private void Getter_Click(object sender, RoutedEventArgs e)
        {
            var temp = (BO.CustomerInParcel)parcel.Getter;
            var newC = new BO.CustomerBl();
            newC = ibl.SearchCostumer(temp.Id);
            new MCustomers.Customer(ibl, newC).ShowDialog();
        }

        private void Sender_Click(object sender, RoutedEventArgs e)
        {
            var temp = (BO.CustomerInParcel)parcel.Sender;
            var newC = new BO.CustomerBl();
            newC = ibl.SearchCostumer(temp.Id);
            new MCustomers.Customer(ibl, newC).ShowDialog();
        }
    }
}
