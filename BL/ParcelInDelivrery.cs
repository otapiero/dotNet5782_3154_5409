using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class ParcelInDelivrery
        {
            public int Id { get; set; }
            public bool statusDelivrery { get; set; }
            public Priorities Priorities { get; set; }
            public WeightCategories weight { get; set; }
            public Location CollectionLocation { get; set; }
            public Location DeliveryLocation { get; set; }
            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Getter { get; set; }

            public double DistanceDelivrery { get; set; }

            public override string ToString()
            {
                string str = "Id: "+Id+"\n Status Delivrery: ";
                str +=  statusDelivrery == true ? "in delevry" : "not in delivrery";
                str+="\n priority: "+Priorities+"\n Wheigt: "+weight+"\n Colection location: "+CollectionLocation+
                    "\n delivrery location: "+DeliveryLocation+"\n sender: "+Sender+"\n getter: "+ Getter +
                    "\n distance: " +DistanceDelivrery;
                return str;
            }
        }
    }
}
