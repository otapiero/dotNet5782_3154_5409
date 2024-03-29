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
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace PL.MCustomers
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : Window
    {
        BlApi.IBL ibl;
        bool update = false;
        BO.CustomerBl customer;

        int cancel = 0;
        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class.
        /// </summary>
        /// <param name="bl1">The bl1.</param>
        public Customer(BlApi.IBL bl1)
        {
            ibl = bl1;
            customer = new BO.CustomerBl();
            InitializeComponent();
            MainGrid.RowDefinitions[5].Height = new GridLength(0);
            MainGrid.RowDefinitions[4].Height = new GridLength(0);
            MainGrid.RowDefinitions[3].Height = new GridLength(0);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class.
        /// </summary>
        /// <param name="bl1">The bl1.</param>
        /// <param name="x">The x.</param>
        public Customer(BlApi.IBL bl1, BO.CustomerBl x)
        {
            ibl = bl1;
            update = true;
            InitializeComponent();
            customer = x;
            CustomerWindows.DataContext = x;
            CustomerId.IsReadOnly = true;
            MainGrid.RowDefinitions[4].Height = x.toCustomers.Count() == 0 ? new GridLength(0) : MainGrid.RowDefinitions[0].Height;
            MainGrid.RowDefinitions[5].Height = x.fromCustomer.Count() == 0 ? new GridLength(0) : MainGrid.RowDefinitions[0].Height;
            MainGrid.RowDefinitions[7].Height = new GridLength(0);
            MainGrid.RowDefinitions[6].Height = new GridLength(0);


        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class.
        /// </summary>
        /// <param name="bl1">The bl1.</param>
        /// <param name="x">The x.</param>
        /// <param name="z">The z.</param>
        public Customer(BlApi.IBL bl1, BO.CustomerBl x, int z)
        {
            ibl = bl1;
            InitializeComponent();
            customer = x;
            CustomerWindows.DataContext = x;
            CustomerId.IsReadOnly = true;
            NameText.IsReadOnly = true;
            Phone.IsReadOnly = true;
            Ok.Visibility = Visibility.Hidden;
            MainGrid.RowDefinitions[4].Height = new GridLength(0);
            MainGrid.RowDefinitions[5].Height = new GridLength(0);
            MainGrid.RowDefinitions[7].Height = new GridLength(0);
            MainGrid.RowDefinitions[6].Height = new GridLength(0);


        }

        /// <summary>
        /// Button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.cancel = 1;
            this.Close();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = true;
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

        private void CustomerCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item1 = FromCustomer.SelectedItem;
            var item2 = ToCustomer.SelectedItem;

            if (item1 != null)
            {

                var temp = (BO.ParcelAtCustomer)item1;
                new MParcels.Parcel(ibl, temp).ShowDialog();
                

            }
            else
            {
                var temp = (BO.ParcelAtCustomer)item2;
                new MParcels.Parcel(ibl, temp).ShowDialog();
                
            }
           
            
        }
            
        private void Map_OnClick(object sender, RoutedEventArgs e)
        {
            var smw = new ShowMapCWindow(customer);
            smw.ShowDialog();
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            try
            {
                var name = NameText.Text;
                int.TryParse(CustomerId.Text, out var id);
                var phone = Phone.Text;
                if (id != new int() && (name != "" && phone != ""))
                {
                    if (update)
                    {
                        try
                        {
                            ibl.UpdateCostumer(id, name, phone);
                            MessageBox.Show("Done", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                        }
                        catch (DO.XMLFileLoadCreateException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        catch (BO.IdDoseNotExist x)
                        {
                            MessageBox.Show($"can not update the customer\nthe {x.ObjectType} with id: {x.Id} is not found!");
                        }
                        catch (BO.thePhoneAndNameInputAreIncorrect x)
                        {
                            MessageBox.Show(x.Message);
                        }
                        catch(BO.XMLFileLoadCreateException x)
                        {
                            MessageBox.Show(x.Message);
                        }
                        cancel = 1;
                        this.Close();
                    }
                    else
                    {

                        double.TryParse(TextBoxLongitude.Text, out var lon);
                        double.TryParse(TextBoxLatitude.Text, out var lat);
                        if ((lat != new double() && lon != new double()))
                        {
                            string pass = "0";
                            try
                            {
                                ibl.AddNewCustomer(id, name, phone, lon, lat, pass);
                                MessageBox.Show("Done", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            catch (BO.IdAlredyExist x)
                            {
                                MessageBox.Show($"can not add the customer\nA {x.ObjectType} with id: {x.Id} is already exsit!");
                            }
                            catch (BO.XMLFileLoadCreateException x)
                            {
                                MessageBox.Show(x.Message);
                            }
                            cancel = 1;
                            this.Close();
                        }
                        else
                            MessageBox.Show("not enough information", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
               
                }
                else
                    MessageBox.Show("not enough information", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            catch (BO.IBException ex)
            {
                MessageBox.Show(ex.Message, "Customer updating Error!");
                return;
            }

            catch (Exception)
            {
                MessageBox.Show("Unknown error", "Customer updating Error!");
                return;
            }

        }

        private void CustomerId_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
