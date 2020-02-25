using System;

namespace AssemblyOne
{
    public class Motorcycle : IVehicle
    {
        private string regNum;
        private DateTime arrivalTime;
        private int size = 1;

        public string RegNum { get => regNum; }
        public DateTime ArrivalTime { get => arrivalTime; }
        public int Size { get => size; }

        public Motorcycle(string regNum)
        {
            this.regNum = regNum.ToUpper();
            this.arrivalTime = DateTime.Now;
        }

        public Motorcycle(string regNum, DateTime arrivalTime)
        {
            this.regNum = regNum.ToUpper();
            this.arrivalTime = arrivalTime;
        }

        public IVehicle Clone()
        {

            string clonedRegNum = this.RegNum;
            DateTime clonedArrivalTime = this.ArrivalTime;

            return new Motorcycle(clonedRegNum, clonedArrivalTime);
        }
    }
}
