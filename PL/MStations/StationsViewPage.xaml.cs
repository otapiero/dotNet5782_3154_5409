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

namespace PL.MStations
{
    /// <summary>
    /// Interaction logic for StationsViewPage.xaml
    /// </summary>
    public partial class StationsViewPage : Page
    {
        BlApi.IBL ibl;
        int cancel = 0;
        bool bo1 = true;
        bool bo2 = true;
        private readonly MainWindow _wnd = (MainWindow)Application.Current.MainWindow;
        public StationsViewPage(BlApi.IBL bl1)
        {
            InitializeComponent();
            ibl = bl1;
            Refresh();
        }


        private void Refresh()
        {
            try
            {
                StationsDataGrid.DataContext = ibl.ListStation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Drones Loading Error!");
            }
        }

        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            var ab = new Stations(ibl);
            ab.ShowDialog();
            Refresh();
        }

        private void ClickedStation(object sender, MouseButtonEventArgs e)
        {
            var item = StationsDataGrid.SelectedItem;

            if (StationsDataGrid.SelectedIndex > -1)
            {
                var temp = (BO.BaseStationToList)item;
                var newStation = new BO.BaseStation();
                newStation = ibl.SearchStation(temp.Id);

                new Stations(ibl, newStation).ShowDialog();
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
                StationsDataGrid.DataContext = ibl.ListStation();
                bo1 = true;
                bo2 = true;
                GroupBy.SelectedItem = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Drones Loading Error!");
            }
        }
        private void GroupBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var group = GroupBy.SelectedIndex.ToString();
            if (group == "1" && bo1)
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(StationsDataGrid.ItemsSource);
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("NumNotAvilableChargeStation");
                view.GroupDescriptions.Add(groupDescription);
                bo1 = false;


            }
            if (group == "0" && bo2)
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(StationsDataGrid.ItemsSource);
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("NumAvilableChargeStation");
                view.GroupDescriptions.Add(groupDescription);
                bo2 = false;

            }
        }

   
    }
}
