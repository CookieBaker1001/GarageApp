using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp.Model.Vehicles
{
    internal abstract class Vehicle
    {
        public abstract string Make { get; set; }
        public abstract string Model { get; set; }
        public abstract int Year { get; set; }
        public abstract string RegistrationNumber { get; set; }
        public abstract void DisplayDetails();
        public abstract void DisplayLowDetails();
    }
}
