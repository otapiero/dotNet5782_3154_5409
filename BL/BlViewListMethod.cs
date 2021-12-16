using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{

    public partial class BL
    {
        public IEnumerable<BO.BaseStationToList> ListStation()
        {

            List<BO.BaseStationToList> allStation = new();
            foreach (IDAL.DO.Station x in idal.AllStation())
            {
                int numOfDronesInCharge = idal.AllDronesIncharge().Count(w => w.StationId==x.Id);
                allStation.Add(new(x.Id, x.Name, x.ChargeSlots, numOfDronesInCharge));
            }
            return allStation;
        }
        public IEnumerable<BO.CustomerToList> ListCustomer()
        {
            List<BO.CustomerToList> allCustomer = new();
            var allParcels = idal.AllParcels();
            foreach (var x in idal.AllCustomers())
            {
                int delivred = allParcels.Count(y => (y.Sender==x.Id)&&y.Delivered!= new DateTime());
                int notDelivred = allParcels.Count(y => (y.Sender==x.Id)&&y.Delivered== new DateTime());
                int getedParcels = allParcels.Count(y => (y.TargetId==x.Id)&&y.Delivered!= new DateTime());
                int inTheWay = allParcels.Count(y => (y.TargetId==x.Id)&&y.Delivered== new DateTime());
                allCustomer.Add(new(x.Id, x.Name, x.Phone, delivred, notDelivred, getedParcels, inTheWay));
            }
            return allCustomer;
        }
        public IEnumerable<BO.DroneToList> ListDrones()
        {
            List<BO.DroneToList> allDrones = new();
            foreach (var x in DronesBl)
            {
                allDrones.Add(x);
            }
            return allDrones;
        }
        public IEnumerable<BO.ParcelToList> ListParcels()
        {
            List<BO.ParcelToList> allParcels = new();

            foreach (var x in idal.AllParcels())
            {
                var sender = SearchCostumer(x.Sender);
                var getter = SearchCostumer(x.TargetId);
                BO.ParcelStatus status;
                if (x.DroneId==0)
                    status=BO.ParcelStatus.Defined;
                else if (x.PickedUp==new DateTime())
                    status=BO.ParcelStatus.Assigned;
                else if (x.Delivered==new DateTime())
                    status=BO.ParcelStatus.Colected;
                else
                    status=BO.ParcelStatus.Delivred;

                allParcels.Add(new(x.Id, sender.name, getter.name, (BO.WeightCategories)x.Wheight, (BO.Priorities)x.Priority, status));
            }

            return allParcels;
        }
        public IEnumerable<BO.ParcelToList> ListParcelsNotAssigned()
        {
            List<BO.ParcelToList> ParcelsNotAssociated = new();
            foreach (var x in idal.ListOfParcels(t=> t.DroneId == 0))
            {
                ; try
                {
                    var sender = SearchCostumer(x.Sender);
                    var getter = SearchCostumer(x.TargetId);


                    BO.ParcelStatus status;
                    if (x.DroneId==0)
                        status=BO.ParcelStatus.Defined;
                    else if (x.PickedUp==new DateTime())
                        status=BO.ParcelStatus.Assigned;
                    else if (x.Delivered==new DateTime())
                        status=BO.ParcelStatus.Colected;
                    else
                        status=BO.ParcelStatus.Delivred;

                    ParcelsNotAssociated.Add(new(x.Id, sender.name, getter.name, (BO.WeightCategories)x.Wheight, (BO.Priorities)x.Priority, status));
                }
                catch
                { Console.WriteLine("error"); }

            }

            return ParcelsNotAssociated;
        }

        public IEnumerable<BO.BaseStationToList> StationWithAvailebalChargePost()
        {
            List<BO.BaseStationToList> stationWithAvailebalChargePost = new();
            foreach (IDAL.DO.Station x in idal.ListOfStations(t=>t.ChargeSlots > 0))
            {
                int numOfDronesInCharge = idal.AllDronesIncharge().Count(w => w.StationId==x.Id);
                stationWithAvailebalChargePost.Add(new(x.Id, x.Name, x.ChargeSlots, numOfDronesInCharge));
            }
            return stationWithAvailebalChargePost;
        }

        public IEnumerable<BO.DroneToList> FilterListDrones2(BO.DroneStatuses status, BO.WeightCategories weight)
        {
            List<BO.DroneToList> allDrones = new();

            foreach (var x in DronesBl)
            {
                if (x.status == status && x.Weight == weight)
                {
                    allDrones.Add(x);
                }
            }
            return allDrones;
        }
        public IEnumerable<BO.DroneToList> FilterListDrones1(BO.WeightCategories weight)
        {
            List<BO.DroneToList> allDrones = new();

            foreach (var x in DronesBl)
            {
                if (x.Weight == weight)
                {
                    allDrones.Add(x);
                }
            }
            return allDrones;
        }
        public IEnumerable<BO.DroneToList> FilterListDrones(BO.DroneStatuses status)
        {
            List<BO.DroneToList> allDrones = new();

            foreach (var x in DronesBl)
            {
                if (x.status == status)
                {
                    allDrones.Add(x);
                }
            }
            return allDrones;
        }

    }
}
