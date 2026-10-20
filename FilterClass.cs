using GarageApp.Model;
using GarageApp.Model.Vehicles;
using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp
{
    internal static class FilterClass
    {
        public static Vehicle[] FilterIndex(Vehicle[] vehicles)
        {
            Console.WriteLine("Välj startindex. (1-indexering)");
            Console.Write(">");
            int start;
            bool validResult = int.TryParse(Console.ReadLine(), out start);
            if (!validResult)
            {
                throw new ArgumentException("Formateringsfel.");
            }
            validResult = start > 0 && start <= vehicles.Length;
            if (!validResult)
            {
                throw new ArgumentOutOfRangeException("Ogiltig index.");
            }
            start--;
            Console.WriteLine("Välj slutindex. (1-indexering)");
            Console.Write(">");
            int end;
            validResult = int.TryParse(Console.ReadLine(), out end);
            if (!validResult)
            {
                throw new ArgumentException("Formateringsfel.");
            }
            validResult = end >= start && end <= vehicles.Length;
            if (!validResult)
            {
                throw new ArgumentOutOfRangeException("Ogiltig index.");
            }
            end--;

            Vehicle[] filteredArray = new Vehicle[vehicles.Length];
            int removeCounter = 0;
            int filteredArrayIndex = 0;
            for (int i = 0; i < vehicles.Length; i++)
            {
                if (vehicles[i] is null) break;
                if (i < start || i > end)
                {
                    removeCounter++;
                    continue;
                }
                filteredArray[filteredArrayIndex++] = vehicles[i];
            }
            Console.WriteLine($"{removeCounter} fordon togs bort.");
            return filteredArray;
        }

        public static Vehicle[] FilterType(Vehicle[] vehicles)
        {
            int choice;
            Console.WriteLine("Välj fordonstyp.");
            Console.WriteLine("1. Bil");
            Console.WriteLine("2. Motorcykel");
            Console.Write(">");
            bool validResult = int.TryParse(Console.ReadLine(), out choice);
            if (!validResult)
            {
                throw new ArgumentException("Formateringsfel.");
            }
            else if (choice < 1 || choice > Translation.VehicleNames.Length)
            {
                throw new ArgumentOutOfRangeException("Ogiltig fordonstyp.");
            }

            Vehicle[] filteredArray = new Vehicle[vehicles.Length];
            int filteredArrayIndex = 0;
            int removeCounter = 0;
            for (int i = 0; i < vehicles.Length; i++)
            {
                if (vehicles[i] is null) break;
                if (vehicles[i].GetType().Name != Translation.VehicleNames[choice - 1])
                {
                    removeCounter++;
                    continue;
                }
                filteredArray[filteredArrayIndex++] = vehicles[i];
            }
            Console.WriteLine($"{removeCounter} fordon togs bort.");
            return filteredArray;
        }

        public static Vehicle[] FilterMakeOrModel(Vehicle[] vehicles, int choice)
        {
            Console.WriteLine($"Ange {(choice == 1 ? "märke" : "modell")}");
            Console.Write(">");
            string? response = Console.ReadLine();
            if (string.IsNullOrEmpty(response)) throw new ArgumentException(
                $"{(choice == 1 ? "Märke" : "Modell")} får inte vara tomt.");

            Vehicle[] filteredArray = new Vehicle[vehicles.Length];
            int filteredArrayIndex = 0;
            int removeCounter = 0;
            for (int i = 0; i < vehicles.Length; i++)
            {
                if (vehicles[i] is null) break;
                if (choice == 1 && !vehicles[i].Make.Equals(response))
                {
                    removeCounter++;
                    continue;
                } else if (choice == 2 && !vehicles[i].Model.Equals(response))
                {
                    removeCounter++;
                    continue;
                }
                filteredArray[filteredArrayIndex++] = vehicles[i];
            }
            Console.WriteLine($"{removeCounter} fordon togs bort.");
            return filteredArray;
        }

        public static Vehicle[] FilterYear(Vehicle[] vehicles)
        {
            Console.WriteLine("Ange årtal");
            Console.Write(">");
            int year;
            bool validResult = int.TryParse(Console.ReadLine(), out year);
            if (!validResult)
            {
                throw new ArgumentException("Formateringsfel.");
            }
            validResult = year >= 0 && year <= DateTime.Now.Year;
            if (!validResult)
            {
                throw new ArgumentOutOfRangeException("Ogiltigt årtal.");
            }

            Vehicle[] filteredArray = new Vehicle[vehicles.Length];
            int filteredArrayIndex = 0;
            int removeCounter = 0;
            for (int i = 0; i < vehicles.Length; i++)
            {
                if (vehicles[i] is null) break;
                if (vehicles[i].Year != year)
                {
                    removeCounter++;
                    continue;
                }
                filteredArray[filteredArrayIndex++] = vehicles[i];
            }
            Console.WriteLine($"{removeCounter} fordon togs bort.");
            return filteredArray;
        }
    }
}
