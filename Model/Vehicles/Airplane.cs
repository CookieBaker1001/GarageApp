using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp.Model.Vehicles
{
    internal class Airplane : Vehicle
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
        public override string RegistrationNumber
        {
            get => _registrationNumber;
            set => _registrationNumber = value;
        }

        private int _engineCount;
        public int EngineCount
        {
            get => _engineCount;
            set => _engineCount = value;
        }

        private int _maximumAltitude;
        public int MaximumAltitude
        {
            get => _maximumAltitude;
            set => _maximumAltitude = value;
        }

        public Airplane() { }

        public Airplane(string make, string model, int year, string registrationNumber, int engineCount, int maximumAltitude)
        {
            Make = make;
            Model = model;
            Year = year;
            RegistrationNumber = registrationNumber;
            EngineCount = engineCount;
            MaximumAltitude = maximumAltitude;
        }

        public override void DisplayDetails()
        {
            Console.WriteLine($"(Flygplan) Märke: {Make}\nModell: {Model} - ({Year})\n" +
                $"Registreringsnummer: {RegistrationNumber}\n{EngineCount} motor(er)\n" +
                $"{MaximumAltitude} meter max höjd");
        }

        public override void DisplayLowDetails()
        {
            Console.WriteLine($"Flygplan, Märke: {Make}, Reg: {RegistrationNumber}");
        }
    }
}
