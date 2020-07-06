using System;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTest
{
    [TestClass]
    public class BuildingTest
    {
        [TestMethod]
        public void TestIfCorrectPropertiesAreAssigned()
        {
            string buildingName = "Test Name";
            string id = "Test id";
            uint floors = 0;
            bool isParkingAllotted = true;

            Building building = new Building(buildingName, id, floors, true);
            Assert.AreEqual(buildingName, building.Name);
            Assert.AreEqual(id, building.Id);
            Assert.AreEqual(floors, building.Floors);
            Assert.AreEqual(true, building.IsParkingSpaceAllotted);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIfBuildingNameIsValidated()
        {
            string buildingName = null;
            string id = "Test id";
            uint floors = 0;

            Building building = new Building(buildingName, id, floors, true);

            buildingName = string.Empty;
            building = new Building(buildingName, id, floors, true);

            buildingName = "   ";
            building = new Building(buildingName, id, floors, true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIfIdIsValidated()
        {
            string buildingName = "Test Name";
            string id = null;
            uint floors = 0;

            Building building = new Building(buildingName, id, floors, true);

            id = string.Empty;
            building = new Building(buildingName, id, floors, true);

            id = "   ";
            building = new Building(buildingName, id, floors, true);
        }

        [TestMethod]
        public void TestIfCorrectVacantRoomCountIsShown()
        {

        }


    }
}
