using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// The parcel to list.
    /// </summary>
    public class ParcelToList
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParcelToList"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="nameSender">The name sender.</param>
        /// <param name="nameGetter">The name getter.</param>
        /// <param name="weight">The weight.</param>
        /// <param name="priorities">The priorities.</param>
        /// <param name="status">The status.</param>
        public ParcelToList(int id, string nameSender, string nameGetter, WeightCategories weight, Priorities priorities, ParcelStatus status)
        {
            Id=id;
            NameSender=nameSender;
            NameGetter=nameGetter;
            Weight=weight;
            Priorities=priorities;
            Status=status;
        }

        public int Id { get; set; }
        public string NameSender { get; set; }
        public string NameGetter { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priorities { get; set; }
        public ParcelStatus Status { get; set; }



        public override string ToString()
        {
            return "Id: "+Id +"\nNameSender: "+ NameSender+ "\nNameGetter: "+NameGetter+"\nWeight: "+Weight+"\nPriorities: "+
                Priorities+"\nStatus: "+Status+"\n";
        }
    }

}