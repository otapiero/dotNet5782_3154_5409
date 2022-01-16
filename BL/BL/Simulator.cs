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
        int droneId;
        Action<string, int> action;
        Func<bool> stop;

        Random random = new Random();
        private int DELAY = 1000;
        private int SPEED = 1;

        public Simulator(BL _bl, int id, Action<string, int> action, Func<bool> stop)
        {
            bl = _bl;
            droneId = id;
            this.action = action;
            this.stop = stop;



            BO.DroneBL drone;
            DateTime? startCharging = null;

            while (stop.Invoke() == false)
            {
                lock (bl)
                {
                    drone = bl.SearchDrone(id); 
                }

                switch (drone.status)
                {

                    case BO.DroneStatuses.Available:

                        try  
                        {
                            lock (bl)
                            {
                                bl.AssignPackageToDrone(id);
                                drone = bl.SearchDrone(id);
                            }
                            Thread.Sleep(DELAY);
                            action("Associate", drone.parcel.Id);
                        }
                        catch (Exception ex) // If an exception is thrown there can be 2 reasons: either there are no packages or there is no battery
                        {
                            if (ex.Message == "There is no package for the Drone")
                            {
                                IEnumerable<BO.ParcelToList> allParcels;
                                lock (bl)
                                {
                                    allParcels = BL.DisplayPackageListWithoutDrone();
                                }
                                var Packages = (from p in allParcels
                                                where (int)p.Weight <= (int)drone.MaxWeight
                                                select p);

                                if (Packages.Count() > 0) // If there are still packages it can take then there is no battery
                                {
                                    lock (BL)
                                    {
                                        try
                                        {
                                            BL.ChargeDrone(id);
                                        }
                                        catch (Exception exe) // If there are no free charging stations at the nearest station or there are no stations at all with free stations we will wait a bit and try again
                                        {
                                            if (exe.Message == "There are no available charging stations at the nearest station" || ex.Message == "The drone can not reach the station, Not enough battery")
                                                Thread.Sleep(7*DELAY);
                                        }
                                    }
                                    Thread.Sleep(DELAY);

                                    int i = 0;
                                    foreach (var item in BL.DisplayStationList()) // A search of the drone's ID station
                                    {
                                        i = item.ID;
                                        if (BL.GetStationWithDrones(item.ID).ChargingDronesList.Any(d => d.ID == drone.ID) == true) break;
                                    }
                                    action("charging", i);

                                }
                                else        // Else- no packages, activating the function that will add packages randomly
                                {
                                    action("No packages", 0);
                                    Thread.Sleep(3 * DELAY);
                                }
                            }
                        }
                        break;




                    case BO.DroneStatuses.Maintenace:


                        lock (bl)
                        {
                            if (drone.Battery == 100)  
                            {
                                int i = 0;
                                foreach (var item in bl.ListStation())
                                {
                                    i = item.Id;
                                    if (bl.SearchStation(item.Id).dronesInCharges.Any(d => d.Id == drone.Id) == true) break;
                                }
                                bl.RelesaeDroneFromCharge(id,);
                                startCharging = null;

                                action("Finish charging", i);
                                Thread.Sleep(DELAY);
                                break;
                            }
                            bl.UpdateBattery(drone.Id, startCharging); // If the battery is not 100, update the battery according to the last time we updated (and not according to charging time)
                        }
                        startCharging = DateTime.Now;  // Last battery update time
                        action("Battery and location", 0);
                        Thread.Sleep(DELAY);
                        break;



                    case BO.DroneStatuses.Delivery:


                        double lonPlus, latPlus, lonMinusLon, latMinusLat;
                        BO.ParcelBl package = bl.SearchParcel(drone.parcel.Id);

                        switch (drone.parcel.statusDelivrery)
                        {
                            case false:

                                // For updating the drone position every second, one should check how much to add on each progression of a second (kilometer)
                                latMinusLat = drone.DronePackageProcess.CollectLocation.Latitude - drone.DroneLocation.Latitude;
                                lonMinusLon = drone.DronePackageProcess.CollectLocation.Longitude - drone.DroneLocation.Longitude;

                                latPlus = latMinusLat / drone.DronePackageProcess.Distance * SPEED;
                                lonPlus = lonMinusLon / drone.DronePackageProcess.Distance * SPEED;

                                while (drone.DronePackageProcess.Distance > 0)
                                {
                                    Thread.Sleep(DELAY);

                                    lock (BL)
                                    {
                                        if (drone.DronePackageProcess.Distance > 1) // If the distance to the current destination is greater than 1 then there will be a location and battery update
                                        {
                                            BL.UpdateDroneLocation(drone.ID, lonPlus, latPlus);
                                            BL.UpdateLessBattery(drone.ID, BL.BatteryByKM(-1, 1 * SPEED));
                                        }
                                        else break;

                                        drone = BL.DisplayDrone(drone.ID);
                                    }
                                    action("Battery and location", 0);
                                }

                                lock (BL)
                                {
                                    BL.PickedUpByDrone(drone.ID); // After the loop there will be a collection
                                }
                                action("PickedUp", drone.DronePackageProcess.Id);
                                break;


                            case .OnGoing:

                                // For updating the drone position every second, one should check how much to add on each progression of a second (kilometer)
                                latMinusLat = drone.DronePackageProcess.DestinationLocation.Latitude - drone.DronePackageProcess.CollectLocation.Latitude;
                                lonMinusLon = drone.DronePackageProcess.DestinationLocation.Longitude - drone.DronePackageProcess.CollectLocation.Longitude;

                                latPlus = latMinusLat / drone.DronePackageProcess.Distance * SPEED;
                                lonPlus = lonMinusLon / drone.DronePackageProcess.Distance * SPEED;

                                while (drone.DronePackageProcess.Distance > 0)
                                {
                                    Thread.Sleep(DELAY);

                                    lock (BL)
                                    {
                                        if (drone.DronePackageProcess.Distance > 1) // If the distance to the current destination is greater than 1 then there will be a location and battery update
                                        {
                                            BL.UpdateDroneLocation(drone.ID, lonPlus, latPlus);
                                            BL.UpdateLessBattery(drone.ID, BL.BatteryByKM((int)drone.DronePackageProcess.Weight, 1 * SPEED));
                                        }
                                        else break;

                                        drone = BL.DisplayDrone(drone.ID);
                                    }
                                    action("Battery and location", 0);
                                }

                                lock (BL)
                                {
                                    BL.DeliveredToClient(drone.ID); // After the loop will be delivery
                                }
                                action("Delivered", drone.DronePackageProcess.Id);
                                Thread.Sleep(DELAY);

                                break;

                        }
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
