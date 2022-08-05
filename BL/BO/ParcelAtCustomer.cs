using System;


namespace BO
{
    /// <summary>
    /// The parcel  in the customer.
    /// </summary>
    public class ParcelAtCustomer
    {
        public int Id { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priorities { get; set; }
        public ParcelStatus Status { get; set; }
        public CustomerInParcel OtherCustomer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParcelAtCustomer"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="weight">The weight.</param>
        /// <param name="priorities">The priorities.</param>
        /// <param name="status">The status.</param>
        /// <param name="otherCustomer">The other customer.</param>
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
            return "  ID: "+Id+"\n";
        }
    }

}
