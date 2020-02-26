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
        /// <summary>
        /// Attempts to add a vehicle to the parkingSpot. Returns true if success, else false
        /// Also changes currentlyOccupiedCapacity accordingly.
        /// </summary>
        /// <param name="vehicleToPark">the iVehicle to park</param>
        /// <returns>Boolean, true if success, else false</returns>
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
        /// <summary>
        /// Removes and returns an iVehicle, given it's regnum
        /// </summary>
        /// <param name="regNum">the vehicle to remove's regnum</param>
        /// <returns>The removed vehicle</returns>
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
        /// <summary>
        /// Checks if a vehicle is present at the spot, returns true if it is, else false
        /// </summary>
        /// <param name="regNum">the regnum to check after</param>
        /// <returns>Boolean, true if present, else false</returns>
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
        /// <summary>
        /// Gets and returns a clone of the list of iVehicles currently present in the parkingSpot.
        /// </summary>
        /// <returns>a list of iVehicles</returns>
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
