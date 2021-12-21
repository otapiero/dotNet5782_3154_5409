using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{

    public partial class BL
    {
        /// <summary>
        /// list of all station
        /// </summary>
        /// <returns>return the lust of all the station </returns>
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
        /// <summary>
        /// list of all costumer
        /// </summary>
        /// <returns>return the lust of all the costumer </returns>
        public IEnumerable<BO.CustomerToList> ListCustomer()
        {
            List<BO.CustomerToList> allCustomer = new();
            var allParcels = idal.AllParcels();
            foreach (var x in idal.AllCustomers())
            {
                int delivred = allParcels.Count(y => (y.Sender==x.Id)&&y.Delivered!=null);
                int notDelivred = allParcels.Count(y => (y.Sender==x.Id)&&y.Delivered== null);
                int getedParcels = allParcels.Count(y => (y.TargetId==x.Id)&&y.Delivered!= null);
                int inTheWay = allParcels.Count(y => (y.TargetId==x.Id)&&y.Delivered== null);
                allCustomer.Add(new(x.Id, x.Name, x.Phone, delivred, notDelivred, getedParcels, inTheWay));
            }
            return allCustomer;
        }
        /// <summary>
        /// list of all drone
        /// </summary>
        /// <returns>return the lust of all the drone </returns>
        public IEnumerable<BO.DroneToList> ListDrones()
        {
            List<BO.DroneToList> allDrones = new();
            foreach (var x in DronesBl)
            {
                allDrones.Add(x);
            }
            return allDrones;
        }
        /// <summary>
        /// list of all parcel
        /// </summary>
        /// <returns>return the lust of all the parcel </returns>
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
                else if (x.PickedUp==null)
                    status=BO.ParcelStatus.Assigned;
                else if (x.Delivered==null)
                    status=BO.ParcelStatus.Colected;
                else
                    status=BO.ParcelStatus.Delivred;

                allParcels.Add(new(x.Id, sender.name, getter.name, (BO.WeightCategories)x.Wheight, (BO.Priorities)x.Priority, status));
            }

            return allParcels;
        }
        /// <summary>
        /// list of the not assined parcels
        /// </summary>
        /// <returns>list of  parcels</returns>
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
                    else if (x.PickedUp==null)
                        status=BO.ParcelStatus.Assigned;
                    else if (x.Delivered==null)
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

        public IEnumerable<BO.DroneToList> ListOfDrones(Predicate<BO.DroneToList> f)
        {
            return DronesBl.FindAll(f);
        }
        /// <summary>
        /// list of station with availebal charge post
        /// </summary>
        /// <returns>list of  station</returns>
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
       

    }
}
