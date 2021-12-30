﻿using System;
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

namespace PL.MParcels
{
    /// <summary>
    /// Interaction logic for ParcelsViewList.xaml
    /// </summary>
    public partial class ParcelsViewList : Page
    {
        BlApi.IBL ibl;
        int cancel = 0;
        private readonly MainWindow _wnd = (MainWindow)Application.Current.MainWindow;
        public ParcelsViewList(BlApi.IBL bl1)
        {
            InitializeComponent();
            ibl = bl1;
            Refresh();
        }
        
        private void Refresh()
        {
            try
            {
                ParcelsDataGrid.DataContext = ibl.ListParcels();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Drones Loading Error!");
            }
        }

        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            var ab = new Parcel();
            ab.ShowDialog();
            Refresh();
        }

        private void ClickedParcel(object sender, MouseButtonEventArgs e)
        {
            var item = ParcelsDataGrid.SelectedItem;

            if (ParcelsDataGrid.SelectedIndex > -1)
            {
                new Parcel().ShowDialog();
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
                ParcelsDataGrid.DataContext = ibl.ListParcels();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Drones Loading Error!");
            }
        }
    }
}
