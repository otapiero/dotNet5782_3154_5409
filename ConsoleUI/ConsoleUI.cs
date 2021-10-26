using System;

namespace ConsoleUI
{
    class Program
    {
       static public DalObject.DalObject options = new();
        static void Main(string[] args)
        {
            string choise="f";
            
            while (choise != "e")
            {
                Console.WriteLine("chose an option:\n a to open the ADD menu.\n b to open the updet menu.\n c to open the display menu.\n" +
                " d to the list wiew option.\n e to exit.\n" + "Enter your choise:");
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
                        ListWiewMenu(); break;
                    case "e":
                        return;
                }
                
            }
        }

        static void AddMenu()
        {
            string choise;
            int id, name, chargeSlot;
            double longattitude, lattitude;
            Console.WriteLine("chose an option:\n a to add a base station.\n b to add a drone.\n" +
                " c to add a costumer.\n d to add a parcel.\n e to return to Main menu.\n" + "Enter your choise.");
            choise = Console.ReadLine();
            switch (choise)
            {
                
                case "a":
                    Console.WriteLine("Enter new Station details: id ,name,Longitude,Lattitude and ChargeSlots. ");
                    int.TryParse(Console.ReadLine(), out id);
                    int.TryParse(Console.ReadLine(), out name);
                    double.TryParse(Console.ReadLine(), out longattitude);
                    double.TryParse(Console.ReadLine(), out lattitude);
                    int.TryParse(Console.ReadLine(), out chargeSlot);
                    
                    options.AddNewStation(id, name, longattitude, lattitude, chargeSlot) ; break;
                case "b":
                    ; break;
                case "c":
                    ; break;
                case "d":
                    ; break;
                case "e":
                    break;
            }
            return;

        }
        static void UpdateMenu()
        {
            string choise;
            Console.WriteLine("chose an option:\n a to .\n b to .\n" +
                " c .\n d to send a drone to charge.\n e to relase a drone from charge.\n f to return to Main menu.\n" +
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
        static void ListWiewMenu()
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
    }
}

