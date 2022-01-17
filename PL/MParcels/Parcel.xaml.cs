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
        bool user = false;
        int sendId;
        public Parcel(BlApi.IBL bl1)
        {
            ibl = bl1;
            
            InitializeComponent();
            MainGrid.RowDefinitions[0].Height = new GridLength(0);
            MainGrid.RowDefinitions[5].Height = new GridLength(0);
            MainGrid.RowDefinitions[6].Height = new GridLength(0);
            MainGrid.RowDefinitions[7].Height = new GridLength(0);
            MainGrid.RowDefinitions[8].Height = new GridLength(0);
            MainGrid.RowDefinitions[9].Height = new GridLength(0);
            MainGrid.RowDefinitions[10].Height = new GridLength(0);
            MainGrid.RowDefinitions[11].Height = new GridLength(0);
            MainGrid.RowDefinitions[12].Height = new GridLength(0);
            Sender.Visibility = Visibility.Hidden;
            Idd.Visibility = Visibility.Hidden;
            Getter.Visibility = Visibility.Hidden;
            Weight.Visibility = Visibility.Hidden;
            Priorites.Visibility = Visibility.Hidden;
            SenderCombo.DataContext = ibl.ListCustomer();
            GetterCombo.DataContext = ibl.ListCustomer();
            WeightCombo.DataContext = Enum.GetValues(typeof(BO.WeightCategories));
            PrioritiesCombo.DataContext = Enum.GetValues(typeof(BO.Priorities));
        }
        public Parcel(BlApi.IBL bl1,int id)
        {
            ibl = bl1;
            sendId = id;
            user = true;
            
            InitializeComponent();
            MainGrid.RowDefinitions[0].Height = new GridLength(0);
            MainGrid.RowDefinitions[1].Height = new GridLength(0);
            MainGrid.RowDefinitions[5].Height = new GridLength(0);
            MainGrid.RowDefinitions[6].Height = new GridLength(0);
            MainGrid.RowDefinitions[7].Height = new GridLength(0);
            MainGrid.RowDefinitions[8].Height = new GridLength(0);
            MainGrid.RowDefinitions[9].Height = new GridLength(0);
            MainGrid.RowDefinitions[10].Height = new GridLength(0);
            MainGrid.RowDefinitions[11].Height = new GridLength(0);
            MainGrid.RowDefinitions[12].Height = new GridLength(0);
            Sender.Visibility = Visibility.Hidden;
            Idd.Visibility = Visibility.Hidden;
            Getter.Visibility = Visibility.Hidden;
            Weight.Visibility = Visibility.Hidden;
            Priorites.Visibility = Visibility.Hidden;
            GetterCombo.DataContext = ibl.ListCustomer();
            WeightCombo.DataContext = Enum.GetValues(typeof(BO.WeightCategories));
            PrioritiesCombo.DataContext = Enum.GetValues(typeof(BO.Priorities));
        }
        public Parcel(BlApi.IBL bl1, BO.ParcelBl x)
        {
            ibl = bl1;
            InitializeComponent();
            Idd.Visibility = Visibility.Hidden;
            MainGrid.RowDefinitions[11].Height = new GridLength(0);
            MainGrid.RowDefinitions[12].Height = new GridLength(0);
            MainGrid.RowDefinitions[13].Height = new GridLength(0);
            MainGrid.RowDefinitions[14].Height = new GridLength(0);
            MainGrid.RowDefinitions[15].Height = new GridLength(0);
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
            SenderCombo.Visibility = Visibility.Hidden;
            GetterCombo.Visibility = Visibility.Hidden;
            WeightCombo.Visibility = Visibility.Hidden;
            PrioritiesCombo.Visibility = Visibility.Hidden;
            Options.Items.Add("Collect Parcel");
            Options.Items.Add("Deliver Parcel");
            Options.Items.Add("Delete Parcel");
            
        }
        public Parcel(BlApi.IBL bl1, BO.ParcelBl x, int z)
        {
            ibl = bl1;
            InitializeComponent();

            parcel = x;
            Ok.Visibility = Visibility.Hidden;
            ParcelWindows.DataContext = x;

            Sender.Content = x.Sender.Name;
            Getter.Content = x.Getter.Name;
            Idd.Visibility = Visibility.Hidden;
            SenderCombo.Visibility = Visibility.Hidden;
            GetterCombo.Visibility = Visibility.Hidden;
            WeightCombo.Visibility = Visibility.Hidden;
            PrioritiesCombo.Visibility = Visibility.Hidden;
            MainGrid.RowDefinitions[5].Height = new GridLength(0);
            MainGrid.RowDefinitions[6].Height = new GridLength(0);
            MainGrid.RowDefinitions[7].Height = new GridLength(0);
            MainGrid.RowDefinitions[8].Height = new GridLength(0);
            MainGrid.RowDefinitions[9].Height = new GridLength(0);
            MainGrid.RowDefinitions[10].Height = new GridLength(0);
            MainGrid.RowDefinitions[11].Height = new GridLength(0);
            MainGrid.RowDefinitions[12].Height = new GridLength(0);
            MainGrid.RowDefinitions[13].Height = new GridLength(0);
            MainGrid.RowDefinitions[14].Height = new GridLength(0);
            MainGrid.RowDefinitions[15].Height = new GridLength(0);


        }

        public Parcel(BlApi.IBL bl1, BO.ParcelInDelivrery x)
        {
            ibl = bl1;
            InitializeComponent();
            Idd.Content = x.Id;
            Sender.Content = x.Sender.Name;
            Getter.Content = x.Getter.Name;
            Weight.Content = x.weight;
            Priorites.Content = x.Priorities;
            DistanceDelivrery.Content = x.DistanceDelivrery;
            CollectionLocation.Content = x.CollectionLocation.ToString();
            if (x.DeliveryLocation != null)
            {
                DeliveryLocation.Content = x.DeliveryLocation.ToString();
            }
            else MainGrid.RowDefinitions[15].Height = new GridLength(0);

            Id.Visibility = Visibility.Hidden;
            Ok.Visibility = Visibility.Hidden;
            SenderCombo.Visibility = Visibility.Hidden;
            GetterCombo.Visibility = Visibility.Hidden;
            WeightCombo.Visibility = Visibility.Hidden;
            PrioritiesCombo.Visibility = Visibility.Hidden;
            Id.Visibility = Visibility.Hidden;

            MainGrid.RowDefinitions[5].Height = new GridLength(0);
            MainGrid.RowDefinitions[6].Height = new GridLength(0);
            MainGrid.RowDefinitions[7].Height = new GridLength(0);
            MainGrid.RowDefinitions[8].Height = new GridLength(0);
            MainGrid.RowDefinitions[9].Height = new GridLength(0);
            MainGrid.RowDefinitions[10].Height = new GridLength(0);
            MainGrid.RowDefinitions[11].Height = new GridLength(0);
            MainGrid.RowDefinitions[12].Height = new GridLength(0);


        }
        public Parcel(BlApi.IBL bl1, BO.ParcelAtCustomer x)
        {
            ibl = bl1;
            InitializeComponent();
            Idd.Content = x.Id;
            Weight.Content = x.Weight;
            Priorites.Content = x.Priorities;
            statusP.Content = x.Status;
            OtherCustomer.Content = x.OtherCustomer.ToString();
            Id.Visibility = Visibility.Hidden;

            Ok.Visibility = Visibility.Hidden;
            SenderCombo.Visibility = Visibility.Hidden;
            GetterCombo.Visibility = Visibility.Hidden;
            WeightCombo.Visibility = Visibility.Hidden;
            PrioritiesCombo.Visibility = Visibility.Hidden;

            MainGrid.RowDefinitions[1].Height = new GridLength(0);
            MainGrid.RowDefinitions[2].Height = new GridLength(0);
            MainGrid.RowDefinitions[5].Height = new GridLength(0);
            MainGrid.RowDefinitions[6].Height = new GridLength(0);
            MainGrid.RowDefinitions[7].Height = new GridLength(0);
            MainGrid.RowDefinitions[8].Height = new GridLength(0);
            MainGrid.RowDefinitions[9].Height = new GridLength(0);
            MainGrid.RowDefinitions[10].Height = new GridLength(0);
            MainGrid.RowDefinitions[13].Height = new GridLength(0);
            MainGrid.RowDefinitions[14].Height = new GridLength(0);
            MainGrid.RowDefinitions[15].Height = new GridLength(0);


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
            if (parcel.Assignation != new DateTime() && parcel.DeliveryTime == new DateTime())
            {
                var temp = (BO.DroneInParcel)parcel.drone;
                var newDrone = new BO.DroneBL();
                newDrone = ibl.SearchDrone(temp.Id);
                new Drone(ibl, newDrone,1).ShowDialog();
            }
        }

        private void Getter_Click(object sender, RoutedEventArgs e)
        {
           
                var temp = (BO.CustomerInParcel)parcel.Getter;
                var newC = new BO.CustomerBl();
                newC = ibl.SearchCostumer(temp.Id);
                new MCustomers.Customer(ibl, newC, 1).ShowDialog();
            
        }

        private void Sender_Click(object sender, RoutedEventArgs e)
        {
            var temp = (BO.CustomerInParcel)parcel.Sender;
            var newC = new BO.CustomerBl();
            newC = ibl.SearchCostumer(temp.Id);
            new MCustomers.Customer(ibl, newC,1).ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            try
            {
                if (!update)
                {
                    if (GetterCombo.SelectedItem != null &&
                        WeightCombo.SelectedItem != null &&
                        PrioritiesCombo.SelectedItem != null)
                    {
                        int seId=0;
                        if (user)
                        {
                            seId = sendId;
                        }
                        else if (SenderCombo.SelectedItem != null)
                        {
                            var se = (BO.CustomerToList)SenderCombo.SelectedItem;
                            seId = se.Id;
                        }
                        else
                        { MessageBox.Show("not enough information", "Error", MessageBoxButton.OK, MessageBoxImage.Warning); }
                        var ge = (BO.CustomerToList)GetterCombo.SelectedItem;
                        int geId = ge.Id;
                        var we = (int)(BO.WeightCategories)WeightCombo.SelectedItem;
                        var pr = (int)(BO.Priorities)PrioritiesCombo.SelectedItem;
                        try
                        {
                            ibl.AddNewParcel(seId, geId, we, pr);

                        }
                        catch(BO.IdDoseNotExist x)
                        {
                            MessageBox.Show($"id {x.Id} of {x.ObjectType} dose not exsit");
                        }
                        MessageBox.Show("Done", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        cancel = 1;
                        this.Close();
                    }
                    else
                        MessageBox.Show("not enough information", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    switch (Options.SelectedValue)
                    {

                        case "Delete Parcel":
                            if (parcel.Assignation == new DateTime())
                            { ibl.DeleteParcel(parcel.Id); }
                            else MessageBox.Show("The parcel was asigment", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            break;
                        case "Deliver Parcel":
                            ibl.DeliverPackage(parcel.Id);
                            break;
                        case "Collect Parcel":
                            ibl.CollectPackage(parcel.Id);
                            break;
                    }
                    MessageBox.Show("Done", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    cancel = 1;
                    this.Close();

                }
                 

            }
            catch (BO.IBException ex)
            {
                MessageBox.Show(ex.Message, "Parcel updating Error!");
                return;
            }

            catch (Exception x)
            {
                MessageBox.Show("Error: "+ x, "Parcel updating Error!");
                return;
            }

        }
    }
}
