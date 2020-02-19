using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyOne;

namespace AssemblyTwo
{
    public class ParkingLot
    {
        private List<ParkingSpace> parkingLot = new List<ParkingSpace>();
        public List<IVehicle> this[int i] => parkingLot[i].GetClone();

        public ParkingLot(int parkingLotSize, int parkingSpaceSize)
        {
            for (int i = 0; i < parkingLotSize + 1; i++)
            {
                parkingLot.Add(new ParkingSpace(parkingSpaceSize));
            }
        }

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

        public int AddVehicle(IVehicle vehicle, int spot)
        {
            return parkingLot[spot].AddVehicle(vehicle) ? spot : -1;
        }

        public IVehicle RemoveVehicle(string regNum)
        {
            int spot = FindVehicle(regNum);

            IVehicle removedVehicle = parkingLot[spot].RemoveVehicle(regNum);

            return removedVehicle;
        }

        public bool MoveVehicle(string regNum, int spot)
        {
            int oldSpot = FindVehicle(regNum);

            IVehicle movingVehicle = RemoveVehicle(regNum);

            if (!parkingLot[spot].AddVehicle(movingVehicle))
            {
                parkingLot[oldSpot].AddVehicle(movingVehicle);
                return false;
            }

            return true;
        }

        public List<string> Optimize()
        {
            List<string> orders = new List<string>();
            for (int i = 1; i < parkingLot.Count; i++)
            {
                if(parkingLot[i].currentlyOccupiedCapacity < parkingLot[i].maxCapacity)
                {
                    for(int j = parkingLot.Count - 1; j > i; j--)
                    {
                        if(parkingLot[j].currentlyOccupiedCapacity <= parkingLot[i].maxCapacity - parkingLot[i].currentlyOccupiedCapacity)
                        {
                            List<IVehicle> vehiclesToMove = parkingLot[j].GetClone();
                            foreach (var vehicle in vehiclesToMove)
                            {
                                parkingLot[i].AddVehicle(vehicle);
                                parkingLot[j].RemoveVehicle(vehicle.RegNum);

                                orders.Add($"Move {vehicle.GetType()} {vehicle.RegNum} from spot {j} to spot {i}");
                              
                            }
                        }
                    }
                }
            }
            return orders;
        }

        public int FindVehicle(string regNum)
        {
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
                throw new Exception("Registration number was not found");
            }

            return spot;
        }

        


    }
}
