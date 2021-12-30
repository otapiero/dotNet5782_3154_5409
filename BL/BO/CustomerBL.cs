using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class CustomerBl
    {
        public CustomerBl()
        {
            fromCustomers=new();
            toCustomers=new();
        }

        public int Id { get; set; }
        public string name { get; set; }
        public string numberPhone { get; set; }
        public Location location { get; set; }

        public List<ParcelAtCustomer> fromCustomers { get; set; }
        public List<ParcelAtCustomer> toCustomers { get; set; }

        public override string ToString()
        {
            string str = "Id: "+Id+"\nName: "+name+"\nPhone:"+numberPhone+"\nLocation: "+location;
            str+="\nlist of parcels from cusomer: ";
            foreach (var x in fromCustomers)
            {
                str+="\n  "+x;
            }
            str+="\nlist of parcels to cusomer: ";
            foreach (var x in toCustomers)
            {
                str+="\n  "+x;
            }


            return str+"\n";

        }
    }
}