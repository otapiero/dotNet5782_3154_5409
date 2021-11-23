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
        
        public void UpdateDroneModel(int id ,string model)
        {
          
            try
            {
                BO.DroneToList result= DronesBl.Find(x=>x.Id==id);
                if(result != null)
                {
                    BO.DroneToList temp = new();
                    temp = result;
                    temp.Model=model;
                    DronesBl.Remove(result);
                    DronesBl.Add(temp);
                    idal.UpdateDroneModel(id, model);
                }
                else throw " id not exist";
               
            }
            catch
            {
                //dorone id not exist
            }
        }
        public void UpdateStation(int id,string name,int chargeSlots)
        {
            try
            {
                 if(idal.AllStation().ToList().Exists(x=>x.Id==stationId))
                 {
                    
                        int numOfDronesInCharge = idal.AllDronesIncharge().Tolist().Count(w => w.StationId==id);
                        if (chargeSlots >= numOfDronesInCharge)
                        {
                            idal.UpdateStation(id, name,chargeSlots);
                      
                        }
                        else throw ... "chargeSlots lees than numOfDronesInCharge "
                  

                }
                else throw "the id not exist";
               
            }
            catch
            {
                //input chargeSlots or name empty
            }

        }
        public void UpdateCostumer(int id, string name,string phone)
        {
            try
            {
               
                if (idal.AllCustomers().ToList().Exists(x=>x.Id==stationId))
                {
                    idal.UpdateCostumer( id, name,phone);
                }
                else throw ..."id not exist"
            }
            catch
            {
                //input phone or name empty
            }
        }
        public void SendDroneToCharge(int id)
        {
            try
            {
                
                if (DronesBl.Exists(x => x.Id == id && x.status == BO.DroneStatuses.Available))
                {
                    List<IDAL.DO.Station> stationData = (List<IDAL.DO.Station>)idal.AllStation();
                    stationData = (List<IDAL.DO.Station>)(from x in stationData
                                                                                    where x.ChargeSlots > 0
                                                                                    select x);
                    if (stationData.Count() > 0)
                    {
                        DroneToList temp = DronesBl.Find(x=>x.Id==id);
                        BO.Location closeStation = FindTheClosestStation(temp.CurrentLocation,stationData);
                        double dis = DistanceLocation(temp.CurrentLocation, closeStation);
                        IDAL.DO.Station chooseStation = stationData.Find(x=>x.Longitude == closeStation.Longitude && x.Lattitude==closeStation.Lattitude);
                        Double[] vs = idal.ElectricityUse();
                        double chargingRate = vs[4];
                        if(temp.Battery - dis*chargingRate > 0)
                        {
                            temp.Battery = temp.Battery - (dis*chargingRate);
                            temp.CurrentLocation = closeStation;
                            temp.status = DroneStatuses.Maintenace;
                            idal.SendDroneToCharge( id, chooseStation.Id);
                        }
                        else throw "Not enough battery"
                    }
                    else throw "There is no station available"
                }
                else throw ..."the drone isnt available"
            }
            catch
            {
                //input phone or name empty
            }
        }
      
        public void RelesaeDroneFromCharge(int id,double time)
        {
            try
            {

                

                if (IBL.BL.DronesBl.Exists(x => x.Id == id && x.status == BO.DroneStatuses.Maintenace))
                {
                    idal.UpdateCostumer(id);

                }
                else throw ..."the drone isnt in Maintenace"
            }
            catch
            {
                //input phone or name empty
            }
        }
        public void AssignPackageToDrone(int id)
        {

        }
        public void CollectPackage(int id)
        {

        }
        public void DeliverPackage(int id)
        {

        }
    }
}
