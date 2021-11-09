using System;

namespace ConsoleUI_BL
{
    class ConsoleUI_BL
    {
        static void Main(string[] args)
        {
            IBL.BL options= new();
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
                int id, chargeSlot, wheigt, status, Priority, battery, senderId, targetId;
                double longattitude, lattitude;
                string model, stringName, name, phone;

                Console.WriteLine("chose an option:\n a to add a base station.\n b to add a drone.\n" +
                    " c to add a costumer.\n d to add a parcel.\n e to return to Main menu.\n" + "Enter your choise.");
                choise = Console.ReadLine();
                switch (choise)
                {

                    case "a":
                        Console.WriteLine("Enter new Station details: name,Longitude,Lattitude and ChargeSlots. ");

                        name = Console.ReadLine();
                        double.TryParse(Console.ReadLine(), out longattitude);
                        double.TryParse(Console.ReadLine(), out lattitude);
                        int.TryParse(Console.ReadLine(), out chargeSlot);

                        options.AddNewStation(name, longattitude, lattitude, chargeSlot); break;
                    case "b":
                        Console.WriteLine("Enter new drone details:\n model ");

                        model = Console.ReadLine();
                        int.TryParse(Console.ReadLine(), out wheigt);
                        int.TryParse(Console.ReadLine(), out status);
                        int.TryParse(Console.ReadLine(), out battery);
                        options.AddNewDrone(model, wheigt);
                        ; break;
                    case "c":
                        Console.WriteLine("Enter new customer details:\n id, name,phone,longattitude and lattitude.");
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
                        options.AddNewParcel(senderId, targetId, wheigt, Priority); break;
                    case "e":

                        break;
                }
                return;

            }






        }
    }
}
