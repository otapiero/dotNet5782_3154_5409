using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;



namespace BL
{

    partial class BL
    {
        #region add station
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void AddNewStation(int id, string name, double longattitude, double lattitude, int numChargeSlot)
        {
            if (numChargeSlot<1)
            {
                throw new BO.NumOfChargeSlots( numChargeSlot);
            }
            try
            {
                idal.AddNewStation(id, name, longattitude, lattitude, numChargeSlot);
            }
            catch (DO.IdAlredyExist x)
            {
                throw new BO.IdAlredyExist(x.ObjectType,x.Id,x.InnerException); 

            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new BO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }

        }
        #endregion
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void AddNewDrone(int id, string model, int wheigt, int stationId)
        {
            if (!( idal.AllStation() ).Any(x => x.Id==stationId))
            {
                throw new BO.IdDoseNotExist("not found station","station",stationId);
            }
            if (wheigt > 2 || wheigt < 0)
            {
                throw new BO.WrongInputWheigt(wheigt);
            }
            try
            {
                idal.AddNewDrone(id, model);
                BO.DroneToList tempDroneToList = new();
                tempDroneToList.Id = id;
                tempDroneToList.Model = model;
                tempDroneToList.Battery=100;
                tempDroneToList.Weight = (BO.WeightCategories)wheigt;
                DO.Station tempStation = idal.SearchStation(stationId);
                tempDroneToList.CurrentLocation = new(tempStation.Longitude, tempStation.Lattitude);
                DronesBl.Add(tempDroneToList);
            }
            catch (DO.IdAlredyExist x)
            {
                throw new BO.IdAlredyExist(x.ObjectType, x.Id, x);

            }
            catch(DO.IdDoseNotExist x)
            { 
                throw new BO.IdDoseNotExist( x.ObjectType, x.Id, x);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new BO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void AddNewCustomer(int id, string name, string phone, double longattitude, double lattitude, string pass)
        {
            try
            {
                //idal.AddNewCustomer(id, name, phone, longattitude, lattitude);
                idal.AddNewCustomer(id, name, phone, longattitude, lattitude, pass);

            }
            catch (DO.IdAlredyExist x)
            {
                throw new BO.IdAlredyExist(x.ObjectType, x.Id, x);

            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new BO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void AddNewParcel(int senderId, int targetId, int wheigt, int Priority)
        {
            try
            {
                idal.SearchCostumer(senderId);

                idal.SearchCostumer(targetId);
              
                if (Priority>2||Priority<0)
                {
                    throw new BO.WrongInputPriorities(Priority);
                }
                if (wheigt > 2 || wheigt < 0)
                {
                    throw new BO.WrongInputWheigt(wheigt);
                }

                idal.AddNewParcel(senderId, targetId, wheigt, Priority);
            }
            catch(BO.WrongInputWheigt )
            {
                throw new BO.WrongInputWheigt(wheigt);
            }
            catch (DO.IdDoseNotExist x)
            {
                throw new BO.IdDoseNotExist(x.ObjectType, x.Id, x);
            }
            catch (DO.IdAlredyExist x)
            {
                throw new BO.IdAlredyExist(x.ObjectType, x.Id, x);

            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new BO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
    }
}

