using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

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
        [MethodImpl(MethodImplOptions.Synchronized)]

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

              IEnumerable< BO.DroneToList> DronesIncharge = from x in DronesBl
                                 where (ListDrones as List<DO.DroneCharge>).Exists(z => z.DroneId == x.Id)
                                 select x;

                temp.dronesInCharges = new();
                foreach (var x in DronesIncharge)
                {
                    var t = new BO.DroneInCharge();
                    t.Battery = x.Battery; 
                    t.Id = x.Id;
                    temp.dronesInCharges.Add(t);
                }

            }
            catch (DO.IdDoseNotExist x)
            {
                throw new BO.IdDoseNotExist( x.ObjectType, x.Id, x);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new BO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
            return temp;
        }
        /// <summary>
        /// search a costumer
        /// </summary>
        /// <param name="id"> searche the costumer by id</param>
        /// <returns>return the costumer if exsist or default costumer</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]

        public BO.CustomerBl SearchCostumer(int id)
        {

            BO.CustomerBl temp = new();

            try
            {
                DO.Costumer x = idal.SearchCostumer(id);
                temp.Id = x.Id;

                temp.name = x.Name;
                temp.numberPhone = x.Phone;
                temp.password = x.Password;
                temp.location = new(x.Longitude, x.Lattitude);
                var parcelsTo = idal.ListOfParcels(x => (x.TargetId==id));
                var parcelsFrom = idal.ListOfParcels(x => x.Sender==id);
                temp.fromCustomer=new();
               temp.toCustomers=new();
                BO.ParcelStatus status;
                foreach (var y in parcelsTo)
                {
                    var s = idal.SearchCostumer(y.Sender);
                    if (y.DroneId==0)
                        status=BO.ParcelStatus.Defined;
                    else if (y.Scheduled==null)
                        status= BO.ParcelStatus.Assigned;
                    else if (y.PickedUp==null)
                    {
                        status=BO.ParcelStatus.Colected;
                    }
                    else status=BO.ParcelStatus.Delivred;
                    temp.fromCustomer.Add(new(y.Id, (BO.WeightCategories)y.Wheight, (BO.Priorities)y.Priority, status, new(s.Id, s.Name)));
                }
                foreach (var z in parcelsFrom)
                {
                    var t = idal.SearchCostumer(z.TargetId);
                    if (z.DroneId==0)
                        status=BO.ParcelStatus.Defined;
                    else if (z.Scheduled==null)
                        status= BO.ParcelStatus.Assigned;
                    else if (z.PickedUp==null)
                    {
                        status=BO.ParcelStatus.Colected;
                    }
                    else status=BO.ParcelStatus.Delivred;
                    temp.toCustomers.Add(new(z.Id, (BO.WeightCategories)z.Wheight, (BO.Priorities)z.Priority, status, new(t.Id, t.Name)));
                }
            }

            catch (DO.IdDoseNotExist x)
            {
                throw new BO.IdDoseNotExist(x.ObjectType, x.Id, x);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new BO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }

            return temp;
        }
        /// <summary>
        /// search a drone
        /// </summary>
        /// <param name="id"> searche the drone by id</param>
        /// <returns>return the drone if exsist or default drone</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]

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
            temp.parcel=new();
            if (x.ParcelId != 0)
            {
                try
                {
                    var y = idal.SearchParcel(x.ParcelId);

                    temp.parcel.Id = y.Id;
                    if(y.PickedUp == null)
                    {
                        temp.parcel.statusDelivrery = false;
                    }
                    else temp.parcel.statusDelivrery = true;
                    temp.parcel.weight =(BO.WeightCategories) y.Wheight;
                    temp.parcel.Priorities =(BO.Priorities) y.Priority;
                    var sender = idal.SearchCostumer(y.Sender);
                    var getter = idal.SearchCostumer(y.TargetId);
                    temp.parcel.CollectionLocation =new BO.Location( sender.Longitude, sender.Lattitude);

                    temp.parcel.Sender = new(sender.Id, sender.Name);
                    temp.parcel.Getter = new(getter.Id, getter.Name);
                    if (temp.parcel.statusDelivrery)
                        temp.parcel.DistanceDelivrery = DistanceLocation(temp.CurrentLocation, new BO.Location(getter.Longitude, getter.Lattitude));//kkkk
                    else
                        temp.parcel.DistanceDelivrery = DistanceLocation(temp.CurrentLocation, new BO.Location(sender.Longitude, sender.Lattitude));//kkkk

                    temp.parcel.DeliveryLocation=new BO.Location(getter.Longitude, getter.Lattitude);
                    temp.parcel.CollectionLocation=new BO.Location(sender.Longitude, sender.Lattitude);
                }
                catch(DO.IdDoseNotExist ex)
                {
                    throw new BO.IdDoseNotExist(ex.ObjectType, ex.Id, ex);
                }
                catch (DO.XMLFileLoadCreateException ex)
                {
                    throw new BO.XMLFileLoadCreateException(ex.XmlFilePath, ex.Message, ex.InnerException);
                }
            }
            return temp;
        }
        /// <summary>
        /// search a parcel
        /// </summary>
        /// <param name="id"> searche the parcel by id</param>
        /// <returns>return the parcel if exsist or default parcel</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.ParcelBl SearchParcel(int id)
        {
            BO.ParcelBl temp = new();
            try
            {

                var x = idal.SearchParcel(id);
                temp.drone = new();
                if (x.DroneId != 0)
                {
                    var drone = SearchDrone(x.DroneId);
                    temp.drone.Battery = drone.Battery;
                    temp.drone.CurrentLocation = drone.CurrentLocation;
                    temp.drone.Id = drone.Id;
                }
                else temp.drone.Id = 0;
                temp.Id = x.Id;
                temp.priorities =(BO.Priorities)x.Priority;
                var sender = idal.SearchCostumer(x.Sender);
                var getter = idal.SearchCostumer(x.TargetId);
                temp.Sender = new(sender.Id, sender.Name);
                temp.Getter = new(getter.Id, getter.Name);
                temp.weight =(BO.WeightCategories)x.Wheight;
                temp.Assignation = x.Scheduled;
                temp.ColectionTime = x.PickedUp;
                temp.DeliveryTime = x.Delivered;
                temp.TimeCreation = x.Requsted;

            }
            catch (DO.IdDoseNotExist x)
            {
                throw new BO.IdDoseNotExist(x.ObjectType, x.Id, x);
            }
            catch (BO.IdDoseNotExist x)
            {
                throw new BO.IdDoseNotExist(x.ObjectType, x.Id, x);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new BO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
            return temp;
        }

    }
}
