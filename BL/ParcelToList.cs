using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
     public   class ParcelToList
        {
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
                return "Id="+Id +"\nNameSender= "+ NameSender+ "\nNameGetter= "+NameGetter+"\nWeight= "+Weight+"\nPriorities= "+Priorities+"\nStatus= "+Status;
            }
        }

    }
   
}
