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
            new Car() { Make = "Benefactor", Model = "Krieger", Year = 2011, RegistrationNumber = "GEW-115", Seats = 5 },
            new Motorcycle() { Make = "Nagasaki", Model = "Shinobi", Year = 2011, RegistrationNumber = "HEI-499"},
            new Motorcycle() { Make = "Pegassi", Model = "Ruffian", Year = 2008, RegistrationNumber = "GEW-115"},
            new Motorcycle() { Make = "Maibatsu", Model = "Manchez", Year = 2016, RegistrationNumber = "ASH-371"},
            new Motorcycle() { Make = "Shitzu", Model = "Hakucho", Year = 2012, RegistrationNumber = "ASH-371"},
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
