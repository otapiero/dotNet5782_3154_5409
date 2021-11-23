
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    ///<summary>Class <c>DalObject</c></summary>
    public class DalObject:IDAL.IDal
    {
        ///<summary>method <c>DalObject</c> initialize the data</summary>
        public DalObject()
        {
            DataSource.Initialize();
        }
        ///<summary>method <c>SearchCustomer</c> </summary>
        ///<param name="id"> searche the customer by id</param>
        public IDAL.DO.Costumer SearchCostumer(int id)
        {
            if (!DataSource.customers.Exists(x => x.Id.Equals(id)))
            {
                throw new IDAL.DO.IdExaption("Id not found.");
            }
            IDAL.DO.Costumer find = new IDAL.DO.Costumer();
            
            find = DataSource.customers.Find(x => x.Id.Equals(id));
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
        ///         
        public void AddNewDrone(int id,string _model)
        {
            IDAL.DO.Drone temp = new();
            temp.Id = id;
            temp.Model = _model;
            DataSource.drones.Add(temp);
        }
        /// <summary>method AddNewStation </summary>
        /// <param name="_name"> station name</param>
        /// <param name="_Longitude"> location of station</param>
        /// <param name="_Lattitude"> location of station</param>
        /// <param name=" _chargeSlots">status of charge</param>
        public void AddNewStation(int id,string _name, double _Longitude, double _Lattitude, int _chargeSlots)
        {
            if (DataSource.stations.Exists(x => x.Id == id))
            {
                //excrption id
            }
            IDAL.DO.Station temp = new();
            temp.Id = id;
            temp.Name = _name;
            temp.Longitude = _Longitude;
            temp.Lattitude = _Lattitude;
            temp.ChargeSlots = _chargeSlots;
            DataSource.stations.Add(temp);
        }
        /// <summary>method AddNewCustomer </summary>
        /// <param name="_id"> customer id</param>
        /// <param name=" _Name">customer name</param>
        /// <param name="_Phone"> customer phone number</param>
        /// <param name="_Longitude"> location of station</param>
        /// <param name="_Lattitude"> location of station</param>
        public void AddNewCustomer(int _id, string _Name, string _Phone, double _Longitude, double _Lattitude)
        {

            if (DataSource.customers.Exists(x => x.Id == _id))
            {
                //excrption id
            }
            IDAL.DO.Costumer temp = new();
            temp.Id = _id;
            temp.Name = _Name;
            temp.Phone = _Phone;
            temp.Longitude = _Longitude;
            temp.Lattitude = _Lattitude;

            DataSource.customers.Add(temp);
        }
        /// <summary>method  AddNewParcel </summary>
        /// <param name="_Sender"> customer id of sender</param>
        /// <param name="_TargetId">customer id of target</param>
        /// <param name="_Wheight"> enum weight of parcel</param>
        /// <param name="_Priority"> enum priority of parcel</param>
        public void AddNewParcel(int _Sender, int _TargetId, int _Wheight, int _Priority)
        {

         
            IDAL.DO.Parcel temp = new();
            temp.Id = DataSource.Config.idParcel++;
            temp.Sender = _Sender;
            temp.TargetId = _TargetId;
            temp.Wheight = (IDAL.DO.WeightCategories)_Wheight;
            temp.Priority = (IDAL.DO.Priorities)_Priority;
            temp.Requsted = DateTime.Now;
            temp.Scheduled = new DateTime();
            temp.PickedUp = new DateTime();
            temp.Delivered = new DateTime();
            
            DataSource.parcels.Add(temp);
        }
         /// <summary>method ConnectParcelToDrone - the function get parcel and connect a avilable drone </summary>
        /// <param name="idParcel"> id parcel to delivery</param>
        public void ConnectParcelToDrone(int idParcel)
        {
            IDAL.DO.Drone find = new IDAL.DO.Drone();
            //if have a avilable drone
           
            
                //connect parcel to drone and update the list
                find = DataSource.drones.Find(x => x.Id>0);
                IDAL.DO.Drone temp = find;
                DataSource.drones.Remove(find);
               
                DataSource.drones.Add(temp);
                IDAL.DO.Parcel pocket = SearchParcel(idParcel);
                IDAL.DO.Parcel tempParcel = pocket;
                tempParcel.DroneId = find.Id;
                DataSource.parcels.Remove(pocket);
                DataSource.parcels.Add(tempParcel);
            

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
            try { 
           
                IDAL.DO.Station foundS = SearchStation(idStation);
                IDAL.DO.Station tempS = foundS;
                tempS.ChargeSlots -=1;
                DataSource.stations.Remove(foundS);
                DataSource.stations.Add(tempS);
                DataSource.DroneCharges.Add(new IDAL.DO.DroneCharge(idDrone, idStation));
            
            }
            catch { }
        }
        /// <summary>method ReleseDroneFromCharge - the function get drone relese it fron station </summary>
        /// <param name = "idDrone"> id drone to relese</param>
        public void ReleseDroneFromCharge(int id)
        {
            try { 
                
                IDAL.DO.Station foundS = SearchStation(id);


                IDAL.DO.Station tempS = foundS;
                    tempS.ChargeSlots +=1;
                    DataSource.stations.Remove(foundS);
                    DataSource.stations.Add(tempS);
                    IDAL.DO.DroneCharge relese = DataSource.DroneCharges.Find(x => x.DroneId.Equals(idDrone));
                    DataSource.drones.Remove(found);
                    DataSource.drones.Add(temp);
                    DataSource.DroneCharges.Remove(relese);
                }}
            catch { }
        }
         
        public void UpdateDroneModel(int id,string model)
        {
            try 
	        {	        
		    IDAL.DO.Drone found = DataSource.drones.First(w => w.Id == id);
            IDAL.DO.Drone temp = found;
            temp.Model = model;
            DataSource.drones.Remove(found);
            DataSource.drones.Add(temp);
	        }
	        catch (Exception)
	        {

		    throw;
	        }
            
        }
          public void UpdateStation(int id, string name,int chargeSlots)
        {
            IDAL.DO.Station found = DataSource.stations.First(w => w.Id == id);
            IDAL.DO.Station temp = found;
            name = name.Length()>0 ? name : found.Name;
            chargeSlots = chargeSlots.ToString().Length()>0 ? chargeSlots : found.ChargeSlots;
            temp.Name = name;
            temp.ChargeSlots=chargeSlots;
            DataSource.stations.Remove(found);
            DataSource.stations.Add(temp);
        }
        public void UpdateCostumer(int id, string name,string phone)
        {
            IDAL.DO.Costumer found = DataSource.customers.First(w => w.Id == id);
            IDAL.DO.Costumer temp = found;
            name = name.Length()>0 ? name : found.Name;
            phone = phone.Length()>0 ? phone : found.Name;
            temp.Name = name;
            temp.Phone = phone;
            temp.ChargeSlots=chargeSlots;
            DataSource.customers.Remove(found);
            DataSource.customers.Add(temp);
        }
        ///<summary>List - copy list of station for the main program</summary>
        ///<returns>list of all stations</returns>
        public IEnumerable<IDAL.DO.Station> AllStation()
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
        public IEnumerable<IDAL.DO.Drone> AllDrones()
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
        public IEnumerable<IDAL.DO.Costumer> AllCustomers()
        {
            List<IDAL.DO.Costumer> allCustomers = new List<IDAL.DO.Costumer>();
            foreach (var t in DataSource.customers)
            {
                allCustomers.Add(t);
            }
            return allCustomers;
        }
        ///<summary>List - copy list of parcels for the main program</summary>
        ///<returns>list of all parcels</returns>
        public IEnumerable<IDAL.DO.Parcel> AllParcels()
        {
            List<IDAL.DO.Parcel> allParcels = new();
            foreach (var t in DataSource.parcels)
            {
                allParcels.Add(t);
            }
            return allParcels;
        }
        public IEnumerable<IDAL.DO.DroneCharge> AllDronesIncharge()
        {
            List<IDAL.DO.DroneCharge> allDronesIncharge = new();
            foreach (var t in DataSource.DroneCharges)
            {
                allDronesIncharge.Add(t);
            }
            return allDronesIncharge;
        }
        ///<summary>List - copy list of all pending parcels </summary>
        ///<returns>list of all free parcel</returns>
        public IEnumerable<IDAL.DO.Parcel> NotAssociatedParcels()
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
        public IEnumerable<IDAL.DO.Station> StationWithAvailebalChargePost()
        {
            List<IDAL.DO.Station> stations = new();
            foreach (var t in DataSource.stations)
            {
                if (t.ChargeSlots > 0)
                    stations.Add(t);
            }
            return stations;
        }
        public double[] ElectricityUse()
        {
            double[] arr = new double[5];
            arr[0]=DataSource.Config.Avilable;
            arr[1] = DataSource.Config.Light;
            arr[2] = DataSource.Config.Intermidiate;
            arr[3] = DataSource.Config.Heavy;
            arr[4] = DataSource.Config.chargingRatePerHoure;

            return arr;
         }
    }
}
