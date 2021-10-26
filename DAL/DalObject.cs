using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public class DalObject
    {
<<<<<<< HEAD

        public IDAL.DO.Customer searchCostumer(int id)
        {
            IDAL.DO.Customer find= new IDAL.DO.Customer();
            if (DataSource.customers.Exists(x => x.Id.Equals(id)))
            {
                find = DataSource.customers.Find(x => x.Id.Equals(id));
            }
            return find;
        }
        public IDAL.DO.Drone searchDrone(int id)
        {
            IDAL.DO.Drone find = new IDAL.DO.Drone();
            if (DataSource.drones.Exists(x => x.Id.Equals(id)))
            {
                find = DataSource.drones.Find(x => x.Id.Equals(id));
            }
            return find;
        }
        public IDAL.DO.Station searchStation(int id)
        {
            IDAL.DO.Station find = new IDAL.DO.Station();
            if (DataSource.stations.Exists(x => x.Id.Equals(id)))
            {
                find = DataSource.stations.Find(x => x.Id.Equals(id));
            }
            return find;
        }


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
