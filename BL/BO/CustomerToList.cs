using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class CustomerToList
    {
        public CustomerToList(int id, string name, string numberPhone, int numParcelsDelivred, int numParcelsNotDelivred, int numParcelsGetted, int numParcelsInTheWay)
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
        public string NumberPhone { get; set; }
        public int NumParcelsDelivred { get; set; }
        public int NumParcelsNotDelivred { get; set; }
        public int NumParcelsGetted { get; set; }
        public int NumParcelsInTheWay { get; set; }
        public override string ToString()
        {
            return "id: "+Id +"\nName: "+Name+"\nPhone: "+NumberPhone+"\n";

        }
    }
}