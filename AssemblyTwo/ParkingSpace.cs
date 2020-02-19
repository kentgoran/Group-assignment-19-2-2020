using System.Collections.Generic;
using AssemblyOne;

namespace AssemblyTwo
{
    public class ParkingSpace
    {
        private List<IVehicle> parkingSpace;
        internal int currentlyOccupiedCapacity;
        internal int maxCapacity;



        public ParkingSpace(int maxNewCapacity)
        {
            this.parkingSpace = new List<IVehicle>();
            this.currentlyOccupiedCapacity = 0;
            this.maxCapacity = maxNewCapacity;
        }

        public bool AddVehicle(IVehicle vehicleToPark)
        {
            if (currentlyOccupiedCapacity + vehicleToPark.Size <= maxCapacity)
            {
                parkingSpace.Add(vehicleToPark);
                currentlyOccupiedCapacity += vehicleToPark.Size;

                return true;
            }

            return false;
        }

        public IVehicle RemoveVehicle(string regNum)
        {
            int space = 0;

            for (int i = 0; i < parkingSpace.Count; i++)
            {

                if (parkingSpace[i].RegNum == regNum)
                {
                    space = i;
                    break;
                }

            }

            IVehicle vehicleToRemove = parkingSpace[space].Clone();

            parkingSpace.RemoveAt(space);

            currentlyOccupiedCapacity -= vehicleToRemove.Size;

            return vehicleToRemove;
        }

        public bool VehiclePresent(string regNum)
        {
            for(int i = 0; i < parkingSpace.Count; i++)
            {
                if(parkingSpace[i].RegNum == regNum)
                {
                    return true;
                }
            }
            return false;

        }

        public List<IVehicle> GetClone()
        {

            List<IVehicle> clonedList = new List<IVehicle>();

            foreach (var vehicle in parkingSpace)
            {
                clonedList.Add(vehicle.Clone());
            }

            return clonedList;
        }



    }
}
