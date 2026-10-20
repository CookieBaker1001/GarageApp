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
            Console.WriteLine(" ######      ###    ########     ###     ######   ######## ######## \r\n##    ##    ## ##   ##     ##   ## ##   ##    ##  ##          ##    \r\n##         ##   ##  ##     ##  ##   ##  ##        ##          ##    \r\n##   #### ##     ## ########  ##     ## ##   #### ######      ##    \r\n##    ##  ######### ##   ##   ######### ##    ##  ##          ##    \r\n##    ##  ##     ## ##    ##  ##     ## ##    ##  ##          ##    \r\n ######   ##     ## ##     ## ##     ##  ######   ########    ##    \n");
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
                        TryToViewGarage(garage);
                        break;
                    case 3:
                        TryToRemoveVehicle(garage);
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

        public static void TryToRemoveVehicle(Garage garage) {
            try
            {
                RemoveVehicle(garage);
            } catch (InvalidOperationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fel i operation: {ex.Message}");
                Console.ResetColor();
            } catch (ArgumentOutOfRangeException ex) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Indexfel: {ex.Message}");
                Console.ResetColor();
            } 
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Generelt fel: {ex.Message}");
                Console.ResetColor();
            }
        }

        public static void RemoveVehicle(Garage garage)
        {
            garage.DisplayWares(false);
            Console.WriteLine("Ange vilket fordon du vill ta bort");
            Console.Write(">");
            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice)) {
                Console.WriteLine("Ogiltigt val. Försök igen.");
                return;
            }
            garage.RemoveVehicle(choice - 1);
        }

        public static void TryToViewGarage(Garage garage)
        {
            try
            {
                ViewGarage(garage);
            }
            catch (InvalidOperationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fel i operation: {ex.Message}");
                Console.ResetColor();
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Ogiltig index: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Generellt fel: {ex.Message}");
                Console.ResetColor();
            }
        }

        public static void ViewGarage(Garage garage)
        {
            garage.DisplayWares(true);
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
                Console.ResetColor();
            }
            catch (InvalidOperationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fel i operation: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Generellt fel: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
