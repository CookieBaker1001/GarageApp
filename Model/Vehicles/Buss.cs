using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp.Model.Vehicles
{
    internal class Buss : Vehicle
    {
        private string _make;
        public override string Make {
            get => _make; 
            set => _make = value; 
        }

        private string _model;
        public override string Model { 
            get => _model; 
            set => _model = value; 
        }

        private int _year;
        public override int Year { 
            get => _year; 
            set => _year = value; 
        }

        private string _registrationNumber;
        public override string RegistrationNumber { 
            get => _registrationNumber; 
            set => _registrationNumber = value; 
        }
        private int _seats;
        public int Seats
        {
            get => _seats;
            set => _seats = value;
        }

        public Buss() { }

        public Buss(string make, string model, int year, string registrationNumber, int seats)
        {
            Make = make;
            Model = model;
            Year = year;
            RegistrationNumber = registrationNumber;
            Seats = seats;
        }

        public override void DisplayDetails()
        {
            Console.WriteLine($"Buss: {Make}\nModell: {Model} - ({Year})\n" +
                $"Registreringsnummer: {RegistrationNumber}\n{Seats} säte(n)");
        }

        public override void DisplayLowDetails()
        {
            Console.WriteLine($"Buss: {Make} Reg: {RegistrationNumber}");
        }
    }
}
