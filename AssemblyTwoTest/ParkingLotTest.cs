using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssemblyOne;
using AssemblyTwo;

namespace AssemblyTwoTest
{
    [TestClass]
    public class ParkingLotTest
    {
        #region AddVehicleTest
        [TestMethod]
        public void AddVehicleCarValid()
        {
            Car testCar = new Car("Legit222");
            ParkingLot testLot = new ParkingLot(10, 2);
            int returnValue = testLot.AddVehicle(testCar);
            Assert.AreEqual<int>(1, returnValue);
        }

        [TestMethod]
        public void AddVehicleMotorcycleValid()
        {
            Motorcycle testMc = new Motorcycle("Legit222");
            ParkingLot testLot = new ParkingLot(10, 2);
            int returnValue = testLot.AddVehicle(testMc);
            Assert.AreEqual<int>(1, returnValue);
        }

        [TestMethod]
        public void AddVehicleValidSpot()
        {
            Car testCar = new Car("Legit222");
            ParkingLot testLot = new ParkingLot(10, 2);
            int validSpot = 4;
            int returnValue = testLot.AddVehicle(testCar, validSpot);
            Assert.AreEqual<int>(validSpot, returnValue);
        }

        [TestMethod]
        public void AddVehicleThreeMcsOneSpot()
        {
            Motorcycle mc1 = new Motorcycle("Legit111");
            Motorcycle mc2 = new Motorcycle("Legit222");
            Motorcycle mc3 = new Motorcycle("Legit333");
            ParkingLot testLot = new ParkingLot(10, 3);
            int expectedResult = 1;
            testLot.AddVehicle(mc1);
            testLot.AddVehicle(mc2);
            int result = testLot.AddVehicle(mc3);
            Assert.AreEqual<int>(expectedResult, result);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void AddVehicleInvalidSpot_ThrowsIndexOutOfRangeException()
        {
            Car testCar = new Car("Legit222");
            ParkingLot testLot = new ParkingLot(10, 2);
            int invalidSpot = -2;
            testLot.AddVehicle(testCar, invalidSpot);
        }

        [TestMethod]
        public void AddVehicleNotEnoughSpaceInSpot()
        {
            Car testCar = new Car("Legit222");
            Car testCar2 = new Car("Legit223");
            ParkingLot testLot = new ParkingLot(10, 3);
            int validSpot = 4;
            int expectedResult = -1;
            testLot.AddVehicle(testCar, validSpot);
            int result = testLot.AddVehicle(testCar2, validSpot);
            Assert.AreEqual<int>(expectedResult, result);
        }

        [TestMethod]
        public void AddVehicleNotEnoughSpaceInParkingLot()
        {
            Car testCar = new Car("Legit222");
            ParkingLot testLot = new ParkingLot(10, 1);
            int expectedResult = -1;
            int result = testLot.AddVehicle(testCar);
            Assert.AreEqual<int>(expectedResult, result);
        }

        [TestMethod]
        public void AddVehicleParkingSpotIsZero()
        {
            Car testCar = new Car("Legit222");
            ParkingLot testLot = new ParkingLot(10, 0);
            int expectedResult = -1;
            int result = testLot.AddVehicle(testCar);
            Assert.AreEqual<int>(expectedResult, result);
        }

        #endregion
        #region RemoveVehicleTest
        [TestMethod]
        public void RemoveVehicleValidCar()
        {
            Car testCar = new Car("LEGIT222");
            ParkingLot testLot = new ParkingLot(10, 2);
            testLot.AddVehicle(testCar);
            IVehicle returnCar = testLot.RemoveVehicle("legit222");
            Assert.AreEqual<string>(returnCar.RegNum, testCar.RegNum);
        }
        public void RemoveVehicleValidMc()
        {
            Motorcycle testMc = new Motorcycle("bikey");
            ParkingLot testLot = new ParkingLot(10, 2);
            testLot.AddVehicle(testMc);
            IVehicle returnMc = testLot.RemoveVehicle("BIKEY");
            Assert.AreEqual<string>(returnMc.RegNum, testMc.RegNum);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveVehicleNotPresent_ThrowsArgumentException()
        {
            ParkingLot testLot = new ParkingLot(10, 2);
            testLot.RemoveVehicle("ImNotHere");
        }


        #endregion
        #region MoveVehicleTest
        [TestMethod]
        public void MoveVehicleEverythingValid()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void MoveVehicleNewSpotFull()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MoveVehicleRegNumNotFound_ThrowsArgumentException()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void MoveVehicleInvalidSpot_ThrowsIndexOutOfRangeException()
        {
            Assert.Inconclusive();
        }

        #endregion
    }
}
