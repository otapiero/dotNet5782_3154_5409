using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
   internal class DataSource
    {
        IDAL.DO.Drone[] drones = new IDAL.DO.Drone[10];
        IDAL.DO.Station[] stations = new IDAL.DO.Station[5];
        IDAL.DO.Customer[] customers = new IDAL.DO.Customer[100];
        IDAL.DO.Parcel[] parcels = new IDAL.DO.Parcel[1000];
        static Random r = new();
        internal void Initialize()
        {
           
            int numDrones = r.Next(5,10) ;
            int numCustomers = r.Next(10, 100);
            int numParcels = r.Next(10, 1000);
            int numStations = r.Next(2, 5);
            for (int i = 0; i < numDrones; i++)
            {
                drones[i] = new(
                    r.Next(0, 1000),
                Convert.ToString( 'A')+(r.Next(0, 25)),
                  GetRandomWeight(),
                   IDAL.DO.DroneStatuses.Available,
                     r.NextDouble());
                Config.dronesIndex++;
            }
           
            for (int i = 0; i < numCustomers; i++)
            {
                customers[i] = new();
            }
            for (int i = 0; i < numParcels; i++)
            {
                parcels[i] = new();
            }
            for (int i = 0; i <  numStations; i++)
            {
                stations[i] = new();
            }

            static  IDAL.DO.Priorities GetRandomPriorities()
            {
                
                Type type = typeof(IDAL.DO.Priorities);

                Array values = type.GetEnumValues();
                //Array values = Enum.GetValues(type);
                int index = r.Next(values.Length);
                IDAL.DO.Priorities value = (IDAL.DO.Priorities)values.GetValue(index);
                return value;
            }
            static IDAL.DO.WeightCategories GetRandomWeight()
            {

                Type type = typeof(IDAL.DO.WeightCategories);

                Array values = type.GetEnumValues();
                //Array values = Enum.GetValues(type);
                int index = r.Next(values.Length);
                IDAL.DO.WeightCategories value = (IDAL.DO.WeightCategories)values.GetValue(index);
                return value;
            }
            static IDAL.DO.DroneStatuses GetRandomDroneStatuses()
            {

                Type type = typeof(IDAL.DO.DroneStatuses);

                Array values = type.GetEnumValues();
                //Array values = Enum.GetValues(type);
                int index = r.Next(values.Length);
                IDAL.DO.DroneStatuses value = (IDAL.DO.DroneStatuses)values.GetValue(index);
                return value;
            }
        }

        internal class Config
        {
            internal static int dronesIndex = 0;
            internal static int stationsIndex = 0;
            internal static int customersIndex = 0;
            internal static int parcelsIndex = 0;
            internal static int IdDefault = 0;
        }
    }
}
