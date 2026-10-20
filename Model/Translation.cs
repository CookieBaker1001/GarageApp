using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp.Model
{
    internal static class Translation
    {
        public static Dictionary<string, string> Vehicles = new Dictionary<string, string>
        {
            { "Car", "Bil" },
            { "Cars", "Bilar" },
            { "Motorcycle", "Motorcykel" },
            { "Motorcycles", "Motorcyklar" }
        };
    }
}
