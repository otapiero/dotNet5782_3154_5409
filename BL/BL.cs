using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    partial class BL
    {
        IDAL.IDal idal;
        IEnumerable<IDAL.DO.Drone> drones;




        BL()
        {
            idal = new DalObject.DalObject();
            IEnumerable<IDAL.DO.Drone> dronesTemp = idal.AllDrones();
           
            Double[] vs = idal.ElectricityUse();
            double Avilable = vs[1];
            double Light = vs[2];
            double Intermidiate = vs[3];
            double Heavy = vs[4];
            double chargingRate= vs[5];
          

        }



        }
}

