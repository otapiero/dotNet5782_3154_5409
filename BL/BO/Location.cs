using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Location
    {
        public Location(double longitude, double lattitude)
        {
            Longitude = longitude;
            Lattitude = lattitude;
        }
        public double Longitude { get; set; }
        public double Lattitude { get; set; }

        public override string ToString()
        {
            return "\nLongitude:    " +
                DAL.Cordinates.LongitudeDoubleToString(Longitude) +
                "\nLattitude:     " + DAL.Cordinates.LatitudeDoubleToString(Lattitude);
        }
    }
}
