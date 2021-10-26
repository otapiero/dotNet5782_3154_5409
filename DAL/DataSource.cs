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
        internal static void Initialize()
        {
           
            int numDrones = r.Next(5,10) ;
            int numCustomers = r.Next(10, 100);
            int numParcels = r.Next(10, 1000);
            int numStations = r.Next(2, 5);
            for (int i = 0; i < numDrones; i++)
            {
                drones[i] = new(GetRandomId(), GetRandomName(), GetRandomWeight(),
                  GetRandomDroneStatuses(), r.NextDouble() * 100);
                Config.dronesIndex++;
            }
           
            for (int i = 0; i < numCustomers; i++)
            {
                customers[i] = new(GetRandomId(),GetRandomName(),
                    GetRandomPhone(),r.NextDouble()*100, r.NextDouble() * 100);
                Config.customersIndex++;
            }
            for (int i = 0; i < numParcels; i++)
            {
                parcels[i] = new(GetRandomId(), GetRandomId(), GetRandomId(), GetRandomWeight(),
                    GetRandomPriorities(), DateTime.Now,GetRandomId(), DateTime.Now, DateTime.Now, DateTime.Now) ;
                Config.parcelsIndex++;
            }
            for (int i = 0; i <  numStations; i++)
            {
                stations[i] = new(GetRandomId(),GetRandomId(), r.NextDouble() * 100,
                    r.NextDouble() * 100,r.Next(0,101));
                Config.stationsIndex++;
            }
            static  string GetRandomPhone()
            {
                string phone = "058";
                
                string[] numbers = {"0123465789" };
                int index;
                for (int i = 0; i < 8; i++)
                {
                    index = r.Next(0, 11);
                    phone += numbers[index];
                }
                return phone;
            }
            static int GetRandomId()
            {
                int id = r.Next(100000, 999999);
                return id;
            }
            static string GetRandomName()
            {
                string name = "A" + (r.Next(0, 26));

                string[] letters = { "azertyuiopqsdfghjklmwxcvbn" };
                int index;
                for (int i = 0; i < 4; i++)
                {
                    index = r.Next(0, 26);
                    name += letters[index];
                }
                return name;
            }
            static  IDAL.DO.Priorities GetRandomPriorities()
            {  
                Type type = typeof(IDAL.DO.Priorities);
                Array values = type.GetEnumValues();
              
                int index = r.Next(values.Length);
                IDAL.DO.Priorities value = (IDAL.DO.Priorities)values.GetValue(index);
                return value;
            }
            static IDAL.DO.WeightCategories GetRandomWeight()
            {
                Type type = typeof(IDAL.DO.WeightCategories);
                Array values = type.GetEnumValues();
              
                int index = r.Next(values.Length);
                IDAL.DO.WeightCategories value = (IDAL.DO.WeightCategories)values.GetValue(index);
                return value;
            }
            static IDAL.DO.DroneStatuses GetRandomDroneStatuses()
            {

                Type type = typeof(IDAL.DO.DroneStatuses);
                Array values = type.GetEnumValues();
                
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
