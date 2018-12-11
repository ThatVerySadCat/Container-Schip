using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Container_Schip;

namespace UnitTestProject
{
    [TestClass]
    public class ContainerStackTests
    {
        [TestMethod]
        public void CanContainerBePlacedValuableOnTopTest()
        {
            ContainerStack stack = new ContainerStack(5, 0, 0);
            Container valuableContainer = new Container(ContainerType.Valuable, 0);
            Container normalContainer = new Container(ContainerType.Normal, 0);

            stack.AddContainer(valuableContainer);
            bool actual = stack.CanContainerBePlaced(normalContainer);

            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void CanContainerBePlacedOverBottomWeightLimitTest()
        {
            ContainerStack stack = new ContainerStack(5, 0, 0);
            Container container1 = new Container(ContainerType.Normal, 1);
            Container container2 = new Container(ContainerType.Normal, 120001);

            stack.AddContainer(container1);
            bool actual = stack.CanContainerBePlaced(container2);

            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void CanContainerBePlacedOverHeightLimitTest()
        {
            ContainerStack stack = new ContainerStack(1, 0, 0);
            Container container1 = new Container(ContainerType.Normal, 0);
            Container container2 = new Container(ContainerType.Normal, 0);

            stack.AddContainer(container1);
            bool actual = stack.CanContainerBePlaced(container2);

            Assert.AreEqual(false, actual);
        }
    }
}
