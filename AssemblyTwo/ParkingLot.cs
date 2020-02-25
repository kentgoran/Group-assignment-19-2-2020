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
            if (string.IsNullOrEmpty(vehicle.RegNum))
            {
                throw new ArgumentNullException("vehicle.RegNum");
            }
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
            //TODO: CHECK SPOT
            if (string.IsNullOrEmpty(vehicle.RegNum))
            {
                throw new ArgumentNullException("vehicle.RegNum");
            }
            return parkingLot[spot].AddVehicle(vehicle) ? spot : -1;
        }

        public IVehicle RemoveVehicle(string regNum)
        {
            regNum = regNum.ToUpper();
            int spot = FindVehicle(regNum);

            IVehicle removedVehicle = parkingLot[spot].RemoveVehicle(regNum);

            return removedVehicle;
        }

        public bool MoveVehicle(string regNum, int spot)
        {
            regNum = regNum.ToUpper();
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
                throw new Exception("Registration number was not found");
            }

            return spot;
        }

        


    }
}
