using System;

namespace IBL
{
    namespace BO
    {
        public class DroneBL
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories Weight { get; set; }
            public double Battery { get; set; }
            public DroneStatuses status { get; set; }
            public ParcelInDelivrery parcel { get; set; }
            public Location CurrentLocation { get; set; }
        }


    }
   
}
