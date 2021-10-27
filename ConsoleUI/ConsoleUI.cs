using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleUI
{
    class Program
    {
        static public DalObject.DalObject options = new();
        static void Main(string[] args)
        {
            string choise = "f";

            while (choise != "e")
            {
                Console.WriteLine(
                    "chose an option:\n " +
                    "a to open the ADD menu.\n " +
                    "b to open the updet menu.\n " +
                    "c to open the display menu.\n " +
                    "d to the list wiew option.\n " +
                    "e to exit.\n" + 
                    "Enter your choise:"
                    );
                choise = Console.ReadLine();
                switch (choise)
                {
                    case "a":
                        AddMenu(); break;
                    case "b":
                        UpdateMenu(); break;
                    case "c":
                        DisplayMenu(); break;
                    case "d":
                        ListViewMenu(); break;
                    case "e":
                        return;
                }

            }
        }

        static void AddMenu()
        {
            string choise ;
            int id,  chargeSlot, wheigt, status, Priority, battery, senderId, targetId;
            double longattitude, lattitude;
            string model, stringName, name, phone;

            Console.WriteLine("chose an option:\n a to add a base station.\n b to add a drone.\n" +
                " c to add a costumer.\n d to add a parcel.\n e to return to Main menu.\n" + "Enter your choise.");
            choise = Console.ReadLine();
            switch (choise)
            {

                case "a":
                    Console.WriteLine("Enter new Station details: id ,name,Longitude,Lattitude and ChargeSlots. ");
                    int.TryParse(Console.ReadLine(), out id);
                    name= Console.ReadLine();
                    double.TryParse(Console.ReadLine(), out longattitude);
                    double.TryParse(Console.ReadLine(), out lattitude);
                    int.TryParse(Console.ReadLine(), out chargeSlot);

                    options.AddNewStation(id, name, longattitude, lattitude, chargeSlot); break;
                case "b":
                    Console.WriteLine($"Enter new drone details:\n model maxwheigt between 0-2,status betxeen 0-2 and battery");
                  
                    model = Console.ReadLine();
                    int.TryParse(Console.ReadLine(), out wheigt);
                    int.TryParse(Console.ReadLine(), out status);
                    int.TryParse(Console.ReadLine(), out battery);
                    options.AddNewDrone(model, wheigt, status, battery);
                    ; break;
                case "c":
                    Console.WriteLine($"Enter new customer details:\n id, name,phone,longattitude and lattitude.");
                    int.TryParse(Console.ReadLine(), out id);
                    stringName = Console.ReadLine();
                    phone = Console.ReadLine();
                    double.TryParse(Console.ReadLine(), out longattitude);
                    double.TryParse(Console.ReadLine(), out lattitude);
                    options.AddNewCustomer(id, stringName, phone, longattitude, lattitude);
                    ; break;
                case "d":
                    Console.WriteLine("Enter new parcel details:\n SenderId,TargetId, wheigt between 0-2" +
                        " and Priority between 0-2.");
                    int.TryParse(Console.ReadLine(), out senderId);
                    int.TryParse(Console.ReadLine(), out targetId);
                    int.TryParse(Console.ReadLine(), out wheigt);
                    int.TryParse(Console.ReadLine(), out Priority);
                    options.AddNewParcel(0, senderId, targetId, wheigt, Priority,0); break;
                case "e":
                    break;
            }
            return;

        }
        static void UpdateMenu()
        {
            string choise;
            int id, stationId;
            Console.WriteLine("chose an option:\n a to Assign a package to a drone.\n" +
                " b to collect a package.\n c to deliver a parcel to a customer.\n d to send a drone to a charge station.\n" +
                " e to relase a drone from the charge station.\n" +
                " f to return to Main menu.\nEnter your choise.");
            choise = Console.ReadLine();
            switch (choise)
            { 
                case "a":
                    Console.WriteLine("Enter the parcel id.");
                    int.TryParse(Console.ReadLine(),out id);
                    options.ConnectParcelToDrone(id);
                     break;
                case "b":
                    Console.WriteLine("Enter the parcel id.");
                    int.TryParse(Console.ReadLine(), out id);
                    options.ParceCollectionByDrone(id);
                    break;
                case "c":
                    Console.WriteLine("Enter the parcel id.");
                    int.TryParse(Console.ReadLine(), out id);
                    options.DeliveryParcelToCustomer(id);
                    break;
                case "d":
                    Console.WriteLine("Enter the drone id.");
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("chose a stations from the list by the Id.");
                    Console.WriteLine("All the stations:");
                    List<IDAL.DO.Station> stations = options.AllStation();
                    foreach (var t in stations)
                    {
                        Console.WriteLine(t.ToString());
                    }
                    int.TryParse(Console.ReadLine(), out stationId);
                    options.SendDroneToCharge(id, stationId);
                    break;
                case "e":
                    ; break;

            }
        }
        static void DisplayMenu()
        {
            string choise;
            Console.WriteLine("chose an option:\n a to .\n b to .\n" +
                " c .\n d to .\n e to .\n f to .\n" +
                "Enter your choise.");
            choise = Console.ReadLine();
            switch (choise)
            {

                case "a":
                    ; break;
                case "b":
                    ; break;
                case "c":
                    ; break;
                case "d":
                    ; break;
                case "e":
                    ; break;

            }
        }
        static void ListViewMenu()
        {
            string choise;
            Console.WriteLine("chose an option:\n a to view staions list.\n b to view drones list.\n" +
                " c to view customers list.\n d to view parcels list.\n e to view not associated parcels list.\n" +
                " f to view station with available charging stations.\n g to return to Main menu.\n" +
                "Enter your choise.");
            choise = Console.ReadLine();
          
            switch (choise)
            {

                case "a":
                    Console.WriteLine("All the stations:");
                    List<IDAL.DO.Station> stations = options.AllStation();
                    foreach(var t in stations)
                    {
                        Console.WriteLine(t.ToString());
                    }
                    break;
                case "b":
                    Console.WriteLine("All the drones:");
                    List<IDAL.DO.Drone> drones = options.AllDrones();
                    foreach (var t in drones)
                    {
                        Console.WriteLine(t.ToString());
                    }
                    break;
                case "c":
                    Console.WriteLine("All the customers:");
                    List<IDAL.DO.Customer> customers = options.AllCustomers();
                    foreach (var t in customers)
                    {
                        Console.WriteLine(t.ToString());
                    }
                    break;
                case "d":
                    Console.WriteLine("All the parcels:");
                    List<IDAL.DO.Parcel> parcels = options.AllParcels();
                    foreach (var t in parcels)
                    {
                        Console.WriteLine(t.ToString());
                    }
                     break;
                case "e":
                    Console.WriteLine("All the parcels:");
                    List<IDAL.DO.Parcel> notAssociatedParcels = options.NotAssociatedParcels();
                    foreach (var t in notAssociatedParcels)
                    {
                        Console.WriteLine(t.ToString());
                    }
                    break;
                case "f":
                    ; break;
                case "g":
                    ; break;


            }
        }
    }
}


