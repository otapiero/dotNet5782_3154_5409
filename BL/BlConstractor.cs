using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
   
    public partial class BL
    {
        IDAL.IDal idal;
        List<BO.DroneToList> DronesBl = new List<BO.DroneToList>();




        public BL()
        {
            Random rand = new();
            idal = new DalObject.DalObject();
            List<IDAL.DO.Drone> dronesData = (List<IDAL.DO.Drone>)idal.AllDrones();
            List<IDAL.DO.Parcel> parcelsData = (List<IDAL.DO.Parcel>)idal.AllParcels();
            List<IDAL.DO.Costumer> costumerData = (List<IDAL.DO.Costumer>)idal.AllCustomers();
            List<IDAL.DO.Station> stationData = (List<IDAL.DO.Station>)idal.AllStation();
            Double[] vs = idal.ElectricityUse();
            double Avilable = vs[0];
            double Light = vs[1];
            double Intermidiate = vs[2];
            double Heavy = vs[3];
            double chargingRate = vs[4];
            //Filter parcel
            List<IDAL.DO.Parcel> parcelsNotDelivred = (List<IDAL.DO.Parcel>)(from x in parcelsData
                                                                                    where x.DroneId != 0 && x.Delivered != new DateTime()
                                                                                    select x);
            foreach (var x in parcelsNotDelivred)
            {
                IDAL.DO.Drone tempDlDrone = dronesData.Find(z => z.Id.Equals(x.DroneId));
                IDAL.DO.Costumer senderCostumer = costumerData.Find(z => z.Id.Equals(x.Sender));
                IDAL.DO.Costumer targetCostumer = costumerData.Find(z => z.Id.Equals(x.TargetId));
                BO.Location senderLocation = new BO.Location(senderCostumer.Longitude, senderCostumer.Lattitude);
                BO.Location targetLocation = new BO.Location(targetCostumer.Longitude, targetCostumer.Lattitude);
                BO.DroneToList temp = new();
                temp.Id = tempDlDrone.Id;
                temp.Model = tempDlDrone.Model;

                temp.Weight = (BO.WeightCategories)x.Wheight;

                temp.status = BO.DroneStatuses.Delivery;
                if (x.PickedUp == new DateTime())
                {
                    temp.CurrentLocation = findTheClosestStation(senderLocation, stationData);
                    double min = distance(temp.CurrentLocation, senderLocation);
                    min += distance(targetLocation, senderLocation);
                    min += distance(targetLocation, findTheClosestStation(targetLocation, stationData));
                    min *= vs[(int)temp.Weight + 1];
                    if (min < 100)
                        temp.Battery = min + rand.NextDouble() * (100 - min);
                    else
                        temp.Battery = 50;
                }
                else
                {

                    temp.CurrentLocation = new BO.Location(senderCostumer.Longitude, senderCostumer.Lattitude);

                    double min = distance(temp.CurrentLocation, new BO.Location(targetCostumer.Longitude, targetCostumer.Lattitude));
                    min += distance(new BO.Location(targetCostumer.Longitude, targetCostumer.Lattitude), findTheClosestStation(new BO.Location(targetCostumer.Longitude, targetCostumer.Lattitude), stationData));
                    min *= vs[(int)temp.Weight + 1];
                    if (min < 100)
                        temp.Battery = min + rand.NextDouble() * (100 - min);
                    else
                        temp.Battery = 50;
                }


                temp.NumParcels = x.Id;
                DronesBl.Add(temp);

            }


            List<IDAL.DO.Drone> freeDrones = (List<IDAL.DO.Drone>)(from t in dronesData
                                                                   where (false == parcelsNotDelivred.Exists(y => y.DroneId == t.Id))
                                                                   select t);
            foreach(var x in freeDrones)
            {
                int randomcase = rand.Next(0, 2);
                if(randomcase==0)
                {
                    //tahzuka
                }
                else
                { 

                }
            }

        }


        double distance(BO.Location x, BO.Location y)
        {
            double locX = Math.Pow(x.Lattitude - y.Lattitude, 2);
            double locY = Math.Pow(x.Longitude - y.Longitude, 2);
            double dis = Math.Sqrt(locX +locY );
            return dis;
        }
        BO.Location findTheClosestStation(BO.Location x,List<IDAL.DO.Station > stations)
        {
            double tempDistance, dis = distance(x, new BO.Location(stations[0].Longitude, stations[0].Longitude));
            BO.Location stationlocation = new BO.Location(stations[0].Longitude, stations[0].Longitude);
            foreach(var y in stations)
            {
                tempDistance=distance(x, new BO.Location(y.Longitude, y.Lattitude));
                if(tempDistance<dis)
                {
                    dis = tempDistance;
                    stationlocation = new BO.Location(y.Longitude, y.Lattitude);
                }
            }
            return stationlocation;
        }
    }
}

