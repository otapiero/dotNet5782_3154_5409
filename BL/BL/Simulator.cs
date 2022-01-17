using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System.Diagnostics;
namespace BL
{
    class Simulator
    {
        BL bl;
        BO.DroneBL drone;
        int id;
        Func<bool> stop;
       // Action< int> action;
        const int delay=100;
        const int speed = 1;
        double lonPlus, latPlus, lonMinusLon, latMinusLat;
        public Simulator(BL _bl,int _id, Func<bool> _stop)
        {
            bl=_bl;
            id=_id;
            stop=_stop;
           // action=_action;
            
            RunSimulator();
        } 
        void RunSimulator()
        {
            while(stop.Invoke()==false)
            {
                lock (bl)
                {
                    drone = bl.SearchDrone(id);
                }
                switch(drone.status)
                {
                    case BO.DroneStatuses.Available:
                        try
                        {
                            lock (bl)
                            {
                                bl.AssignPackageToDrone(id);
                                drone = bl.SearchDrone(id);
                            }
                            Thread.Sleep(delay);
                            //action(drone.Id);
                        }
                        catch (BO.BatteryExaption)
                        {
                            sendToCharge();
                            Thread.Sleep(delay);
                           //action(id);
                        }
                        #region other catches
                        catch (BO.NoParcelAvilable )
                        {
                            throw new BO.NoParcelAvilable();
                        }
                        catch (BO.IdDoseNotExist x)
                        {
                            throw new BO.IdDoseNotExist(x.Message, x.ObjectType, x.Id);
                        }
                        catch (BO.WrongStatusObject x)
                        {
                            throw new BO.WrongStatusObject(x.ObjectType, x.Id, x.Error);
                        }
                        catch (BO.NoStationAvailable x)
                        {
                            throw new BO.NoStationAvailable(x.ObjectType, x.Id);
                        }
                        catch (BO.XMLFileLoadCreateException x)
                        {
                            throw new BO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
                        }
                        #endregion
                        break;
                    case BO.DroneStatuses.Delivery:
                        try
                        {
                            lock (bl)
                            {
                                if (drone.parcel.statusDelivrery)
                                {
                                    Deliver();
                                    Thread.Sleep(delay);
                                    //action(drone.Id);
                                }
                                else
                                {
                                    Colecte();
                                    Thread.Sleep(delay);
                                 //   action(drone.Id);
                                }
                            }
                        }
                        catch (BO.IdDoseNotExist x)
                        {
                            throw new BO.IdDoseNotExist(x.Message, x.ObjectType, x.Id);
                        }
                        catch (BO.WrongStatusObject x)
                        {
                            throw new BO.WrongStatusObject(x.ObjectType, x.Id, x.Message);
                        }
                        catch (DO.XMLFileLoadCreateException x)
                        {
                            throw new BO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
                        }
                        break;
                    case BO.DroneStatuses.Maintenace:
                        try
                        {
                            if (drone.Battery<95)
                            {
                                bl.BatteryPlus(id, 5);
                                Thread.Sleep(delay);
                            }
                            else
                            {
                                bl.RelesaeDroneFromCharge(id, 10);
                                Thread.Sleep(delay);
                            }
                            break;
                        }
                        catch (BO.IdDoseNotExist x)
                        {
                            throw new BO.IdDoseNotExist(x.ObjectType, x.Id, x);
                        }
                        catch (BO.XMLFileLoadCreateException x)
                        {
                            throw new BO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
                        }
                        catch(BO.WrongStatusObject x)
                        {
                            throw new BO.WrongStatusObject(x.ObjectType, x.Id, x.Error);
                        }
                }

            }
        }
        void sendToCharge()
        {
            try
            {
                lock (bl)
                {
                    bl.SendDroneToCharge(id);
                }
            }
            catch (BO.WrongStatusObject x)
            {
                throw new BO.WrongStatusObject(x.ObjectType, x.Id, x.Error);
            }
            catch (BO.NoStationAvailable ex)
            {
                throw new BO.NoStationAvailable(ex.ObjectType, ex.Id);
            }
            catch (BO.BatteryExaption ex)
            {
                throw new BO.BatteryExaption(ex.Message, ex.MinumumBattery);
            }
        }
        void Colecte()
        {
            try
            {
                lock (bl)
                {
                    if (drone.parcel.DistanceDelivrery>1.5)
                    {
                        latMinusLat = drone.CurrentLocation.Lattitude - drone.parcel.CollectionLocation.Lattitude;
                        lonMinusLon = drone.CurrentLocation.Longitude - drone.parcel.CollectionLocation.Longitude;
                        latPlus = latMinusLat / drone.parcel.DistanceDelivrery;
                        lonPlus = lonMinusLon / drone.parcel.DistanceDelivrery;
                        bl.UpdateDroneLocation(drone.Id, lonPlus, latPlus);
                        bl.BatteryMinus(drone.Id, Math.Sqrt(Math.Pow(lonPlus, 2) + Math.Pow(latPlus, 2)));
                    }
                    else
                    {
                        bl.CollectPackage(id);
                        //action(id);
                    }
                }
            }
            catch (BO.IdDoseNotExist x)
            {
                throw new BO.IdDoseNotExist(x.Message, x.ObjectType, x.Id);
            }
            catch (BO.WrongStatusObject x)
            {
                throw new BO.WrongStatusObject(x.ObjectType, x.Id, x.Message);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new BO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
        void Deliver()
        {
            try
            {
                lock (bl)
                {
                    if (drone.parcel.DistanceDelivrery>1.5)
                    {
                        latMinusLat = drone.CurrentLocation.Lattitude - drone.parcel.DeliveryLocation.Lattitude;
                        lonMinusLon = drone.CurrentLocation.Longitude - drone.parcel.DeliveryLocation.Longitude;

                        latPlus = latMinusLat / drone.parcel.DistanceDelivrery;
                        lonPlus = lonMinusLon / drone.parcel.DistanceDelivrery;
                        bl.UpdateDroneLocation(drone.Id, lonPlus, latPlus);
                        bl.BatteryMinus(drone.Id, Math.Sqrt(Math.Pow(lonPlus, 2) + Math.Pow(latPlus, 2)));
                    }
                    else
                    {
                        bl.DeliverPackage(id); 
                    }
                }
            }
            catch (BO.IdDoseNotExist x)
            {
                throw new BO.IdDoseNotExist(x.Message, x.ObjectType, x.Id);
            }
            catch (BO.WrongStatusObject x)
            {
                throw new BO.WrongStatusObject(x.ObjectType, x.Id, x.Message);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new BO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
    }
}