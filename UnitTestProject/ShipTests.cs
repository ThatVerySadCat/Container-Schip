using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Container_Schip;

namespace UnitTestProject
{
    [TestClass]
    public class ShipTests
    {
        [TestMethod]
        public void AddRandomContainersTest()
        {
            ContainerGenerator generator = new ContainerGenerator();
            Ship ship = new Ship(5, 10, 5, 5);
            ship.AddContainers(generator.GetRandomContainers(40));

            bool actual = ship.PlaceContainers();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void AddCooledContainersUnevenTest()
        {
            ContainerGenerator generator = new ContainerGenerator();
            Ship ship = new Ship(2, 5, 5, 3);
            ship.AddContainers(generator.GetRandomCooledContainers(3));

            bool actual = ship.PlaceContainers();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void AddCooledContainersEvenTest()
        {
            ContainerGenerator generator = new ContainerGenerator();
            Ship ship = new Ship(2, 6, 5, 4);
            ship.AddContainers(generator.GetRandomCooledContainers(3));

            bool actual = ship.PlaceContainers();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void AddNormalContainersUnevenTest()
        {
            ContainerGenerator generator = new ContainerGenerator();
            Ship ship = new Ship(2, 5, 5, 3);
            ship.AddContainers(generator.GetRandomNormalContainers(15));

            bool actual = ship.PlaceContainers();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void AddNormalContainersEvenTest()
        {
            ContainerGenerator generator = new ContainerGenerator();
            Ship ship = new Ship(2, 6, 5, 4);
            ship.AddContainers(generator.GetRandomNormalContainers(15));

            bool actual = ship.PlaceContainers();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void AddValuableContainerUnevenTest()
        {
            ContainerGenerator generator = new ContainerGenerator();
            Ship ship = new Ship(2, 5, 5, 3);
            ship.AddContainers(generator.GetRandomValuableContainers(5));

            bool actual = ship.PlaceContainers();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void AddValuableContainerEvenTest()
        {
            ContainerGenerator generator = new ContainerGenerator();
            Ship ship = new Ship(2, 6, 5, 4);
            ship.AddContainers(generator.GetRandomValuableContainers(5));

            bool actual = ship.PlaceContainers();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void AddCooledContainerOverflowTest()
        {
            Ship ship = new Ship(1, 1, 5, 1);

            Container cooledContainer1 = new Container(ContainerType.Cooled, 0);
            Container cooledContainer2 = new Container(ContainerType.Cooled, 0);

            ship.AddContainer(cooledContainer1);
            bool actual = ship.AddContainer(cooledContainer2);

            Assert.AreEqual(false, actual);
        }

        //[TestMethod]
        public void CheatySheetyPleaseIgnore()
        {
            ContainerGenerator generator = new ContainerGenerator();

            Ship ship = new Ship(5, 19, 5, 11);
            ship.AddContainers(generator.GetRandomNormalContainers(50));
            ship.AddContainers(generator.GetRandomCooledContainers(25));
            ship.AddContainers(generator.GetRandomValuableContainers(15));

            ship.PlaceContainers();
            SheetMaker.ShipToExcelSheet(ship);
        }
    }
}
