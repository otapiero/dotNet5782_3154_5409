
﻿using System;
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
        //search by id
        public IDAL.DO.Customer SearchCustomer(int id)
        {
            IDAL.DO.Customer find = new IDAL.DO.Customer();
            if (DataSource.customers.Exists(x => x.Id.Equals(id)))
            {
                find = DataSource.customers.Find(x => x.Id.Equals(id));
            }
            return find;
        }
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
        public IDAL.DO.Station SearchStation(int id)
        {
            IDAL.DO.Station find = new IDAL.DO.Station();
            if (DataSource.stations.Exists(x => x.Id.Equals(id)))
            {
                find = DataSource.stations.Find(x => x.Id.Equals(id));
            }
            return find;
        }
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
        //Add items
        public void AddNewDrone(string _model, int _MaxWheight, int _status, double _battery)
        {
            DataSource.drones.Add(new IDAL.DO.Drone(DataSource.Config.IdDefault++, _model, (IDAL.DO.WeightCategories)_MaxWheight, (IDAL.DO.DroneStatuses)_status, _battery));
        }
        public void AddNewStation(string _name, double _Longitude, double _Lattitude, int _chargeSlots)
        {
            DataSource.stations.Add(new IDAL.DO.Station(DataSource.Config.idObject++, _name, _Longitude, _Lattitude, _chargeSlots));
        }
        public void AddNewCustomer(int _id, string _Name, string _Phone, double _Longitude, double _Lattitude)
        {

            DataSource.customers.Add(new IDAL.DO.Customer(_id, _Name, _Phone, _Longitude,  _Lattitude));
        }
        public void AddNewParcel(int _Sender, int _TargetId, int _Wheight, int _Priority)
        {
            DateTime _Requsted = DateTime.Now;
            DataSource.parcels.Add(new IDAL.DO.Parcel(DataSource.Config.idObject++, _Sender, _TargetId, (IDAL.DO.WeightCategories)_Wheight, (IDAL.DO.Priorities)_Priority, _Requsted, 0, _Requsted, _Requsted, _Requsted));
        }
        //update items
        public void ConnectParcelToDrone(int idParcel)
        {
            IDAL.DO.Drone find = new IDAL.DO.Drone();
            if (DataSource.drones.Exists(x => x.Status.Equals(IDAL.DO.DroneStatuses.Available)))
            {
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
        public void ParceCollectionByDrone(int idParcel)
        {
            IDAL.DO.Parcel pocket = SearchParcel(idParcel);

            if (pocket.Id > 0)
            {
                IDAL.DO.Parcel tempPocket = pocket;
                tempPocket.PickedUp = DateTime.Now;
                DataSource.parcels.Remove(pocket);
                DataSource.parcels.Add(tempPocket);
            }

        }
        public void DeliveryParcelToCustomer(int idParcel)
        {
            IDAL.DO.Parcel pocket = SearchParcel(idParcel);
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
        //view lists
        public List<IDAL.DO.Station> AllStation()
        {
            List<IDAL.DO.Station> allStations = new List<IDAL.DO.Station>();
            foreach(var t in DataSource.stations)
            {
                allStations.Add(t);
            }
            return allStations;
        }
        public List<IDAL.DO.Drone> AllDrones()
        {
            List<IDAL.DO.Drone> allDrones = new List<IDAL.DO.Drone>();
            foreach (var t in DataSource.drones)
            {
                allDrones.Add(t);
            }
            return allDrones;
        }
        public List<IDAL.DO.Customer> AllCustomers()
        {
            List<IDAL.DO.Customer> allCustomers = new List<IDAL.DO.Customer>();
            foreach (var t in DataSource.customers)
            {
                allCustomers.Add(t);
            }
            return allCustomers;
        }
        public List<IDAL.DO.Parcel> AllParcels()
        {
            List<IDAL.DO.Parcel> allParcels = new List<IDAL.DO.Parcel>();
            foreach (var t in DataSource.parcels)
            {
                allParcels.Add(t);
            }
            return allParcels;
        }
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
        public static string LatitudeDoubleToString(double Latitude)
        {
            Latitude = Math.Abs(Latitude);
            string cordinates = Convert.ToString((int)Latitude) + "°";
            Latitude -= (int)Latitude;
            Latitude *= 60;
            cordinates += Convert.ToString((int)Latitude) + "'";
            Latitude -= (int)Latitude;
            Latitude *= 60;
            cordinates += Convert.ToString((int)Latitude) + ".";
            Latitude -= (int)Latitude;
            Latitude *= 1000;
            cordinates += Convert.ToString((int)Latitude) + "''";
            cordinates += "E";
            return cordinates;
        }
        public static string LongitudeDoubleToString(double longitude)
        {
            longitude = Math.Abs(longitude);
            string cordinates = Convert.ToString((int)longitude) + "°";
            longitude -= (int)longitude;
            longitude *= 60;
            cordinates += Convert.ToString((int)longitude) + "'";
            longitude -= (int)longitude;
            longitude *= 60;
            cordinates += Convert.ToString((int)longitude) + ".";
            longitude -= (int)longitude;
            longitude *= 1000;
            cordinates += Convert.ToString((int)longitude) + "''";
            cordinates += "s";
            return cordinates;
        }
    }
}
