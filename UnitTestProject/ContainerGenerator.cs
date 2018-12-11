using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Container_Schip;

namespace UnitTestProject
{
    class ContainerGenerator
    {
        /// <summary>
        /// The random number generator used to generate random numbers.
        /// </summary>
        private Random rng;

        public ContainerGenerator()
        {
            rng = new Random((int)DateTime.Now.Ticks);
        }

        /// <summary>
        /// Returns a container with a random Type and weight.
        /// </summary>
        /// <returns></returns>
        public Container GetRandomContainer()
        {
            ContainerType randomType = (ContainerType)rng.Next(0, 3);
            int randomWeight = rng.Next(4000, 30001);

            return new Container(randomType, randomWeight);
        }

        /// <summary>
        /// Returns a cooled container with a random weight.
        /// </summary>
        /// <returns></returns>
        public Container GetRandomCooledContainer()
        {
            int randomWeight = rng.Next(4000, 30001);

            return new Container(ContainerType.Cooled, randomWeight);
        }

        /// <summary>
        /// Returns a normal container with a random weight.
        /// </summary>
        /// <returns></returns>
        public Container GetRandomNormalContainer()
        {
            int randomWeight = rng.Next(4000, 30001);

            return new Container(ContainerType.Normal, randomWeight);
        }

        /// <summary>
        /// Returns a valuable container with a random weight.
        /// </summary>
        /// <returns></returns>
        public Container GetRandomValuableContainer()
        {
            int randomWeight = rng.Next(4000, 30001);

            return new Container(ContainerType.Valuable, randomWeight);
        }

        /// <summary>
        /// Returns a list of containers with random types and random weights.
        /// </summary>
        /// <param name="count">The amount of containers to generate.</param>
        /// <returns></returns>
        public List<Container> GetRandomContainers(int count)
        {
            List<Container> returnList = new List<Container>(count);
            for(int i = 0; i < count; i++)
            {
                returnList.Add(GetRandomContainer());
            }

            return returnList;
        }

        /// <summary>
        /// Returns a list of cooled containers with random weights.
        /// </summary>
        /// <param name="count">The amount of cooled containers to generate.</param>
        /// <returns></returns>
        public List<Container> GetRandomCooledContainers(int count)
        {
            List<Container> returnList = new List<Container>(count);
            for (int i = 0; i < count; i++)
            {
                returnList.Add(GetRandomCooledContainer());
            }

            return returnList;
        }

        /// <summary>
        /// Returns a list of normal containers with random weights.
        /// </summary>
        /// <param name="count">The amount of normal containers to generate.</param>
        /// <returns></returns>
        public List<Container> GetRandomNormalContainers(int count)
        {
            List<Container> returnList = new List<Container>(count);
            for (int i = 0; i < count; i++)
            {
                returnList.Add(GetRandomNormalContainer());
            }

            return returnList;
        }

        /// <summary>
        /// Returns a list of valuable containers with random weights.
        /// </summary>
        /// <param name="count">The amount of normal containers to generate.</param>
        /// <returns></returns>
        public List<Container> GetRandomValuableContainers(int count)
        {
            List<Container> returnList = new List<Container>(count);
            for(int i = 0; i < count; i++)
            {
                returnList.Add(GetRandomValuableContainer());
            }

            return returnList;
        }
    }
}
