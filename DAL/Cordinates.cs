using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
   static public class Cordinates
    {
        public static string LatitudeDoubleToString(double Latitude)
        {
            Latitude = Math.Abs(Latitude);
            string cordinates = Convert.ToString((int)Latitude) + "°";
            Latitude -= (int)Latitude;
            Latitude *= 60;
            cordinates += Convert.ToString((int)Latitude) + "'";
            Latitude -= (int)Latitude;
            Latitude *= 60;
            cordinates += Convert.ToString((int)Latitude) + ".";
            Latitude -= (int)Latitude;
            Latitude *= 1000;
            cordinates += Convert.ToString((int)Latitude) + "''";
            cordinates += "E";
            return cordinates;
        }
        public static string LongitudeDoubleToString(double longitude)
        {
            longitude = Math.Abs(longitude);
            string cordinates = Convert.ToString((int)longitude) + "°";
            longitude -= (int)longitude;
            longitude *= 60;
            cordinates += Convert.ToString((int)longitude) + "'";
            longitude -= (int)longitude;
            longitude *= 60;
            cordinates += Convert.ToString((int)longitude) + ".";
            longitude -= (int)longitude;
            longitude *= 1000;
            cordinates += Convert.ToString((int)longitude) + "''";
            cordinates += "s";
            return cordinates;
        }
    }
}
