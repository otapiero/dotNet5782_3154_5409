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
        public void AddNewStation(int id, string name, double longattitude, double lattitude, int numChargeSlot)
        {
            if (numChargeSlot<1)
            {
                throw new BO.IBException("ChargeSlot full");
            }
            try
            {
                idal.AddNewStation(id, name, longattitude, lattitude, numChargeSlot);
            }
            catch (Exception x)
            {
                throw new BO.IBException(x.Message); ;

            }


        }
        public void AddNewDrone(int id, string model, int wheigt, int stationId)
        {
            if (!idal.AllStation().ToList().Exists(x => x.Id==stationId))
            {
                throw new BO.IBException("not found station");
            }
            if (wheigt > 2 || wheigt < 0)
            {
                throw new BO.IBException("weight not good");
            }
            try
            {
                idal.AddNewDrone(id, model);
            }
            catch (Exception x)
            {
                throw new BO.IBException(x.Message); ;

            }


            BO.DroneToList tempDroneToList = new();
            tempDroneToList.Id = id;
            tempDroneToList.Model = model;
            tempDroneToList.Battery=100;
            tempDroneToList.Weight = (BO.WeightCategories)wheigt;
            DO.Station tempStation = idal.AllStation().ToList().Find(x => x.Id == stationId);
            tempDroneToList.CurrentLocation = new(tempStation.Longitude, tempStation.Lattitude);
            DronesBl.Add(tempDroneToList);
        }
        public void AddNewCustomer(int id, string name, string phone, double longattitude, double lattitude)
        {
            try
            {
                idal.AddNewCustomer(id, name, phone, longattitude, lattitude);
            }
            catch (Exception x)
            {
                throw new BO.IBException(x.Message); ;

            }
        }

        public void AddNewParcel(int senderId, int targetId, int wheigt, int Priority)
        {
            if (idal.SearchCostumer(senderId).Equals(new DO.Costumer()))
            {
                throw new BO.IBException("customer not found");

            }
            if (idal.SearchCostumer(targetId).Equals(new DO.Costumer()))
            {
                throw new BO.IBException("customer not found");
            }
            if (Priority>2||Priority<0)
            {
                throw new BO.IBException("customer not found");
            }
            if (wheigt > 2 || wheigt < 0)
            {
                throw new BO.IBException("customer not found");
            }
            try
            {
                idal.AddNewParcel(senderId, targetId, wheigt, Priority);
            }
            catch (Exception x)
            {
                throw new BO.IBException(x.Message); ;

            }
        }
    }
}

