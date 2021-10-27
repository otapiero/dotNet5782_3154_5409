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
        internal static void Initialize()
        {

            
            int numDrones = r.Next(5, 10);
            int numCustomers = r.Next(10, 100);
            int numParcels = r.Next(10, 1000);
            int numStations = r.Next(2, 5);
            for (int i = 0; i < numDrones; i++)
            {
                IDAL.DO.Drone newDrone = new IDAL.DO.Drone();
                newDrone.Id = Config.idObject++;
                newDrone.Model = GetRandomName();
                newDrone.MaxWheight = (IDAL.DO.WeightCategories)r.Next(0, 3);
                newDrone.Status = (IDAL.DO.DroneStatuses)r.Next(0, 3);
                newDrone.Battery= r.NextDouble() * 100;
                drones.Add(newDrone);
            }

            for (int i = 0; i < numCustomers; i++)
            {
                IDAL.DO.Customer newCustomer = new IDAL.DO.Customer();
                newCustomer.Id = Config.IdDefault++;
                newCustomer.Name = GetRandomName();
                newCustomer.Phone = GetRandomPhone();
                newCustomer.Longitude = r.NextDouble() * 100;
                newCustomer.Lattitude = r.NextDouble() * 100;
                customers.Add(newCustomer);

            }
            for (int i = 0; i < numParcels; i++)
            {
             
                IDAL.DO.Parcel newParcel = new IDAL.DO.Parcel();
                newParcel.Id = Config.idObject++;
                newParcel.Sender = r.Next(1, numCustomers);
                newParcel.TargetId = r.Next(1, numCustomers);
                newParcel.Wheight = (IDAL.DO.WeightCategories)r.Next(0, 3);
                newParcel.Priority = (IDAL.DO.Priorities)r.Next(0, 3);
                newParcel.Requsted = DateTime.Now;
                newParcel.DroneId = r.Next(1, numDrones);
                newParcel.Scheduled = DateTime.Now;
                newParcel.PickedUp = DateTime.Now;
                newParcel.Delivered = DateTime.Now;
                parcels.Add(newParcel);

            }
            for (int i = 0; i < numStations; i++)
            {
             
                IDAL.DO.Station newStation = new IDAL.DO.Station();
                newStation.Id = Config.idObject++;
                newStation.Name = GetRandomName();
                newStation.Longitude = r.NextDouble() * 100;
                newStation.Lattitude = r.NextDouble() * 100;
                newStation.ChargeSlots = r.Next(0, 101);
                stations.Add(newStation);
            }
            static string GetRandomPhone()
            {
                string phone = "058";

                string numbers =  "0123465789" ;
                int index;
                for (int i = 0; i < 8; i++)
                {
                    index = r.Next(0, 10);
                    phone += numbers[index];
                }
                return phone;
            }
            
            static string GetRandomName()
            {
                int index;
                string name="";
                string letters = "azertyuiopqsdfghjklmwxcvbn" ;
               
                for (int i = 0; i < 4; i++)
                {
                    index = r.Next(0, 25);
                    name += letters[index];
                }
             
                return name;
            }
        }
            
        internal class Config
        {
            internal static int idObject = 0;
            internal static int IdDefault = 1;
        }
    }
}
