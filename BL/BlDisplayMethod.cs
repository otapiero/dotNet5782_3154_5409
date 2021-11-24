 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IDAL.DO;
namespace IBL
{

    public partial class BL
    {
        public BO.BaseStation SearchStation(int id)
        {
            BO.BaseStation temp = new();
            try
            {
                IDAL.DO.Station y = idal.SearchStation(id);
                temp.Location = new(y.Longitude, y.Lattitude);
                temp.Name = y.Name;
                temp.Id = y.Id;
                temp.NumAvilableChargeStation = y.ChargeSlots;//
                IEnumerable<IDAL.DO.DroneCharge> all = idal.AllDronesIncharge();
                all = from x in all
                      where x.StationId == id
                      select x;
                IEnumerable<IDAL.DO.Drone> DronesIncharge = idal.AllDrones();
                DronesIncharge = from x in DronesIncharge
                                 where all.ToList().Exists(z => z.DroneId == x.Id)
                                 select x;

                temp.dronesInCharges = new();
                foreach (var x in DronesIncharge)
                {
                    var t = new BO.DroneInCharge();
                    t.Battery = 0;
                    t.Id = x.Id;
                    temp.dronesInCharges.Add(t);
                }
                
            }
            catch (Exception x)
            {
                throw new BO.IBException(x.Message); ;

            }

            return temp;
        }
        public BO.CustomerBl SearchCostumer(int id)
        {
            
            BO.CustomerBl temp = new();
            
            try
            {
                IDAL.DO.Costumer x = idal.SearchCostumer(id);
                temp.Id = x.Id;
                temp.name = x.Name;
                temp.numberPhone = x.Phone;
                temp.location = new(x.Longitude, x.Lattitude);

            }
            catch (Exception x)
            {
                throw new BO.IBException(x.Message); ;

            }

            return temp;
        }
        public BO.DroneBL SearchDrone(int id)
        {
            if (DronesBl.Exists(y=>y.Id==id))
            {
                throw new BO.IBException("id not esist");
            }
            var x = DronesBl.Find(x => x.Id == id);
          
            BO.DroneBL temp = new();
            temp.Battery = x.Battery;
            temp.CurrentLocation = x.CurrentLocation;
            temp.Id = x.Id;
            temp.Model = x.Model;
            temp.status = x.status;
            temp.Weight = x.Weight;
            if (x.ParcelId != 0)
            {
                try
                {
                    var y = SearchParcel(x.ParcelId);
                    BO.ParcelInDelivrery z = new();
                    z.Id = y.Id;
                    z.statusDelivrery = true;
                    z.weight = y.weight;
                    z.Priorities = y.priorities;
                    var sender = SearchCostumer(y.Sender.Id);
                    var getter = SearchCostumer(y.Getter.Id);
                    z.CollectionLocation = sender.location;

                    z.Sender = new(sender.Id, sender.name);
                    z.Getter = new(getter.Id, getter.name);
                    z.DistanceDelivrery = DistanceLocation(temp.CurrentLocation, getter.location);
                    temp.parcel = z;
                }
                catch 
                {
                    Console.WriteLine("error");

                }

            }
            return temp;
        }
        public BO.ParcelBl SearchParcel(int id)
        {

            BO.ParcelBl temp = new();
            try
            {
                var x = idal.SearchParcel(id);

                var drone = SearchDrone(x.DroneId);
                temp.drone = new();
                temp.drone.Battery = drone.Battery;
                temp.drone.CurrentLocation = drone.CurrentLocation;
                temp.drone.Id = drone.Id;
                temp.Id = x.Id;
                temp.priorities =(BO.Priorities)x.Priority;
                var sender = SearchCostumer(x.Sender);
                var getter = SearchCostumer(x.TargetId);
                temp.Sender = new(sender.Id, sender.name);
                temp.Getter = new(getter.Id, getter.name);
                temp.weight =(BO.WeightCategories)x.Wheight;
            }
            catch (Exception x)
            {
                throw new BO.IBException(x.Message); ;

            }
            return temp;
        }

    }
}
