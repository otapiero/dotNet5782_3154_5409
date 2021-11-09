using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


 namespace IDAL
{
   public interface IDal
    {
        IDAL.DO.Customer SearchCustomer(int id);
        IDAL.DO.Drone SearchDrone(int id);
        IDAL.DO.Parcel SearchParcel(int id);
        IDAL.DO.Station SearchStation(int id);
        void AddNewDrone(string _model, int _MaxWheight);
        void AddNewStation(string _name, double _Longitude, double _Lattitude, int _chargeSlots);
        void AddNewCustomer(int _id, string _Name, string _Phone, double _Longitude, double _Lattitude);
        void AddNewParcel(int _Sender, int _TargetId, int _Wheight, int _Priority);
        void ConnectParcelToDrone(int idParcel);
        void ParceCollectionByDrone(int idParcel);
        void DeliveryParcelToCustomer(int idParcel);
        void SendDroneToCharge(int idDrone, int idStation);
        void ReleseDroneFromCharge(int idDrone);
        IEnumerable<IDAL.DO.Station> AllStation();
        IEnumerable<IDAL.DO.Drone> AllDrones();
        IEnumerable<IDAL.DO.Customer> AllCustomers();
        IEnumerable<IDAL.DO.Parcel> AllParcels();
        IEnumerable<IDAL.DO.Parcel> NotAssociatedParcels();
        IEnumerable<IDAL.DO.Station> StationWithAvailebalChargePost();

        double[] ElectricityUse();
    }
}
