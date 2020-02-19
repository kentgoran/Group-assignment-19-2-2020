using System;

namespace AssemblyOne
{
    public class Car : IVehicle
    {
        private string regNum;
        private DateTime arrivalTime;
        private int size = 2;

        public string RegNum { get => regNum;}
        public DateTime ArrivalTime { get => arrivalTime;}
        public int Size { get => size;}

        public Car(string regNum)
        {
            this.regNum = regNum;
            this.arrivalTime = DateTime.Now;
        }

        public Car(string regNum, DateTime arrivalTime)
        {
            this.regNum = regNum;
            this.arrivalTime = arrivalTime;
        }

        public IVehicle Clone()
        {

            string clonedRegNum = this.RegNum;
            DateTime clonedArrivalTime = this.ArrivalTime;

            return new Car(clonedRegNum, clonedArrivalTime);
        }
    }
}
