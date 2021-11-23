using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


 namespace IDAL
{
   public interface IDal
    {
        IDAL.DO.Costumer SearchCostumer(int id);
        IDAL.DO.Drone SearchDrone(int id);
        IDAL.DO.Parcel SearchParcel(int id);
        IDAL.DO.Station SearchStation(int id);
        void AddNewDrone(int id,string _model);
        void AddNewStation(int id,string _name, double _Longitude, double _Lattitude, int _chargeSlots);
        void AddNewCustomer(int _id, string _Name, string _Phone, double _Longitude, double _Lattitude);
        void AddNewParcel(int _Sender, int _TargetId, int _Wheight, int _Priority);
        void ConnectParcelToDrone(int idParcel);
        void ParceCollectionByDrone(int idParcel);
        void DeliveryParcelToCustomer(int idParcel);
        void SendDroneToCharge(int idDrone, int idStation);
        void ReleseDroneFromCharge(int idDrone);
        void AssignPackageToDrone(int idParcel,int idDrone);
        void CollectPackage(int idParcel);
        void DeliverPackage(int idParcel);
        void UpdateDroneModel(int id, string model);
        void UpdateStation(int id, string name,int chargeSlots);
        void UpdateCostumer(int id, string name,string phone);
        IEnumerable<IDAL.DO.Station> AllStation();
        IEnumerable<IDAL.DO.Drone> AllDrones();
        IEnumerable<IDAL.DO.Costumer> AllCustomers();
        IEnumerable<IDAL.DO.Parcel> AllParcels();
        public IEnumerable<IDAL.DO.DroneCharge> AllDronesIncharge();
        IEnumerable<IDAL.DO.Parcel> NotAssociatedParcels();
        IEnumerable<IDAL.DO.Station> StationWithAvailebalChargePost();

        double[] ElectricityUse();
    }
}
