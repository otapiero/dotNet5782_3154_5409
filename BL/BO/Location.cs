using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Class for the location.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="longitude">The longitude.</param>
        /// <param name="lattitude">The lattitude.</param>
        public Location(double longitude, double lattitude)
        {
            Longitude = longitude;
            Lattitude = lattitude;
        }
        public double Longitude { get; set; }
        public double Lattitude { get; set; }

        public override string ToString()
        {
            return "\nLong: " +
                DO.Cordinates.LongitudeDoubleToString(Longitude) +
                "\nLat:  " + DO.Cordinates.LatitudeDoubleToString(Lattitude);
        }
    }
}
