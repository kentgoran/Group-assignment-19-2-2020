using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyOne;

namespace AssemblyTwo
{
    /// <summary>
    /// The ParkingLot-class, instanciated with size of the lot and size of the spots in the lot, stores different types of iVehicle
    /// </summary>
    public class ParkingLot
    {
        private List<ParkingSpace> parkingLot = new List<ParkingSpace>();
        /// <summary>
        /// Returns a cloned list of iVehicles, situated at spot i
        /// </summary>
        /// <param name="i">the parking-spot of which to return the list</param>
        /// <returns>A list of iVehicles</returns>
        public List<IVehicle> this[int i] => parkingLot[i].GetClone();

        /// <summary>
        /// Constructor for ParkingLot. ParkingLotSize determines the amount of parkingspots, and parkingSpaceSize determines the size of each spot.
        /// </summary>
        /// <param name="parkingLotSize">Amount of spots in the parkinglot</param>
        /// <param name="parkingSpaceSize">Size of each individual spot in parkinglot</param>
        public ParkingLot(int parkingLotSize, int parkingSpaceSize)
        {
            for (int i = 0; i < parkingLotSize + 1; i++)
            {
                parkingLot.Add(new ParkingSpace(parkingSpaceSize));
            }
        }
        /// <summary>
        /// Adds a vehicle to the parkingLot. Returns the spot in which the vehicle will be situated. Returns -1 if it can't be added
        /// </summary>
        /// <param name="vehicle">The iVehicle to park at the lot</param>
        /// <returns>An integer, containing the spot the vehicle got. -1 means failure</returns>
        public int AddVehicle(IVehicle vehicle)
        {
            int spot = -1;
            for (int i = 1; i < parkingLot.Count; i++)
            {
                if (parkingLot[i].AddVehicle(vehicle))
                {
                    spot = i;
                    break;
                }
            }
            return spot;
        }
        /// <summary>
        /// Adds a vehicle to the parkingLot, at given spot. Returns the spot in which the vehicle will be situated. Returns -1 if it can't be added
        /// </summary>
        /// <param name="vehicle">The iVehicle to park</param>
        /// <param name="spot">the spot in which to park</param>
        /// <returns>an integer containing the spot in which to park. -1 means failure</returns>
        public int AddVehicle(IVehicle vehicle, int spot)
        {
            if(spot < 1 || spot >= parkingLot.Count)
            {
                throw new IndexOutOfRangeException($"Spotnumber must be between 1 and {parkingLot.Count - 1}.");
            }
            return parkingLot[spot].AddVehicle(vehicle) ? spot : -1;
        }
        /// <summary>
        /// Removes an iVehicle from the parkingLot, given it's regnum, and returns it. Throws ArgumentException if the regnumber isn't present.
        /// </summary>
        /// <param name="regNum">The regnumber of the vehicle to remove</param>
        /// <returns>an iVehicle that has been removed from the parkingLot</returns>
        public IVehicle RemoveVehicle(string regNum)
        {
            regNum = regNum.ToUpper();
            int spot = FindVehicle(regNum);

            IVehicle removedVehicle = parkingLot[spot].RemoveVehicle(regNum);

            return removedVehicle;
        }
        /// <summary>
        /// Attempts to move a vehicle, given it's regnum, from it's old spot to a new, given one. If it fails, the iVehicle is left in it's place.
        /// Throws IndexOutOfRangeException if the spot isn't in the parkingLot, and ArgumentException if the regnumber isn't present.
        /// </summary>
        /// <param name="regNum">regnum of the vehicle to move</param>
        /// <param name="spot">The spot to move to</param>
        /// <returns>Boolean, true for success, false for fail</returns>
        public bool MoveVehicle(string regNum, int spot)
        {
            regNum = regNum.ToUpper();
            int oldSpot = FindVehicle(regNum);
            if (spot < 1 || spot >= parkingLot.Count)
            {
                throw new IndexOutOfRangeException($"Spotnumber must be between 1 and {parkingLot.Count - 1}.");
            }
            IVehicle movingVehicle = RemoveVehicle(regNum);

            if (!parkingLot[spot].AddVehicle(movingVehicle))
            {
                parkingLot[oldSpot].AddVehicle(movingVehicle);
                return false;
            }

            return true;
        }
        /// <summary>
        /// Optimizes all vehicle in the parkingLot, and returns a work order in form of a list of strings, like this: "Move car ABC123 from spot 5 to spot 2"
        /// </summary>
        /// <returns>Work orders in form of a list of strings</returns>
        public List<string> Optimize()
        {
            List<string> orders = new List<string>();

            for (int i = parkingLot.Count-1; i > 1; i--)
            {
                if(parkingLot[i].currentlyOccupiedCapacity > 0)
                {
                    for (int j = 1; j < i; j++)
                    {
                        if(parkingLot[j].currentlyOccupiedCapacity + parkingLot[i].currentlyOccupiedCapacity <= parkingLot[j].maxCapacity && parkingLot[j].maxCapacity != parkingLot[j].currentlyOccupiedCapacity)
                        {
                            List<IVehicle> vehiclesToMove = parkingLot[i].GetClone();
                            foreach (var vehicle in vehiclesToMove)
                            {
                                parkingLot[j].AddVehicle(vehicle);
                                parkingLot[i].RemoveVehicle(vehicle.RegNum);

                                orders.Add($"Move {vehicle.GetType().ToString().Substring(12).ToLower()} {vehicle.RegNum} from spot {i} to spot {j}");
                            }
                            break; 
                        }
                    }
                }
            }


            return orders;
        }
        /// <summary>
        /// Finds a vehicle given it's regnum, and returns it's parkingSpot.
        /// Throws ArgumentException if regnum is not present.
        /// </summary>
        /// <param name="regNum">The regnum to search for</param>
        /// <returns>an integer containing the given vehicle's parkingSpot</returns>
        public int FindVehicle(string regNum)
        {
            regNum = regNum.ToUpper();
            int spot = -1;
            for (int i = 1; i < parkingLot.Count; i++)
            {
                if (parkingLot[i].VehiclePresent(regNum))
                {
                    spot = i;
                    break;
                }
            }
            if (spot == -1)
            {
                throw new ArgumentException("Registration number was not found");
            }

            return spot;
        }

        


    }
}
