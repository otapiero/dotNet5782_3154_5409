﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleUI
{
    /// <summary>Class main program - Management of a skimmer delivery company  </summary>
    class Program
    {
        static public DalApi.IDal options = DalApi.DalFactory.GetDal();
        /// <summary>method main program - Management of a skimmer delivery company  
        /// the program running until the user return "e" to exit</summary>
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
        /// <summary>method add menu - Management of add object  
        /// the user can add base, drone,customer and parcel to the data </summary
        static void AddMenu()
        {
            string choise;
            int id, chargeSlot, wheigt, Priority, senderId, targetId;
            double longattitude, lattitude;
            string model, stringName, name, phone;

            Console.WriteLine("chose an option:\n a to add a base station.\n b to add a drone.\n" +
                " c to add a costumer.\n d to add a parcel.\n e to return to Main menu.\n" + "Enter your choise.");
            choise = Console.ReadLine();
            switch (choise)
            {

                case "a":
                    Console.WriteLine("Enter new Station details:id, name,Longitude,Lattitude and ChargeSlots. ");
                    int.TryParse(Console.ReadLine(), out id);
                    name = Console.ReadLine();
                    double.TryParse(Console.ReadLine(), out longattitude);
                    double.TryParse(Console.ReadLine(), out lattitude);
                    int.TryParse(Console.ReadLine(), out chargeSlot);

                    options.AddNewStation(id, name, longattitude, lattitude, chargeSlot);
                    break;
                case "b":
                    Console.WriteLine("Enter new drone details: id and model.");
                    int.TryParse(Console.ReadLine(), out id);
                    model = Console.ReadLine();

                    options.AddNewDrone(id, model);
                    break;
                case "c":
                    Console.WriteLine("Enter new customer details:\n id, name,phone,longattitude and lattitude.");
                    int.TryParse(Console.ReadLine(), out id);
                    stringName = Console.ReadLine();
                    phone = Console.ReadLine();
                    double.TryParse(Console.ReadLine(), out longattitude);
                    double.TryParse(Console.ReadLine(), out lattitude);
                    string pass = "0";
                    options.AddNewCustomer(id, stringName, phone, longattitude, lattitude, pass);
                    ; break;
                case "d":
                    Console.WriteLine("Enter new parcel details:\n SenderId,TargetId, wheigt between 0-2" +
                        " and Priority between 0-2.");
                    int.TryParse(Console.ReadLine(), out senderId);
                    int.TryParse(Console.ReadLine(), out targetId);
                    int.TryParse(Console.ReadLine(), out wheigt);
                    int.TryParse(Console.ReadLine(), out Priority);
                    options.AddNewParcel(senderId, targetId, wheigt, Priority); break;
                case "e":

                    break;
            }
            return;

        }
        /// <summary>method update menu - Management of update object  
        /// the user can update Assign a package to a drone, collect a package,deliver a parcel to a customer
        /// send a drone to a charge station and relase a drone from the charge station</summary
        static void UpdateMenu()
        {
            string choise;
            int id, stationId,parcelId;
            Console.WriteLine("chose an option:\n a to Assign a package to a drone.\n" +
                " b to collect a package.\n c to deliver a parcel to a customer.\n d to send a drone to a charge station.\n" +
                " e to relase a drone from the charge station.\n" +
                " f to return to Main menu.\nEnter your choise.");
            choise = Console.ReadLine();
            switch (choise)
            {
                case "a":
                    Console.WriteLine("Enter the parcel id.");
                    int.TryParse(Console.ReadLine(), out parcelId);
                    Console.WriteLine("Enter the drone id.");
                    int.TryParse(Console.ReadLine(), out id);
                    options.AssignPackageToDrone(parcelId,id);
                    break;
                case "b":
                    Console.WriteLine("Enter the parcel id.");
                    int.TryParse(Console.ReadLine(), out id);
                    options.CollectPackage(id);
                    break;
                case "c":
                    Console.WriteLine("Enter the parcel id.");
                    int.TryParse(Console.ReadLine(), out id);
                    options.DeliverPackage(id);
                    break;
                case "d":
                    Console.WriteLine("Enter the drone id.");
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("chose a stations from the list by the Id.");
                    Console.WriteLine("All the stations:");
                    List<DO.Station> stations = (List<DO.Station>)options.AllStation();
                    foreach (var t in stations)
                    {
                        Console.WriteLine(t.ToString());
                    }
                    Console.WriteLine("Enter the station id:");
                    int.TryParse(Console.ReadLine(), out stationId);
                    options.SendDroneToCharge(id, stationId);
                    break;
                case "e":
                    Console.WriteLine("Enter the drone id:");
                    int.TryParse(Console.ReadLine(), out id);
                    options.ReleseDroneFromCharge(id);
                    break;
                case "f":
                    break;
            }
        }
        /// <summary>method display menu - view of display object  
        /// the user can display a station,  a drone,a customer and a parcel</summary
        static void DisplayMenu()
        {
            string choise;
            int id;
            Console.WriteLine("chose an option:\n a to display a station.\n b to display a drone.\n" +
                " c to display a customer.\n d to display a parcel.\n e to return to main menu.\n" +
                "Enter your choise.");
            choise = Console.ReadLine();
            switch (choise)
            {

                case "a":
                    Console.WriteLine("Enter station id.");
                    int.TryParse(Console.ReadLine(), out id);
                    DO.Station station = options.SearchStation(id);
                    Console.WriteLine(station.ToString());
                    break;
                case "b":
                    Console.WriteLine("Enter drone id.");
                    int.TryParse(Console.ReadLine(), out id);
                    DO.Drone drone = options.SearchDrone(id);
                    Console.WriteLine(drone.ToString());
                    break;
                case "c":
                    Console.WriteLine("Enter customer id.");
                    int.TryParse(Console.ReadLine(), out id);
                    DO.Costumer customer = options.SearchCostumer(id);
                    Console.WriteLine(customer.ToString());
                    break;
                case "d":
                    Console.WriteLine("Enter parcel id.");
                    int.TryParse(Console.ReadLine(), out id);
                    DO.Parcel parcel = options.SearchParcel(id);
                    Console.WriteLine(parcel.ToString());
                    break;
                case "e":
                    break;

            }
        }
        /// <summary>method display list menu - view of list display object  
        /// the user can display station list, drone list, customer list ,parcel list,
        /// not associated parcels list and view station with available charging stations  </summary
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
                    List<DO.Station> stations = (List<DO.Station>)options.AllStation();
                    foreach (var t in stations)
                    {
                        Console.WriteLine(t);
                    }
                    break;
                case "b":
                    Console.WriteLine("All the drones:");
                    List<DO.Drone> drones = (List<DO.Drone>)options.AllDrones();
                    foreach (var t in drones)
                    {
                        Console.WriteLine(t.ToString());
                    }
                    break;
                case "c":
                    Console.WriteLine("All the customers:");
                    List<DO.Costumer> customers = (List<DO.Costumer>)options.AllCustomers();
                    foreach (var t in customers)
                    {
                        Console.WriteLine(t.ToString());
                    }
                    break;
                case "d":
                    Console.WriteLine("All the parcels:");
                    List<DO.Parcel> parcels = (List<DO.Parcel>)options.AllParcels();
                    foreach (var t in parcels)
                    {
                        Console.WriteLine(t.ToString());
                    }
                    break;
                case "e":
                    Console.WriteLine("Not associated parcels:");
                    List<DO.Parcel> notAssociatedParcels = options.ListOfParcels(t => t.DroneId == 0).ToList();
                    foreach (var t in notAssociatedParcels)
                    {
                        Console.WriteLine(t.ToString());
                    }
                    break;
                case "f":
                    Console.WriteLine("list of station with available charging stations:");
                    List<DO.Station> PartOfStations = (List<DO.Station>)options.ListOfStations(t => t.ChargeSlots > 0);
                    foreach (var t in PartOfStations)
                    {
                        Console.WriteLine(t.ToString());
                    }
                    break;
                case "g":
                    ; break;


            }
        }
    }
}


