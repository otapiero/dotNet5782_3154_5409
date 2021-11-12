using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{

    namespace BO
    {
        public class Location
        {
            Location(double longitude, double lattitude)
            {
                Longitude = longitude;
                Lattitude = lattitude;
            }
            public double Longitude { get; set; }
            public double Lattitude { get; set; }
        }
    }
   
}
