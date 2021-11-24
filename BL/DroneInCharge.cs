using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
      public  class DroneInCharge
        {
            public int Id { get; set; }
            public double Battery { get; set; }

            public override string ToString()
            {
                return "\n  Id: "+Id+"\n  battery: "+Battery+"\n";
            }
        }
    }
}