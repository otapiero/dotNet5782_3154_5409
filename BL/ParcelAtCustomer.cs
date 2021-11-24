using System;

namespace IBL
{
    namespace BO
    {
        public class ParcelAtCustomer
        {
            public int Id { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priorities { get; set; }
            public ParcelStatus Status { get; set; }
            public CustomerInParcel OtherCustomer;
            public override string ToString()
            {
                return "  Id: "+Id+"\n  Weight"+Weight+"\n  priorities: "+Priorities+"\n  status:  "+Status+"\n  Second Customer: "+OtherCustomer +"\n";
            }
        }

    }

}
