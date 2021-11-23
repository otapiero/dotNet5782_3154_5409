using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
      public class DroneToList
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories Weight { get; set; }
            public double Battery { get; set; }
            public DroneStatuses status { get; set; }
            public Location CurrentLocation { get; set; }
            public int ParcelId { get; set; }
            public override string ToString()
            {
                return "id: " + Id + "\nmodel: " + Model + "\nWeight: " + Weight + "\nBattery: " + Battery + "\nStattus: " + status + "\nlocation: " + CurrentLocation.ToString() +
                    "\nparcel id: " + ParcelId;

            }
        }
    }
    
}
