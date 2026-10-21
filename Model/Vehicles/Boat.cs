using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp.Model.Vehicles
{
    internal class Boat : Vehicle
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

        private PropulsionType _propulsionType;
        public PropulsionType PropulsionType { 
            get => _propulsionType;
            set => _propulsionType = value; 
        }

        public Boat() { }

        public Boat(string make, string model, int year, string registrationNumber, PropulsionType propulsionType)
        {
            Make = make;
            Model = model;
            Year = year;
            RegistrationNumber = registrationNumber;
            PropulsionType = propulsionType;
        }

        public override void DisplayDetails()
        {
            string details = $"(Båt) Märke: {Make}\nModell: {Model} - ({Year})\n" +
                $"Registreringsnummer: {RegistrationNumber}\nDrivs med ";
            if (PropulsionType == PropulsionType.Engine) details += "motor.";
            else if (PropulsionType == PropulsionType.Sail) details += "segel.";
            else details += "åror.";
            Console.WriteLine(details);
        }

        public override void DisplayLowDetails()
        {
            Console.WriteLine($"Båt, Märke: {Make}, Reg: {RegistrationNumber}");
        }
    }
}
