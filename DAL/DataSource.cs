using System;
using System.Collections.Generic;

namespace DAL
{
    ///<summary>Class - initialize class for the program</summary>
    internal static class DataSource
    {

        ///<summary>list of data for the programing<summary>
        internal static List<DO.DroneCharge> drone = new List<DO.DroneCharge>();
        internal static List<DO.Drone> drones = new List<DO.Drone>();
        internal static List<DO.Station> stations = new List<DO.Station>();
        internal static List<DO.Costumer> customers = new List<DO.Costumer>();
        internal static List<DO.Parcel> parcels = new List<DO.Parcel>();
        internal static List<DO.DroneCharge> DroneCharges = new List<DO.DroneCharge>();
        //var random for initialize function
        static Random r = new();
        //Initialize function
        internal static void Initialize()
        {
            //Set quantity of var 
            int numDrones = r.Next(5, 10);
            int numCustomers = r.Next(6, 10);
            int numParcels = r.Next(20, 55);
            int numStations = r.Next(2, 5);
            //Initialize random drones
            for (int i = 0; i < numDrones; i++)
            {
                DO.Drone newDrone = new();

                newDrone.Id = Config.idDrone++;
                newDrone.Model = GetModelName();


                drones.Add(newDrone);
            }
            //Initialize random customers
            for (int i = 0; i < numCustomers; i++)
            {

                DO.Costumer newCustomer = new();

                newCustomer.Id = Config.IdCustomer++;
                newCustomer.Name = GetRandomName();
                newCustomer.Phone = GetRandomPhone();
                newCustomer.Longitude = r.NextDouble() * 60;
                newCustomer.Lattitude = r.NextDouble() * 60;
                newCustomer.Password = "0";
                customers.Add(newCustomer);

            }
            //Initialize random parcels
            int randomCase;
            for (int i = 0; i < numParcels; i++)
            {
                randomCase = r.Next(5);
                DO.Parcel newParcel = new DO.Parcel();
                newParcel.Availble = true;
                newParcel.Id = Config.idParcel++;
                newParcel.Sender = r.Next(1, numCustomers);
                newParcel.TargetId = r.Next(1, numCustomers);
                newParcel.Wheight = (DO.WeightCategories)r.Next(0, 3);
                newParcel.Priority = (DO.Priorities)r.Next(0, 3);
                newParcel.Requsted = DateTime.Now;
                if (Config.idDrone == 0)
                {
                    randomCase = 0;

                }
                switch (randomCase)
                {



                    case 0:
                        newParcel.DroneId = 0;
                        newParcel.Scheduled = null;
                        newParcel.PickedUp = null;
                        newParcel.Delivered = null;
                        break;
                    case 1:
                        newParcel.DroneId = Config.idDrone--;
                        newParcel.Scheduled = null;
                        newParcel.PickedUp = null;
                        newParcel.Delivered = null;
                        break;
                    case 2:
                        newParcel.DroneId = Config.idDrone--;
                        newParcel.Scheduled = DateTime.Now.AddMinutes(r.Next(15));
                        newParcel.PickedUp = null;
                        newParcel.Delivered = null;
                        break;
                    case 3:
                        newParcel.DroneId = Config.idDrone--;
                        newParcel.Scheduled = DateTime.Now.AddMinutes(r.Next(15));
                        newParcel.PickedUp = DateTime.Now.AddMinutes(r.Next(15));
                        newParcel.Delivered = null;
                        break;
                    case 4:
                        newParcel.DroneId = Config.idDrone--;
                        newParcel.Scheduled = DateTime.Now.AddMinutes(r.Next(15));
                        newParcel.PickedUp = DateTime.Now.AddMinutes(r.Next(15));
                        newParcel.Delivered = DateTime.Now.AddMinutes(r.Next(15));
                        break;
                }


                parcels.Add(newParcel);
            }
           ; //Initialize random station
            for (int i = 0; i < numStations; i++)
            {
                DO.Station newStation = new DO.Station();
                newStation.Id = Config.IdStation++;
                newStation.Name = GetRandomName();
                ; newStation.Longitude = r.NextDouble() * 60;
                newStation.Lattitude = r.NextDouble() * 60;
                newStation.ChargeSlots = r.Next(0, 101);
                stations.Add(newStation);
            }
           ; ///<summary>method<c>GetRandomPhone</c> - return random phone</summary>
            static string GetRandomPhone()
            {
                string phone = "058";

                string numbers = "0123465789";
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
                string name = "";
                string letters = "azertyuiopqsdfghjklmwxcvbn";

                int index;
                for (int i = 0; i < 4; i++)
                {
                    index = r.Next(0, 25);
                    name += letters[index];
                }

                return name;
            }
            static string GetModelName()
            {


                return Config.models[r.Next(5)];
            }
        }
        //static var for initialize
        internal class Config
        {




            public static int idParcel = 1;
            public static int idDrone = 1;
            public static int IdStation = 1;
            public static int IdCustomer = 1;
            //per kilometer
            public static double Avilable = 0.5;
            public static double Light = 0.7;
            public static double Intermidiate = 1;
            public static double Heavy = 1.5;
            //per houre
            public static double chargingRatePerHoure = 0.5;


            public static string[] models = new string[] { "Alpha", "beta", "gema", "delta", "omicorion" };

        }
    }
}

