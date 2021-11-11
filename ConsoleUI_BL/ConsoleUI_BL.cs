﻿using System;

namespace ConsoleUI_BL
{
    class ConsoleUI_BL
    {
        static IBL.BL options = new();
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
                        catch
                        {

                        }
                      ; break;
                    case "b":
                        try
                        {
                            UpdateMenu();
                        }
                        catch
                        {

                        }
                        break;
                    case "c":
                        try
                        {
                            DisplayMenu();
                        }
                        catch
                        {

                        }
                        break;
                    case "d":
                        try
                        {
                            ListViewMenu();
                        }
                        catch
                        {

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

                    name = Console.ReadLine();
                    double.TryParse(Console.ReadLine(), out longattitude);
                    double.TryParse(Console.ReadLine(), out lattitude);
                    int.TryParse(Console.ReadLine(), out chargeSlot);

                    options.AddNewStation(0, name, longattitude, lattitude, chargeSlot);
                    break;
                case "b":
                    Console.WriteLine("Enter new drone details:\n Id,model,max wheigt to cary and id of base station.");
                    int.TryParse(Console.ReadLine(), out id);
                    model = Console.ReadLine();
                    int.TryParse(Console.ReadLine(), out wheigt);
                    int.TryParse(Console.ReadLine(), out status);
                    int.TryParse(Console.ReadLine(), out battery);
                    int.TryParse(Console.ReadLine(), out idStation);
                    options.AddNewDrone(id,model, wheigt, idStation);
                    break;
                case "c":
                    Console.WriteLine("Enter new customer details:\n id, name,phone and location.");
                    int.TryParse(Console.ReadLine(), out id);
                    stringName = Console.ReadLine();
                    phone = Console.ReadLine();
                    double.TryParse(Console.ReadLine(), out longattitude);
                    double.TryParse(Console.ReadLine(), out lattitude);
                    options.AddNewCustomer(id, stringName, phone, longattitude, lattitude);
                    break;
                case "d":
                    Console.WriteLine("Enter new parcel details:\n Id of sender,id of target, wheigt between 0-2 and priority.");
                    int.TryParse(Console.ReadLine(), out senderId);
                    int.TryParse(Console.ReadLine(), out targetId);
                    int.TryParse(Console.ReadLine(), out wheigt);
                    int.TryParse(Console.ReadLine(), out Priority);
                    options.AddNewParcel(senderId, targetId, wheigt, Priority); 
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
                    int.TryParse(Console.ReadLine(), out id);
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
                    List<IDAL.DO.Station> stations = (List<IDAL.DO.Station>)options.AllStation();
                    foreach (var t in stations)
                    {
                        Console.WriteLine(t.ToString());
                    }
                    Console.WriteLine("Enter the station id:");
                    int.TryParse(Console.ReadLine(), out stationId);
                    options.SendDroneToCharge(id, stationId);
                    break;
                case "e":
                    ; break;
                    
            }
        }





    }

}
