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
        IEnumerable<BO.DroneToList> Drones;




       public BL()
        {
            List<BO.DroneToList> DronesBl = new List<BO.DroneToList>();
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
                IDAL.DO.Drone tempDlDrone;
                tempDlDrone = dronesData.Find(z => z.Id.Equals(x.DroneId));
                BO.DroneToList temp = new();
                temp.Id = tempDlDrone.Id;
                temp.Model = tempDlDrone.Model;
                
                temp.Weight =(BO.WeightCategories) x.Wheight;
              
                temp.status = BO.DroneStatuses.Delivery;
                if (x.PickedUp == new DateTime())
                    temp.CurrentLocation;
                else
                    temp.CurrentLocation;
                temp.Battery = 0;
                temp.NumParcels = x.Id;
                DronesBl.Add(temp);
            }
        }



    }
}

