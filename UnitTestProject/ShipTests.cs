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
            Ship ship = new Ship(5, 10, 5);
            ship.AddContainers(generator.GetRandomContainers(40));

            bool actual = ship.PlaceContainers();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void AddCooledContainersUnevenTest()
        {
            ContainerGenerator generator = new ContainerGenerator();
            Ship ship = new Ship(2, 5, 3);
            ship.AddContainers(generator.GetRandomCooledContainers(3));

            bool actual = ship.PlaceContainers();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void AddCooledContainersEvenTest()
        {
            ContainerGenerator generator = new ContainerGenerator();
            Ship ship = new Ship(2, 6, 4);
            ship.AddContainers(generator.GetRandomCooledContainers(3));

            bool actual = ship.PlaceContainers();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void AddNormalContainersUnevenTest()
        {
            ContainerGenerator generator = new ContainerGenerator();
            Ship ship = new Ship(2, 5, 3);
            ship.AddContainers(generator.GetRandomNormalContainers(15));

            bool actual = ship.PlaceContainers();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void AddNormalContainersEvenTest()
        {
            ContainerGenerator generator = new ContainerGenerator();
            Ship ship = new Ship(2, 6, 4);
            ship.AddContainers(generator.GetRandomNormalContainers(15));

            bool actual = ship.PlaceContainers();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void AddValuableContainerUnevenTest()
        {
            ContainerGenerator generator = new ContainerGenerator();
            Ship ship = new Ship(2, 5, 3);
            ship.AddContainers(generator.GetRandomValuableContainers(5));

            bool actual = ship.PlaceContainers();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void AddValuableContainerEvenTest()
        {
            ContainerGenerator generator = new ContainerGenerator();
            Ship ship = new Ship(2, 6, 4);
            ship.AddContainers(generator.GetRandomValuableContainers(5));

            bool actual = ship.PlaceContainers();

            Assert.AreEqual(true, actual);
        }
    }
}
