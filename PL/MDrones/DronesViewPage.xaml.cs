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
using System.Windows.Navigation;
using System.Windows.Shapes;
using IBL.BO;

namespace PL.MDrones
{
    /// <summary>
    /// Interaction logic for DronesViewPage.xaml
    /// </summary>
    public partial class DronesViewPage : Page
    {
        IBL.IBL ibl;
        int cancel = 0;
        private readonly MainWindow _wnd = (MainWindow)Application.Current.MainWindow;
        public DronesViewPage(IBL.IBL bl1)
        {
            InitializeComponent();
            ibl = bl1;
            Refresh();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));

        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (WeightSelector.SelectedItem == null && StatusSelector.SelectedItem == null)
            {

                DronesDataGrid.ItemsSource = ibl.ListOfDrones(x => 0 == 0);
            }
            else if (WeightSelector.SelectedItem == null)
            {

                DronesDataGrid.ItemsSource = ibl.ListOfDrones(x => x.status == (DroneStatuses)StatusSelector.SelectedItem);
            }
            else if (StatusSelector.SelectedItem == null)
            {

                DronesDataGrid.ItemsSource = ibl.ListOfDrones(x => x.Weight == (WeightCategories)WeightSelector.SelectedItem);
            }
            else
            {
                DronesDataGrid.ItemsSource = ibl.ListOfDrones(x => x.status == (DroneStatuses)StatusSelector.SelectedItem && x.Weight == (WeightCategories)WeightSelector.SelectedItem);

            }

        }


        private void ClearStatusButton_Click(object sender, RoutedEventArgs e)
        {
            StatusSelector.SelectedItem = null;
            if (WeightSelector.SelectedItem != null)
            {
                DronesDataGrid.ItemsSource = ibl.ListOfDrones(x => x.Weight == (WeightCategories)WeightSelector.SelectedItem);
            }
            else DronesDataGrid.ItemsSource = ibl.ListDrones();

        }

        private void ClearWeightButton_Click(object sender, RoutedEventArgs e)
        {
            WeightSelector.SelectedItem = null;
            if (StatusSelector.SelectedItem != null)
            {
                DronesDataGrid.ItemsSource = ibl.ListOfDrones(x => x.status == (DroneStatuses)StatusSelector.SelectedItem);
            }
            else DronesDataGrid.ItemsSource = ibl.ListDrones();

        }
        private void Refresh()
        {
            try
            {
                DronesDataGrid.DataContext = ibl.ListDrones();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Drones Loading Error!");
            }
        }

        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            var ab = new Drone(ibl);
            ab.ShowDialog();
            Refresh();
        }

        private void ClickedDrone(object sender, MouseButtonEventArgs e)
        {
            var item = DronesDataGrid.SelectedItem;

            if (DronesDataGrid.SelectedIndex > -1)
            {
                new Drone(ibl, (IBL.BO.DroneToList)item).ShowDialog();
                Refresh();
            }
        }

        private void InActive_Click(object sender, RoutedEventArgs e)
        {
            //_wnd.DataDisplay.Content = new InActiveBusesViewPage(_bl);
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (BusesDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please choose at least one bus and then click remove!");
            }
            else
            {
                var lb = (IEnumerable)(BusesDataGrid.SelectedItems);

                foreach (var b in lb)
                {
                    try
                    {
                        _bl.DeleteBus(((Bus)b).LicenseNum);
                    }
                    catch (BO.DoesNotExistException ex)
                    {
                        MessageBox.Show(ex.Message, "Buses Loading Error!");
                    }
                }

                Refresh();

            }
            */
        }


        private void Add_MouseEnter(object sender, MouseEventArgs e)
        {
            Button b = sender as Button;
            if (b != null)
            {
                b.Height += b.Height;
                b.Width += b.Width;

            }
        }

        private void Add_MouseLeave(object sender, MouseEventArgs e)
        {
            Button b = sender as Button;
            if (b != null)
            {
                b.Height = b.Height / 2;
                b.Width = b.Width / 2;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WeightSelector.SelectedItem = null;
                StatusSelector.SelectedItem = null;
                DronesDataGrid.DataContext = ibl.ListDrones();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Drones Loading Error!");
            }
        }
    }
}
