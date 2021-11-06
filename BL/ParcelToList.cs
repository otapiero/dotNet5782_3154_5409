using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class ParcelToList
        {
            public int Id { get; set; }
            public string NameSender { get; set; }
            public string NameGetter { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priorities { get; set; }
            public ParcelStatus Status { get; set; }

        }

    }
   
}
