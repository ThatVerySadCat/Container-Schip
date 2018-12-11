﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container_Schip
{
    public class ContainerStack
    {
        public bool HasContainers
        {
            get
            {
                if (containers.Count > 0)
                {
                    return true;
                }

                return false;
            }
        }
        public bool HasValuableContainer
        {
            get
            {
                if(containers.Count > 0 && containers[containers.Count - 1].Type == ContainerType.Valuable)
                {
                    return true;
                }

                return false;
            }
        }
        public int X
        {
            get;
            private set;
        }
        public int Y
        {
            get;
            private set;
        }
        public IReadOnlyList<Container> iContainers
        {
            get
            {
                return containers;
            }
        }

        /// <summary>
        /// The maximum height of the stack, in containers.
        /// </summary>
        private int maxHeight;
        /// <summary>
        /// A list containing the containers on the stack.
        /// </summary>
        private List<Container> containers;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxHeight">The maximum allowed height of the container stack, in containers.</param>
        public ContainerStack(int _maxHeight, int _x, int _y)
        {
            maxHeight = _maxHeight;
            X = _x;
            Y = _y;
            containers = new List<Container>(maxHeight);
        }

        /// <summary>
        /// Adds a container to the stack and returns true, if possible. Returns false otherwise.
        /// </summary>
        /// <param name="container">The container to place.</param>
        /// <returns></returns>
        public bool AddContainer(Container container)
        {
            if(containers.Count < maxHeight)
            {
                containers.Add(container);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns true if the given container can be placed on the stack. Returns false otherwise.
        /// </summary>
        /// <param name="container">The container to check.</param>
        /// <returns></returns>
        public bool CanContainerBePlaced(Container container)
        {
            return (containers.Count < maxHeight && 
                (containers.Count == 0 || (containers.Count > 0 && containers[containers.Count - 1].Type != ContainerType.Valuable)) && 
                GetBottomContainerLoad() + container.Weight <= 120000);
        }

        public int GetStackWeight()
        {
            int stackWeight = 0;
            containers.ForEach(container => stackWeight += container.Weight);
            return stackWeight;
        }

        private int GetBottomContainerLoad()
        {
            if(containers.Count > 1)
            {
                int containerLoad = 0;
                for(int i = 1; i < containers.Count; i++)
                {
                    containerLoad += containers[i].Weight;
                }
            }

            return 0;
        }
    }
}
