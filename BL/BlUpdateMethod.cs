using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public partial class BL
    {

        public void UpdateDroneModel(int id, string model)
        {

            try
            {
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
                throw new BO.IBException("Id not found.");

            }
            catch (Exception x)
            {
                throw new BO.IBException(x.Message); ;

            }
        }
            public void UpdateStation(int id,string name,int chargeSlots)
        {
            try
            {
                 if(idal.AllStation().ToList().Exists(x=>x.Id==id))
                 {
                    
                        int numOfDronesInCharge = idal.AllDronesIncharge().Count(w => w.StationId==id);
                        if (chargeSlots >= numOfDronesInCharge)
                        {
                            idal.UpdateStation(id, name,chargeSlots);
                      
                        }
                        throw new BO.IBException("chargeSlots lees than numOfDronesInCharge");
                    


                }
                throw new BO.IBException("Id not found.");
                
            }
            catch (Exception x)
            {
                throw new BO.IBException(x.Message); ;

            }

        }
        public void UpdateCostumer(int id, string name,string phone)
        {
            try
            {
               
                if (idal.AllCustomers().ToList().Exists(x=>x.Id==id))
                {
                    idal.UpdateCostumer( id, name,phone);
                }
               throw new BO.IBException("id not exist");
            }
            catch (Exception x)
            {
                throw new BO.IBException(x.Message); ;

            }
        }
        public void SendDroneToCharge(int id)
        {
            try
            {
                
                if (DronesBl.Exists(x => x.Id == id && x.status == BO.DroneStatuses.Available))
                {
                    List<IDAL.DO.Station> stationData = (List<IDAL.DO.Station>)idal.AllStation();
                    stationData = (from x in stationData where x.ChargeSlots > 0
                                                                                    select x).ToList();
                    if (stationData.Count() > 0)
                    {
                        DroneToList temp = DronesBl.Find(x=>x.Id==id);
                        BO.Location closeStation = FindTheClosestStation(temp.CurrentLocation,stationData);
                        double dis = DistanceLocation(temp.CurrentLocation, closeStation);
                        IDAL.DO.Station chooseStation = stationData.Find(x=>x.Longitude == closeStation.Longitude && x.Lattitude==closeStation.Lattitude);
                        Double[] vs = idal.ElectricityUse();
                        double Rate = vs[0];
                        if(temp.Battery - dis*Rate > 0)
                        {
                            temp.Battery = temp.Battery - (dis*Rate);
                            temp.CurrentLocation = closeStation;
                            temp.status = DroneStatuses.Maintenace;

                            idal.SendDroneToCharge( id, chooseStation.Id);
                        }
                        throw new BO.IBException("Not enough battery");
                        // else throw "Not enough battery"
                    }
                    throw new BO.IBException("There is no station available");
                    // else throw "There is no station available"
                }
                throw new BO.IBException("the drone isnt available");
                //  else throw ..."the drone isnt available"
            }
            catch (Exception x)
            {
                throw new BO.IBException(x.Message); ;

            }
        }
        public void RelesaeDroneFromCharge(int id,double time)
        {
            try
            {
                if (DronesBl.Exists(x => x.Id == id && x.status == BO.DroneStatuses.Maintenace))
                {
                    DroneToList temp = DronesBl.Find(x => x.Id==id);
                    Double[] vs = idal.ElectricityUse();
                    double chargingRate = vs[4];
                    temp.Battery+= time*chargingRate;
                    temp.Battery = temp.Battery > 1000 ? temp.Battery=100 : temp.Battery;
                    temp.status =  DroneStatuses.Available;
                    idal.ReleseDroneFromCharge(id);

                }
                throw new BO.IBException("the drone isnt in Maintenace");
                // else throw ..."the drone isnt in Maintenace"
            }
            catch (Exception x)
            {
                throw new BO.IBException(x.Message); ;

            }
        }
        public void AssignPackageToDrone(int id)
        {
            try
            {
                if (DronesBl.Exists(x => x.Id == id && x.status == BO.DroneStatuses.Available))
                {
                    DroneToList temp = DronesBl.Find(x => x.Id==id);
                    var parcelsData = (List<IDAL.DO.Parcel>)idal.AllParcels();
                    parcelsData = (List<IDAL.DO.Parcel>)parcelsData.Select(x => x.DroneId==0 && x.Wheight <= (IDAL.DO.WeightCategories)temp.Weight);
                    parcelsData=(List<IDAL.DO.Parcel>)parcelsData.OrderBy(x=> (int)x.Priority);
                    var person = idal.SearchCostumer(parcelsData[0].Sender);
                    double tempDistance, dis = DistanceLocation(temp.CurrentLocation, new BO.Location(person.Longitude, person.Lattitude));
                    BO.Location parcelLocation = new(person.Longitude, person.Lattitude);
                    int parcelTarget = 0;
                    int parcelId = 0;
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
                    DroneToList targetId = DronesBl.Find(x => x.Id==parcelTarget);
                    List<IDAL.DO.Station> stationData = (List<IDAL.DO.Station>)idal.AllStation();
                    stationData = (from x in stationData
                                                          where x.ChargeSlots > 0
                                                          select x).ToList();
                   if (stationData.Count()<1) throw new BO.IBException("no free empty"); 
                    BO.Location closeStation = FindTheClosestStation(targetId.CurrentLocation, stationData);
                    double fullDis = DistanceLocation(temp.CurrentLocation, parcelLocation)+DistanceLocation(parcelLocation, targetId.CurrentLocation)+DistanceLocation(closeStation, targetId.CurrentLocation);
                    Double[] vs = idal.ElectricityUse();
                    double Rate = vs[0];

                    if (temp.Battery-Rate*fullDis>0)
                    {
                        temp.status=DroneStatuses.Delivery;
                        temp.ParcelId= parcelId;
                        idal.AssignPackageToDrone(parcelId, temp.Id);
                    }
                    throw new BO.IBException("Not enough battery"); 
                }
                throw new BO.IBException("the drone isnt in avilable");
                
            }
            catch (Exception x)
            {
                throw new BO.IBException(x.Message); ;

            }
        }
        public void CollectPackage(int id)
        {
            try
            {
                if (DronesBl.Exists(x => x.Id == id && x.status == BO.DroneStatuses.Delivery))
                {
                    DroneToList temp = DronesBl.Find(x => x.Id==id);
                    var parcelsData = (List<IDAL.DO.Parcel>)idal.AllParcels();
                    var parcel = parcelsData.Find(x => x.Id==temp.ParcelId);
                    if (parcel.PickedUp== new DateTime())
                    {
                        var person = idal.SearchCostumer(parcel.Sender);
                        double fullDis = DistanceLocation(temp.CurrentLocation, new BO.Location(person.Longitude, person.Lattitude));
                        Double[] vs = idal.ElectricityUse();
                        temp.Battery-=fullDis*vs[(int)parcel.Wheight+1];
                        temp.CurrentLocation=new BO.Location(person.Longitude, person.Lattitude);
                        idal.CollectPackage(parcel.Id);
                    }
                    throw new BO.IBException("the parcel was picked up");
                    // else throw ..."the parcel was picked up"
                }
                throw new BO.IBException("the drone isnt in Delivery");
                // else throw ..."the drone isnt in Delivery"
            }
            catch (Exception x)
            {
                throw new BO.IBException(x.Message); ;

            }
        }
        public void DeliverPackage(int id)
        {
            try
            {
                if (DronesBl.Exists(x => x.Id == id && x.status == BO.DroneStatuses.Delivery))
                {
                    DroneToList temp = DronesBl.Find(x => x.Id==id);
                    var parcelsData = (List<IDAL.DO.Parcel>)idal.AllParcels();
                    var parcel = parcelsData.Find(x => x.Id==temp.ParcelId);
                    if (parcel.Delivered == new DateTime())
                    {
                        var person = idal.SearchCostumer(parcel.TargetId);
                        double fullDis = DistanceLocation(temp.CurrentLocation, new BO.Location(person.Longitude, person.Lattitude));
                        Double[] vs = idal.ElectricityUse();
                        temp.Battery-=fullDis*vs[(int)parcel.Wheight+1];
                        temp.CurrentLocation=new BO.Location(person.Longitude, person.Lattitude);
                        temp.status=DroneStatuses.Available;
                        idal.DeliverPackage(parcel.Id);
                    }
                    throw new BO.IBException("the parcel was delivered");
                    //else throw ..."the parcel was delivered"
                }
                throw new BO.IBException("the drone isnt in Delivery");
                // else throw ..."the drone isnt in Delivery"
            }
            catch (Exception x)
            {
                throw new BO.IBException(x.Message); ;

            }
        }
    }
}
