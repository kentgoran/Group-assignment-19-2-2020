using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyOne;
using AssemblyTwo;

namespace Avancerad.NET_GruppArbete
{
    class Program
    {
        static void Main(string[] args)
        {
            ParkingLot parking = new ParkingLot(100,2);

            IVehicle simonCar = new Car("OCF712");
            IVehicle jesperCar = new Car("GDH475");
            IVehicle gipCar = new Car("HJU774");
            IVehicle simonMotorcycle = new Motorcycle("vroom");
            IVehicle jesperMotorcycle = new Motorcycle("vromvrom");


            parking.AddVehicle(simonCar, 10);
            parking.AddVehicle(jesperCar);
            parking.AddVehicle(gipCar, 40);
            parking.AddVehicle(simonMotorcycle, 20);
            parking.AddVehicle(jesperMotorcycle, 50);

            for(int i = 1; i < 101; i++)
            {
                foreach(var vehicle in parking[i])
                {
                    Console.WriteLine(vehicle.RegNum + " " + i.ToString());
                }
            }
            parking.Optimize();
            Console.WriteLine("-----------------------------------------------------------------------------");
            for (int i = 1; i < 101; i++)
            {
                foreach (var vehicle in parking[i])
                {
                    Console.WriteLine(vehicle.RegNum + " " + i.ToString());
                }
            }
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
