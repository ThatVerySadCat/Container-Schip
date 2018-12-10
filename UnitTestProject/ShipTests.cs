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
        public void AddCooledContainerTest()
        {
            ContainerGenerator generator = new ContainerGenerator();
            List<Container> containers = generator.GetRandomCooledContainers(15);
            Ship ship = new Ship(2, 5, 3);

            bool allContainersAdded = true;
            foreach (Container container in containers)
            {
                if(!(allContainersAdded = ship.AddContainer(container)))
                {
                    allContainersAdded = false;
                    break;
                }
            }

            Assert.AreEqual(true, allContainersAdded);
        }

        [TestMethod]
        public void AddNormalContainerTest()
        {
            ContainerGenerator generator = new ContainerGenerator();
            List<Container> containers = generator.GetRandomNormalContainers(15);
            Ship ship = new Ship(2, 5, 3);

            bool allContainersAdded = true;
            foreach(Container container in containers)
            {
                if(!(allContainersAdded = ship.AddContainer(container)))
                {
                    allContainersAdded = false;
                    break;
                }
            }

            Assert.AreEqual(true, allContainersAdded);
        }
    }
}
