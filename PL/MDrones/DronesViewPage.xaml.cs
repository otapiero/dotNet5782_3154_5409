﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
namespace PL.MDrones
{
    /// <summary>
    /// Interaction logic for DronesViewPage.xaml
    /// </summary>
    public partial class DronesViewPage : Page
    {
        BlApi.IBL ibl;
        bool boStatus = true;
        bool boWeight = true;
        private readonly MainWindow _wnd = (MainWindow)Application.Current.MainWindow;
       

        public DronesViewPage(BlApi.IBL bl1)
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
                this.DronesDataGrid.DataContext = ibl.ListDrones();
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
                var temp = (BO.DroneToList)item;
                var newDrone = new BO.DroneBL();
                newDrone = ibl.SearchDrone(temp.Id);
                new Drone(ibl, newDrone, Refresh).Show();
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
                DronesDataGrid.ItemsSource = ibl.ListDrones();
                WeightSelector.SelectedItem = null;
                StatusSelector.SelectedItem = null;
                GroupBy.SelectedItem = null;
                boWeight = true;
                boStatus = true;
    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Drones Loading Error!");
            }
        }

        private void GroupBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var group = GroupBy.SelectedIndex.ToString();
            if (group == "1" && boStatus)
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DronesDataGrid.ItemsSource);
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("status");
                view.GroupDescriptions.Add(groupDescription);
                boStatus = false;


            }
            if (group == "0" && boWeight)
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DronesDataGrid.ItemsSource);
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("Weight");
                view.GroupDescriptions.Add(groupDescription);
                boWeight = false;

            }
        }
    }
}
