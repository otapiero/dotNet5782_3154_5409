﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IBL
{
    public partial class BL
    {
        /// <summary>
        /// uptate the drone model
        /// </summary>
        /// <param name="id">the id of drone to change</param>
        /// <param name="model">the new model</param>
        public void UpdateDroneModel(int id, string model)
        {

            try
            {
                if (model=="")
                    throw new BO.IBException("not enough information");
                BO.DroneToList result = DronesBl.Find(x => x.Id==id);
                if (result != null)
                {
                    BO.DroneToList temp;
                    temp = result;
                    temp.Model=model;
                    DronesBl.Remove(result);
                    DronesBl.Add(temp);
                    idal.UpdateDroneModel(id, model);
                }
                else throw new BO.IBException("Id not found.");

            }
            catch (Exception x)
            {
                throw new BO.IBException(x.Message); ;

            }
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
                if (idal.AllStation().ToList().Exists(x => x.Id==id))
                {

                    int numOfDronesInCharge = idal.ListOfDronesInCharge(w => w.StationId==id).Count();
                    if (chargeSlots >= numOfDronesInCharge)
                    {
                        idal.UpdateStation(id, name, chargeSlots);

                    }
                    throw new BO.IBException("chargeSlots lees than numOfDronesInCharge");



                }
                else throw new BO.IBException("Id not found.");

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
            try
            {

                if (idal.AllCustomers().ToList().Exists(x => x.Id==id))
                {
                    idal.UpdateCostumer(id, name, phone);
                }
                else throw new BO.IBException("id not exist");
            }
            catch (Exception x)
            {
                throw new BO.IBException(x.Message); ;

            }
        }
        /// <summary>
        /// send a drone to charge
        /// </summary>
        /// <param name="id">the id of drone sended to charge</param>
        public void SendDroneToCharge(int id)
        {
            try
            {

                if (DronesBl.Exists(x => x.Id == id && x.status == BO.DroneStatuses.Available))
                {
                    List<DO.Station> stationData = idal.ListOfStations(x => x.ChargeSlots > 0).ToList();

                    if (stationData.Count > 0)
                    {
                        BO.DroneToList temp = DronesBl.Find(x => x.Id==id);
                        BO.Location closeStation = FindTheClosestStation(temp.CurrentLocation, stationData);
                        double dis = DistanceLocation(temp.CurrentLocation, closeStation);
                        DO.Station chooseStation = stationData.Find(x => x.Longitude == closeStation.Longitude && x.Lattitude==closeStation.Lattitude);
                        Double[] vs = idal.ElectricityUse();
                        double Rate = vs[0];
                        if (temp.Battery - dis*Rate > 0)
                        {
                            temp.Battery = temp.Battery - (dis*Rate);
                            temp.CurrentLocation = closeStation;
                            temp.status = BO.DroneStatuses.Maintenace;

                            idal.SendDroneToCharge(id, chooseStation.Id);
                        }
                        else throw new BO.IBException("Not enough battery");
                        // else throw "Not enough battery"
                    }
                    else throw new BO.IBException("There is no station available");
                    // else throw "There is no station available"
                }
                else throw new BO.IBException("the drone isnt available");
                //  else throw ..."the drone isnt available"
            }
            catch (Exception x)
            {
                throw new BO.IBException(x.Message); ;

            }
        }
        /// <summary>
        /// relesae a drone from charging
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time"></param>
        public void RelesaeDroneFromCharge(int id, double time)
        {
            try
            {
                if (DronesBl.Exists(x => x.Id == id && x.status == BO.DroneStatuses.Maintenace))
                {
                    BO.DroneToList temp = DronesBl.Find(x => x.Id==id);
                    Double[] vs = idal.ElectricityUse();
                    double chargingRate = vs[4];
                    temp.Battery+= time*chargingRate;
                    temp.Battery = temp.Battery > 1000 ? temp.Battery=100 : temp.Battery;
                    temp.status =  BO.DroneStatuses.Available;
                    idal.ReleseDroneFromCharge(id);

                }
                else throw new BO.IBException("the drone isnt in Maintenace");
                // else throw ..."the drone isnt in Maintenace"
            }
            catch (Exception x)
            {
                throw new BO.IBException(x.Message); ;

            }
        }
        /// <summary>
        /// asssign a parcel to a drone
        /// </summary>
        /// <param name="id">the id of drone to assign</param>
        public void AssignPackageToDrone(int id)
        {
            try
            {
                if (DronesBl.Exists(x => x.Id == id && x.status == BO.DroneStatuses.Available))
                {

                    BO.DroneToList temp = DronesBl.Find(x => x.Id==id);
                    var parcelsData = idal.ListOfParcels(x => x.DroneId==0 && x.Wheight <= (DO.WeightCategories)temp.Weight).ToList();
                    parcelsData=parcelsData.OrderBy(x => (int)x.Priority).ToList();
                    var person = idal.SearchCostumer(parcelsData[0].Sender);
                    BO.Location parcelLocation = new(person.Longitude, person.Lattitude);

                    double tempDistance, dis = DistanceLocation(temp.CurrentLocation, parcelLocation);
                    int parcelTarget = parcelsData[0].TargetId;
                    int parcelId = parcelsData[0].Id;
                    foreach (var y in parcelsData)
                    {
                        var personT = idal.SearchCostumer(parcelsData[0].Sender);
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
                    List<DO.Station> stationData = idal.ListOfStations(x => x.ChargeSlots > 0).ToList();

                    if (stationData.Count()<1) throw new BO.IBException("no free empty");
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
                        throw new BO.IBException("Not enough battery");
                }
                else
                    throw new BO.IBException("the drone isnt in avilable");

            }
            catch (Exception x)
            {
                throw new BO.IBException(x.Message);

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
                if (DronesBl.Exists(x => x.Id == id && x.status == BO.DroneStatuses.Delivery))
                {
                    BO.DroneToList temp = DronesBl.Find(x => x.Id==id);
                    var parcelsData = idal.AllParcels().ToList();
                    var parcel = parcelsData.Find(x => x.Id==temp.ParcelId);
                    if (parcel.PickedUp== null)
                    {
                        var person = idal.SearchCostumer(parcel.Sender);
                        double fullDis = DistanceLocation(temp.CurrentLocation, new BO.Location(person.Longitude, person.Lattitude));
                        Double[] vs = idal.ElectricityUse();
                        temp.Battery-=fullDis*vs[(int)parcel.Wheight+1];
                        temp.CurrentLocation=new BO.Location(person.Longitude, person.Lattitude);
                        idal.CollectPackage(parcel.Id);
                    }
                    else throw new BO.IBException("the parcel was picked up");
                    // else throw ..."the parcel was picked up"
                }
                else throw new BO.IBException("the drone isnt in Delivery");
                // else throw ..."the drone isnt in Delivery"
            }
            catch (Exception x)
            {
                throw new BO.IBException(x.Message); ;

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
                if (DronesBl.Exists(x => x.Id == id && x.status == BO.DroneStatuses.Delivery))
                {
                    BO.DroneToList temp = DronesBl.Find(x => x.Id==id);
                    var parcelsData = idal.AllParcels().ToList();
                    var parcel = parcelsData.Find(x => x.Id==temp.ParcelId);
                    if (parcel.PickedUp==null)
                    {
                        throw new BO.IBException("the parcel is not pickedup!");
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
                    else throw new BO.IBException("the parcel was delivered");
                    //else throw ..."the parcel was delivered"
                }
                else throw new BO.IBException("the drone isnt in Delivery");
                // else throw ..."the drone isnt in Delivery"
            }
            catch (Exception x)
            {
                throw new BO.IBException(x.Message); ;

            }
        }
    }
}
