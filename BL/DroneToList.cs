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
            public DroneToList()
            {
            }

            public DroneToList(int id, string model, WeightCategories weight, double battery, DroneStatuses status, Location currentLocation, int parcelId)
            {
                Id=id;
                Model=model;
                Weight=weight;
                Battery=battery;
                this.status=status;
                CurrentLocation=currentLocation;
                ParcelId=parcelId;
            }

            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories Weight { get; set; }
            public double Battery { get; set; }
            public DroneStatuses status { get; set; }
            public Location CurrentLocation { get; set; }
            public int ParcelId { get; set; }
            public override string ToString()
            {
                return "id: " + Id + "\nmodel: " + Model + "\nWeight: " + Weight + "\nBattery: " + (Battery -Battery% 0.001)+ "\nStattus: " + status + "\nlocation: " + CurrentLocation.ToString() +
                    "\nparcel id: " + ParcelId+"\n";

            }
        }
    }

}
