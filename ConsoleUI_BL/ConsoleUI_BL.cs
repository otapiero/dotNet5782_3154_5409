﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleUI_BL
{
    class ConsoleUI_BL
    {
        static BlApi.IBL options =BlApi.BlFactory.GetBl();
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
                        try
                        {
                            AddMenu();
                        }
                        catch (BO.IBException x)
                        {
                            Console.WriteLine(x);
                        }
                      ; break;
                    case "b":
                        try
                        {
                            UpdateMenu();
                        }
                        catch (BO.IBException x)
                        {
                            Console.WriteLine(x);
                        }
                        break;
                    case "c":
                        try
                        {
                            DisplayMenu();
                        }
                        catch (BO.IBException x)
                        {
                            Console.WriteLine(x);
                        }
                        break;
                    case "d":
                        ListViewMenu();

                        try
                        {
                        }
                        catch (BO.IBException x)
                        {
                            Console.WriteLine(x);
                        }
                        break;
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
            int id, chargeSlot, wheigt, status, Priority, battery, senderId, targetId, idStation;
            double longattitude, lattitude;
            string model, stringName, name, phone;

            Console.WriteLine("chose an option:\n a to add a base station.\n b to add a drone.\n" +
                " c to add a costumer.\n d to add a parcel.\n e to return to Main menu.\n" + "Enter your choise.");
            choise = Console.ReadLine();
            switch (choise)
            {

                case "a":
                    Console.WriteLine("Enter new Station details:Id, name,location and num of ChargeSlots. ");
                    if (int.TryParse(Console.ReadLine(), out id) == false) { Console.WriteLine("wrong format of input"); break; };
                    name = Console.ReadLine();
                    if (double.TryParse(Console.ReadLine(), out longattitude) == false) { Console.WriteLine("wrong format of input"); break; };
                    double.TryParse(Console.ReadLine(), out lattitude);
                    if (int.TryParse(Console.ReadLine(), out chargeSlot) == false) { Console.WriteLine("wrong format of input"); break; };
                    try
                    {
                        options.AddNewStation(id, name, longattitude, lattitude, chargeSlot);

                    }
                    catch (BO.IBException x)
                    {
                        Console.WriteLine(x);
                    }
                    break;
                case "b":
                    Console.WriteLine("Enter new drone details:\n Id,model,max wheigt to cary and id of base station.");
                    if (int.TryParse(Console.ReadLine(), out id) == false) { Console.WriteLine("wrong format of input"); break; };
                    model = Console.ReadLine();
                    if (int.TryParse(Console.ReadLine(), out wheigt) == false) { Console.WriteLine("wrong format of input"); break; };
                    if (int.TryParse(Console.ReadLine(), out status) == false) { Console.WriteLine("wrong format of input"); break; };
                    if (int.TryParse(Console.ReadLine(), out battery) == false) { Console.WriteLine("wrong format of input"); break; };
                    if (int.TryParse(Console.ReadLine(), out idStation) == false) { Console.WriteLine("wrong format of input"); break; };
                    try
                    {
                        options.AddNewDrone(id, model, wheigt, idStation);

                    }
                    catch (BO.IBException x)
                    {
                        Console.WriteLine(x);
                    }
                    break;
                case "c":
                    Console.WriteLine("Enter new customer details:\n id, name,phone and location.");
                    if (int.TryParse(Console.ReadLine(), out id) == false) { Console.WriteLine("wrong format of input"); break; };
                    stringName = Console.ReadLine();
                    phone = Console.ReadLine();
                    if (double.TryParse(Console.ReadLine(), out longattitude) == false) { Console.WriteLine("wrong format of input"); break; };
                    if (double.TryParse(Console.ReadLine(), out lattitude) == false) { Console.WriteLine("wrong format of input"); break; };
                    try
                    {
                        string pass = "0";
                        options.AddNewCustomer(id, stringName, phone, longattitude, lattitude, pass);

                    }
                    catch (BO.IBException x)
                    {
                        Console.WriteLine(x);
                    }
                    break;
                case "d":
                    Console.WriteLine("Enter new parcel details:\n Id of sender,id of target, wheigt between 0-2 and priority.");
                    if (int.TryParse(Console.ReadLine(), out senderId) == false) { Console.WriteLine("wrong format of input"); break; };
                    if (int.TryParse(Console.ReadLine(), out targetId) == false) { Console.WriteLine("wrong format of input"); break; };
                    if (int.TryParse(Console.ReadLine(), out wheigt) == false) { Console.WriteLine("wrong format of input"); break; };
                    if (int.TryParse(Console.ReadLine(), out Priority) == false) { Console.WriteLine("wrong format of input"); break; };
                    try
                    {
                        options.AddNewParcel(senderId, targetId, wheigt, Priority);
                    }
                    catch (BO.IBException x)
                    {
                        Console.WriteLine(x);
                    }
                    break;
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
            string choise, model, name, phone;
            int id, chargeSlots;
            double time;
            Console.WriteLine("chose an option:\n a to rename a drone model.\n" +
                " b to update a station base details.\n c to update a costumer details .\n " +
                "d to send a drone to a charge.\n" +
                " e to relase a drone from the charge station.\n" +
                " f to Assign a package to a drone.\n" +
                 " g to collect a package by a drone.\n" +
                 " k to deliver a package.\n" +
                " l to return to Main menu.\nEnter your choise.");
            choise = Console.ReadLine();
            switch (choise)
            {
                case "a":
                    Console.WriteLine("Enter the drone id.");
                    if (int.TryParse(Console.ReadLine(), out id) == false) { Console.WriteLine("wrong format of input"); break; };
                    Console.WriteLine("Enter the new drone model.");
                    model = Console.ReadLine();
                    try
                    {
                        options.UpdateDroneModel(id, model);
                    }
                    catch (BO.IBException x)
                    {
                        Console.WriteLine(x);
                    }

                    break;
                case "b":
                    Console.WriteLine("Enter the station base id.");
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("one or more of the follow details:name,max num of charge slots.");
                    name = Console.ReadLine();
                    int.TryParse(Console.ReadLine(), out chargeSlots);
                    try
                    {
                        options.UpdateStation(id, name, chargeSlots);
                    }
                    catch (BO.IBException x)
                    {
                        Console.WriteLine(x);
                    }

                    break;
                case "c":
                    Console.WriteLine("Enter the costumer id.");
                    int.TryParse(Console.ReadLine(), out id);
                    Console.WriteLine("one or more of the follow details:name and phone.");
                    name = Console.ReadLine();
                    phone = Console.ReadLine();
                    try
                    {
                        options.UpdateCostumer(id, name, phone);
                    }
                    catch (BO.IBException x)
                    {
                        Console.WriteLine(x);
                    }

                    break;
                case "d":
                    Console.WriteLine("Enter the drone id.");
                    if (int.TryParse(Console.ReadLine(), out id) == false) { Console.WriteLine("wrong format of input"); break; };


                    try
                    {
                        options.SendDroneToCharge(id);

                    }
                    catch (BO.IBException x)
                    {
                        Console.WriteLine(x);
                    }
                    break;
                case "e":

                    Console.WriteLine("Enter the drone id.");
                    if (int.TryParse(Console.ReadLine(), out id) == false) { Console.WriteLine("wrong format of input"); break; };
                    Console.WriteLine("Enter the carge time.");
                    if (double.TryParse(Console.ReadLine(), out time) == false) { Console.WriteLine("wrong format of input"); break; };

                    try
                    {
                        options.RelesaeDroneFromCharge(id, time);
                    }
                    catch (BO.IBException x)
                    {
                        Console.WriteLine(x);
                    }
                    break;
                case "f":
                    Console.WriteLine("Enter the drone id.");
                    if (int.TryParse(Console.ReadLine(), out id) == false) { Console.WriteLine("wrong format of input"); break; };
                    try
                    {
                        options.AssignPackageToDrone(id);
                    }
                    catch (BO.IBException x)
                    {
                        Console.WriteLine(x);
                    }

                    break;
                case "g":
                    Console.WriteLine("Enter the drone id.");
                    if (int.TryParse(Console.ReadLine(), out id) == false) { Console.WriteLine("wrong format of input"); break; };
                    try
                    {
                        options.CollectPackage(id);
                    }
                    catch (BO.IBException x)
                    {
                        Console.WriteLine(x);
                    }
                    break;
                case "k":
                    Console.WriteLine("Enter the drone id.");
                    if (int.TryParse(Console.ReadLine(), out id) == false) { Console.WriteLine("wrong format of input"); break; };
                    try
                    {
                        options.DeliverPackage(id);
                    }
                    catch (BO.IBException x)
                    {
                        Console.WriteLine(x);
                    }
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
                    if (int.TryParse(Console.ReadLine(), out id) == false) { Console.WriteLine("wrong format of input"); break; };
                    try
                    {
                        BO.BaseStation station = options.SearchStation(id);
                        Console.WriteLine(station.ToString());

                    }
                    catch (BO.IBException x)
                    {
                        Console.WriteLine(x);
                    }
                    break;
                case "b":
                    Console.WriteLine("Enter drone id.");
                    if (int.TryParse(Console.ReadLine(), out id) == false) { Console.WriteLine("wrong format of input"); break; };
                    try
                    {
                        BO.DroneBL drone = options.SearchDrone(id);
                        Console.WriteLine(drone);
                    }
                    catch (BO.IBException x)
                    {
                        Console.WriteLine(x);
                    }
                    break;
                case "c":
                    Console.WriteLine("Enter costumer id.");
                    if (int.TryParse(Console.ReadLine(), out id) == false) { Console.WriteLine("wrong format of input"); break; };
                    try
                    {
                        BO.CustomerBl costumer = options.SearchCostumer(id);
                        Console.WriteLine(costumer);
                    }
                    catch (BO.IBException x)
                    {
                        Console.WriteLine(x);
                    }
                    break;
                case "d":
                    Console.WriteLine("Enter parcel id.");
                    if (int.TryParse(Console.ReadLine(), out id) == false)
                    {
                        Console.WriteLine("wrong format of input");
                        break;
                    };
                    try
                    {
                        BO.ParcelBl parcel = options.SearchParcel(id);
                        Console.WriteLine(parcel);
                    }
                    catch (BO.IBException x)
                    {
                        Console.WriteLine(x);
                    }

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
                " c to view customers list.\n d to view parcels list.\n e to view not assingted parcels list.\n" +
                " f to view station with available charging spolts.\n g to return to Main menu.\n" +
                "Enter your choise.");
            choise = Console.ReadLine();

            switch (choise)
            {

                case "a":
                    Console.WriteLine("All the stations:");
                    IEnumerable<BO.BaseStationToList> lst = options.ListStation();

                    foreach (var x in lst)
                    {

                        Console.WriteLine(x);
                    }



                    break;
                case "b":
                    Console.WriteLine("All the drones:");
                    var ListDrones = options.ListDrones();
                    foreach (var x in ListDrones)
                    {
                        Console.WriteLine(x);
                    }
                    break;
                case "c":
                    Console.WriteLine("All the customers:");
                    var ListCustomer = options.ListCustomer();
                    foreach (var x in ListCustomer)
                    {
                        Console.WriteLine(x);
                    }
                    break;
                case "d":
                    Console.WriteLine("All the parcels:");
                    var ListParcels = options.ListParcels();
                    foreach (var x in ListParcels)
                    {
                        Console.WriteLine(x);
                    }
                    break;
                case "e":
                    Console.WriteLine("Not associated parcels:");
                    var ListParcelsNotAssigned = options.ListParcelsNotAssigned();
                    foreach (var x in ListParcelsNotAssigned)
                    {
                        Console.WriteLine(x);
                    }
                    break;
                case "f":
                    Console.WriteLine("list of station with available charging stations:");
                    var StationWithAvailebalChargePost = options.StationWithAvailebalChargePost();
                    foreach (var x in StationWithAvailebalChargePost)
                    {
                        Console.WriteLine(x);
                    }
                    break;
                case "g":
                    break;


            }
        }

    }

}
