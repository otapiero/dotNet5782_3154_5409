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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for UserWindows.xaml
    /// </summary>
    public partial class UserWindows : Window
    {
        MainWindow wind = (MainWindow)Application.Current.MainWindow;
        BlApi.IBL ibl;
        public BO.CustomerBl user;
        public UserWindows(BlApi.IBL bl1, BO.CustomerBl _user)
        {
            InitializeComponent();
            ibl = bl1;
            user = _user;
            Refresh();
        }
        private void Refresh()
        {
            try
            {
                ParcelsDataGrid.DataContext = ibl.ListOfParcels(x=>x.NameSender == user.name || x.NameGetter==user.name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Parcels Loading Error!");
            }
        }
        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            var ab = new MParcels.Parcel(ibl, user.Id);
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
                    new MParcels.Parcel(ibl, newParcel).ShowDialog();
                    Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Parcels Loading Error!");
                }

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
        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
            wind.Close();
        }


    }
}
