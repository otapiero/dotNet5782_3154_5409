using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class DeliveryByTransfer
        {
            public int Id { get; set; }
            public WeightCategories weight { get; set; }
            public Priorities Priorities { get; set; }
            public bool statusesDelivrery { get; set; }
            public LocationBl Collection { get; set; }
            public LocationBl DeliveryDestinationLocation { get; set; }
            public LocationBl Transportdistance { get; set; }
            public double DistanceDelivrery { get; set; }

        }
    }
}
