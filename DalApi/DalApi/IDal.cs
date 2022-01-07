using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DalApi
{
    public interface IDal
    {
        DO.Costumer SearchCostumer(int id);
        DO.Drone SearchDrone(int id);
        DO.Parcel SearchParcel(int id);
        DO.Station SearchStation(int id);
        void AddNewDrone(int id, string _model);
        void AddNewStation(int id, string _name, double _Longitude, double _Lattitude, int _chargeSlots);
        void AddNewCustomer(int _id, string _Name, string _Phone, double _Longitude, double _Lattitude);
        void AddNewCustomer(int _id, string _Name, string _Phone, double _Longitude, double _Lattitude, string pass);
        void AddNewParcel(int _Sender, int _TargetId, int _Wheight, int _Priority);
        void ConnectParcelToDrone(int idParcel);
        void ParceCollectionByDrone(int idParcel);
        void DeliveryParcelToCustomer(int idParcel);
        void SendDroneToCharge(int idDrone, int idStation);
        void ReleseDroneFromCharge(int idDrone);
        void AssignPackageToDrone(int idParcel, int idDrone);
        void CollectPackage(int idParcel);
        void DeliverPackage(int idParcel);
        void UpdateDroneModel(int id, string model);
        void UpdateStation(int id, string name, int chargeSlots);
        void UpdateCostumer(int id, string name, string phone);
        public void DeleteParcel(int idParcel);
        IEnumerable<DO.Station> AllStation();
        IEnumerable<DO.Drone> AllDrones();
        IEnumerable<DO.Costumer> AllCustomers();
        IEnumerable<DO.Parcel> AllParcels();
        public IEnumerable<DO.DroneCharge> AllDronesIncharge();



        public IEnumerable<DO.Parcel> ListOfParcels(Predicate<DO.Parcel> f);
        public IEnumerable<DO.Costumer> ListOfCostumers(Predicate<DO.Costumer> f);
        public IEnumerable<DO.DroneCharge> ListOfDronesInCharge(Predicate<DO.DroneCharge> f);
        public IEnumerable<DO.Station> ListOfStations(Predicate<DO.Station> f);
        public IEnumerable<DO.Drone> ListOfDrones(Predicate<DO.Drone> f);




        double[] ElectricityUse();
    }
}
