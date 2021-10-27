using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
   ///<summary>Class - initialize class for the program</summary>
   internal static class DataSource
    {
        
        ///<summary>list of data for the programing<summary>
        internal static List<IDAL.DO.DroneCharge> drone = new List<IDAL.DO.DroneCharge>();
        internal static List<IDAL.DO.Drone> drones = new List<IDAL.DO.Drone>();
        internal static List<IDAL.DO.Station> stations = new List<IDAL.DO.Station>();
        internal static List<IDAL.DO.Customer> customers = new List<IDAL.DO.Customer>();
        internal static List<IDAL.DO.Parcel> parcels = new List<IDAL.DO.Parcel>();
        internal static List<IDAL.DO.DroneCharge> DroneCharges = new List<IDAL.DO.DroneCharge>();
        //var random for initialize function
        static Random r = new();
        //Initialize function
        internal static void Initialize()
        {
            //Set quantity of var 
            int numDrones = r.Next(5, 10);
            int numCustomers = r.Next(10, 100);
            int numParcels = r.Next(10, 1000);
            int numStations = r.Next(2, 5);
            //Initialize random drones
            for (int i = 0; i < numDrones; i++)
            {
                IDAL.DO.Drone newDrone = new IDAL.DO.Drone();
                newDrone.Id = Config.idObject++;
                IDAL.DO.Drone newDrone = new IDAL.DO.Drone();
                newDrone.Id = Config.idDrone++;
                newDrone.Model = GetRandomName();
                newDrone.MaxWheight = (IDAL.DO.WeightCategories)r.Next(0, 3);
                newDrone.Status = (IDAL.DO.DroneStatuses)r.Next(0, 3);
                newDrone.Battery= r.NextDouble() * 100;
                drones.Add(newDrone);
            }
            //Initialize random customers
            for (int i = 0; i < numCustomers; i++)
            {

                IDAL.DO.Customer newCustomer = new IDAL.DO.Customer();
                newCustomer.Id = Config.IdDefault++;
                IDAL.DO.Customer newCustomer = new IDAL.DO.Customer();
                newCustomer.Id = Config.IdRandomCustomer++;
                newCustomer.Name = GetRandomName();
                newCustomer.Phone = GetRandomPhone();
                newCustomer.Longitude = r.NextDouble() * 60;
                newCustomer.Lattitude =r.NextDouble() * 60;
                customers.Add(newCustomer);

            }
            //Initialize random parcels
            for (int i = 0; i < numParcels; i++)
            {
             
                IDAL.DO.Parcel newParcel = new IDAL.DO.Parcel();
                newParcel.Id = Config.idObject++;
                customers.Add(newCustomer);
            }
            for (int i = 0; i < numParcels; i++)
            {
                IDAL.DO.Parcel newParcel = new IDAL.DO.Parcel();
                newParcel.Id = Config.idParcel++;
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
            //Initialize random station
            for (int i = 0; i < numStations; i++)
            {
                IDAL.DO.Station newStation = new IDAL.DO.Station();
                newStation.Id = Config.idObject++;
                newStation.Name = GetRandomName();
                newStation.Longitude = r.NextDouble() * 60;
                newStation.Lattitude = r.NextDouble() * 60;
                newStation.ChargeSlots = r.Next(0, 101);
                stations.Add(newStation);
            }
            ///<summary>method<c>GetRandomPhone</c> - return random phone</summary>
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
            ///<summary>method<c>GetRandomName</c> - return random name</summary>
            static string GetRandomName()
            { 
                newParcel.Scheduled =  DateTime.Now.AddMinutes(r.Next(60));
                newParcel.PickedUp = newParcel.Scheduled;
                newParcel.PickedUp.AddMinutes(r.Next(60));
                newParcel.Delivered = newParcel.PickedUp;
                newParcel.Delivered.AddMinutes(r.Next(60));
                parcels.Add(newParcel);
            }
        }
 
        //static var for initialize
        internal class Config
        {
            internal static int idObject = 1;
            internal static int IdDefault = 1;
            internal static int idParcel = 1;
            internal static int idDrone = 1;
            internal static int IdStation = 1;
            internal static int IdRandomCustomer = 1;
        }
    }
}

