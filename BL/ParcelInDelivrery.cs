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
            public bool statusesDelivrery { get; set; }
            public Priorities Priorities { get; set; }
            public WeightCategories weight { get; set; }
            public Location CollectionLocation { get; set; }
            public Location DeliveryLocation { get; set; }
            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Getter { get; set; }

            public double DistanceDelivrery { get; set; }

        }
    }
}
