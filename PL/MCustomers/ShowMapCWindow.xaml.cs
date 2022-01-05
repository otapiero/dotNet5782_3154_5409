using System;
using System.Windows;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ShowMapWindow.xaml
    /// </summary>
    public partial class ShowMapCWindow : Window
    {
        public ShowMapCWindow(CustomerBl bs)
        {
            InitializeComponent();

            var station = bs;

            DataContext = station;

            var longitude = station.location.Longitude;
            var latitude = station.location.Lattitude;
            try
            {
                var googleMapsAddress = $"https://www.google.co.il//maps/@{longitude},{latitude},18z?hl=iw";


                //var bingMapsAddress = $"https://www.bing.com/maps?cp={longitude}~{latitude}&lvl=18";

                ShowMap.Source = new Uri(googleMapsAddress);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't load the map of Customer! \n" + ex.Message, "map Loading Error!");
            }

            #region K
            //var k = "AtbpkGlznerExttC1tAEa7wPmubvzBDQa4Byq33BCkde0PKsuOV2PelJw_Zvnx1-";
            //ShowMap.Source = new Uri($@"http://dev.virtualearth.net/REST/v1/Locations/{longitude},{latitude}?includeEntityTypes=countryRegion&o=xml&key={k}");
            #endregion
        }
    }
}