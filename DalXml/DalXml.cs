using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Runtime.CompilerServices;

namespace DAL
{


    public class DalXml : DalApi.IDal
    {
        #region singelton
        static readonly DalXml instance = new DalXml();

        #endregion
        #region DS XML Files

        private const string dronpath = @"Drone.xml"; //XElement
        private const string stationPath = @"Station.xml"; //XMLSerializer
        private const string parcelPath = @"Parcel.xml"; //XMLSerializer
        private const string costumerPath = @"Customer.xml"; //XMLSerializer
        private const string DroneChargePath = @"DroneCharge.xml"; //XElement
        private const string configPath = @"config.xml"; //XMLSerializer

        #endregion
        DalXml()
        {
            try
            {

                var t = AllDronesIncharge();
                foreach (var x in t)
                {
                    ReleseDroneFromCharge(x.DroneId);
                }
            }
            catch (DO.IdDoseNotExist x)
            {
                throw new DO.IdDoseNotExist(x.Message, x.ObjectType, x.Id);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        public static DalApi.IDal Instance
        {
            get { return instance; }
        }
      

        #region search costumer
        [MethodImpl(MethodImplOptions.Synchronized)]
        ///<summary>method <c>SearchCustomer</c> </summary>
        ///<param name="id"> searche the customer by id</param>
      
        public DO.Costumer SearchCostumer(int id)
        {
            try
            {
                var costumerList = XmlTools.LoadListFromXMLSerializer<DO.Costumer>(costumerPath);


                if (!costumerList.Exists(x => x.Id == id))
                {
                    throw new DO.IdDoseNotExist("Id not found.", "costumer", id);
                }
                DO.Costumer find = costumerList.Find(x => x.Id.Equals(id));

                return find;
            }
            catch(DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        #endregion
        #region search drone
        [MethodImpl(MethodImplOptions.Synchronized)]
        /// <summary>method <c>SearchDrone</c> </summary>
        /// <param name="id"> searche the drone by id</param>
        /// <returns>the drone if exsist</returns>
        public DO.Drone SearchDrone(int id)
        {
            try
            {
                var droneRootElem = XmlTools.LoadListFromXMLElement(dronpath);
                var drone = (from d in droneRootElem.Elements()
                             where int.Parse(d.Element("Id").Value) == id
                             select new DO.Drone()
                             {
                                 Id = int.Parse(d.Element("Id").Value),
                                 Model = d.Element("Model").Value
                             }).FirstOrDefault();
                if (drone.Equals(new DO.Drone()))
                {
                    throw new DO.IdDoseNotExist("Id not found.", "costumer", id);
                }
                return drone;
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        #endregion
        #region search parcel
        [MethodImpl(MethodImplOptions.Synchronized)]
        /// <summary>method <c> SearchParcel</c> </summary>
        /// <param name="id"> searche the parcel by id</param>
        /// <returns>return the parcel if exsist</returns>
        public DO.Parcel SearchParcel(int id)
        {
            try
            {
                var parcelList = XmlTools.LoadListFromXMLSerializer<DO.Parcel>(parcelPath);
                var parcel = parcelList.FirstOrDefault(x => x.Id == id);

                if (parcel.Equals(new DO.Parcel()))
                {
                    throw new DO.IdDoseNotExist("Id not found.", "parcel", id);
                }

                return parcel;
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        #endregion
        #region search station
        [MethodImpl(MethodImplOptions.Synchronized)]
        ///<summary>method <c>SearchStation</c> </summary>
        ///<param name="id"> searche the Station by id</param>
        public DO.Station SearchStation(int id)
        {
            try
            {


                var StationsList = XmlTools.LoadListFromXMLSerializer<DO.Station>(stationPath);
                if (!StationsList.Exists(x => x.Id.Equals(id)))
                {
                    throw new DO.IdDoseNotExist("Id not found.", "station", id);
                }
                DO.Station find = StationsList.Find(x => x.Id.Equals(id));
                return find;
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        #endregion
        #region add drone
        [MethodImpl(MethodImplOptions.Synchronized)]
        /// <summary>method AddNewDrone </summary>
        /// <param name="_model"> model of drone</param>
        /// <param name="_MaxWheight"> enum of weight drone</param>
        /// <param name="_status"> enum status of drone</param>
        /// <param name=" _battery">life battery of drone</param>  
        public void AddNewDrone(int id, string _model)
        {
            try
            {


                var droneRootElem = XmlTools.LoadListFromXMLElement(dronpath);

                //make shore bus does not exist already
                var drones = (from x in droneRootElem.Elements()
                              where int.Parse(x.Element("Id").Value) == id
                              select x).FirstOrDefault();

                if (drones == null)
                {
                    var droneElem = new XElement("Drone",
                                  new XElement("Id", id),
                                  new XElement("Model", _model));

                    droneRootElem.Add(droneElem);
                }
                else throw new DO.IdAlredyExist("id already exist", "drone", id);

                XmlTools.SaveListToXMLElement(droneRootElem, dronpath);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }

        }
        #endregion
        #region add station
        [MethodImpl(MethodImplOptions.Synchronized)]
        /// <summary>method AddNewStation </summary>
        /// <param name="_name"> station name</param>
        /// <param name="_Longitude"> location of station</param>
        /// <param name="_Lattitude"> location of station</param>
        /// <param name=" _chargeSlots">status of charge</param>
        public void AddNewStation(int id, string _name, double _Longitude, double _Lattitude, int _chargeSlots)
        {
            try
            {
                var StationsList = XmlTools.LoadListFromXMLSerializer<DO.Station>(stationPath);
                if (StationsList.Exists(x => x.Id == id))
                {
                    throw new DO.IdAlredyExist("Id alredy use.", "station", id);
                }
                DO.Station temp = new();
                temp.Id = id;
                temp.Name = _name;
                temp.Longitude = _Longitude;
                temp.Lattitude = _Lattitude;
                temp.ChargeSlots = _chargeSlots;
                StationsList.Add(temp);
                XmlTools.SaveListToXMLSerializer(StationsList, stationPath);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        #endregion
        #region add customer
        [MethodImpl(MethodImplOptions.Synchronized)]
        /// <summary>method AddNewCustomer </summary>
        /// <param name="_id"> customer id</param>
        /// <param name=" _Name">customer name</param>
        /// <param name="_Phone"> customer phone number</param>
        /// <param name="_Longitude"> location of station</param>
        /// <param name="_Lattitude"> location of station</param>
        public void AddNewCustomer(int _id, string _Name, string _Phone, double _Longitude, double _Lattitude, string pass)
        {
            try
            {
                var costumerList = XmlTools.LoadListFromXMLSerializer<DO.Costumer>(costumerPath);
                if (costumerList.Exists(x => x.Id == _id))
                {
                    throw new DO.IdAlredyExist("Id alredy use.", "costumer", _id);
                }
                DO.Costumer temp = new();
                temp.Id = _id;
                temp.Name = _Name;
                temp.Phone = _Phone;
                temp.Password = pass;
                temp.Longitude = _Longitude;
                temp.Lattitude = _Lattitude;
                costumerList.Add(temp);
                XmlTools.SaveListToXMLSerializer(costumerList, costumerPath);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        #endregion
        #region add parcel
        [MethodImpl(MethodImplOptions.Synchronized)]
        /// <summary>method  AddNewParcel </summary>
        /// <param name="_Sender"> customer id of sender</param>
        /// <param name="_TargetId">customer id of target</param>
        /// <param name="_Wheight"> enum weight of parcel</param>
        /// <param name="_Priority"> enum priority of parcel</param>
        public void AddNewParcel(int _Sender, int _TargetId, int _Wheight, int _Priority)
        {
            try
            {

                var parcelList = XmlTools.LoadListFromXMLSerializer<DO.Parcel>(parcelPath);
                var configRootElem = XmlTools.LoadListFromXMLElement(configPath);
                

                DO.Parcel temp = new();
                temp.Availble = true;
                temp.Id = int.Parse(configRootElem.Element("idParcel").Value);
                configRootElem.Element("idParcel").Value = (int.Parse(configRootElem.Element("idParcel").Value) + 1).ToString();
                temp.Sender = _Sender;
                temp.TargetId = _TargetId;
                temp.Wheight = (DO.WeightCategories)_Wheight;
                temp.Priority = (DO.Priorities)_Priority;
                temp.Requsted = DateTime.Now;
                temp.Scheduled = null;
                temp.PickedUp = null;
                temp.Delivered = null;
                parcelList.Add(temp);
                XmlTools.SaveListToXMLElement(configRootElem, configPath);
                XmlTools.SaveListToXMLSerializer(parcelList, parcelPath);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        #endregion
        #region send to charge
        [MethodImpl(MethodImplOptions.Synchronized)]
        /// <summary>method DeliveryParcelToCustomer - the function get parcel and update dilevry time </summary>
        /// <param name="idParcel"> id parcel delivery</param>

        /// <summary>method SendDroneToCharge - the function get drone and station and coonect them </summary>
        /// <param name = "idDrone"> id drone to charge</param>
        /// <param name = "idStation"> id free station</param>
        public void SendDroneToCharge(int idDrone, int idStation)
        { 
            try
            {
                var StationsList = XmlTools.LoadListFromXMLSerializer<DO.Station>(stationPath);
                var droneCargeList= XmlTools.LoadListFromXMLSerializer<DO.DroneCharge>(DroneChargePath);
                DO.Station foundS = SearchStation(idStation);
                DO.Station tempS = foundS;
                tempS.ChargeSlots -= 1;
                StationsList.Remove(foundS);
                StationsList.Add(tempS);
                droneCargeList.Add(new DO.DroneCharge(idDrone, idStation));
                XmlTools.SaveListToXMLSerializer(StationsList, stationPath);
                XmlTools.SaveListToXMLSerializer(droneCargeList, DroneChargePath);
            }
            catch (DO.IdDoseNotExist x)
            {
                throw new DO.IdDoseNotExist(x.Message, x.ObjectType, x.Id);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }

        }
        #endregion
        #region relese from charge
        [MethodImpl(MethodImplOptions.Synchronized)]
        /// <summary>method ReleseDroneFromCharge - the function get drone relese it fron station </summary>
        /// <param name = "idDrone"> id drone to relese</param>
        public void ReleseDroneFromCharge(int id)
        {      
            try
            {
                var droneCargeList = XmlTools.LoadListFromXMLSerializer<DO.DroneCharge>(DroneChargePath);
                var StationsList = XmlTools.LoadListFromXMLSerializer<DO.Station>(stationPath);
                if (!droneCargeList.Exists(x => x.DroneId.Equals(id)))
                    throw new DO.IdDoseNotExist("Id not found.", "droneCharge ", id);
                DO.DroneCharge relese = droneCargeList.Find(x => x.DroneId.Equals(id));
                DO.Station foundS = SearchStation(relese.StationId);
                DO.Station tempS = foundS;
                tempS.ChargeSlots += 1;
                StationsList.Remove(foundS);
                StationsList.Add(tempS);
                droneCargeList.Remove(relese);
                XmlTools.SaveListToXMLSerializer(StationsList, stationPath);
                XmlTools.SaveListToXMLSerializer(droneCargeList, DroneChargePath);
            }
            catch (DO.IdDoseNotExist ex)
            {
                throw new DO.IdDoseNotExist(ex.Message, ex.ObjectType, ex.Id);

            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        #endregion
        #region assign parcel
        /// <summary>
        /// assing a parcel to a drone
        /// </summary>
        /// <param name="idParcel">id of parcel</param>
        /// <param name="idDrone">id of drone</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AssignPackageToDrone(int idParcel, int idDrone)
        {
            try
            {
                var parcelList = XmlTools.LoadListFromXMLSerializer<DO.Parcel>(parcelPath);

                DO.Parcel found = SearchParcel(idParcel);
                DO.Parcel temp = found;
                temp.DroneId = idDrone;
                temp.Scheduled = DateTime.Now;
                parcelList.Remove(found);
                parcelList.Add(temp);
                XmlTools.SaveListToXMLSerializer(parcelList, parcelPath);

            }
            catch (DO.IdDoseNotExist x)
            {
                throw new DO.IdDoseNotExist(x.Message, x.ObjectType, x.Id);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        #endregion
        #region collect parcel
        /// <summary>
        /// collect a parcel by drone
        /// </summary>
        /// <param name="idParcel">id of parcel</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void CollectPackage(int idParcel)
        {
            try
            {
                var parcelList = XmlTools.LoadListFromXMLSerializer<DO.Parcel>(parcelPath);

                DO.Parcel found = SearchParcel(idParcel);
                DO.Parcel temp = found;
                temp.PickedUp = DateTime.Now;
                parcelList.Remove(found);
                parcelList.Add(temp);
                XmlTools.SaveListToXMLSerializer(parcelList, parcelPath);

            }
            catch (DO.IdDoseNotExist x)
            {
                throw new DO.IdDoseNotExist(x.Message, x.ObjectType, x.Id);

            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        #endregion
        #region deliver parcel
        /// <summary>
        /// deliver a parcel by drone
        /// </summary>
        /// <param name="idParcel">id of parcel</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeliverPackage(int idParcel)
        {
            try
            {
                var parcelList = XmlTools.LoadListFromXMLSerializer<DO.Parcel>(parcelPath);

                DO.Parcel found = SearchParcel(idParcel);
                DO.Parcel temp = found;
                temp.Delivered = DateTime.Now;
                parcelList.Remove(found);
                parcelList.Add(temp);
                XmlTools.SaveListToXMLSerializer(parcelList, parcelPath);

            }
            catch (DO.IdDoseNotExist x)
            {
                throw new DO.IdDoseNotExist(x.Message, x.ObjectType, x.Id);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        #endregion
        #region update drone
        [MethodImpl(MethodImplOptions.Synchronized)]
        /// <summary>
        /// update drone model
        /// </summary>
        /// <param name="id"> id of drone</param>
        /// <param name="model">new model of drone</param>
        public void UpdateDroneModel(int id, string model)
        {
            try
            {
                var droneRootElem = XmlTools.LoadListFromXMLElement(dronpath);
                //make shore bus does not exist already
                var drone = (from x in droneRootElem.Elements()
                          where int.Parse(x.Element("Id").Value) == id
                          select x).FirstOrDefault();

                if(drone!= null)
                {
                    drone.Element("Model").Value = model;
                    XmlTools.SaveListToXMLElement(droneRootElem, dronpath);
                }
                else
                    throw new DO.IdDoseNotExist("Id not found.", "drone", id);
            }
            catch (DO.IdDoseNotExist x)
            {
                throw new DO.IdDoseNotExist(x.Message, x.ObjectType, x.Id);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        #endregion
        #region update station
        /// <summary>
        /// update station
        /// </summary>
        /// <param name="id">station id</param>
        /// <param name="name">new stattion name</param>
        /// <param name="chargeSlots">new num of charge slots in station</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(int id, string name, int chargeSlots)
        {
            try
            {
                var StationsList = XmlTools.LoadListFromXMLSerializer<DO.Station>(stationPath);

                DO.Station found = SearchStation(id);
                DO.Station temp = found;
                name = name.Length > 0 ? name : found.Name;
                chargeSlots = chargeSlots.ToString().Length > 0 ? chargeSlots : found.ChargeSlots;
                temp.Name = name;
                temp.ChargeSlots = chargeSlots;
                StationsList.Remove(found);
                StationsList.Add(temp);
                XmlTools.SaveListToXMLSerializer(StationsList, stationPath);

            }
            catch (DO.IdDoseNotExist x)
            {
                throw new DO.IdDoseNotExist(x.Message, x.ObjectType, x.Id);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        #endregion
        #region update costumer
        /// <summary>
        /// update customer
        /// </summary>
        /// <param name="id">id cusomers</param>
        /// <param name="name">new name of customers</param>
        /// <param name="phone">new phone of customers</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCostumer(int id, string name, string phone)
        {
            try
            {
                var costumerList = XmlTools.LoadListFromXMLSerializer<DO.Costumer>(costumerPath);
                DO.Costumer found = SearchCostumer(id);
                DO.Costumer temp = found;
                name = name.Length > 0 ? name : found.Name;
                phone = (phone.Length > 7) ? phone : found.Name;
                temp.Name = name;
                temp.Phone = phone;
                costumerList.Remove(found);
                costumerList.Add(temp);
                XmlTools.SaveListToXMLSerializer(costumerList, costumerPath);

            }
            catch (DO.IdDoseNotExist x)
            {
                throw new DO.IdDoseNotExist(x.Message, x.ObjectType, x.Id);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }

        }
        #endregion
        #region delete parcel
        /// <summary>
        /// delete a parcel
        /// </summary>
        /// <param name="idParcel">id of parcel</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        
        public void DeleteParcel(int idParcel)
        {
            
            try
            {
                var parcelList = XmlTools.LoadListFromXMLSerializer<DO.Parcel>(parcelPath);
                var parcel = SearchParcel(idParcel);


                var temp = parcel;
                temp.Availble = false;
                parcelList.Remove(parcel);
                parcelList.Add(temp);
                XmlTools.SaveListToXMLSerializer(parcelList, parcelPath);

            }
            catch (DO.IdDoseNotExist x)
            {
                throw new DO.IdDoseNotExist(x.Message, x.ObjectType, x.Id);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        #endregion
        #region stations
        ///<summary>List - copy list of station for the main program</summary>
        ///<returns>list of all stations</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Station> AllStation()
        {

            try
            {
                var StationsList = XmlTools.LoadListFromXMLSerializer<DO.Station>(stationPath);
                return StationsList;
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }

        }
        #endregion
        #region drones
        ///<summary>List - copy list of drones for the main program</summary>
        ///<returns>list of all drones</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Drone> AllDrones()
        {
            try
            {
                var dronesRootElem = XmlTools.LoadListFromXMLElement(dronpath);

                var drones = from x in dronesRootElem.Elements()
                             select new DO.Drone()
                             {
                                 Id = int.Parse(x.Element("Id").Value),
                                 Model = x.Element("Model").Value
                             };

                return drones;
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        #endregion
        #region customers
        ///<summary>List - copy list of customers for the main program</summary>
        ///<returns>list of all costomers</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Costumer> AllCustomers()
        {
            try
            {
                var costumerList = XmlTools.LoadListFromXMLSerializer<DO.Costumer>(costumerPath);
                return costumerList;
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        #endregion
        #region parcels
        ///<summary>List - copy list of parcels for the main program</summary>
        ///<returns>list of all parcels</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Parcel> AllParcels()
        {
            var parcelList = XmlTools.LoadListFromXMLSerializer<DO.Parcel>(parcelPath);

            return parcelList.FindAll(item => item.Availble == true);

        }
        #endregion
        #region drones in charge
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.DroneCharge> AllDronesIncharge()
        {
            try
            {
                var droneCargeList = XmlTools.LoadListFromXMLSerializer<DO.DroneCharge>(DroneChargePath);
                return droneCargeList;
            }
            catch(DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        #endregion
        #region part of parcels
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Parcel> ListOfParcels(Predicate<DO.Parcel> f)
        {
            try
            {

                var parcelList = XmlTools.LoadListFromXMLSerializer<DO.Parcel>(parcelPath);

                var temp = parcelList.FindAll(f).FindAll(x => x.Availble == true);
                return temp;
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        #endregion
        #region part of customers
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Costumer> ListOfCostumers(Predicate<DO.Costumer> f)
        {
            try
            {
                var costumerList = XmlTools.LoadListFromXMLSerializer<DO.Costumer>(costumerPath);

                return costumerList.FindAll(f);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        #endregion
        #region part of dronecharge
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.DroneCharge> ListOfDronesInCharge(Predicate<DO.DroneCharge> f)
        {
            try
            {
                var droneCargeList = XmlTools.LoadListFromXMLSerializer<DO.DroneCharge>(DroneChargePath);

                return droneCargeList.FindAll(f);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }

        }
        #endregion
        #region part of stations
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Station> ListOfStations(Predicate<DO.Station> f)
        {
            try
            {

                var StationsList = XmlTools.LoadListFromXMLSerializer<DO.Station>(stationPath);

                return StationsList.FindAll(f);

            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        #endregion
        #region part of Drones
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Drone> ListOfDrones(Predicate<DO.Drone> f)
        {
            try
            {


                var dronesRootElem = XmlTools.LoadListFromXMLElement(dronpath);

                var drones = (from x in dronesRootElem.Elements()
                              select new DO.Drone()
                              {
                                  Id = int.Parse(x.Element("Id").Value),
                                  Model = x.Element("Model").Value
                              }).ToList();

                return (drones as List<DO.Drone>).FindAll(f);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        #endregion
        [MethodImpl(MethodImplOptions.Synchronized)]
        public double[] ElectricityUse()
        {
            try
            {
                var configRootElem = XmlTools.LoadListFromXMLElement(configPath);

                double[] arr = new double[5];
                arr[0] = double.Parse(configRootElem.Element("Avilable").Value);
                arr[1] = double.Parse(configRootElem.Element("Light").Value);
                arr[2] = double.Parse(configRootElem.Element("Intermidiate").Value);
                arr[3] = double.Parse(configRootElem.Element("Heavy").Value);
                arr[4] = double.Parse(configRootElem.Element("chargingRatePerHoure").Value);
                return arr;

            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new DO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        ~DalXml()
        {
            
        }

    }
}
