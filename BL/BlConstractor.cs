using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{

    public partial class BL : IBL
    {
        private IDAL.IDal idal;
        private List<DroneToList> DronesBl = new();

        public BL()
        {ghfg
            Random rand = new();
            idal = new DalObject.DalObject();
            //data lists
            List<IDAL.DO.Drone> dronesData = (List<IDAL.DO.Drone>)idal.AllDrones();
            List<IDAL.DO.Parcel> parcelsData = (List<IDAL.DO.Parcel>)idal.AllParcels();
            List<IDAL.DO.Costumer> costumerData = (List<IDAL.DO.Costumer>)idal.AllCustomers();
            List<IDAL.DO.Station> stationData = (List<IDAL.DO.Station>)idal.AllStation();

            ;
            Double[] vs = idal.ElectricityUse();
            double Avilable = vs[0];
            double Light = vs[1];
            double Intermidiate = vs[2];
            double Heavy = vs[3];
            double chargingRate = vs[4];
            ; //Filter parcel


            List<IDAL.DO.Parcel> parcelsNotDelivred = idal.ListOfParcels(x => ((x.DroneId != 0) &&(x.Delivered==null))).ToList();

            foreach (var x in parcelsNotDelivred)
            {

                IDAL.DO.Drone tempDlDrone = dronesData.Find(z => z.Id.Equals(x.DroneId));
                if (tempDlDrone.Id!=0)
                {
                    IDAL.DO.Costumer senderCostumer = idal.SearchCostumer(x.Sender);
                    IDAL.DO.Costumer targetCostumer = idal.SearchCostumer(x.TargetId);
                    BO.Location senderLocation = new(senderCostumer.Longitude, senderCostumer.Lattitude);
                    BO.Location targetLocation = new(targetCostumer.Longitude, targetCostumer.Lattitude);
                    BO.DroneToList temp = new();
                    temp.Id = tempDlDrone.Id;
                    temp.Model = tempDlDrone.Model;

                    temp.Weight = (BO.WeightCategories)x.Wheight;

                    temp.status = BO.DroneStatuses.Delivery;
                    if (x.PickedUp == new DateTime())
                    {
                        temp.CurrentLocation = FindTheClosestStation(senderLocation, stationData);
                        double min = DistanceLocation(temp.CurrentLocation, senderLocation);
                        min += DistanceLocation(targetLocation, senderLocation);
                        min += DistanceLocation(targetLocation, FindTheClosestStation(targetLocation, stationData));
                        min *= vs[(int)temp.Weight + 1];
                        if (min < 100)
                            temp.Battery = min + rand.NextDouble() * (100 - min);
                        else
                            temp.Battery = 50;

                    }
                    else
                    {

                        temp.CurrentLocation = new BO.Location(senderCostumer.Longitude, senderCostumer.Lattitude);

                        double min = DistanceLocation(temp.CurrentLocation, new BO.Location(targetCostumer.Longitude, targetCostumer.Lattitude));
                        min += DistanceLocation(new BO.Location(targetCostumer.Longitude, targetCostumer.Lattitude), FindTheClosestStation(new BO.Location(targetCostumer.Longitude, targetCostumer.Lattitude), stationData));
                        min *= vs[(int)temp.Weight + 1];
                        if (min < 100)
                            temp.Battery = min + rand.NextDouble() * (100 - min);
                        else
                            temp.Battery = 50;
                    }


                    temp.ParcelId = x.Id;
                    DronesBl.Add(temp);
                }
            }


            List<IDAL.DO.Drone> freeDrones = (from t in dronesData
                                                                   where (false == parcelsNotDelivred.Exists(y => y.DroneId == t.Id))
                                                                   select t).ToList();
            
            List<IDAL.DO.Parcel> parcelsDelivred = idal.ListOfParcels(x=>x.DroneId != 0 && x.Delivered != null).ToList();


            foreach (var x in freeDrones)
            {
                int randomcase = rand.Next(0, 2);
                BO.DroneToList temp = new();

                temp.Id = x.Id;
               
                temp.Model = x.Model;
                temp.Weight = (BO.WeightCategories)rand.Next(3);
                temp.ParcelId = 0;

                if (randomcase==0)
                {
                    temp.status = BO.DroneStatuses.Maintenace;
                    int i = rand.Next(stationData.Count());
                    temp.CurrentLocation = new(stationData[i].Longitude, stationData[i].Lattitude);
                    temp.Battery = rand.NextDouble() * 20;
                }
                else
                {

                    temp.status = BO.DroneStatuses.Available;
                    int i = rand.Next(parcelsDelivred.Count());
                    IDAL.DO.Costumer targetCostumer = idal.SearchCostumer(parcelsDelivred[i].TargetId);

                    temp.CurrentLocation = new(targetCostumer.Longitude, targetCostumer.Lattitude);
                    double min = DistanceLocation(temp.CurrentLocation, FindTheClosestStation(temp.CurrentLocation, stationData))* Avilable;
                    temp.Battery = min+rand.NextDouble()*(100-min);
                }
                DronesBl.Add(temp);
            }

        }



        private static double DistanceLocation(BO.Location x, BO.Location y)
        {
            double locX = Math.Pow(x.Lattitude - y.Lattitude, 2);
            double locY = Math.Pow(x.Longitude - y.Longitude, 2);
            double dis = Math.Sqrt(locX +locY);
            return dis;
        }

        private static BO.Location FindTheClosestStation(BO.Location x, List<IDAL.DO.Station> stations)
        {
            double tempDistance, dis = DistanceLocation(x, new BO.Location(stations[0].Longitude, stations[0].Lattitude));
            BO.Location stationlocation = new(stations[0].Longitude, stations[0].Lattitude);
            foreach (var y in stations)
            {
                tempDistance= DistanceLocation(x, new BO.Location(y.Longitude, y.Lattitude));
                if (tempDistance<dis)
                {
                    dis = tempDistance;
                    stationlocation = new BO.Location(y.Longitude, y.Lattitude);
                }
            }
            return stationlocation;
        }
    }
}

