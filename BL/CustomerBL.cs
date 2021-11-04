using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class CustomerInDeliveryBl
        {
            public int Id { get; set; }
            public string name { get; set; }
            public string numberPhone { get; set; }
            public LocationBL location { get; set; } 
            List <ParcelToCustomer> toCustomers { get; set; }
            List<ParcelToCustomer> fromCustomers { get; set; }
        }
    }
}
