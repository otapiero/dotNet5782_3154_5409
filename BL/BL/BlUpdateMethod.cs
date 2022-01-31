using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using BO;
namespace BL
{
    partial class BL
    {
        /// <summary>
        /// uptate the drone model
        /// </summary>
        /// <param name="id">the id of drone to change</param>
        /// <param name="model">the new model</param>
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void UpdateDroneModel(int id, string model)
        {


            if (model=="")
                throw new BO.TheNameModelNotValid();
            BO.DroneToList result = DronesBl.Find(x => x.Id==id);
            if (result.Id == id)
            {
                BO.DroneToList temp;
                temp = result;
                temp.Model=model;
                DronesBl.Remove(result);
                DronesBl.Add(temp);
                idal.UpdateDroneModel(id, model);
            }
            else throw new BO.IdDoseNotExist("Id not found.", "drone", id);


        }
        /// <summary>
        /// update a station
        /// </summary>
        /// <param name="id">the id of the station </param>
        /// <param name="name">the name to update</param>
        /// <param name="chargeSlots">num of charge slots </param>
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void UpdateStation(int id, string name, int chargeSlots)
        {
            try
            {
                if ((idal.AllStation()).Any(x => x.Id==id))
                {

                    int numOfDronesInCharge = idal.ListOfDronesInCharge(w => w.StationId==id).Count();
                    if (chargeSlots >= numOfDronesInCharge)
                    {
                        idal.UpdateStation(id, name, chargeSlots);

                    }
                    throw new BO.NumOfChargeSlots("chargeSlots lees than numOfDronesInCharge", numOfDronesInCharge);
                }
                else throw new BO.IdDoseNotExist("Id not found.", "station", id);

            }
            catch (Exception x)
            {
                throw new BO.IBException(x.Message); ;

            }

        }
        /// <summary>
        /// update a costumer
        /// </summary>
        /// <param name="id">the id of the costumer to update</param>
        /// <param name="name">the name to update</param>
        /// <param name="phone">the phoneto update</param>
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void UpdateCostumer(int id, string name, string phone)
        {
            if (phone.Length<=0&&name.Length<=0)
            {
                throw new BO.thePhoneAndNameInputAreIncorrect();
            }
            try
            {
                idal.UpdateCostumer(id, name, phone);
            }
            catch (DO.IdDoseNotExist x)
            {
                throw new BO.IdDoseNotExist(x.ObjectType, x.Id, x);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new BO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        /// <summary>
        /// send a drone to charge
        /// </summary>
        /// <param name="id">the id of drone sended to charge</param>
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void SendDroneToCharge(int id)
        {


            if (DronesBl.Exists(x => x.Id == id && x.status == BO.DroneStatuses.Available))
            {
                IEnumerable<DO.Station> stationData = idal.ListOfStations(x => x.ChargeSlots > 0);

                if ((stationData).Count() > 0)
                {
                    BO.DroneToList temp = DronesBl.Find(x => x.Id==id);
                    BO.Location closeStation = FindTheClosestStation(temp.CurrentLocation, stationData);
                    double dis = DistanceLocation(temp.CurrentLocation, closeStation);
                    DO.Station chooseStation = (stationData).FirstOrDefault(x => x.Longitude == closeStation.Longitude && x.Lattitude==closeStation.Lattitude);
                    Double[] vs = idal.ElectricityUse();
                    double Rate = vs[0];
                    if (temp.Battery - dis*Rate > 0)
                    {
                        temp.Battery = temp.Battery - (dis*Rate);
                        temp.CurrentLocation = closeStation;
                        temp.status = BO.DroneStatuses.Maintenace;

                        idal.SendDroneToCharge(id, chooseStation.Id);
                    }
                    else throw new BO.BatteryExaption("Not enough battery the drone will fall of the sky!!", dis*Rate);
                    // else throw "Not enough battery"
                }
                else throw new BO.NoStationAvailable("drone", id);
                // else throw "There is no station available"
            }
            else throw new BO.WrongStatusObject("drone", id, "not availble");
            //  else throw ..."the drone isnt available"

        }
        /// <summary>
        /// relesae a drone from charging
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void RelesaeDroneFromCharge(int id, double time)
        {

            if (DronesBl.Exists(x => x.Id == id && x.status == BO.DroneStatuses.Maintenace))
            {
                try
                {

                    BO.DroneToList temp = DronesBl.Find(x => x.Id==id);
                    Double[] vs = idal.ElectricityUse();
                    double chargingRate = vs[4];
                    temp.Battery+= time*chargingRate;
                    temp.Battery = temp.Battery > 100 ? temp.Battery=100 : temp.Battery;
                    temp.status =  BO.DroneStatuses.Available;
                    idal.ReleseDroneFromCharge(id);
                }
                catch (DO.IdDoseNotExist x)
                {
                    throw new BO.IdDoseNotExist(x.ObjectType, x.Id, x);
                }
                catch (DO.XMLFileLoadCreateException x)
                {
                    throw new BO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
                }

            }
            else throw new BO.WrongStatusObject("drone", id, "not in Maintenace");
            // else throw ..."the drone isnt in Maintenace"

        }
        /// <summary>
        /// asssign a parcel to a drone
        /// </summary>
        /// <param name="id">the id of drone to assign</param>
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void AssignPackageToDrone(int id)
        {
            try
            {

                if (!DronesBl.Exists(x => x.Id==id))
                    throw new BO.IdDoseNotExist("id dose not exist", "drone", id);
                if (DronesBl.Exists(x => x.Id == id && x.status == BO.DroneStatuses.Available))
                {


                    BO.DroneToList temp = DronesBl.Find(x => x.Id==id);
                    var parcelsData = idal.ListOfParcels(x => x.DroneId==0 && x.Wheight <= (DO.WeightCategories)temp.Weight);
                    parcelsData=parcelsData.OrderBy(x => (int)x.Priority);
                    if (parcelsData.Count()==0)
                    {
                        throw new BO.NoParcelAvilable();
                    }
                    DO.Costumer person = idal.SearchCostumer((parcelsData).First().Sender);
                    BO.Location parcelLocation = new(person.Longitude, person.Lattitude);



                    double tempDistance, dis = DistanceLocation(temp.CurrentLocation, parcelLocation);
                    int parcelTarget = parcelsData.First().TargetId;
                    int wheight = (int)parcelsData.First().Wheight;
                    int parcelId = parcelsData.First().Id;
                    foreach (var y in parcelsData)
                    {
                        var personT = idal.SearchCostumer((parcelsData).First().Sender);
                        tempDistance= DistanceLocation(temp.CurrentLocation, new BO.Location(personT.Longitude, personT.Lattitude));
                        if (tempDistance<dis)
                        {
                            dis = tempDistance;
                            parcelLocation = new BO.Location(personT.Longitude, personT.Lattitude);
                            parcelTarget =y.TargetId;
                            wheight = (int)y.Wheight;
                            parcelId= y.Id;
                        }
                    }

                    var target = idal.SearchCostumer(parcelTarget);
                    IEnumerable<DO.Station> stationData = idal.ListOfStations(x => x.ChargeSlots > 0);

                    if (stationData.Count()<1)
                        throw new BO.NoStationAvailable("drone", id);
                    BO.Location closeStation = FindTheClosestStation(new(target.Longitude, target.Lattitude), stationData);
                    double disToCollect = DistanceLocation(temp.CurrentLocation, parcelLocation);
                    double disToDeliver = DistanceLocation(parcelLocation, new(target.Longitude, target.Lattitude));
                    double disToStation = DistanceLocation(closeStation, new(target.Longitude, target.Lattitude));
                    Double[] vs = idal.ElectricityUse();
                    double batteryTemp = (disToCollect+disToStation)*vs[0]+ disToDeliver*vs[wheight+1];
                    
                    if (temp.Battery-batteryTemp>10)
                    {
                        temp.status=BO.DroneStatuses.Delivery;
                        temp.ParcelId= parcelId;
                        idal.AssignPackageToDrone(parcelId, temp.Id);
                    }
                    else
                        throw new BO.BatteryExaption("Not enough battery", batteryTemp, (batteryTemp-temp.Battery)/vs[4]);
                }
                else
                    throw new BO.WrongStatusObject("drone", id, "not avilable");
            }
            catch (DO.IdDoseNotExist ex)
            {
                throw new BO.IdDoseNotExist(ex.ObjectType, ex.Id, ex);
            }
            catch (BO.IdDoseNotExist ex)
            {
                throw new BO.IdDoseNotExist(ex.Message, ex.ObjectType, ex.Id);
            }
            catch (BO.WrongStatusObject ex)
            {
                throw new BO.WrongStatusObject(ex.ObjectType, ex.Id, ex.Error);
            }
            catch (BO.NoStationAvailable ex)
            {
                throw new BO.NoStationAvailable(ex.ObjectType, ex.Id);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.XmlFilePath, ex.Message, ex.InnerException);
            }
        }
        /// <summary>
        /// colect a parcel by a drone
        /// </summary>
        /// <param name="id">the id of drone to collect a parcel</param>
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void CollectPackage(int id)
        {
            try
            {

                if (!DronesBl.Exists(x => x.Id==id))
                    throw new BO.IdDoseNotExist("id dose not exist", "drone", id);
                if (DronesBl.Exists(x => x.Id == id && x.status == BO.DroneStatuses.Delivery))
                {
                    BO.DroneToList temp = DronesBl.Find(x => x.Id==id);
                    var parcel = idal.SearchParcel(temp.ParcelId);
                    if (parcel.PickedUp== null)
                    {
                        var person = idal.SearchCostumer(parcel.Sender);
                        double fullDis = DistanceLocation(temp.CurrentLocation, new BO.Location(person.Longitude, person.Lattitude));
                        Double[] vs = idal.ElectricityUse();
                        temp.Battery =  temp.Battery - fullDis*vs[(int)parcel.Wheight+1] < 0 ? 4.34654 : temp.Battery - fullDis*vs[(int)parcel.Wheight+1];
                      
                        temp.CurrentLocation=new BO.Location(person.Longitude, person.Lattitude);
                        idal.CollectPackage(parcel.Id);
                    }
                    else throw new BO.WrongStatusObject("parcel", temp.ParcelId, "already picked up");
                    // else throw ..."the parcel was picked up"
                }
                else throw new BO.WrongStatusObject("drone", id, "not in Delivery");
                // else throw ..."the drone isnt in Delivery"
            }
            catch (DO.IdDoseNotExist x)
            {
                throw new BO.IdDoseNotExist(x.ObjectType, x.Id, x);
            }
            catch (BO.IdDoseNotExist x)
            {
                throw new BO.IdDoseNotExist(x.Message, x.ObjectType, x.Id);
            }
            catch (BO.WrongStatusObject x)
            {
                throw new BO.WrongStatusObject(x.ObjectType, x.Id, x.Error);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new BO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }


        }
        /// <summary>
        /// deliver a parcel
        /// </summary>
        /// <param name="id">the id of drone to deliver a parcel</param>
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void DeliverPackage(int id)
        {
            try
            {
                if (!DronesBl.Exists(x => x.Id==id))
                    throw new BO.IdDoseNotExist("id dose not exist", "drone", id);
                if (DronesBl.Exists(x => x.Id == id && x.status == BO.DroneStatuses.Delivery))
                {
                    BO.DroneToList temp = DronesBl.Find(x => x.Id==id);

                    var parcel = idal.SearchParcel(temp.ParcelId);
                    if (parcel.PickedUp==null)
                    {
                        throw new BO.WrongStatusObject("parcel", temp.ParcelId, "not pickedup!");
                    }
                    if (parcel.Delivered == null)
                    {
                        idal.DeliverPackage(parcel.Id);
                        var person = idal.SearchCostumer(parcel.TargetId);
                        double fullDis = DistanceLocation(temp.CurrentLocation, new BO.Location(person.Longitude, person.Lattitude));
                        Double[] vs = idal.ElectricityUse();
                        temp.Battery =temp.Battery - fullDis*vs[(int)parcel.Wheight+1] < 0 ? 7.34 : temp.Battery - fullDis*vs[(int)parcel.Wheight+1];
                        temp.CurrentLocation=new BO.Location(person.Longitude, person.Lattitude);
                        temp.status=BO.DroneStatuses.Available;
                        temp.ParcelId=0;


                        parcel.Delivered=DateTime.Now;

                    }
                    else
                        throw new BO.WrongStatusObject("parcel", temp.ParcelId, "already delivered");
                    //else throw ..."the parcel was delivered"
                }
                else throw new BO.WrongStatusObject("drone ", id, "not in Delivery");
                // else throw ..."the drone isnt in Delivery"
            }
            catch (DO.IdDoseNotExist x)
            {
                throw new BO.IdDoseNotExist(x.ObjectType, x.Id, x);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new BO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void DeleteParcel(int id)
        {
            try
            {
                var parcel = idal.SearchParcel(id);
                if (parcel.Scheduled==null)
                {
                    idal.DeleteParcel(id);
                }
                else
                {
                    throw new BO.WrongStatusObject("parcel", id, "alredy Scheduled so it can not be delted");
                }
            }
            catch (DO.IdDoseNotExist x)
            {
                throw new BO.IdDoseNotExist(x.ObjectType, x.Id, x);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new BO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        internal void UpdateDroneLocation(int id, double lonPlus, double latPlus)
        {

            DroneToList drone = DronesBl.Find(x => x.Id == id);

            drone.CurrentLocation.Lattitude += latPlus;
            drone.CurrentLocation.Longitude += lonPlus;
        }
        internal void BatteryMinus(int id, double distance)
        {
            try
            {
                double[] vs = idal.ElectricityUse();
                DroneToList drone = DronesBl.Find(x => x.Id == id);
                var t = vs[(int)drone.Weight+1];
                double minus = t*distance;
                drone.Battery-= minus;
                if (drone.Battery<0)
                { drone.Battery=0; }
            }
            catch (DO.IdDoseNotExist x)
            {
                throw new BO.IdDoseNotExist(x.ObjectType, x.Id, x);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new BO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        public  void startSimulator( int id, Func<bool> stop, Action report)
        {
            try
            {
                Simulator sim = new Simulator(this, id, stop,report);
            }
            catch(BO.BatteryExaption ex)
            {
                throw new BO.BatteryExaption(ex.Message, ex);
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
        }

        public void  BatteryPlus(int id, double plus)
        {
          
            try
            {
                
                var temp = DronesBl.Find(x=> x.Id == id);
                temp.Battery+=plus;
                if (temp.Battery>100) temp.Battery=100;
               
            }
            catch (DO.IdDoseNotExist x)
            {
                throw new BO.IdDoseNotExist(x.ObjectType, x.Id, x);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new BO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
    }
}
    