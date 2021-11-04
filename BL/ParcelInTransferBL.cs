using System;

namespace IBL
{
    namespace BO
    {
        public class ParcelToCustomerBL
        {
            public int Id { get; set; }
            public Priorities priorities { get; set; }
            public CustomerInDeliveryBL costomerGetter { get; set; }
            public CustomerInDeliveryBL costomerSender { get; set; }
        }

    }

}
