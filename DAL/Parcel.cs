using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Parcel
        {
            public int Id { get; set; }
            public int Sender { get; set; }
            public int TargetId { get; set; }
            //WeightCategories Wheight{ get; set; }
            // Priorities  Priority{ get; set; }
            public DateTime Requsted { get; set; }
            public int DroneId { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }

        }

    }
}
