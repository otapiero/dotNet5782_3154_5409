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
using BO;

namespace PL.MParcels
{
    /// <summary>
    /// Interaction logic for ParcelsViewList.xaml
    /// </summary>
    public partial class ParcelsViewList : Page
    {
        BlApi.IBL ibl;
        bool boSender = true;
        bool boGeeter = true;
        int cancel = 0;
        private readonly MainWindow _wnd = (MainWindow)Application.Current.MainWindow;
        public ParcelsViewList(BlApi.IBL bl1)
        {
            InitializeComponent();
            ibl = bl1;
            Refresh();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(ParcelStatus));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            PrioritiesSelector.ItemsSource = Enum.GetValues(typeof(Priorities));
        }
        
        private void Refresh()
        {
            try
            {
                ParcelsDataGrid.DataContext = ibl.ListParcels();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Parcels Loading Error!");
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter_Parcel();

        }
       
        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            var ab = new Parcel(ibl);
            ab.ShowDialog();
            Refresh();
        }

        private void ClickedParcel(object sender, MouseButtonEventArgs e)
        {
            var item = ParcelsDataGrid.SelectedItem;

            if (ParcelsDataGrid.SelectedIndex > -1)
            {
                try
                {
                    var temp = (ParcelToList)item;
                    var newParcel = new ParcelBl();
                    newParcel = ibl.SearchParcel(temp.Id);
                    new Parcel(ibl, newParcel).ShowDialog();
                    Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Parcels Loading Error!");
                }
               
            }
        }

      

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            
            if (ParcelsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please choose Parcel and then click remove!");
            }
            else
            {
                var p = (BO.ParcelToList)ParcelsDataGrid.SelectedItem;

                try
                {
                    if (p.Status != ParcelStatus.Assigned)
                    { ibl.DeleteParcel(p.Id); }
                    else MessageBox.Show("The parcel was asigment", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (BO.IBException ex)
                {
                    MessageBox.Show(ex.Message, "Try again!");
                }
                
                MessageBox.Show("The parcel was removed");
                Refresh();

            }
            
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
                ParcelsDataGrid.DataContext = ibl.ListParcels();
                WeightSelector.SelectedItem = null;
                StatusSelector.SelectedItem = null;
                GroupBy.SelectedItem = null;
                boGeeter = true;
                boSender = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Drones Loading Error!");
            }
        }
        private void GroupBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var group = GroupBy.SelectedIndex.ToString();
            if (group == "1" && boSender)
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelsDataGrid.DataContext);
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("NameSender");
                view.GroupDescriptions.Add(groupDescription);
                boSender = false;


            }
            if (group == "0" && boGeeter)
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelsDataGrid.DataContext);
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("NameGetter");
                view.GroupDescriptions.Add(groupDescription);
                boGeeter = false;

            }
        }

        private void Button_Click_Weight(object sender, RoutedEventArgs e)
        {
            WeightSelector.SelectedItem = null;
            Filter_Parcel();
        }

        private void Filter_Parcel()
        {
            var s = StatusSelector.SelectedItem;
            var w = WeightSelector.SelectedItem;
            var p = PrioritiesSelector.SelectedItem;
            // all choise
            if (s == null && w == null && p == null)
            { ParcelsDataGrid.DataContext = ibl.ListOfParcels(x => 0 == 0); }
            //one choise
            else if (s != null && w == null && p == null)
            { ParcelsDataGrid.DataContext = ibl.ListOfParcels(x => x.Status == (ParcelStatus)s); }
            else if (s == null && w != null && p == null)
            { ParcelsDataGrid.DataContext = ibl.ListOfParcels(x => x.Weight == (WeightCategories)w); }
            else if (s == null && w == null && p != null)
            { ParcelsDataGrid.DataContext = ibl.ListOfParcels(x => x.Priorities == (Priorities)p); }
            //two choise
            else if (s != null && w != null && p == null)
            { ParcelsDataGrid.DataContext = ibl.ListOfParcels(x => x.Status == (ParcelStatus)s && x.Weight == (WeightCategories)w); }
            else if (s != null && w == null && p != null)
            { ParcelsDataGrid.DataContext = ibl.ListOfParcels(x => x.Priorities == (Priorities)p && x.Status == (ParcelStatus)s); }
            else if (s == null && w != null && p != null)
            { ParcelsDataGrid.DataContext = ibl.ListOfParcels(x => x.Priorities == (Priorities)p && x.Weight == (WeightCategories)w); }
            //three choise
            else if (s != null && w != null && p != null)
            { ParcelsDataGrid.DataContext = ibl.ListOfParcels(x => x.Weight == (WeightCategories)w && x.Status == (ParcelStatus)s && x.Priorities == (Priorities)p); }
        }

        private void Button_Click_Status(object sender, RoutedEventArgs e)
        {
            StatusSelector.SelectedItem = null;
            Filter_Parcel();
        }

        private void Button_Click_Pr(object sender, RoutedEventArgs e)
        {
            PrioritiesSelector.SelectedItem = null;
            Filter_Parcel();
        }
    }
}
