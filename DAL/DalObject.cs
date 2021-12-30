
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    ///<summary>Class <c>DalObject</c></summary>
    internal sealed class DalObject : DalApi.IDal
    {
        #region singelton
        static readonly DalObject instance=new DalObject();

        #endregion
        ///<summary>method <c>DalObject</c> initialize the data</summary>
        static DalObject()
        {
            DataSource.Initialize();
        }
        DalObject() { }
        public static DalApi.IDal Instance { get { return instance; } }
        ///<summary>method <c>SearchCustomer</c> </summary>
        ///<param name="id"> searche the customer by id</param>
        public DO.Costumer SearchCostumer(int id)
        {
            if (!DataSource.customers.Exists(x => x.Id.Equals(id)))
            {
                throw new DO.IdExaption("Id not found.");
            }
            DO.Costumer find = DataSource.customers.Find(x => x.Id.Equals(id));
            return find;
        }

        /// <summary>method <c>SearchDrone</c> </summary>
        /// <param name="id"> searche the drone by id</param>
        /// <returns>the drone if exsist</returns>
        public DO.Drone SearchDrone(int id)
        {
            if (!DataSource.drones.Exists(x => x.Id.Equals(id)))
            {
                throw new DO.IdExaption("Id not found.");
            }
            DO.Drone found = DataSource.drones.Find(x => x.Id.Equals(id));
            return found;
        }
        ///<summary>method <c>SearchStation</c> </summary>
        ///<param name="id"> searche the Station by id</param>
        public DO.Station SearchStation(int id)
        {

            if (!DataSource.stations.Exists(x => x.Id.Equals(id)))
            {
                throw new DO.IdExaption("Id not found.");
            }
            DO.Station find = DataSource.stations.Find(x => x.Id.Equals(id));
            return find;

        }
        /// <summary>method <c> SearchParcel</c> </summary>
        /// <param name="id"> searche the parcel by id</param>
        /// <returns>return the parcel if exsist</returns>
        public DO.Parcel SearchParcel(int id)
        {
            if (!DataSource.parcels.Exists(x => x.Id.Equals(id)))
            {
                throw new DO.IdExaption("Id not found.");
            }
            DO.Parcel found = DataSource.parcels.Find(x => x.Id.Equals(id));
            return found;

        }
        /// <summary>method AddNewDrone </summary>
        /// <param name="_model"> model of drone</param>
        /// <param name="_MaxWheight"> enum of weight drone</param>
        /// <param name="_status"> enum status of drone</param>
        /// <param name=" _battery">life battery of drone</param>
        ///         
        public void AddNewDrone(int id, string _model)
        {
            if (DataSource.drones.Exists(x => x.Id.Equals(id)))
            {
                throw new DO.IdExaption("Drone Id alredy use.");
            }
            DO.Drone temp = new();
            temp.Id = id;
            temp.Model = _model;
            DataSource.drones.Add(temp);
        }
        /// <summary>method AddNewStation </summary>
        /// <param name="_name"> station name</param>
        /// <param name="_Longitude"> location of station</param>
        /// <param name="_Lattitude"> location of station</param>
        /// <param name=" _chargeSlots">status of charge</param>
        public void AddNewStation(int id, string _name, double _Longitude, double _Lattitude, int _chargeSlots)
        {
            if (DataSource.stations.Exists(x => x.Id == id))
            {
                throw new DO.IdExaption("Id alredy use.");
            }
            DO.Station temp = new();
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
                throw new DO.IdExaption("Id alredy use.");
            }
            DO.Costumer temp = new();
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
            if (!DataSource.customers.Exists(x => x.Id.Equals(_Sender)))
            {
                throw new DO.IdExaption("Id of sender dose not found.");
            }
            if (!DataSource.customers.Exists(x => x.Id.Equals(_TargetId)))
            {
                throw new DO.IdExaption("Id of target dose not found.");
            }

            DO.Parcel temp = new();
            temp.Id = DataSource.Config.idParcel++;
            temp.Sender = _Sender;
            temp.TargetId = _TargetId;
            temp.Wheight = (DO.WeightCategories)_Wheight;
            temp.Priority = (DO.Priorities)_Priority;
            temp.Requsted = DateTime.Now;
            temp.Scheduled = null;
            temp.PickedUp = null;
            temp.Delivered = null;
            DataSource.parcels.Add(temp);
        }
        /// <summary>method ConnectParcelToDrone - the function get parcel and connect a avilable drone </summary>
        /// <param name="idParcel"> id parcel to delivery</param>
        public void ConnectParcelToDrone(int idParcel)
        {
            try
            {
                DO.Drone find = DataSource.drones.Find(x => x.Id > 0);
                DO.Drone temp = find;
                DataSource.drones.Remove(find);
                DataSource.drones.Add(temp);
                DO.Parcel pocket = SearchParcel(idParcel);
                DO.Parcel tempParcel = pocket;
                tempParcel.DroneId = find.Id;
                DataSource.parcels.Remove(pocket);
                DataSource.parcels.Add(tempParcel);
            }
            catch
            { }



        }
        /// <summary>method ParceCollectionByDrone - the function get parcel and update time picked up </summary>
        /// <param name="idParcel"> id parcel pickedup</param>
        public void ParceCollectionByDrone(int idParcel)
        {
            DO.Parcel pocket = SearchParcel(idParcel);
            //if the parcel exsist
            if (pocket.Id > 0)
            {
                //update picked up time on the parcel lists
                DO.Parcel tempPocket = pocket;
                tempPocket.PickedUp = DateTime.Now;
                DataSource.parcels.Remove(pocket);
                DataSource.parcels.Add(tempPocket);
            }

        }
        /// <summary>method DeliveryParcelToCustomer - the function get parcel and update dilevry time </summary>
        /// <param name="idParcel"> id parcel delivery</param>
        public void DeliveryParcelToCustomer(int idParcel)
        {
            DO.Parcel pocket = SearchParcel(idParcel);
            //if the parcel exsist
            if (pocket.Id > 0)
            {
                DO.Parcel tempParcel = pocket;
                DO.Drone found = SearchDrone(pocket.Id);
                DO.Drone tempDrone = found;
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
            try
            {

                DO.Station foundS = SearchStation(idStation);
                DO.Station tempS = foundS;
                tempS.ChargeSlots -= 1;
                DataSource.stations.Remove(foundS);
                DataSource.stations.Add(tempS);
                DataSource.DroneCharges.Add(new DO.DroneCharge(idDrone, idStation));

            }
            catch { }
        }
        /// <summary>method ReleseDroneFromCharge - the function get drone relese it fron station </summary>
        /// <param name = "idDrone"> id drone to relese</param>
        public void ReleseDroneFromCharge(int id)
        {
            try
            {

                DO.DroneCharge relese = DataSource.DroneCharges.Find(x => x.DroneId.Equals(id));
                DO.Station foundS = SearchStation(relese.StationId);
                DO.Station tempS = foundS;
                tempS.ChargeSlots += 1;
                DataSource.stations.Remove(foundS);
                DataSource.stations.Add(tempS);
                DataSource.DroneCharges.Remove(relese);
            }
            catch { }
        }

        public void UpdateDroneModel(int id, string model)
        {
            try
            {
                DO.Drone found = DataSource.drones.First(w => w.Id == id);
                DO.Drone temp = found;
                temp.Model = model;
                DataSource.drones.Remove(found);
                DataSource.drones.Add(temp);
            }
            catch (Exception)
            {

                throw new DO.IdExaption("Id not found."); ;
            }

        }
        public void AssignPackageToDrone(int idParcel, int idDrone)
        {
            DO.Parcel found = DataSource.parcels.Find(x => x.Id == idParcel);
            DO.Parcel temp = found;
            temp.DroneId = idDrone;
            temp.Scheduled = DateTime.Now;
            DataSource.parcels.Remove(found);
            DataSource.parcels.Add(temp);
        }
        public void CollectPackage(int idParcel)
        {
            DO.Parcel found = DataSource.parcels.Find(x => x.Id == idParcel);
            DO.Parcel temp = found;
            temp.PickedUp = DateTime.Now;
            DataSource.parcels.Remove(found);
            DataSource.parcels.Add(temp);
        }
        public void DeliverPackage(int idParcel)
        {
            DO.Parcel found = DataSource.parcels.Find(x => x.Id == idParcel);
            DO.Parcel temp = found;
            temp.Delivered = DateTime.Now;
            DataSource.parcels.Remove(found);
            DataSource.parcels.Add(temp);
        }
        public void UpdateStation(int id, string name, int chargeSlots)
        {
            DO.Station found = DataSource.stations.First(w => w.Id == id);
            DO.Station temp = found;
            name = name.Length > 0 ? name : found.Name;
            chargeSlots = chargeSlots.ToString().Length > 0 ? chargeSlots : found.ChargeSlots;
            temp.Name = name;
            temp.ChargeSlots = chargeSlots;
            DataSource.stations.Remove(found);
            DataSource.stations.Add(temp);
        }
        public void UpdateCostumer(int id, string name, string phone)
        {
            DO.Costumer found = DataSource.customers.First(w => w.Id == id);
            DO.Costumer temp = found;
            name = name.Length > 0 ? name : found.Name;
            phone = phone.Length > 0 ? phone : found.Name;
            temp.Name = name;
            temp.Phone = phone;
            DataSource.customers.Remove(found);
            DataSource.customers.Add(temp);
        }



        public IEnumerable<DO.Station> ListOfStations(Predicate<DO.Station> f)
        {
            return DataSource.stations.FindAll(f);
        }
        public IEnumerable<DO.Drone> ListOfDrones(Predicate<DO.Drone> f)
        {
            return DataSource.drones.FindAll(f);
        }
        public IEnumerable<DO.Costumer> ListOfCostumers(Predicate<DO.Costumer> f)
        {
            return DataSource.customers.FindAll(f);
        }
        public IEnumerable<DO.Parcel> ListOfParcels(Predicate<DO.Parcel> f)
        {
            return DataSource.parcels.FindAll(f);
        }
        public IEnumerable<DO.DroneCharge> ListOfDronesInCharge(Predicate<DO.DroneCharge> f)
        {
            return DataSource.DroneCharges.FindAll(f);
        }
        ///<summary>List - copy list of station for the main program</summary>
        ///<returns>list of all stations</returns>
        public IEnumerable<DO.Station> AllStation()
        {
            List<DO.Station> allStations = new List<DO.Station>();
            foreach (var t in DataSource.stations)
            {
                allStations.Add(t);
            }
            return allStations;
        }
        ///<summary>List - copy list of drones for the main program</summary>
        ///<returns>list of all drones</returns>
        public IEnumerable<DO.Drone> AllDrones()
        {
            List<DO.Drone> allDrones = new List<DO.Drone>();
            foreach (var t in DataSource.drones)
            {
                allDrones.Add(t);
            }
            return allDrones;
        }
        ///<summary>List - copy list of customers for the main program</summary>
        ///<returns>list of all costomers</returns>
        public IEnumerable<DO.Costumer> AllCustomers()
        {
            List<DO.Costumer> allCustomers = new List<DO.Costumer>();
            foreach (var t in DataSource.customers)
            {
                allCustomers.Add(t);
            }
            return allCustomers;
        }
        ///<summary>List - copy list of parcels for the main program</summary>
        ///<returns>list of all parcels</returns>
        public IEnumerable<DO.Parcel> AllParcels()
        {
            List<DO.Parcel> allParcels = new();
            foreach (var t in DataSource.parcels)
            {
                allParcels.Add(t);
            }
            return allParcels;
        }
        public IEnumerable<DO.DroneCharge> AllDronesIncharge()
        {
            List<DO.DroneCharge> allDronesIncharge = new();
            foreach (var t in DataSource.DroneCharges)
            {
                allDronesIncharge.Add(t);
            }
            return allDronesIncharge;
        }




        public double[] ElectricityUse()
        {
            double[] arr = new double[5];
            arr[0] = DataSource.Config.Avilable;
            arr[1] = DataSource.Config.Light;
            arr[2] = DataSource.Config.Intermidiate;
            arr[3] = DataSource.Config.Heavy;
            arr[4] = DataSource.Config.chargingRatePerHoure;

            return arr;
        }
    }
}
