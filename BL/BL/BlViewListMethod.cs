using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{

    partial class BL
    {
        /// <summary>
        /// list of all station
        /// </summary>
        /// <returns>return the lust of all the station </returns>
        public IEnumerable<BO.BaseStationToList> ListStation()
        {
            try
            {


                List<BO.BaseStationToList> allStation = new();
                foreach (DO.Station x in idal.AllStation())
                {
                    int numOfDronesInCharge = idal.AllDronesIncharge().Count(w => w.StationId==x.Id);
                    allStation.Add(new(x.Id, x.Name, x.ChargeSlots, numOfDronesInCharge));
                }
                return allStation;
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.XmlFilePath, ex.Message, ex.InnerException);
            }
        }
        /// <summary>
        /// list of all costumer
        /// </summary>
        /// <returns>return the lust of all the costumer </returns>
        public IEnumerable<BO.CustomerToList> ListCustomer()
        {
            try
            {

                List<BO.CustomerToList> allCustomer = new();
                var allParcels = idal.AllParcels();

                allCustomer=(from x in idal.AllCustomers()
                             select (new BO.CustomerToList(x.Id, x.Name, x.Phone, allParcels.Count(y => (y.Sender==x.Id)&&y.Delivered!=null), allParcels.Count(y => (y.Sender==x.Id)&&y.Delivered== null)
                     , allParcels.Count(y => (y.TargetId==x.Id)&&y.Delivered!= null), allParcels.Count(y => (y.TargetId==x.Id)&&y.Delivered== null)))).ToList();

                return allCustomer;
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.XmlFilePath, ex.Message, ex.InnerException);
            }
        }


        /// <summary>
        /// list of all drone
        /// </summary>
        /// <returns>return the lust of all the drone </returns>
        public IEnumerable<BO.DroneToList> ListDrones()
        {
            try
            {

                List<BO.DroneToList> allDrones = new();
                foreach (var x in DronesBl)
                {
                    allDrones.Add(x);
                }
                return allDrones;
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.XmlFilePath, ex.Message, ex.InnerException);
            }
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
                try
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
                catch(DO.IdDoseNotExist ex)
                {
                    throw new BO.IdDoseNotExist(ex.ObjectType, ex.Id, ex);
                }
                catch (DO.XMLFileLoadCreateException ex)
                {
                    throw new BO.XMLFileLoadCreateException(ex.XmlFilePath, ex.Message, ex.InnerException);
                }
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
            foreach (var x in idal.ListOfParcels(t => t.DroneId == 0))
            {
                 try
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
                catch (DO.IdDoseNotExist ex)
                {
                    throw new BO.IdDoseNotExist(ex.ObjectType, ex.Id, ex);
                }
                catch (DO.XMLFileLoadCreateException ex)
                {
                    throw new BO.XMLFileLoadCreateException(ex.XmlFilePath, ex.Message, ex.InnerException);
                }

            }

            return ParcelsNotAssociated;
        }

        public IEnumerable<BO.DroneToList> ListOfDrones(Predicate<BO.DroneToList> f)
        {
            return DronesBl.FindAll(f);
        }
       
        public IEnumerable<BO.ParcelToList> ListOfParcels(Predicate<BO.ParcelToList> f)
        {
            try
            {
                IEnumerable<BO.ParcelToList> Parcel = ListParcels();

                return (Parcel as List<BO.ParcelToList>).FindAll(f);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.XmlFilePath, ex.Message, ex.InnerException);
            }
        }
        /// <summary>
        /// list of station with availebal charge post
        /// </summary>
        /// <returns>list of  station</returns>
        public IEnumerable<BO.BaseStationToList> StationWithAvailebalChargePost()
        {
            try
            {
                List<BO.BaseStationToList> stationWithAvailebalChargePost = new();
                foreach (DO.Station x in idal.ListOfStations(t => t.ChargeSlots > 0))
                {
                    int numOfDronesInCharge = idal.AllDronesIncharge().Count(w => w.StationId==x.Id);
                    stationWithAvailebalChargePost.Add(new(x.Id, x.Name, x.ChargeSlots, numOfDronesInCharge));
                }
                return stationWithAvailebalChargePost;
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.XmlFilePath, ex.Message, ex.InnerException);
            }
        }


    }
}
