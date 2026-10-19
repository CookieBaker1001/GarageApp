using GarageApp.Vehicles;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace GarageApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Välkommen till Garaget!");
            Console.WriteLine("Hur stort är garaget?");
            Console.Write(">");
            int capacity;
            while (!int.TryParse(Console.ReadLine(), out capacity) || capacity <= 0)
            {
                Console.WriteLine("Ogiltigt värde. Försök igen.");
                Console.Write(">");
            }
            Garage garage = new Garage(capacity);

            bool exit = false;
            int choice;
            do
            {
                Utillities.PrintMainMenu();
                if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5) {
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    continue;
                }
                switch (choice)
                {
                    case 1:
                        TryToAddVehicle(garage);
                        break;
                    case 2:
                        ViewGarage(garage);
                        break;
                    case 3:
                        RemoveVehicle();
                        break;
                    case 4:
                        Search();
                        break;
                    case 5:
                    default:
                        exit = true;
                        break;
                }
            } while (!exit);
        }

        public static void Search()
        {
            Console.WriteLine("Sök fordon - Funktionalitet ej implementerad än.");
        }

        public static void RemoveVehicle()
        {
            Console.WriteLine("Ta bort fordon - Funktionalitet ej implementerad än.");
        }

        public static void ViewGarage(Garage garage)
        {
            garage.DisplayWares();
        }

        // Försöker skapa ett fordon och lägga till det i garaget,
        // hanterar eventuella undantag som kan uppstå.
        public static void TryToAddVehicle(Garage garage) {
            try
            {
                if (garage.IsFull()) throw new InvalidOperationException("Garaget är fullt. Kan inte lägga till fler fordon.");
                Vehicle v = Utillities.CreateVehicle();
                if (v != null) { 
                    garage.AddVehicle(v);
                }
            }
            catch (FormatException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Formateringsfel: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (InvalidOperationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fel i operation: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Generellt fel: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
