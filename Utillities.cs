using GarageApp.Model;
using GarageApp.Model.Vehicles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GarageApp
{
    internal static class Utillities
    {
        // Start-sekvens med välkomnandet. Frågar användaren om garagets kapacitet och om
        // användaren vill fylla garaget med initial-data. Om ja, så fylls garaget med
        // slumpdata. Returnerar sedan garaget i sitt start-stadie.
        public static Garage StartUp() { 
            Console.WriteLine("Välkommen till Garaget!");
            Console.WriteLine("Hur stort ska garaget vara? (Maxkapacitet)");
            Console.Write(">");
            int capacity;
            while (!int.TryParse(Console.ReadLine(), out capacity) || capacity <= 0)
            {
                Console.WriteLine("Ogiltigt värde. Försök igen.");
                Console.Write(">");
            }
            Garage garage = new Garage(capacity);
            Console.WriteLine("Vill du att garaget ska fyllas med slumpmässig startdata? (j/n)");
            Console.Write(">");
            char? answer;
            do {
                answer = Console.ReadLine()[0];
                if (answer != 'j' && answer != 'n') {
                    Console.WriteLine("Ogiltigt värde. Försök igen.");
                    Console.Write(">");
                }
            } while (answer != 'j' && answer != 'n');
            if (answer == 'j') {
                Vehicle[] startArray = StartList.GetRandomList(capacity - 1);
                garage.Fill(startArray);
            }
            return garage;
        }

        // Skriver ut huvudmenyn till konsolen.
        public static void PrintMainMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-------------------");
            Console.WriteLine("Välj ett alternativ");
            Console.WriteLine("1. Lägg till fordon");
            Console.WriteLine("2. Visa alla fordon");
            Console.WriteLine("3. Ta bort fordon");
            Console.WriteLine("4. Sök på registreringsnummer");
            Console.WriteLine("5. Avancerad sökning");
            Console.WriteLine("6. Avsluta");
            Console.WriteLine("-------------------");
            Console.ResetColor();
            Console.Write(">");
        }

        // Försöker skapa ett fordon baserat på användarens val.
        // Om valet inte är giltigt kastas ett FormatException.
        public static Vehicle CreateVehicle(Garage garage)
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

            Vehicle v = PromptForVehicleDetails(choice, garage);
            return v;
        }

        // Försöker parsa inmatning från användaren för att skapa parametrarna till ett
        // fordon. Om inmatningen inte kan parsa kastas ett FormatException.
        public static Vehicle PromptForVehicleDetails(int choice, Garage garage)
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
            string? regNumber;
            bool isValidRegNumber = false;
            do {
                regNumber = Console.ReadLine();
                if (regNumber is null || regNumber == string.Empty) throw new FormatException("Registreringsnummer får inte vara tomt.");
                isValidRegNumber = garage.DoesThisRegNumberExist(regNumber);
                if (isValidRegNumber) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Registreringsnumret finns redan i garaget.");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Ange ett nytt registreringsnummer");
                    Console.ResetColor();
                    Console.Write(">");
                }
            } while (isValidRegNumber);
            Console.WriteLine("Antal platser");
            Console.Write(">");
            int seats;
            if (!int.TryParse(Console.ReadLine(), out seats))
            {
                throw new FormatException("Antal platser måste vara ett giltigt heltal.");
            }
            if (choice == 1) return new Car(make, model, year, regNumber, seats);
            else if (choice == 2) return new Motorcycle(make, model, year, regNumber);
            else throw new FormatException("Ogiltigt fordonstyp.");
        }

        public static void Search(Garage garage)
        {
            if (garage.IsEmpty())
            {
                throw new InvalidOperationException("Garaget är tomt.");
            }
            Console.WriteLine("Ange fordonets registreringsnummer");
            Console.Write(">");

            string? regNumber = Console.ReadLine();
            if (string.IsNullOrEmpty(regNumber))
            {
                throw new ArgumentException("Registreringsnummer får inte vara tomt.");
            }
            Vehicle? v = garage.Search(regNumber);
            if (v is null)
            {
                throw new ArgumentException($"Fordon med registreringsnummer {regNumber} hittades inte i garaget.");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Fordon hittat!\n");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            v.DisplayDetails();
            Console.ResetColor();
        }

        public static void AdvancedSearch(Garage garage)
        {
            Vehicle[] vehicles = garage.Copy();
            bool finnished = false;
            int choice;
            while (!finnished) {
                PrintAdvancecdSearchMenu();
                if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 7)
                {
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    continue;
                }
                switch (choice) {
                    case 1:
                        vehicles = FilterIndex(vehicles);
                        break;
                    case 2:
                        vehicles = FilterType(vehicles);
                        break;
                    case 3:
                        vehicles = FilterMakeOrModel(vehicles, 1);
                        break;
                    case 4:
                        vehicles = FilterMakeOrModel(vehicles, 2);
                        break;
                    case 5:
                        vehicles = FilterYear(vehicles);
                        break;
                    case 6:
                        finnished = true;
                        break;
                    default:
                        finnished = true;
                        break;
                }
            }
            PrintArray(vehicles);
        }

        private static Vehicle[] FilterIndex(Vehicle[] vehicles) {
            Console.WriteLine("Välj startindex. (1-indexering)");
            Console.Write(">");
            int start;
            bool result = int.TryParse(Console.ReadLine(), out start);
            if (!result) { 
                throw new ArgumentException("Formateringsfel.");
            }
            result = start > 0 && start <= vehicles.Length;
            if (!result) { 
                throw new ArgumentOutOfRangeException("Ogiltig index.");
            }
            start--;
            Console.WriteLine("Välj slutindex. (1-indexering)");
            Console.Write(">");
            int end;
            result = int.TryParse(Console.ReadLine(), out end);
            if (!result)
            {
                throw new ArgumentException("Formateringsfel.");
            }
            result = end >= start && end <= vehicles.Length;
            if (!result)
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
                if (i < start || i > end) {
                    removeCounter++;
                    continue;
                }
                filteredArray[filteredArrayIndex++] = vehicles[i];
            }
            Console.WriteLine($"{removeCounter} fordon togs bort.");
            return filteredArray;
        }

        private static Vehicle[] FilterType(Vehicle[] vehicles) {
            int choice;
            bool result;
            Console.WriteLine("Välj fordonstyp.");
            Console.WriteLine("1. Bil");
            Console.WriteLine("2. Motorcykel");
            Console.Write(">");
            result = int.TryParse(Console.ReadLine(), out choice);
            if (!result) { 
                throw new ArgumentException("Formateringsfel.");
            }
            else if (choice < 1 || choice > Translation.VehicleNames.Length)
            {
                throw new ArgumentOutOfRangeException("Ogiltig fordonstyp.");
            }

            Vehicle[] filteredArray = new Vehicle[vehicles.Length];
            int filteredArrayIndex = 0;
            int removeCounter = 0;
            for (int i = 0; i < vehicles.Length; i++) {
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

        private static Vehicle[] FilterMakeOrModel(Vehicle[] vehicles, int choice)
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
                if (!vehicles[i].Make.Equals(response))
                {
                    removeCounter++;
                    continue;
                }
                filteredArray[filteredArrayIndex++] = vehicles[i];
            }
            Console.WriteLine($"{removeCounter} fordon togs bort.");
            return filteredArray;
        }

        private static Vehicle[] FilterYear(Vehicle[] vehicles) {
            Console.WriteLine("Ange årtal");
            Console.Write(">");
            int year;
            bool result = int.TryParse(Console.ReadLine(), out year);
            if (!result)
            {
                throw new ArgumentException("Formateringsfel.");
            }
            result = year <= 0 || year > DateTime.Now.Year;
            if (!result)
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

        private static void PrintArray(Vehicle[] vehicles)
        {
            if (vehicles.Length == 0)
            {
                Console.WriteLine("Inga fordon matchade alla kriterier.");
                return;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Följande fordon matchade alla kriterier.\n");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            foreach (var vehicle in vehicles)
            {
                if (vehicle is not null)
                {
                    vehicle.DisplayDetails();
                    Console.WriteLine();
                }
            }
            Console.ResetColor();
        }

        private static void PrintAdvancecdSearchMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-------------------");
            Console.WriteLine("Välj kriterier för avancerad sökning. Går att stapla flera stycken.");
            Console.WriteLine("1. Index-interval");
            Console.WriteLine("2. Fordonstyp");
            Console.WriteLine("3. Märke");
            Console.WriteLine("4. Modell");
            Console.WriteLine("5. Årtal");
            Console.WriteLine("6. Klar/Tillbaka till huvudmenyn");
            Console.WriteLine("-------------------");
            Console.ResetColor();
            Console.Write(">");
        }

        public static void ViewGarage(Garage garage)
        {
            garage.DisplayWares(true);
        }

        public static void RemoveVehicle(Garage garage)
        {
            garage.DisplayWares(false);
            Console.WriteLine("Ange vilket fordon du vill ta bort. (1-indexering)");
            Console.Write(">");
            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Ogiltigt val. Försök igen.");
                return;
            }
            garage.RemoveVehicle(choice - 1);
        }
    }
}
