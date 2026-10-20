using GarageApp.Model;
using GarageApp.Model.Vehicles;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace GarageApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ASCII art
            Console.WriteLine(" ######      ###    ########     ###     ######   ######## ######## \r\n##    ##    ## ##   ##     ##   ## ##   ##    ##  ##          ##    \r\n##         ##   ##  ##     ##  ##   ##  ##        ##          ##    \r\n##   #### ##     ## ########  ##     ## ##   #### ######      ##    \r\n##    ##  ######### ##   ##   ######### ##    ##  ##          ##    \r\n##    ##  ##     ## ##    ##  ##     ## ##    ##  ##          ##    \r\n ######   ##     ## ##     ## ##     ##  ######   ########    ##    \n");
            Garage garage = Utillities.StartUp();

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
                        TryToSearchVehicle(garage);
                        break;
                    case 5:
                    default:
                        exit = true;
                        break;
                }
            } while (!exit);
        }

        // Försöker söka efter ett fordon som må finnas i garaget
        // Hanterar eventuella undantag som kan uppstå.
        public static void TryToSearchVehicle(Garage garage)
        {
            try
            {
                Utillities.Search(garage);
            }
            catch (InvalidOperationException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fel i operation: {ex.Message}");
                Console.ResetColor();
            }
            catch (ArgumentException ex) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Argumentfel: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Generellt fel: {ex.Message}");
                Console.ResetColor();
            }
        }

        // Försöker ta bort ett fordon från garaget
        // Hanterar eventuella undantag som kan uppstå.
        public static void TryToRemoveVehicle(Garage garage) {
            try
            {
                Utillities.RemoveVehicle(garage);
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
                Console.WriteLine($"Generellt fel: {ex.Message}");
                Console.ResetColor();
            }
        }

        // Försöker se vad som finns i garaget.
        // Hanterar eventuella undantag som kan uppstå.
        public static void TryToViewGarage(Garage garage)
        {
            try
            {
                Utillities.ViewGarage(garage);
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

        // Försöker skapa ett fordon och lägga till det i garaget.
        // Hanterar eventuella undantag som kan uppstå.
        public static void TryToAddVehicle(Garage garage) {
            try
            {
                if (garage.IsFull()) throw new InvalidOperationException("Garaget är fullt. Kan inte lägga till fler fordon.");
                Vehicle v = Utillities.CreateVehicle(garage);
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
