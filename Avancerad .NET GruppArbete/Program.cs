using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyOne;
using AssemblyTwo;

namespace Avancerad.NET_GruppArbete
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Everything found here is purely for testing-purposes. Please disregard
            ParkingLot parking = new ParkingLot(100, 2);

            parking.AddVehicle(new Car("OCF712"), 10);
            parking.AddVehicle(new Car("GDH475"));
            parking.AddVehicle(new Car("HJU774"), 40);
            parking.AddVehicle(new Motorcycle("Vroom"), 20);
            parking.AddVehicle(new Motorcycle("vromvrom"), 50);

            for (int i = 1; i < 101; i++)
            {
                foreach (var vehicle in parking[i])
                {
                    Console.WriteLine(vehicle.RegNum + " " + i.ToString());
                }
            }

            Console.WriteLine("-----------------------------------------------------------------------------");
            List<string> workOrder = parking.Optimize();
            foreach (string order in workOrder)
            {
                Console.WriteLine(order);
            }

            Console.WriteLine("-----------------------------------------------------------------------------");
            for (int i = 1; i < 101; i++)
            {
                foreach (var vehicle in parking[i])
                {
                    Console.WriteLine(vehicle.RegNum + " " + i.ToString());
                }
            }
            
            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.WriteLine(parking.MoveVehicle("OCF712", 2));
            parking.RemoveVehicle("vroom");
            for (int i = 1; i < 101; i++)
            {
                foreach (var vehicle in parking[i])
                {
                    Console.WriteLine(vehicle.RegNum + " " + i.ToString());
                }
            }

               Console.ReadLine(); 
        }
    }
}
