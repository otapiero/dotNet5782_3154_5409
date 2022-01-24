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
using System.Windows.Media.Animation;
using MaterialDesignThemes.Wpf;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

using BO;

namespace PL
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BlApi.IBL Ibl;
        private bool _hidden;
        public MainWindow()
        {
            Ibl = BlApi.BlFactory.GetBl();
            var lp = new LoginPage(Ibl);
            lp.ShowDialog();

            if (lp.working)
            {
                /*
                UserInfo newWin = new UserInfo(win.User);
                newWin.Show();
                this.Close();
                */
                MessageBox.Show("gfgdf","gfdgdf");
            }
           
          
            InitializeComponent();
           
            //_simulationPage = new SimulationPage(_bl);
        }

        #region Mouse effects and functionality for exit button
        private void Exit_OnMouseEnter(object sender, MouseEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Exit.Background = (Brush)bc.ConvertFrom("#F1707A");
        }
        private void Exit_OnMouseLeave(object sender, MouseEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Exit.Background = (Brush)bc.ConvertFrom("#E81123");
        }
        private void Exit_OnMouseDown(object sender, MouseEventArgs e)
        {
            Close();
        }

        /* private static void PlaySound(string path)
         {
             var sp = new SoundPlayer(path);
             sp.Load();
             sp.Play();
         }*/

        #endregion

        #region open close menu and drag window

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (_hidden)
            {
                //PlaySound(@"..\PR_PL\Icons\navigation_forward-selection-minimal.wav");
                var sb = Resources["OpenMenu"] as Storyboard;
                sb?.Begin(SideBar);
                _hidden = false;
                OpenCloseButtonIcon.Kind = PackIconKind.MenuOpen;
            }
            else
            {
                //PlaySound(@"..\PR_PL\Icons\navigation_backward-selection-minimal.wav");
                var sb = Resources["CloseMenu"] as Storyboard;
                sb?.Begin(SideBar);
                _hidden = true;
                OpenCloseButtonIcon.Kind = PackIconKind.Menu;
            }
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        #endregion

        #region Drones button
        private void DronesSidePanel_OnMouseEnter(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();
            DronesSidePanel.Background = (Brush)bc.ConvertFrom("#30ABFF");
        }
        private void DronesSidePanel_OnMouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            DronesSidePanel.Background = (Brush)bc.ConvertFrom("#FF0D3251");
        }
        private void DronesSidePanel_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_hidden)
            {
                ButtonBase_OnClick(sender, e);
            }

            if (!(DataDisplay.Content is PL.MDrones.DronesViewPage))
            {
                DataDisplay.Content = new PL.MDrones.DronesViewPage(Ibl);
            }
        }
        #endregion

        #region Stations button
        private void StationsSidePanel_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_hidden)
            {
                ButtonBase_OnClick(sender, e);
            }
            
            if (!(DataDisplay.Content is PL.MStations.StationsViewPage))
            {
                DataDisplay.Content = new PL.MStations.StationsViewPage(Ibl);
            }
            
        }

        #region colors
        private void StationsSidePanel_OnMouseEnter(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            StationsSidePanel.Background = (Brush)bc.ConvertFrom("#30ABFF");
        }

        private void StationsSidePanel_OnMouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            StationsSidePanel.Background = (Brush)bc.ConvertFrom("#FF0D3251");
        }

        #endregion
        #endregion

        #region Customers button
        private void CustomersSidePanel_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_hidden)
            {
                ButtonBase_OnClick(sender, e);
            }
            
            if (!(DataDisplay.Content is PL.MCustomers.CustomersViewPage))
            {
                DataDisplay.Content = new PL.MCustomers.CustomersViewPage(Ibl);
            }
            
        }
        private void CustomersSidePanel_OnMouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            CustomersSidePanel.Background = (Brush)bc.ConvertFrom("#FF0D3251");
        }
        private void CustomersSidePanel_OnMouseEnter(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            CustomersSidePanel.Background = (Brush)bc.ConvertFrom("#30ABFF");
        }
        #endregion

        #region  Parcels button
        private void ParcelsSidePanel_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_hidden)
            {
                ButtonBase_OnClick(sender, e);
            }
            
            if (!(DataDisplay.Content is PL.MParcels.ParcelsViewList))
            {
                DataDisplay.Content = new PL.MParcels.ParcelsViewList(Ibl);
            }
           
        }
        private void ParcelsSidePanel_OnMouseEnter(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            ParcelsSidePanel.Background = (Brush)bc.ConvertFrom("#30ABFF");
        }
        private void ParcelsSidePanel_OnMouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            ParcelsSidePanel.Background = (Brush)bc.ConvertFrom("#FF0D3251");
        }
        #endregion

        

       
    }
}
