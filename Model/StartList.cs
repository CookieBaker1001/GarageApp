using GarageApp.Model.Vehicles;
using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp.Model
{
    internal static class StartList
    {
        private static Vehicle[] startList = new Vehicle[] {
            new Car() { Make = "Progen", Model = "Emerus", Year = 2003, RegistrationNumber = "DJR-273", Seats = 2 },
            new Car() { Make = "Pfister", Model = "811", Year = 2016, RegistrationNumber = "JEK-736", Seats = 4 },
            new Car() { Make = "Dewbauchee", Model = "Vagner", Year = 2010, RegistrationNumber = "TBA-558", Seats = 2 },
            new Car() { Make = "Benefactor", Model = "Krieger", Year = 2011, RegistrationNumber = "ETU-266", Seats = 5 },
            new Motorcycle() { Make = "Nagasaki", Model = "Shinobi", Year = 2011, RegistrationNumber = "HEI-490"},
            new Motorcycle() { Make = "Pegassi", Model = "Ruffian", Year = 2008, RegistrationNumber = "GEW-115"},
            new Motorcycle() { Make = "Maibatsu", Model = "Manchez", Year = 2016, RegistrationNumber = "ASH-371"},
            new Motorcycle() { Make = "Shitzu", Model = "Hakucho", Year = 2012, RegistrationNumber = "ASH-371"},
            new Airplane() { Make = "Nagasaki", Model = "Ultralight", Year = 2002, RegistrationNumber = "JEU-631", EngineCount = 1, MaximumAltitude = 10000 },
            new Airplane() { Make = "Mamoth", Model = "Hydra", Year = 2018, RegistrationNumber = "JWY-991", EngineCount = 1, MaximumAltitude = 35000 },
            new Airplane() { Make = "Buckingham", Model = "Miljet", Year = 2012, RegistrationNumber = "VCE-732", EngineCount = 2, MaximumAltitude = 22500 },
            new Airplane() { Make = "Buckingham", Model = "Nimbus", Year = 2021, RegistrationNumber = "HWP-826", EngineCount = 2, MaximumAltitude = 25000 },
            new Boat() { Make = "Shitzu", Model = "Longfin", Year = 2011, RegistrationNumber = "KSE-182", PropulsionType = PropulsionType.Engine },
            new Boat() { Make = "Pegassi", Model = "Speeder", Year = 2014, RegistrationNumber = "ELJ-221", PropulsionType = PropulsionType.Engine },
            new Boat() { Make = "Dinka", Model = "Marquis", Year = 2008, RegistrationNumber = "SUF-291", PropulsionType = PropulsionType.Sail },
            new Boat() { Make = "Buckingham", Model = "Tug", Year = 2009, RegistrationNumber = "EDA-457", PropulsionType = PropulsionType.Rowing },
            new Bus() { Make = "MLT", Model = "Dune", Year = 2004, RegistrationNumber = "GWO-838", Seats = 30 },
            new Bus() { Make = "Brute", Model = "Shuttle", Year = 2009, RegistrationNumber = "KEI-534", Seats = 75 },
            new Bus() { Make = "Vapid", Model = "Dashound", Year = 2014, RegistrationNumber = "BRW-368", Seats = 85 },
            new Bus() { Make = "Karin", Model = "Brickade", Year = 2005, RegistrationNumber = "HJV-903", Seats = 60 },
        };

        public static Vehicle[] GetRandomList(int size) {
            if (size > startList.Length) size = startList.Length;
            Vehicle[] list = new Vehicle[size];
            int[] indeces = new int[size];
            for (int i = 0; i < size; i++) indeces[i] = i;
            Random rnd = new Random();
            for (int i = 0; i < size; i++) {
                int j = rnd.Next(0, size);

                int tmp = indeces[i];
                indeces[i] = indeces[j];
                indeces[j] = tmp;
            }
            for (int i = 0; i < size; i++) {
                list[i] = startList[indeces[i]];
            }

            return list;
        }
    }
}
