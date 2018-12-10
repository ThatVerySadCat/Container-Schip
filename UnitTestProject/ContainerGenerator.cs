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
        private Random rng;

        public ContainerGenerator()
        {
            rng = new Random((int)DateTime.Now.Ticks);
        }

        public Container GetRandomContainer()
        {
            ContainerType randomType = (ContainerType)rng.Next(0, 3);
            int randomWeight = rng.Next(4000, 30001);

            return new Container(randomType, randomWeight);
        }

        public Container GetRandomCooledContainer()
        {
            int randomWeight = rng.Next(4000, 30001);

            return new Container(ContainerType.Cooled, randomWeight);
        }

        public Container GetRandomNormalContainer()
        {
            int randomWeight = rng.Next(4000, 30001);

            return new Container(ContainerType.Normal, randomWeight);
        }

        public List<Container> GetRandomContainers(int count)
        {
            List<Container> returnList = new List<Container>(count);
            for(int i = 0; i < count; i++)
            {
                returnList.Add(GetRandomContainer());
            }

            return returnList;
        }

        public List<Container> GetRandomCooledContainers(int count)
        {
            List<Container> returnList = new List<Container>(count);
            for (int i = 0; i < count; i++)
            {
                returnList.Add(GetRandomCooledContainer());
            }

            return returnList;
        }

        public List<Container> GetRandomNormalContainers(int count)
        {
            List<Container> returnList = new List<Container>(count);
            for (int i = 0; i < count; i++)
            {
                returnList.Add(GetRandomNormalContainer());
            }

            return returnList;
        }
    }
}
