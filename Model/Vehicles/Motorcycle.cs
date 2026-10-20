using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp.Model.Vehicles
{
    internal class Motorcycle : Vehicle
    {
        private string _make = string.Empty;
        public override string Make
        {
            get => _make;
            set => _make = value;
        }

        private string _model = string.Empty;
        public override string Model
        {
            get => _model;
            set => _model = value;
        }

        private int _year;
        public override int Year
        {
            get => _year;
            set => _year = value;
        }

        private string _registrationNumber = string.Empty;
        public override string RegistrationNumber
        {
            get => _registrationNumber;
            set => _registrationNumber = value;
        }

        public Motorcycle() { }

        public Motorcycle(string make, string model, int year, string registrationNumber)
        {
            Make = make;
            Model = model;
            Year = year;
            RegistrationNumber = registrationNumber;
        }

        public override void DisplayDetails()
        {
            Console.WriteLine($"Motorcykel: {Make}\nModel: {Model} - ({Year})\nRegistreringsnummer: {RegistrationNumber}");
        }

        public override void DisplayLowDetails()
        {
            Console.WriteLine($"Motorcykel: {Make} Reg: {RegistrationNumber}");
        }
    }
}
