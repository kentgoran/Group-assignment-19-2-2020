using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssemblyOne;
using AssemblyTwo;

namespace AssemblyTwoTest
{
    [TestClass]
    public class ParkingLotTest
    {
        [TestMethod]
        public void AddVehicleValidReg()
        {
            Car testCar = new Car("Legit222");
            ParkingLot testLot = new ParkingLot(10, 2);
            int returnValue = testLot.AddVehicle(testCar);
            Assert.AreEqual<int>(1, returnValue);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddVehicleRegNumIsNullOrEmpty()
        {
            Car testCar = new Car("");
            ParkingLot testLot = new ParkingLot(10, 2);
            testLot.AddVehicle(testCar);
        }

        [TestMethod]
        public void AddVehicleInvalidSpot()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void AddVehicleValidSpot()
        {
            Assert.Inconclusive();
        }
    }
}
