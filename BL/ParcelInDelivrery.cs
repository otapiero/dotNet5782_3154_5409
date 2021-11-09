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
            public LocationBl CollectionLocation { get; set; }
            public LocationBl DeliveryLocation { get; set; }
            CustomerInParcel Sender { get; set; }
            CustomerInParcel Getter { get; set; }

            public double DistanceDelivrery { get; set; }

        }
    }
}
