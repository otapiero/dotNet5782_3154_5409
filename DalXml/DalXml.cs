using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;


namespace DAL
{
    public class DalXml: DalApi.IDal
    {
        #region singelton
        static readonly DalXml instance = new DalXml();

        #endregion
        #region DS XML Files

        private const string dronpath = @"Drone.xml"; //XElement
        private const string stationPath = @"Station.xml"; //XMLSerializer
        private const string parcelPath = @"Parcel.xml"; //XMLSerializer
        private const string costumerPath = @"Costumer.xml"; //XMLSerializer
        private const string DroneChargePath = @"DroneCarge.xml"; //XElement
        private const string configPath = @"config.xml"; //XMLSerializer
      


        #endregion

       public DO.Costumer SearchCostumer(int id)
        {
            var costumerList = XmlTools.LoadListFromXMLSerializer<DO.Costumer>(costumerPath);

            if (!costumerList.Exists(x => x.Id==id))
            {
                throw new DO.IdDoseNotExist("Id not found.", "costumer", id);
            }
            DO.Costumer find = costumerList.Find(x => x.Id.Equals(id));

            return find;
        }
        public DO.Drone SearchDrone(int id)
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
        public DO.Parcel SearchParcel(int id)
        {
            var parcelList = XmlTools.LoadListFromXMLSerializer<DO.Parcel>(parcelPath);

            if (!parcelList.Exists(x => x.Id.Equals(id)))
            {
                throw new DO.IdDoseNotExist("Id not found.", "parcel", id);
            }
            DO.Parcel found = parcelList.Find(x => x.Id.Equals(id));
            return found;
        }
        public DO.Station SearchStation(int id)
        {
            var StationsList = XmlTools.LoadListFromXMLSerializer<DO.Station>(stationPath);
            if (!StationsList.Exists(x => x.Id.Equals(id)))
            {
                throw new DO.IdDoseNotExist("Id not found.", "station", id);
            }
            DO.Station find = StationsList.Find(x => x.Id.Equals(id));
            return find;
        }
        public void AddNewDrone(int id, string _model)
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

        }
        public void AddNewStation(int id, string _name, double _Longitude, double _Lattitude, int _chargeSlots)
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
        public void AddNewCustomer(int _id, string _Name, string _Phone, double _Longitude, double _Lattitude)
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
            temp.Longitude = _Longitude;
            temp.Lattitude = _Lattitude;
            costumerList.Add(temp);
            XmlTools.SaveListToXMLSerializer(costumerList, costumerPath);
        }
        public void AddNewParcel(int _Sender, int _TargetId, int _Wheight, int _Priority)
        {
            var parcelList = XmlTools.LoadListFromXMLSerializer<DO.Parcel>(parcelPath);
            if (parcelList.Exists(x => x.Id.Equals(_Sender)))
            {
                throw new DO.IdDoseNotExist("Id of sender dose not found.", "costumer", _Sender);
            }
            if (!parcelList.Exists(x => x.Id.Equals(_TargetId)))
            {
                throw new DO.IdDoseNotExist("Id of target dose not found.", "costumer", _TargetId);
            }

            DO.Parcel temp = new();
            temp.Availble = true;
            temp.Id = DataSource.Config.idParcel++;
            temp.Sender = _Sender;
            temp.TargetId = _TargetId;
            temp.Wheight = (DO.WeightCategories)_Wheight;
            temp.Priority = (DO.Priorities)_Priority;
            temp.Requsted = DateTime.Now;
            temp.Scheduled = null;
            temp.PickedUp = null;
            temp.Delivered = null;
            parcelList.Add(temp);
        }
        public void ConnectParcelToDrone(int idParcel)
        {
            DO.Drone find = DataSource.drones.Find(x => x.Id > 0);
            DO.Drone temp = find;
            DO.Parcel pocket = SearchParcel(idParcel);
            DO.Parcel tempParcel = pocket;
            tempParcel.DroneId = find.Id;
            DataSource.parcels.Remove(pocket);
            DataSource.parcels.Add(tempParcel);


        }

        public void DeliveryParcelToCustomer(int idParcel)
        {
            try
            {
                var parcelList = XmlTools.LoadListFromXMLSerializer<DO.Parcel>(parcelPath);

                DO.Parcel pocket = SearchParcel(idParcel);
                //if the parcel exsist
                if (pocket.Id > 0)
                {
                    DO.Parcel tempParcel = pocket;
                    DO.Drone found = SearchDrone(pocket.Id);
                    DO.Drone tempDrone = found;
                    tempParcel.Delivered = DateTime.Now;
                    parcelList.Remove(pocket);
                    parcelList.Add(tempParcel);
                    parcelList.Remove(found);
                    parcelList.drones.Add(tempDrone);
                }
                XmlTools.SaveListToXMLSerializer(parcelList, parcelPath);

            }
            catch (DO.IdDoseNotExist x)
            {
                throw new DO.IdDoseNotExist(x.Message, x.ObjectType, x.Id);
            }
        }
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


        }
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
        }
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
        }
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
        }
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
        }
        public void UpdateDroneModel(int id, string model)
        {
            try
            {
                DO.Drone found = SearchDrone(id);
                DO.Drone temp = found;
                temp.Model = model;
                DataSource.drones.Remove(found);
                DataSource.drones.Add(temp);
            }
            catch (DO.IdDoseNotExist x)
            {

                throw new DO.IdDoseNotExist(x.Message, x.ObjectType, x.Id);
            }
        }
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
        }
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

        }
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
        }
        public IEnumerable<DO.Station> AllStation()
        {

            try
            {
                var StationsList = XmlTools.LoadListFromXMLSerializer<DO.Station>(stationPath);
                return StationsList;
            }
            catch
            {
                throw new Exception("er");
            }
           
        }
        public IEnumerable<DO.Drone> AllDrones()
        {


        }
        public IEnumerable<DO.Costumer> AllCustomers()
        {
            try
            {
                var costumerList = XmlTools.LoadListFromXMLSerializer<DO.Costumer>(costumerPath);
                return costumerList;
            }
            catch
            {
                throw new Exception("er");
            }
        }
        public IEnumerable<DO.Parcel> AllParcels()
        {
            var parcelList = XmlTools.LoadListFromXMLSerializer<DO.Parcel>(parcelPath);

            return parcelList.FindAll(item => item.Availble == true);

        }
        public IEnumerable<DO.DroneCharge> AllDronesIncharge()
        {
            try
            {
                var droneCargeList = XmlTools.LoadListFromXMLSerializer<DO.DroneCharge>(DroneChargePath);
                return droneCargeList;
            }
            catch
            {
                throw new Exception("er");
            }
        }
        public IEnumerable<DO.Parcel> ListOfParcels(Predicate<DO.Parcel> f)
        {
            var parcelList = XmlTools.LoadListFromXMLSerializer<DO.Parcel>(parcelPath);

            return parcelList.FindAll(x => (x.Availble == true)).FindAll(f);

        }
        public IEnumerable<DO.Costumer> ListOfCostumers(Predicate<DO.Costumer> f)
        {
            var costumerList = XmlTools.LoadListFromXMLSerializer<DO.Costumer>(costumerPath);

            return costumerList.FindAll(f);

        }
        public IEnumerable<DO.DroneCharge> ListOfDronesInCharge(Predicate<DO.DroneCharge> f)
        {
            var droneCargeList = XmlTools.LoadListFromXMLSerializer<DO.DroneCharge>(DroneChargePath);

            return droneCargeList.FindAll(f);

        }
        public IEnumerable<DO.Station> ListOfStations(Predicate<DO.Station> f)
        {
            var StationsList = XmlTools.LoadListFromXMLSerializer<DO.Station>(stationPath);

            return StationsList.FindAll(f);

        }
        public IEnumerable<DO.Drone> ListOfDrones(Predicate<DO.Drone> f)
        {
            return DataSource.drones.FindAll(f);

        }

        public double[] ElectricityUse()
        {


        }


    }
}
