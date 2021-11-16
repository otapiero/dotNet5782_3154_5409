using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
      public  class BaseStationToList
        {
            public int Id { get; set; }
            public string Name { get; set; }
           
            public int NumAvilableChargeStation { get; set; }
            public int NumNotAvilableChargeStation { get; set; }
        }
    }
    
}
