using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IBL
    {
        void AddNewStation(int id, string name, double longattitude, double lattitude, int numChargeSlot);
        void AddNewDrone(int id, string model, int wheigt, int stationId);
        void AddNewCustomer(int id, string name, string phone, double longattitude, double lattitude);
        void AddNewParcel(int senderId, int targetId, int wheigt, int Priority);
        BO.BaseStation SearchStation(int id);
        BO.CustomerBl SearchCostumer(int id);
        BO.DroneBL SearchDrone(int id);
        BO.ParcelBl SearchParcel(int id);
        void UpdateDroneModel(int id, string model);
        void UpdateStation(int id, string name, int chargeSlots);
        void UpdateCostumer(int id, string name, string phone);
        void SendDroneToCharge(int id);

        void CollectPackage(int id);
        void AssignPackageToDrone(int id);
        void RelesaeDroneFromCharge(int id, double time);
        void DeliverPackage(int id);
        IEnumerable<BO.BaseStationToList> ListStation();
        IEnumerable<BO.CustomerToList> ListCustomer();
        IEnumerable<BO.DroneToList> ListDrones();
        IEnumerable<BO.ParcelToList> ListParcels();
        IEnumerable<BO.ParcelToList> ListParcelsNotAssigned();
        IEnumerable<BO.BaseStationToList> StationWithAvailebalChargePost();

        public IEnumerable<BO.DroneToList> ListOfDrones(Predicate<BO.DroneToList> f);

    }

}
