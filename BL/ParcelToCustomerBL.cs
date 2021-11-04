using System;

namespace IBL
{
    namespace BO
    {
        public class ParcelToCustomerBL
        {
            public int Id { get; set; }
            public Priorities priorities { get; set; }
            public WeightCategories weight { get; set; }
            public bool statusesDelivery { get; set; }
            public CustomerInDeliveryBL destinationOrTarget{ get; set; }
        }
            
    }

}
