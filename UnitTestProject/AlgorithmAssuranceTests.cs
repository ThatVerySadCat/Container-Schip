using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Container_Schip;

namespace UnitTestProject
{
    [TestClass]
    public class AlgorithmAssuranceTests
    {
        [TestMethod]
        public void CanPlaceContainersTest()
        {
            ContainerGenerator generator = new ContainerGenerator();
            Ship ship = new Ship(5, 10, 5, 12);

            List<Container> cooledContainers = generator.GetRandomCooledContainers(60);
            List<Container> normalContainers = generator.GetRandomNormalContainers(100);
            List<Container> valuableContainers = generator.GetRandomValuableContainers(25);

            ship.AddContainers(cooledContainers);
            ship.AddContainers(normalContainers);
            ship.AddContainers(valuableContainers);

            bool actual = ship.PlaceContainers();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void EnsureNoValuableIsPlacedOnValuableTest()
        {
            ContainerGenerator generator = new ContainerGenerator();
            Ship ship = new Ship(5, 10, 5, 10);

            List<Container> valuableContainers = generator.GetRandomValuableContainers(100);

            ship.AddContainers(valuableContainers);
            ship.PlaceContainers();

            bool valuableOnTop = true;
            foreach(ContainerStack stack in ship.iContainerStacks)
            {
                if (stack.HasValuableContainer)
                {
                    for(int i = 0; i < stack.iContainers.Count - 1; i++)
                    {
                        if(stack.iContainers[i].Type == ContainerType.Valuable)
                        {
                            valuableOnTop = false;
                            break;
                        }
                    }

                    if(valuableOnTop)
                    {
                        break;
                    }
                }
            }

            Assert.AreEqual(true, valuableOnTop);
        }

        [TestMethod]
        public void WeightMarginRightLeftTest()
        {
            ContainerGenerator generator = new ContainerGenerator();
            Ship ship = new Ship(5, 15, 5, 10);

            List<Container> cooledContainers = generator.GetRandomCooledContainers(50);
            List<Container> normalContainers = generator.GetRandomNormalContainers(250);

            ship.AddContainers(cooledContainers);
            ship.AddContainers(normalContainers);

            ship.PlaceContainers();

            int rightSideWeight = ship.GetRightSideWeight();
            int leftSideWeight = ship.GetLeftSideWeight();

            float onePercent = rightSideWeight / 100.0f;
            float bottomPercentage = leftSideWeight / onePercent;

            float difference = bottomPercentage - 100.0f;
            if (difference < 0.0f)
            {
                difference *= -1.0f;
            }

            bool isLowerThanTwenty = difference <= 20.0f;
            Assert.AreEqual(true, isLowerThanTwenty);
        }

        [TestMethod]
        public void WeightMarginTopBottomTest()
        {
            ContainerGenerator generator = new ContainerGenerator();
            Ship ship = new Ship(5, 15, 5, 10);

            List<Container> cooledContainers = generator.GetRandomCooledContainers(50);
            List<Container> normalContainers = generator.GetRandomNormalContainers(250);

            ship.AddContainers(cooledContainers);
            ship.AddContainers(normalContainers);

            ship.PlaceContainers();

            int topSideWeight = ship.GetTopSideWeight();
            int bottomSideWeight = ship.GetBottomSideWeight();

            float onePercent = topSideWeight / 100.0f;
            float bottomPercentage = bottomSideWeight / onePercent;

            float difference = bottomPercentage - 100.0f;
            if(difference < 0.0f)
            {
                difference *= -1.0f;
            }

            bool isLowerThanTwenty = difference <= 20.0f;
            Assert.AreEqual(true, isLowerThanTwenty);
        }

        [TestMethod]
        public void MinimumCargoWeightSuccessTest()
        {
            Ship ship = new Ship(5, 5, 5, 5);
            Container container = new Container(ContainerType.Normal, 3);

            ship.AddContainer(container);
            bool actual = ship.PlaceContainers();

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void MinimumCargoWeightFailureTest()
        {
            Ship ship = new Ship(5, 5, 5, 5);
            Container container = new Container(ContainerType.Normal, 2);

            ship.AddContainer(container);
            bool actual = ship.PlaceContainers();

            Assert.AreEqual(false, actual);
        }
    }
}
