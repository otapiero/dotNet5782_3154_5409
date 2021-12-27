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
using System.Collections.ObjectModel;
using BO;


namespace PL
{
    /// <summary>
    /// Interaction logic for DronesDisplay.xaml
    /// </summary>
    public partial class DronesDisplay : Window
    {



        IBL.IBL ibl;
        int cancel = 0;

        public DronesDisplay(IBL.IBL bl1)
        {
            InitializeComponent();
            ibl = bl1;
            DataContext = bl1.ListDrones();
            //DronesListView.ItemsSource = ibl.ListDrones();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));


        }
        private void AddDroneButton_Click(object sender, RoutedEventArgs e)
        {
            new Drone(ibl).ShowDialog();
            DataContext = ibl.ListDrones();
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (WeightSelector.SelectedItem == null && StatusSelector.SelectedItem == null)
            {

                DronesListView.ItemsSource = ibl.ListOfDrones(x => 0 == 0);
            }
            else if (WeightSelector.SelectedItem == null)
            {

                DronesListView.ItemsSource = ibl.ListOfDrones(x => x.status == (DroneStatuses)StatusSelector.SelectedItem);
            }
            else if (StatusSelector.SelectedItem == null)
            {

                DronesListView.ItemsSource = ibl.ListOfDrones(x => x.Weight == (WeightCategories)WeightSelector.SelectedItem);
            }
            else
            {
                DronesListView.ItemsSource = ibl.ListOfDrones(x => x.status == (DroneStatuses)StatusSelector.SelectedItem && x.Weight == (WeightCategories)WeightSelector.SelectedItem);

            }

        }


        private void ClearStatusButton_Click(object sender, RoutedEventArgs e)
        {
            StatusSelector.SelectedItem = null;
            if (WeightSelector.SelectedItem != null)
            {
                DronesListView.ItemsSource = ibl.ListOfDrones(x => x.Weight == (WeightCategories)WeightSelector.SelectedItem);
            }
            else DronesListView.ItemsSource = ibl.ListDrones();

        }

        private void ClearWeightButton_Click(object sender, RoutedEventArgs e)
        {
            WeightSelector.SelectedItem = null;
            if (StatusSelector.SelectedItem != null)
            {
                DronesListView.ItemsSource = ibl.ListOfDrones(x => x.status == (DroneStatuses)StatusSelector.SelectedItem);
            }
            else DronesListView.ItemsSource = ibl.ListDrones();

        }

        private void ClickedDrone(object sender, MouseButtonEventArgs e)
        {
            var item = DronesListView.SelectedItem;
           
            if (DronesListView.SelectedIndex > -1)
            {
                new Drone(ibl, (BO.DroneToList)item).ShowDialog();
                DataContext = ibl.ListDrones();
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WeightSelector.SelectedItem = null;
            StatusSelector.SelectedItem = null;
            DronesListView.ItemsSource = ibl.ListDrones();
        }

        private void CloseList(object sender, RoutedEventArgs e)
        {
            this.cancel = 1;
            this.Close();
        }


        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.cancel == 1)
            {
                e.Cancel = false;
            }
            else e.Cancel = true;
        }
    }
}
