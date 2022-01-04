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
        /// uptate the drone model
        /// </summary>
        /// <param name="id">the id of drone to change</param>
        /// <param name="model">the new model</param>
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
        }
        /// <summary>
        /// send a drone to charge
        /// </summary>
        /// <param name="id">the id of drone sended to charge</param>
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
            else throw new BO.WrongStatusObject("drone",id,"not availble");
            //  else throw ..."the drone isnt available"

        }
        /// <summary>
        /// relesae a drone from charging
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time"></param>
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
                    temp.Battery = temp.Battery > 1000 ? temp.Battery=100 : temp.Battery;
                    temp.status =  BO.DroneStatuses.Available;
                    idal.ReleseDroneFromCharge(id);
                }
                catch(DO.IdDoseNotExist x)
                {
                    throw new BO.IdDoseNotExist(x.ObjectType, x.Id, x);
                }
                
            }
            else throw new BO.WrongStatusObject("drone",id, "not in Maintenace");
            // else throw ..."the drone isnt in Maintenace"

        }
        /// <summary>
        /// asssign a parcel to a drone
        /// </summary>
        /// <param name="id">the id of drone to assign</param>
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
                    DO.Costumer person = idal.SearchCostumer((parcelsData).First().Sender);
                    BO.Location parcelLocation = new(person.Longitude, person.Lattitude);



                    double tempDistance, dis = DistanceLocation(temp.CurrentLocation, parcelLocation);
                    int parcelTarget = (parcelsData).First().TargetId;
                    int parcelId = (parcelsData).First().Id;
                    foreach (var y in parcelsData)
                    {
                        var personT = idal.SearchCostumer((parcelsData).First().Sender);
                        tempDistance= DistanceLocation(temp.CurrentLocation, new BO.Location(personT.Longitude, personT.Lattitude));
                        if (tempDistance<dis)
                        {
                            dis = tempDistance;
                            parcelLocation = new BO.Location(personT.Longitude, personT.Lattitude);
                            parcelTarget =(y.TargetId);
                            parcelId= y.Id;
                        }
                    }

                    var target = idal.SearchCostumer(parcelTarget);
                    IEnumerable<DO.Station> stationData = idal.ListOfStations(x => x.ChargeSlots > 0);

                    if (stationData.Count()<1)
                        throw new BO.NoStationAvailable("drone", id);
                    BO.Location closeStation = FindTheClosestStation(new(target.Longitude, target.Lattitude), stationData);
                    double fullDis = DistanceLocation(temp.CurrentLocation, parcelLocation)+DistanceLocation(parcelLocation, new(target.Longitude, target.Lattitude))+DistanceLocation(closeStation, new(target.Longitude, target.Lattitude));
                    Double[] vs = idal.ElectricityUse();
                    double Rate = vs[0];

                    if (temp.Battery-Rate*fullDis>0)
                    {

                        temp.status=BO.DroneStatuses.Delivery;
                        temp.ParcelId= parcelId;
                        idal.AssignPackageToDrone(parcelId, temp.Id);
                    }
                    else
                        throw new BO.BatteryExaption("Not enough battery", Rate*fullDis, (Rate*fullDis-temp.Battery)/vs[4]);
                }
                else
                    throw new BO.WrongStatusObject("drone", id, "not avilable");
            }
            catch (DO.IdDoseNotExist ex)
            {
                throw new BO.IdDoseNotExist(ex.ObjectType, ex.Id, ex);
            }
            catch (BO.IdDoseNotExist x)
            {
                throw new BO.IdDoseNotExist(x.Message, x.ObjectType, x.Id);
            }
            catch (BO.BatteryExaption x)
            {
                throw new BO.BatteryExaption(x.Message, x.MinumumBattery, x.TimeReqested);
            }
            catch (BO.WrongStatusObject x)
            {
                throw new BO.WrongStatusObject(x.ObjectType, x.Id, x.Error);
            }
            catch (BO.NoStationAvailable x)
            {
                throw new BO.NoStationAvailable(x.ObjectType, x.Id);
            }
        }
        /// <summary>
        /// colect a parcel by a drone
        /// </summary>
        /// <param name="id">the id of drone to collect a parcel</param>
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
                        temp.Battery-=fullDis*vs[(int)parcel.Wheight+1];
                        temp.CurrentLocation=new BO.Location(person.Longitude, person.Lattitude);
                        idal.CollectPackage(parcel.Id);
                    }
                    else throw new BO.WrongStatusObject("parcel", temp.ParcelId, "already picked up");
                    // else throw ..."the parcel was picked up"
                }
                else throw new BO.WrongStatusObject("drone", id, "not in Delivery");
                // else throw ..."the drone isnt in Delivery"
            }
            catch(DO.IdDoseNotExist x)
            {
                throw new BO.IdDoseNotExist(x.ObjectType, x.Id, x);
            }
            catch (BO.IdDoseNotExist x)
            {
                throw new BO.IdDoseNotExist(x.Message, x.ObjectType, x.Id);
            }
            catch (BO.WrongStatusObject x)
            {
                throw new BO.WrongStatusObject(x.ObjectType, x.Id, x.Message);
            }
           

        }
        /// <summary>
        /// deliver a parcel
        /// </summary>
        /// <param name="id">the id of drone to deliver a parcel</param>
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
                        temp.Battery-=fullDis*vs[(int)parcel.Wheight+1];
                        temp.CurrentLocation=new BO.Location(person.Longitude, person.Lattitude);
                        temp.status=BO.DroneStatuses.Available;
                        temp.ParcelId=0;


                        parcel.Delivered=DateTime.Now;

                    }
                    else
                        throw new BO.WrongStatusObject("parcel", temp.ParcelId, "already delivered");
                    //else throw ..."the parcel was delivered"
                }
                else throw new BO.WrongStatusObject("drone ",id,"not in Delivery");
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
                throw new BO.WrongStatusObject(x.ObjectType, x.Id, x.Message);
            }
        }
    }
}
    