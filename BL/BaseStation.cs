using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class BaseStation
        {
            public BaseStation() { dronesInCharges=new(); }
            public int Id { get; set; }
            public string Name { get; set; }
            public Location Location { get; set; }
            public int NumAvilableChargeStation { get; set; }
            public List<DroneInCharge> dronesInCharges { get; set; }

            public override string ToString()
            {
                string str = "Id: "+Id+"\nName: \n"+Name+"\nLocation: "+Location+"\nnumber of avilable charge spolt: "+ NumAvilableChargeStation+
                    "\nlist of drone in charge: ";
                foreach (var x in dronesInCharges)
                {
                    str+="\n  "+x.ToString();
                }
                return str+" \n\n";
            }
        }



    }
}