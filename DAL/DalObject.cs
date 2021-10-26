using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public class DalObject
    {
       


        public DalObject()
        {
            DataSource.Initialize();
        }

        public IDAL.DO.Parcel SearchParcel(int _id)
        {
            IDAL.DO.Parcel finded = new();

            finded = DataSource.parcels.Find(x => x.Id.Equals(_id));

            return finded;
        }
        public void AddNewDrone(string _model,IDAL.DO.WeightCategories _MaxWheight, IDAL.DO.DroneStatuses _status, double _battery)
        {
            int _id = DataSource.Config.IdDefault++;
            DataSource.drones.Add(new IDAL.DO.Drone(_id, _model, _MaxWheight, _status, _battery));
        }



    }
}
