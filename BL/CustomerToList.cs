using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
      public  class CustomerToList
        {
            public CustomerToList(int id, string name, int numberPhone, int numParcelsDelivred, int numParcelsNotDelivred, int numParcelsGetted, int numParcelsInTheWay)
            {
                Id=id;
                Name=name;
                NumberPhone=numberPhone;
                NumParcelsDelivred=numParcelsDelivred;
                NumParcelsNotDelivred=numParcelsNotDelivred;
                NumParcelsGetted=numParcelsGetted;
                NumParcelsInTheWay=numParcelsInTheWay;
            }

            public int Id { get; set; }
            public string Name { get; set; }
            public int NumberPhone { get; set; }
            public int NumParcelsDelivred { get; set; }
            public int NumParcelsNotDelivred { get; set; }
            public int NumParcelsGetted { get; set; }
            public int NumParcelsInTheWay { get; set; }
            
        }
    }
   
}
