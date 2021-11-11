using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class CustomerBl
        {
            public int Id { get; set; }
            public string name { get; set; }
            public int numberPhone { get; set; }
            public Location location { get; set; } 
            
            List<ParcelAtCustomer> fromCustomers { get; set; }
            List<ParcelAtCustomer> toCustomers { get; set; }
        }
    }
}
