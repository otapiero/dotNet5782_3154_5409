using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System.Diagnostics;
namespace BL
{
    /// <summary>
    /// The simulator.
    /// </summary>
    class Simulator
    {
        BL bl;
        BO.DroneBL drone;
        int id;
        Func<bool> stop;
        Action action;
        const int delay= 500;
        double lonPlus, latPlus, lonMinusLon, latMinusLat;
        /// <summary>
        /// Initializes a new instance of the <see cref="Simulator"/> class.
        /// </summary>
        /// <param name="_bl">The _bl.</param>
        /// <param name="_id">The _id.</param>
        /// <param name="_stop">The _stop.</param>
        /// <param name="report">The report.</param>
        public Simulator(BL _bl,int _id, Func<bool> _stop, Action report)
        {
            bl=_bl;
            id=_id;
            stop=_stop;
            action=report;
            try
            {
                RunSimulator();
            }
            catch (BO.NoParcelAvilable)
            {
                throw new BO.NoParcelAvilable();
            }
            catch (BO.BatteryExaption ex)
            {
                throw new BO.BatteryExaption(ex.Message, ex.MinumumBattery);
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
        }

        /// <summary>
        /// Runs the simulator.
        /// </summary>
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
                            Thread.Sleep(delay*6);
                            action();
                            //action(drone.Id);
                        }
                        catch (BO.BatteryExaption)
                        {
                            sendToCharge();
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
                             if (drone.parcel.statusDelivrery)
                                {
                                    Deliver();
                                Thread.Sleep(delay);
                                action();
                                    
                                }
                                else
                                {
                                    Colecte();
                                    action();
                                }
                            }
                        
                        catch (BO.IdDoseNotExist x)
                        {
                            throw new BO.IdDoseNotExist(x.Message, x.ObjectType, x.Id);
                        }
                        catch (BO.BatteryExaption ex)
                        {
                            throw new BO.BatteryExaption(ex.Message, ex.MinumumBattery);
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
                                bl.BatteryPlus(id, 7.5348);
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
                        catch (BO.BatteryExaption ex)
                        {
                            throw new BO.BatteryExaption(ex.Message, ex.MinumumBattery);
                        }
                }

            }
        }
        /// <summary>
        /// sends the drone to charge.
        /// </summary>
        void sendToCharge()
        {
            try
            {
                lock (bl)
                {
                    bl.SendDroneToCharge(id);
                }
                Thread.Sleep(delay);
                action();
                
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
        /// <summary>
        /// Colectes the parcel
        /// </summary>
        void Colecte()
        {
            try
            {

                latMinusLat = drone.parcel.CollectionLocation.Lattitude - drone.CurrentLocation.Lattitude;
                lonMinusLon = drone.parcel.CollectionLocation.Longitude - drone.CurrentLocation.Longitude;
                latPlus = latMinusLat / 12;
                lonPlus = lonMinusLon / 12;

                for (int i = 0; i < 12; i++)
                {
                    lock (bl)
                    {
                        bl.UpdateDroneLocation(drone.Id, lonPlus, latPlus);
                        bl.BatteryMinus(drone.Id, Math.Sqrt(Math.Pow(lonPlus, 2) + Math.Pow(latPlus, 2)));
                        drone=bl.SearchDrone(id);
                    }
                    Thread.Sleep(delay);
                    action();
                    
                }
                lock (bl)
                {
                    bl.CollectPackage(id);
                }
                Thread.Sleep(delay);
                action();
                

            }

            catch (BO.IdDoseNotExist x)
            {
                throw new BO.IdDoseNotExist(x.Message, x.ObjectType, x.Id);
            }
            catch (BO.BatteryExaption ex)
            {
                throw new BO.BatteryExaption(ex.Message, ex.MinumumBattery);
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
        /// <summary>
        /// Delivers the parcel
        /// </summary>
        void Deliver()
        {
            try
            {

                latMinusLat = drone.parcel.DeliveryLocation.Lattitude - drone.CurrentLocation.Lattitude;
                lonMinusLon = drone.parcel.DeliveryLocation.Longitude - drone.CurrentLocation.Longitude;
                latPlus = latMinusLat / 12;
                lonPlus = lonMinusLon / 12;

                for (int i = 0; i < 12; i++)
                {
                    lock (bl)
                    {
                        bl.UpdateDroneLocation(drone.Id, lonPlus, latPlus);
                        bl.BatteryMinus(drone.Id, Math.Sqrt(Math.Pow(lonPlus, 2) + Math.Pow(latPlus, 2)));
                        drone=bl.SearchDrone(id);
                    }
                    Thread.Sleep(delay);
                    action();
                    

                }
                lock (bl)
                {
                    bl.DeliverPackage(id);
                }
                Thread.Sleep(delay);
                action();
                

            }
            catch (BO.IdDoseNotExist x)
            {
                throw new BO.IdDoseNotExist(x.Message, x.ObjectType, x.Id);
            }
            catch (BO.WrongStatusObject x)
            {
                throw new BO.WrongStatusObject(x.ObjectType, x.Id, x.Message);
            }
            catch (BO.BatteryExaption ex)
            {
                throw new BO.BatteryExaption(ex.Message, ex.MinumumBattery);
            }
            catch (DO.XMLFileLoadCreateException x)
            {
                throw new BO.XMLFileLoadCreateException(x.XmlFilePath, x.Message, x.InnerException);
            }
        }
    }
}