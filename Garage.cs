using GarageApp.Vehicles;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace GarageApp
{
    internal class Garage
    {
        private int _capacity;
        public int Capacity { 
            get { return _capacity; } 
            set { _capacity = value; } 
        }

        private Vehicle[] _vehicles;
        public Vehicle[] Vehicles
        {
            get => _vehicles;
            private set => _vehicles = value;
        }

        int nextPosition = 0;

        public Garage(int capacity)
        {
            Capacity = capacity;
            Vehicles = new Vehicle[capacity];
        }

        public void AddVehicle(Vehicle vehicle) {
            if (nextPosition == Capacity) {
                throw new InvalidOperationException($"Garage är fullt ({Capacity} platser). Kan inte lägga till fler fordon.");
            }
            Vehicles[nextPosition] = vehicle;
            nextPosition++;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Fordon av typ {vehicle.GetType().Name} med " +
                $"registreringsnummer {vehicle.RegistrationNumber} lades till i garaget.");
            Console.ResetColor();
        }

        // Visar alla fordon i garaget, med möjlighet att visa detaljerad information eller
        // enbart grundläggande information. Detaljerna visas när användaren väljer att
        // visa detaljerad information i huvudmenyn. Vill användaren ta bort ett fordon
        // kommer icke-detaljerad information att visas.
        public void DisplayWares(bool showDetails)
        {
            if (nextPosition == 0) {
                throw new InvalidOperationException("Garaget är tomt.");
            }
            Console.WriteLine("Fordon i garaget\n(Kapacitet: {0}/{1})", nextPosition, Capacity);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            int index = 1;
            foreach (var vehicle in Vehicles)
            {
                if (vehicle is not null)
                {
                    Console.WriteLine("-------------------");
                    Console.Write((index++) + ". ");
                    if (showDetails) vehicle.DisplayDetails();
                    else vehicle.DisplayLowDetails();
                }
            }
            Console.ResetColor();
        }

        // Tar bort ett fordon från garaget baserat på indexet i listan.
        // Skiftar alla fordon efter det borttagna fordonet en position bakåt för att inte
        // lämna hål i listan.
        public void RemoveVehicle(int index)
        {
            if (index < 0 || index >= nextPosition)
            {
                throw new ArgumentOutOfRangeException("Ogiltigt index för att ta bort fordon.");
            }
            var vehicleToRemove = Vehicles[index];
            
            // Flytta alla fordon efter det borttagna fordonet en position bakåt
            for (int i = index; i < nextPosition - 1; i++)
            {
                Vehicles[i] = Vehicles[i + 1];
            }
            Vehicles[nextPosition - 1] = null;
            nextPosition--;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{vehicleToRemove.GetType().Name} med registreringsnummer " +
                $"{vehicleToRemove.RegistrationNumber} har tagits bort från garaget.");
            Console.ResetColor();
        }

        public bool IsFull()
        {
            return nextPosition >= Capacity;
        }

        public bool IsEmpty()
        {
            return nextPosition == 0;
        }

        public Vehicle? Search(string registrationNumber)
        {
            foreach (var vehicle in Vehicles) {
                if (vehicle is null) continue;
                string regNum = vehicle.RegistrationNumber.ToLower();
                if (regNum == registrationNumber.ToLower()) { 
                    return vehicle;
                }
            }
            return null;
        }
    }
}
