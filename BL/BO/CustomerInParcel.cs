using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// The customer in parcel.
    /// </summary>
    public class CustomerInParcel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerInParcel"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="name">The name.</param>
        public CustomerInParcel(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return "Id:  " + Id+ "\n"+ "name:  " + Name+"\n";
        }
    }
}

