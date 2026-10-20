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
            Console.WriteLine("-=-----=- Huvudmeny -=-----=-");
            Console.WriteLine("Välj ett alternativ");
            Console.WriteLine("1. Lägg till fordon");
            Console.WriteLine("2. Visa alla fordon");
            Console.WriteLine("3. Ta bort fordon");
            Console.WriteLine("4. Sök på registreringsnummer");
            Console.WriteLine("5. Avancerad sökning");
            Console.WriteLine("6. Avsluta");
            Console.WriteLine("-=-------------------------=-");
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
                if (vehicles[0] is null) break;
                PrintAdvancecdSearchMenu();
                if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 8)
                {
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    continue;
                }
                switch (choice) {
                    case 1:
                        vehicles = FilterClass.FilterIndex(vehicles);
                        break;
                    case 2:
                        vehicles = FilterClass.FilterType(vehicles);
                        break;
                    case 3:
                        // Märke
                        vehicles = FilterClass.FilterMakeOrModel(vehicles, 1);
                        break;
                    case 4:
                        // Modell
                        vehicles = FilterClass.FilterMakeOrModel(vehicles, 2);
                        break;
                    case 5:
                        vehicles = FilterClass.FilterYear(vehicles);
                        break;
                    case 6:
                        PrintArray(vehicles);
                        break;
                    case 7:
                        finnished = true;
                        break;
                    default:
                        finnished = true;
                        break;
                }
            }
            PrintArray(vehicles);
        }

        private static void PrintArray(Vehicle[] vehicles)
        {
            if (vehicles[0] is null)
            {
                Console.WriteLine("\nInga fordon matchade alla kriterier. " +
                    "Återgår till huvudmenyn\n");
                return;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Följande fordon matchar alla kriterier.\n");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            int index = 1;
            foreach (var vehicle in vehicles)
            {
                if (vehicle is not null)
                {
                    Console.Write(index + ". ");
                    vehicle.DisplayDetails();
                    Console.WriteLine();
                    index++;
                }
            }
            Console.ResetColor();
            Console.Write("Fortsätt? <Enter>");
            Console.ReadLine();
        }

        private static void PrintAdvancecdSearchMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-=-=- Avancerad sökmeny -=-=-");
            Console.WriteLine("Välj kriterier för avancerad sökning. Går att stapla flera stycken.");
            Console.WriteLine("1. Index-interval");
            Console.WriteLine("2. Fordonstyp");
            Console.WriteLine("3. Märke");
            Console.WriteLine("4. Modell");
            Console.WriteLine("5. Årtal");
            Console.WriteLine("6. Visa nuvarande resultat");
            Console.WriteLine("7. Klar");
            Console.WriteLine("-----------------------------");
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
