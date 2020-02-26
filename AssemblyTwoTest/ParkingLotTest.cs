using System;
using System.Collections;
using System.Collections.Generic;
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
            Assert.Fail("This should have caused an IndexOutOfRangeException");
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
            Motorcycle toInsert = new Motorcycle("ValidVroom");
            ParkingLot parkingLot = new ParkingLot(10, 2);
            parkingLot.AddVehicle(toInsert);
            bool success = parkingLot.MoveVehicle("validvroom", 5);
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void MoveVehicleNewSpotFull()
        {
            Car toInsert = new Car("LZC418");
            Car toInsertTwo = new Car("ORU270");
            ParkingLot parkingLot = new ParkingLot(10, 2);
            parkingLot.AddVehicle(toInsert);
            parkingLot.AddVehicle(toInsertTwo);

            bool insertResult = parkingLot.MoveVehicle("ORU270", 1);

            Assert.IsFalse(insertResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MoveVehicleRegNumNotFound_ThrowsArgumentException()
        {
            ParkingLot parkingLot = new ParkingLot(10, 2);

            parkingLot.MoveVehicle("Hej", 10);

        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void MoveVehicleInvalidSpot_ThrowsIndexOutOfRangeException()
        {
            ParkingLot parkingLot = new ParkingLot(10, 2);
            Car toInsert = new Car("UZU840");
            parkingLot.AddVehicle(toInsert);


            parkingLot.MoveVehicle("UZU840", 11);

        }

        [TestMethod]
        public void MoveCarToPartiallyFilledSpot()
        {
            ParkingLot parkingLot = new ParkingLot(10, 4);
            Car toInsert = new Car("UZU840");
            Car toInsertTwo = new Car("ABC666");
            parkingLot.AddVehicle(toInsert);
            parkingLot.AddVehicle(toInsertTwo, 5);

            bool result = parkingLot.MoveVehicle("ABC666", 1);


            Assert.IsTrue(result);
        }

        [TestMethod]
        public void MoveMotorcycleToPartiallyFilledSpot()
        {
            ParkingLot parkingLot = new ParkingLot(10, 2);
            Motorcycle toInsert = new Motorcycle("UZU840");
            Motorcycle toInsertTwo = new Motorcycle("ABC666");
            parkingLot.AddVehicle(toInsert);
            parkingLot.AddVehicle(toInsertTwo, 5);

            bool result = parkingLot.MoveVehicle("ABC666", 1);

            Assert.IsTrue(result);
        }



        #endregion
        #region OptimizeTest

        [TestMethod]
        public void OptimizeParkingLotTest()
        {
            Motorcycle toInsert1 = new Motorcycle("MC001");
            Motorcycle toInsert2 = new Motorcycle("MC002");
            Motorcycle toInsert3 = new Motorcycle("MC003");
            Motorcycle toInsert4 = new Motorcycle("MC004");
            Motorcycle toInsert5 = new Motorcycle("MC005");
            Motorcycle toInsert6 = new Motorcycle("MC006");

            Car toInsert7 = new Car("CAR001");
            Car toInsert8 = new Car("CAR002");
            Car toInsert9 = new Car("CAR003");
            Car toInsert10 = new Car("CAR004");
            Car toInsert11 = new Car("CAR005");
            Car toInsert12 = new Car("CAR006");

            ParkingLot parkingLot = new ParkingLot(100, 2);

            parkingLot.AddVehicle(toInsert1, 45);
            parkingLot.AddVehicle(toInsert2, 45);
            parkingLot.AddVehicle(toInsert3, 56);
            parkingLot.AddVehicle(toInsert4, 33);
            parkingLot.AddVehicle(toInsert5, 34);
            parkingLot.AddVehicle(toInsert6, 2);
            parkingLot.AddVehicle(toInsert7, 67);
            parkingLot.AddVehicle(toInsert8, 43);
            parkingLot.AddVehicle(toInsert9, 98);
            parkingLot.AddVehicle(toInsert10, 10);
            parkingLot.AddVehicle(toInsert11, 23);
            parkingLot.AddVehicle(toInsert12, 88);

            parkingLot.Optimize();

            Assert.IsTrue(parkingLot[10].Count == 0);

        }

        [TestMethod]
        public void OptimizeParkingLotTestOrders()
        {
            Motorcycle toInsert1 = new Motorcycle("MC001");
            Motorcycle toInsert2 = new Motorcycle("MC002");
            Motorcycle toInsert3 = new Motorcycle("MC003");
            Motorcycle toInsert4 = new Motorcycle("MC004");
            Motorcycle toInsert5 = new Motorcycle("MC005");
            Motorcycle toInsert6 = new Motorcycle("MC006");

            Car toInsert7 = new Car("CAR001");
            Car toInsert8 = new Car("CAR002");
            Car toInsert9 = new Car("CAR003");
            Car toInsert10 = new Car("CAR004");
            Car toInsert11 = new Car("CAR005");
            Car toInsert12 = new Car("CAR006");

            ParkingLot parkingLot = new ParkingLot(100, 2);

            parkingLot.AddVehicle(toInsert1, 45);
            parkingLot.AddVehicle(toInsert2, 45);
            parkingLot.AddVehicle(toInsert3, 56);
            parkingLot.AddVehicle(toInsert4, 33);
            parkingLot.AddVehicle(toInsert5, 34);
            parkingLot.AddVehicle(toInsert6, 2);
            parkingLot.AddVehicle(toInsert7, 67);
            parkingLot.AddVehicle(toInsert8, 43);
            parkingLot.AddVehicle(toInsert9, 98);
            parkingLot.AddVehicle(toInsert10, 10);
            parkingLot.AddVehicle(toInsert11, 23);
            parkingLot.AddVehicle(toInsert12, 88);

            List<string> orders = parkingLot.Optimize();

            Assert.IsTrue(orders[5] == "Move motorcycle MC002 from spot 45 to spot 5");

        }
        #endregion
    }
}
