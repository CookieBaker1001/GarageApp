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
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public void DisplayWares()
        {
            if (nextPosition == 0) {
                Console.WriteLine("Garaget är tomt.");
                return;
            }
            Console.WriteLine("Fordon i garaget\n(Kapacitet: {0}/{1})", nextPosition, Capacity);
            foreach (var vehicle in Vehicles)
            {
                if (vehicle is not null)
                {
                    Console.WriteLine("-------------------");
                    vehicle.DisplayDetails();
                }
            }
        }

        public bool IsFull()
        {
            return nextPosition >= Capacity;
        }
    }
}
