using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class ParcelBl
        {
            public int Id { get; set; }
            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Getter { get; set; }
            public WeightCategories weight { get; set; }
            public Priorities priorities { get; set; }
            public DroneInParcel drone { get; set; }
            DateTime TimeCreation { get; set; }
            DateTime Assignation { get; set; }
            DateTime ColectionTime { get; set; }
            DateTime DeliveryTime { get; set; }

            public override string ToString()
            {
                return 
                        "Id" + Id +
                        "\nSender" + Sender +
                        "\nGetter" + Getter +
                        "\nweight" + weight +
                        "\npriorities" + priorities +
                        "\ndrone" + drone +
                        "\nTime of Creation" + TimeCreation +
                        "\nAssignation" + Assignation +
                        "\nTime of Colection" + ColectionTime +
                        "\nTime of Delivery" + DeliveryTime;
            }
        
        }
    }
    
}
