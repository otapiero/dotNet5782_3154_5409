using System;


namespace BO
{
    public class DroneBL
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DroneBL"/> class.
        /// </summary>
        public DroneBL()
        {
            this.parcel=new();
        }

        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories Weight { get; set; }
        public double Battery { get; set; }
        public DroneStatuses status { get; set; }
        public ParcelInDelivrery parcel { get; set; }
        public Location CurrentLocation { get; set; }

        public override string ToString()
        {
            string str = "Id: "+Id+"\nModel: "+Model+"\nWeight: "+Weight+"\nBattery: "+Battery+"\nStatus: "+status;
            if (parcel!=new ParcelInDelivrery())
                str+= "\nparcel in delivrery: "+parcel;
            return str +"\nLocation: "+CurrentLocation+"\n";
        }
    }


}
