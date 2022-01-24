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
        Action action;
        const int delay= 500;
        const int speed = 1;
        double lonPlus, latPlus, lonMinusLon, latMinusLat;
        public Simulator(BL _bl,int _id, Func<bool> _stop, Action report)
        {
            bl=_bl;
            id=_id;
            stop=_stop;
            action=report;
            
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
                                action();
                            }
                            Thread.Sleep(delay);
                            //action(drone.Id);
                        }
                        catch (BO.BatteryExaption)
                        {
                            sendToCharge();
                            Thread.Sleep(delay);
                            action();
                           
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
                                    action();
                                }
                                else
                                {
                                    Colecte();
                                    Thread.Sleep(delay);
                                    action();
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
                            while (drone.Battery<95)
                            {
                                bl.BatteryPlus(id, 5);
                                drone=bl.SearchDrone(id);
                                Thread.Sleep(delay);
                                action();
                            }
                            
                            bl.RelesaeDroneFromCharge(id, 10);
                            drone=bl.SearchDrone(id);
                            Thread.Sleep(delay);
                            action();

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
                    action();
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
                    latMinusLat = drone.parcel.CollectionLocation.Lattitude - drone.CurrentLocation.Lattitude;
                    lonMinusLon = drone.parcel.CollectionLocation.Longitude - drone.CurrentLocation.Longitude;
                    latPlus = latMinusLat / drone.parcel.DistanceDelivrery;
                    lonPlus = lonMinusLon / drone.parcel.DistanceDelivrery;

                    while (drone.parcel.DistanceDelivrery>1.5)
                    {
                        action();
                        bl.UpdateDroneLocation(drone.Id, lonPlus, latPlus);
                        bl.BatteryMinus(drone.Id, Math.Sqrt(Math.Pow(lonPlus, 2) + Math.Pow(latPlus, 2)));
                        drone=bl.SearchDrone(id);
                        
                    }
                    
                    {
                        bl.CollectPackage(id);
                        action();
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
                    


                    latMinusLat = drone.parcel.DeliveryLocation.Lattitude - drone.CurrentLocation.Lattitude;
                    lonMinusLon = drone.parcel.DeliveryLocation.Longitude - drone.CurrentLocation.Longitude;
                    latPlus = latMinusLat / drone.parcel.DistanceDelivrery;
                    lonPlus = lonMinusLon / drone.parcel.DistanceDelivrery;

                    while (drone.parcel.DistanceDelivrery>1.5)
                    {
                        
                        
                        bl.UpdateDroneLocation(drone.Id, lonPlus, latPlus);
                        bl.BatteryMinus(drone.Id, Math.Sqrt(Math.Pow(lonPlus, 2) + Math.Pow(latPlus, 2)));
                        drone=bl.SearchDrone(id);
                        action();

                    }

                    bl.DeliverPackage(id);
                    action();
                   
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