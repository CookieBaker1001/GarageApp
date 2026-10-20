using GarageApp.Vehicles;
using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp
{
    internal static class Utillities
    {
        // Skriver ut huvudmenyn till konsolen.
        public static void PrintMainMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-------------------");
            Console.WriteLine("Välj ett alternativ");
            Console.WriteLine("1. Lägg till fordon");
            Console.WriteLine("2. Visa alla fordon");
            Console.WriteLine("3. Ta bort fordon");
            Console.WriteLine("4. Sök bland fordon");
            Console.WriteLine("5. Avsluta");
            Console.WriteLine("-------------------");
            Console.ResetColor();
            Console.Write(">");
        }

        // Försöker skapa ett fordon baserat på användarens val.
        // Om valet inte är giltigt kastas ett FormatException.
        public static Vehicle CreateVehicle()
        {
            Console.WriteLine("Vilket fordon vill du lägga till?");
            Console.WriteLine("1. Bil");
            Console.WriteLine("2. Motorcykel");
            Console.Write(">");
            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 2)
            {
                throw new FormatException("Ogiltig fordonstyp.");
            }

            Vehicle v = Utillities.PromptForVehicleDetails(choice);
            return v;
        }

        // Försöker parsa inmatning från användaren för att skapa parametrarna till ett
        // fordon. Om inmatningen inte kan parsa kastas ett FormatException.
        public static Vehicle PromptForVehicleDetails(int choice)
        {
            Console.WriteLine("Ange fordonets uppgifter");
            Console.WriteLine("Märke");
            Console.Write(">");
            string? make = Console.ReadLine();
            if (make is null || make == string.Empty) throw new FormatException("Märke får inte vara tomt.");
            Console.WriteLine("Model");
            Console.Write(">");
            string? model = Console.ReadLine();
            if (model is null || model == string.Empty) throw new FormatException("Model får inte vara tomt.");
            Console.WriteLine("År");
            Console.Write(">");
            int year;
            if (!int.TryParse(Console.ReadLine(), out year))
            {
                throw new FormatException("Årtalet måste vara ett giltigt heltal.");
            }
            Console.WriteLine("Registreringsnummer");
            Console.Write(">");
            string? regNumber = Console.ReadLine();
            if (regNumber is null || regNumber == string.Empty) throw new FormatException("Registreringsnummer får inte vara tomt.");
            Console.WriteLine("Antal platser");
            Console.Write(">");
            int seats;
            if (!int.TryParse(Console.ReadLine(), out seats))
            {
                throw new FormatException("Antal platser måste vara ett giltigt heltal.");
            }
            if (choice == 1) return new Car(make, model, year, regNumber, seats);
            else if (choice == 2) return new Motorcycle(make, model, year, regNumber, seats);
            else throw new FormatException("Ogiltigt fordonstyp.");
        }

        //public static void Log(Exception ex)
        //{
        //    Console.ForegroundColor = ConsoleColor.Red;
        //    if (ex is FormatException) Console.WriteLine($"Formateringsfel: {ex.Message}");
        //    else if (ex is InvalidOperationException) Console.WriteLine($"Fel i operation: {ex.Message}");
        //    else Console.WriteLine($"Generellt fel: {ex.Message}");
        //    Console.ForegroundColor = ConsoleColor.White;
        //}
    }
}
