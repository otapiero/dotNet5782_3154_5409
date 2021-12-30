using System;


namespace BO
{
    public class ParcelAtCustomer
    {
        public int Id { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priorities { get; set; }
        public ParcelStatus Status { get; set; }
        public CustomerInParcel OtherCustomer;

        public ParcelAtCustomer(int id, WeightCategories weight, Priorities priorities, ParcelStatus status, CustomerInParcel otherCustomer)
        {
            Id=id;
            Weight=weight;
            Priorities=priorities;
            Status=status;
            OtherCustomer=otherCustomer;
        }

        public override string ToString()
        {
            return "  Id: "+Id+"\n  Weight"+Weight+"\n  priorities: "+Priorities+"\n  status:  "+Status+"\n  Second Customer: "+OtherCustomer +"\n";
        }
    }

}
