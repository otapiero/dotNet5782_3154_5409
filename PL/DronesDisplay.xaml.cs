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
    /// Interaction logic for DronesDisplay.xaml
    /// </summary>
    public partial class DronesDisplay : Window
    {
        IBL.IBL ibl;

        public DronesDisplay(IBL.IBL bl1)
        {
            InitializeComponent();
            ibl = bl1;
            DronesListView.ItemsSource = ibl.ListDrones();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));


        }



        private void AddDroneButton_Click(object sender, RoutedEventArgs e)
        {
            new Drone(ibl).ShowDialog();
            DronesListView.ItemsSource = null;
        }
        private void CloseList(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (WeightSelector.SelectedItem == default)
            {

                DronesListView.ItemsSource = ibl.FilterListDrones((DroneStatuses)StatusSelector.SelectedItem);
            }
            else if (StatusSelector.SelectedItem == default)
            {

                DronesListView.ItemsSource = ibl.FilterListDrones1((WeightCategories)WeightSelector.SelectedItem);

            }
            else
            {

                DronesListView.ItemsSource = ibl.FilterListDrones2((DroneStatuses)StatusSelector.SelectedItem, (WeightCategories)WeightSelector.SelectedItem);
            }
        }


        private void ClearStatusButton_Click(object sender, RoutedEventArgs e)
        {
            StatusSelector.SelectedItem = default;
            if (WeightSelector.SelectedItem != default)
            {
                DronesListView.ItemsSource = ibl.FilterListDrones1((WeightCategories)WeightSelector.SelectedItem);
            }
            else DronesListView.ItemsSource = ibl.ListDrones();

        }

        private void ClearWeightButton_Click(object sender, RoutedEventArgs e)
        {
            WeightSelector.SelectedItem = default;
            if (StatusSelector.SelectedItem != default)
            {
                DronesListView.ItemsSource = ibl.FilterListDrones((DroneStatuses)StatusSelector.SelectedItem);
            }
            else DronesListView.ItemsSource = ibl.ListDrones();

        }

        private void ClickedDrone(object sender, MouseButtonEventArgs e)
        {
            var item = DronesListView.SelectedItem;

            new Drone(ibl, (IBL.BO.DroneToList)item).ShowDialog();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DronesListView.ItemsSource = ibl.ListDrones();
        }
    }
}
