using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{

    public partial class BL
    {
        IDAL.IDal idal;
        IEnumerable<BO.DroneToList> drones;




       public BL()
        {
            drones = new List<BO.DroneToList>();
            idal = new DalObject.DalObject();
            List<IDAL.DO.Drone> dronesData = (List < IDAL.DO.Drone > )idal.AllDrones();
            IEnumerable<IDAL.DO.Parcel> parcelsData = idal.AllParcels();
            Double[] vs = idal.ElectricityUse();
            double Avilable = vs[1];
            double Light = vs[2];
            double Intermidiate = vs[3];
            double Heavy = vs[4];
            double chargingRate = vs[5];
            //Filter parcel
            IEnumerable<IDAL.DO.Parcel> parcelsNotDelivred = from x in parcelsData
                                                             where x.DroneId != 0 && x.Delivered != new DateTime()
                                                             select x;
            foreach(var x in parcelsNotDelivred)
            {
                IDAL.DO.Drone y;
                y = dronesData.Find(y => y.Id.Equals(x.DroneId));
                
            }
        }



    }
}

