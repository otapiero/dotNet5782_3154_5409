using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
   internal static class DataSource
    {
        
        
        internal static List<IDAL.DO.DroneCharge> drone = new List<IDAL.DO.DroneCharge>();
        internal static List<IDAL.DO.Drone> drones = new List<IDAL.DO.Drone>();
        internal static List<IDAL.DO.Station> stations = new List<IDAL.DO.Station>();
        internal static List<IDAL.DO.Customer> customers = new List<IDAL.DO.Customer>();
        internal static List<IDAL.DO.Parcel> parcels = new List<IDAL.DO.Parcel>();
        internal static List<IDAL.DO.DroneCharge> DroneCharges = new List<IDAL.DO.DroneCharge>();

        static Random r = new();
        /*internal static void Initialize()
        {


            int numDrones = r.Next(5, 10);
            int numCustomers = r.Next(10, 100);
            int numParcels = r.Next(10, 1000);
            int numStations = r.Next(2, 5);
            for (int i = 0; i < numDrones; i++)
            {

                drones.Add(new IDAL.DO.Drone(GetRandomId(), GetRandomName(), (IDAL.DO.WeightCategories)r.Next(0, 3),
                  (IDAL.DO.DroneStatuses)r.Next(0, 3), r.NextDouble() * 100));

            }

            for (int i = 0; i < numCustomers; i++)
            {
                customers.Add(new(Config.IdDefault++, GetRandomName(),
                    GetRandomPhone(), r.NextDouble() * 100, r.NextDouble() * 100));

            }
            for (int i = 0; i < numParcels; i++)
            {
                parcels.Add(new(GetRandomId(), GetRandomId(), GetRandomId(), (IDAL.DO.WeightCategories)r.Next(0, 3),
                    (IDAL.DO.Priorities)r.Next(0, 3), DateTime.Now, GetRandomId(), DateTime.Now, DateTime.Now, DateTime.Now));

            }
            for (int i = 0; i < numStations; i++)
            {
                stations.Add(new(GetRandomId(), GetRandomId(), r.NextDouble() * 100,
                    r.NextDouble() * 100, r.Next(0, 101)));

            }
            static string GetRandomPhone()
            {
                string phone = "058";

                string[] numbers = { "0123465789" };
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
        }*/
            
        internal class Config
        {
            
            internal static int IdDefault = 1;
        }
    }
}
