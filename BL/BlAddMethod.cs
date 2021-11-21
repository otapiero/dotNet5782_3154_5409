using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IBL
{

    public partial class BL
    {
        public void AddNewStation(int id,string name,double longattitude,double lattitude,int numChargeSlot)
        {
            if(numChargeSlot<1)
            {
                //excaption
            }
            try
            {
                idal.AddNewStation(id, name, longattitude, lattitude, numChargeSlot);
            }
            catch
            {

            }


        }
        public void AddNewDrone(int id,string model,int wheigt,int stationId)
        {
            if(!idal.AllStation().ToList().Exists(x=>x.Id==stationId))
            {
                //excaption
            }
            if (wheigt > 2 || wheigt < 0)
            {
                //excaption wheigt
            }
            try
            {
                idal.AddNewDrone(id, model);
            }
            catch
            {

            }
            BO.DroneToList tempDroneToList= new();
            tempDroneToList.Id = id;
            tempDroneToList.Model = model;
            tempDroneToList.Weight = (BO.WeightCategories)wheigt;
            IDAL.DO.Station tempStation = idal.AllStation().ToList().Find(x => x.Id == stationId);
            tempDroneToList.CurrentLocation = new(tempStation.Longitude, tempStation.Lattitude);
            DronesBl.Add(tempDroneToList);
        }
        public void AddNewCustomer(int id,string name,string phone,double longattitude, double lattitude)
        {
            try
            {
                idal.AddNewCustomer(id, name, phone, longattitude, lattitude);
            }
            catch
            {

            }
        }

        public void AddNewParcel(int senderId,int targetId,int wheigt,int Priority)
        {
            if(idal.SearchCostumer(senderId).Equals(new IDAL.DO.Costumer()))
            {
              //  excaption sender dose not exsit

            }
            if (idal.SearchCostumer(targetId).Equals(new IDAL.DO.Costumer()))
            {
                //  excaption target dose not exsit
            }
            if(Priority>2||Priority<0)
            {
                //excaption priority
            }
            if (wheigt > 2 || wheigt < 0)
            {
                //excaption wheigt
            }
            try
            {
                idal.AddNewParcel(senderId, targetId, wheigt, Priority);
            }
            catch
            {

            }
        }
    }
}

