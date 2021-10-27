
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    ///<summary>Class <c>DalObject</c></summary>
    public class DalObject
    {
        ///<summary>method <c>DalObject</c> initialize the data</summary>
        public DalObject()
        {
            DataSource.Initialize();
        }
        ///<summary>method <c>SearchCustomer</c> </summary>
        ///<param name="id"> searche the customer by id</param>
        public IDAL.DO.Customer SearchCustomer(int id)
        {
            IDAL.DO.Customer find = new IDAL.DO.Customer();
            if (DataSource.customers.Exists(x => x.Id.Equals(id)))
            {
                find = DataSource.customers.Find(x => x.Id.Equals(id));
            }
            return find;
        }

        /// <summary>method <c>SearchDrone</c> </summary>
        /// <param name="id"> searche the drone by id</param>
        /// <returns>the drone if exsist</returns>
        public IDAL.DO.Drone SearchDrone(int id)
        {
            if (DataSource.drones.Exists(x => x.Id.Equals(id)))
            {
                IDAL.DO.Drone found = DataSource.drones.Find(x => x.Id.Equals(id));
                return found;
            }
            IDAL.DO.Drone notfound = new IDAL.DO.Drone();
            return notfound;
        }
        ///<summary>method <c>SearchStation</c> </summary>
        ///<param name="id"> searche the Station by id</param>
        public IDAL.DO.Station SearchStation(int id)
        {
            IDAL.DO.Station find = new IDAL.DO.Station();
            if (DataSource.stations.Exists(x => x.Id.Equals(id)))
            {
                find = DataSource.stations.Find(x => x.Id.Equals(id));
            }
            return find;
        }
        /// <summary>method <c> SearchParcel</c> </summary>
        /// <param name="id"> searche the parcel by id</param>
        /// <returns>return the parcel if exsist</returns>
        public IDAL.DO.Parcel SearchParcel(int id)
        {
            if (DataSource.parcels.Exists(x => x.Id.Equals(id)))
            {
                IDAL.DO.Parcel found = DataSource.parcels.Find(x => x.Id.Equals(id));
                return found;
            }
            IDAL.DO.Parcel notfound = new IDAL.DO.Parcel();
            return notfound;
        }
        /// <summary>method AddNewDrone </summary>
        /// <param name="_model"> model of drone</param>
        /// <param name="_MaxWheight"> enum of weight drone</param>
        /// <param name="_status"> enum status of drone</param>
        /// <param name=" _battery">life battery of drone</param>
        public void AddNewDrone(string _model, int _MaxWheight, int _status, double _battery)
        {
            DataSource.drones.Add(new IDAL.DO.Drone(DataSource.Config.idDrone++, _model, (IDAL.DO.WeightCategories)_MaxWheight, (IDAL.DO.DroneStatuses)_status, _battery));
        }
        /// <summary>method AddNewStation </summary>
        /// <param name="_name"> station name</param>
        /// <param name="_Longitude"> location of station</param>
        /// <param name="_Lattitude"> location of station</param>
        /// <param name=" _chargeSlots">status of charge</param>
        public void AddNewStation(string _name, double _Longitude, double _Lattitude, int _chargeSlots)
        {
            DataSource.stations.Add(new IDAL.DO.Station(DataSource.Config.IdStation++, _name, _Longitude, _Lattitude, _chargeSlots));
        }
        /// <summary>method AddNewCustomer </summary>
        /// <param name="_id"> customer id</param>
        /// <param name=" _Name">customer name</param>
        /// <param name="_Phone"> customer phone number</param>
        /// <param name="_Longitude"> location of station</param>
        /// <param name="_Lattitude"> location of station</param>
        public void AddNewCustomer(int _id, string _Name, string _Phone, double _Longitude, double _Lattitude)
        {

            DataSource.customers.Add(new IDAL.DO.Customer(_id, _Name, _Phone, _Longitude,  _Lattitude));
        }
        /// <summary>method  AddNewParcel </summary>
        /// <param name="_Sender"> customer id of sender</param>
        /// <param name="_TargetId">customer id of target</param>
        /// <param name="_Wheight"> enum weight of parcel</param>
        /// <param name="_Priority"> enum priority of parcel</param>
        public void AddNewParcel(int _Sender, int _TargetId, int _Wheight, int _Priority)
        {
            DateTime _Requsted = DateTime.Now;
            DataSource.parcels.Add(new IDAL.DO.Parcel(DataSource.Config.idParcel++, _Sender, _TargetId, (IDAL.DO.WeightCategories)_Wheight, (IDAL.DO.Priorities)_Priority, _Requsted, 0, _Requsted, _Requsted, _Requsted));
        }
         /// <summary>method ConnectParcelToDrone - the function get parcel and connect a avilable drone </summary>
        /// <param name="idParcel"> id parcel to delivery</param>
        public void ConnectParcelToDrone(int idParcel)
        {
            IDAL.DO.Drone find = new IDAL.DO.Drone();
            //if have a avilable drone
            if (DataSource.drones.Exists(x => x.Status.Equals(IDAL.DO.DroneStatuses.Available)))
            {
                //connect parcel to drone and update the list
                find = DataSource.drones.Find(x => x.Status.Equals(IDAL.DO.DroneStatuses.Available));
                IDAL.DO.Drone temp = find;
                DataSource.drones.Remove(find);
                temp.Status = IDAL.DO.DroneStatuses.Delivery;
                DataSource.drones.Add(temp);
                IDAL.DO.Parcel pocket = SearchParcel(idParcel);
                IDAL.DO.Parcel tempParcel = pocket;
                tempParcel.DroneId = find.Id;
                DataSource.parcels.Remove(pocket);
                DataSource.parcels.Add(tempParcel);
            }

        }
        /// <summary>method ParceCollectionByDrone - the function get parcel and update time picked up </summary>
        /// <param name="idParcel"> id parcel pickedup</param>
        public void ParceCollectionByDrone(int idParcel)
        {
            IDAL.DO.Parcel pocket = SearchParcel(idParcel);
            //if the parcel exsist
            if (pocket.Id > 0)
            {
                //update picked up time on the parcel lists
                IDAL.DO.Parcel tempPocket = pocket;
                tempPocket.PickedUp = DateTime.Now;
                DataSource.parcels.Remove(pocket);
                DataSource.parcels.Add(tempPocket);
            }

        }
        /// <summary>method DeliveryParcelToCustomer - the function get parcel and update dilevry time </summary>
        /// <param name="idParcel"> id parcel delivery</param>
        public void DeliveryParcelToCustomer(int idParcel)
        {
            IDAL.DO.Parcel pocket = SearchParcel(idParcel);
            //if the parcel exsist
            if (pocket.Id > 0)
            {
                IDAL.DO.Parcel tempParcel = pocket;
                IDAL.DO.Drone found = SearchDrone(pocket.Id);
                IDAL.DO.Drone tempDrone = found;
                tempDrone.Status = IDAL.DO.DroneStatuses.Available;
                tempParcel.Delivered = DateTime.Now;
                DataSource.parcels.Remove(pocket);
                DataSource.parcels.Add(tempParcel);
                DataSource.drones.Remove(found);
                DataSource.drones.Add(tempDrone);
            }
        }
        /// <summary>method SendDroneToCharge - the function get drone and station and coonect them </summary>
        /// <param name = "idDrone"> id drone to charge</param>
        /// <param name = "idStation"> id free station</param>
        public void SendDroneToCharge(int idDrone, int idStation)
        {
            IDAL.DO.Drone found = SearchDrone(idDrone);
            if (found.Id > 0)
            {
                IDAL.DO.Drone temp = found;
                temp.Battery += 1;
                temp.Status = IDAL.DO.DroneStatuses.Maintenace;
                DataSource.drones.Remove(found);
                DataSource.drones.Add(temp);
                DataSource.DroneCharges.Add(new IDAL.DO.DroneCharge(idDrone, idStation));
            }
        }
            
        /// <summary>method ReleseDroneFromCharge - the function get drone relese it fron station </summary>
        /// <param name = "idDrone"> id drone to relese</param>
        public void ReleseDroneFromCharge(int idDrone)
        {
            IDAL.DO.Drone found = SearchDrone(idDrone);
            if (found.Id > 0)
            {
                IDAL.DO.Drone temp = found;
                temp.Status = IDAL.DO.DroneStatuses.Available;
                IDAL.DO.DroneCharge relese = DataSource.DroneCharges.Find(x => x.DroneId.Equals(idDrone));
                DataSource.drones.Remove(found);
                DataSource.drones.Add(temp);
                DataSource.DroneCharges.Remove(relese);
            }
        }
        ///<summary>List - copy list of station for the main program</summary>
        ///<returns>list of all stations</returns>
        public List<IDAL.DO.Station> AllStation()
        {
            List<IDAL.DO.Station> allStations = new List<IDAL.DO.Station>();
            foreach(var t in DataSource.stations)
            {
                allStations.Add(t);
            }
            return allStations;
        }
        ///<summary>List - copy list of drones for the main program</summary>
        ///<returns>list of all drones</returns>
        public List<IDAL.DO.Drone> AllDrones()
        {
            List<IDAL.DO.Drone> allDrones = new List<IDAL.DO.Drone>();
            foreach (var t in DataSource.drones)
            {
                allDrones.Add(t);
            }
            return allDrones;
        }
        ///<summary>List - copy list of customers for the main program</summary>
        ///<returns>list of all costomers</returns>
        public List<IDAL.DO.Customer> AllCustomers()
        {
            List<IDAL.DO.Customer> allCustomers = new List<IDAL.DO.Customer>();
            foreach (var t in DataSource.customers)
            {
                allCustomers.Add(t);
            }
            return allCustomers;
        }
        ///<summary>List - copy list of parcels for the main program</summary>
        ///<returns>list of all parcels</returns>
        public List<IDAL.DO.Parcel> AllParcels()
        {
            List<IDAL.DO.Parcel> allParcels = new List<IDAL.DO.Parcel>();
            foreach (var t in DataSource.parcels)
            {
                allParcels.Add(t);
            }
            return allParcels;
        }
        ///<summary>List - copy list of all pending parcels </summary>
        ///<returns>list of all free parcel</returns>
        public List<IDAL.DO.Parcel> NotAssociatedParcels()
        {
            List<IDAL.DO.Parcel> notAssociatedParcels = new List<IDAL.DO.Parcel>();
            foreach(var t in DataSource.parcels)
            {
                if (t.DroneId == 0)
                    notAssociatedParcels.Add(t);
            }

            return notAssociatedParcels;
        }
        ///<summary>List - copy list of all free stations </summary>
        ///<returns>list of all free stations for charge</returns>
        public List<IDAL.DO.Station> StationWithAvailebalChargePost()
        {
            List<IDAL.DO.Station> stations = new();
            foreach (var t in DataSource.stations)
            {
                if (t.ChargeSlots > 0)
                    stations.Add(t);
            }
            return stations;
        }
    }
}
