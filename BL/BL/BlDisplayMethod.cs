using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DO;
namespace BL
{


    partial class BL
    {
        /// <summary>
        /// search a station
        /// </summary>
        /// <param name="id"> searche the station by id</param>
        /// <returns>return the station if exsist </returns>
        public BO.BaseStation SearchStation(int id)
        {
            BO.BaseStation temp = new();
            try
            {
                DO.Station y = idal.SearchStation(id);
                temp.Location = new(y.Longitude, y.Lattitude);
                temp.Name = y.Name;
                temp.Id = y.Id;
                temp.NumAvilableChargeStation = y.ChargeSlots;//
                IEnumerable<DO.DroneCharge> ListDrones = idal.ListOfDronesInCharge(x => x.StationId == id);

                IEnumerable<DO.Drone> DronesIncharge = idal.AllDrones();
                DronesIncharge = from x in DronesIncharge
                                 where (ListDrones as List<DO.DroneCharge>).Exists(z => z.DroneId == x.Id)
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
            catch (DO.IdDoseNotExist x)
            {
                throw new BO.IdDoseNotExist( x.ObjectType, x.Id, x);
            }

            return temp;
        }
        /// <summary>
        /// search a costumer
        /// </summary>
        /// <param name="id"> searche the costumer by id</param>
        /// <returns>return the costumer if exsist or default costumer</returns>
        public BO.CustomerBl SearchCostumer(int id)
        {

            BO.CustomerBl temp = new();

            try
            {
                DO.Costumer x = idal.SearchCostumer(id);
                temp.Id = x.Id;
                temp.name = x.Name;
                temp.numberPhone = x.Phone;
                temp.location = new(x.Longitude, x.Lattitude);
                var parcels = idal.AllParcels();
                foreach (var y in parcels)
                {
                    BO.ParcelStatus status;
                    var s = idal.SearchCostumer(y.Sender);
                    var t = idal.SearchCostumer(y.TargetId);
                    if (y.DroneId==0)
                        status=BO.ParcelStatus.Defined;
                    else if (y.Scheduled==null)
                        status= BO.ParcelStatus.Assigned;
                    else if (y.PickedUp==null)
                    {
                        status=BO.ParcelStatus.Colected;
                    }
                    else status=BO.ParcelStatus.Delivred;

                    if (y.TargetId==id)
                        temp.fromCustomer.Add(new(y.DroneId, (BO.WeightCategories)y.Wheight, (BO.Priorities)y.Priority, status, new(t.Id, t.Name)));


                    if (y.Sender==id)
                        temp.toCustomers.Add(new(y.DroneId, (BO.WeightCategories)y.Wheight, (BO.Priorities)y.Priority, status, new(s.Id, s.Name)));

                }
            }
            catch (DO.IdDoseNotExist x)
            {
                throw new BO.IdDoseNotExist( x.ObjectType, x.Id,x);
            }

            return temp;
        }
        /// <summary>
        /// search a drone
        /// </summary>
        /// <param name="id"> searche the drone by id</param>
        /// <returns>return the drone if exsist or default drone</returns>
        public BO.DroneBL SearchDrone(int id)
        {
            if (!DronesBl.Exists(y => y.Id==id))
            {
                throw new BO.IdDoseNotExist("id dose not exsist","drone",id);
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
                    var y = idal.SearchParcel(x.ParcelId);
                    BO.ParcelInDelivrery z = new();
                    z.Id = y.Id;
                    z.statusDelivrery = true;
                    z.weight =(BO.WeightCategories) y.Wheight;
                    z.Priorities =(BO.Priorities) y.Priority;
                    var sender = SearchCostumer(y.Sender);
                    var getter = SearchCostumer(y.TargetId);
                    z.CollectionLocation = sender.location;

                    z.Sender = new(sender.Id, sender.name);
                    z.Getter = new(getter.Id, getter.name);
                    z.DistanceDelivrery = DistanceLocation(temp.CurrentLocation, getter.location);
                    temp.parcel = z;
                }
                catch(DO.IdDoseNotExist ex)
                {
                    throw new BO.IdDoseNotExist(ex.ObjectType, ex.Id, ex);

                }

            }
            return temp;
        }
        /// <summary>
        /// search a parcel
        /// </summary>
        /// <param name="id"> searche the parcel by id</param>
        /// <returns>return the parcel if exsist or default parcel</returns>
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
            catch (BO.IdDoseNotExist x)
            {
                throw new BO.IdDoseNotExist(x.ObjectType, x.Id, x);

            }
            return temp;
        }

    }
}
