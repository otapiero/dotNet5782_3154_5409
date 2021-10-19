using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }

            //WeightCategories MaxWheight{ get; set; }
            //DroneStatuses Status{ get; set; }
            public double Battery { get; set; }


        }

    }
}
