using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Excel = Microsoft.Office.Interop.Excel;

namespace Container_Schip
{
    public class Ship
    {
        /// <summary>
        /// The height of the ship, in containers.
        /// </summary>
        private int height;
        /// <summary>
        /// The length of the ship, in containers.
        /// </summary>
        private int length;
        /// <summary>
        /// The width of the ship, in containers.
        /// </summary>
        private int width;
        /// <summary>
        /// A list containg container stacks which hold the containers.
        /// </summary>
        private List<ContainerStack> containerStacks;
        private List<Container> normalContainers = new List<Container>();
        private List<Container> cooledContainers = new List<Container>();
        private List<Container> valuableContainers = new List<Container>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_height">The height of the ship, in containers.</param>
        /// <param name="_length">The length of the ship, in containers.</param>
        /// <param name="_width">The width of the ship, in containers.</param>
        public Ship(int _height, int _length, int _width)
        {
            height = _height;
            length = _length;
            width = _width;

            containerStacks = new List<ContainerStack>(length * width);
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < length; y++)
                {
                    containerStacks.Add(new ContainerStack(height, x, y));
                }
            }
        }

        /// <summary>
        /// Adds the given container to the most optimal container stack and returns true. Returns false if unable to place the container.
        /// </summary>
        /// <param name="container">The container to place.</param>
        /// <returns></returns>
        public void AddContainer(Container container)
        {
            if (container.Type == ContainerType.Cooled)
            {
                cooledContainers.Add(container);
            }
            else if(container.Type == ContainerType.Normal)
            {
                normalContainers.Add(container);
            }
            else if(container.Type == ContainerType.Valuable)
            {
                valuableContainers.Add(container);
            }
        }

        public void AddContainers(List<Container> containers)
        {
            foreach(Container container in containers)
            {
                AddContainer(container);
            }
        }

        private bool PlaceCooledContainer(Container container)
        {
            if (container.Type != ContainerType.Cooled)
            {
                return false;
            }

            List<WeightDirectionWrapper> optimalWrappers = GetOptimalDirections();

            int startPosX = 0;
            int startPosY = length - 1;

            if (optimalWrappers[0].X < 0)
            {
                startPosX = width / 2;
            }
            else
            {
                startPosX = width / 2 + 1;
            }

            int currentPosX = startPosX;

            int directionWrapperIndex = 0;
            do
            {
                ContainerStack selectedStack = containerStacks.Find(stack => stack.X == currentPosX && stack.Y == startPosY);
                if (!selectedStack.CanContainerBePlaced(container))
                {
                    if ((optimalWrappers[directionWrapperIndex].X > 0 && currentPosX >= width - 1) || (optimalWrappers[directionWrapperIndex].X < 0 && currentPosX <= 0))
                    {
                        currentPosX = startPosX;

                        directionWrapperIndex += 1;
                        if(directionWrapperIndex >= optimalWrappers.Count)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        currentPosX += optimalWrappers[directionWrapperIndex].X;
                    }
                }
                else
                {
                    return selectedStack.AddContainer(container);
                }
            } while (true);
        }

        private bool PlaceNormalContainer(Container container)
        {
            if (container.Type != ContainerType.Normal)
            {
                return false;
            }

            List<WeightDirectionWrapper> optimalWrappers = GetOptimalDirections();

            int startPosX = 0;
            int startPosY = 0;

            if (optimalWrappers[0].X < 0)
            {
                startPosX = width / 2;
            }
            else
            {
                startPosX = width / 2 + 1;
            }

            if (optimalWrappers[0].Y < 0)
            {
                startPosY = length / 2;
            }
            else
            {
                startPosY = length / 2 + 1;
            }

            int currentPosX = startPosX;
            int currentPosY = startPosY;

            int directionWrapperIndex = 0;
            do
            {
                ContainerStack selectedStack = containerStacks.Find(stack => stack.X == currentPosX && stack.Y == currentPosY);
                if (!selectedStack.CanContainerBePlaced(container))
                {
                    if ((optimalWrappers[directionWrapperIndex].X > 0 && currentPosX >= width - 1) || (optimalWrappers[directionWrapperIndex].X < 0 && currentPosX <= 0))
                    {
                        if ((optimalWrappers[directionWrapperIndex].Y > 0 && currentPosY >= length - 1) || (optimalWrappers[directionWrapperIndex].Y < 0 && currentPosY <= 0))
                        {
                            currentPosX = startPosX;
                            currentPosY = startPosY;

                            directionWrapperIndex += 1;
                            if(directionWrapperIndex >= optimalWrappers.Count)
                            {
                                return false;
                            }
                            else
                            {
                                currentPosY += optimalWrappers[directionWrapperIndex].Y;
                            }
                        }
                        else
                        {
                            currentPosX = startPosX;
                            currentPosY += optimalWrappers[directionWrapperIndex].Y;
                        }
                    }
                    else
                    {
                        currentPosX += optimalWrappers[directionWrapperIndex].X;
                    }
                }
                else
                {
                    return selectedStack.AddContainer(container);
                }
            } while (true);
        }

        private bool PlaceValuableContainer(Container container)
        {
            if (container.Type != ContainerType.Valuable)
            {
                return false;
            }

            List<WeightDirectionWrapper> optimalWrappers = GetOptimalDirections();

            int top = GetTopSideWeight();
            int bottom = GetBottomSideWeight();
            int left = GetLeftSideWeight();
            int right = GetRightSideWeight();

            int startPosX = 0;
            int startPosY = 0;

            if (optimalWrappers[0].X < 0)
            {
                startPosX = width / 2;
            }
            else
            {
                startPosX = width / 2 + 1;
            }

            if (optimalWrappers[0].Y < 0)
            {
                startPosY = length / 2;
            }
            else
            {
                startPosY = length / 2 + 1;
            }

            int currentPosX = startPosX;
            int currentPosY = startPosY;

            int directionWrapperIndex = 0;
            do
            {
                ContainerStack selectedStack = containerStacks.Find(stack => stack.X == currentPosX && stack.Y == currentPosY);
                bool containerCanBePlaced = selectedStack.CanContainerBePlaced(container);

                bool frontOrBackFree = false;
                bool valuableContainerFrontOrBack = false;
                if (currentPosY + 1 <= length - 1)
                {
                    ContainerStack frontStack = containerStacks.Find(stack => stack.X == currentPosX && stack.Y == currentPosY + 1);
                    if(!frontStack.HasContainers)
                    {
                        frontOrBackFree = true;
                    }
                    if(frontStack.HasValuableContainer)
                    {
                        valuableContainerFrontOrBack = true;
                    }
                }
                if (!frontOrBackFree && currentPosY - 1 >= 0)
                {
                    ContainerStack backStack = containerStacks.Find(stack => stack.X == currentPosX && stack.Y == currentPosY - 1);
                    if(!backStack.HasContainers)
                    {
                        frontOrBackFree = true;
                    }
                    
                    if(backStack.HasValuableContainer)
                    {
                        valuableContainerFrontOrBack = true;
                    }
                }

                if(containerCanBePlaced && frontOrBackFree && !valuableContainerFrontOrBack)
                {
                    return selectedStack.AddContainer(container);
                }
                else
                {
                    if ((optimalWrappers[directionWrapperIndex].X > 0 && currentPosX >= width - 1) || (optimalWrappers[directionWrapperIndex].X < 0 && currentPosX <= 0))
                    {
                        if ((optimalWrappers[directionWrapperIndex].Y > 0 && currentPosY >= length - 1) || (optimalWrappers[directionWrapperIndex].Y < 0 && currentPosY <= 0))
                        {
                            currentPosX = startPosX;
                            currentPosY = startPosY;

                            directionWrapperIndex += 1;
                            if (directionWrapperIndex >= optimalWrappers.Count)
                            {
                                return false;
                            }
                            else
                            {
                                currentPosY += optimalWrappers[directionWrapperIndex].Y;
                            }
                        }
                        else
                        {
                            currentPosX = startPosX;
                            currentPosY += optimalWrappers[directionWrapperIndex].Y;
                        }
                    }
                    else
                    {
                        currentPosX += optimalWrappers[directionWrapperIndex].X;
                    }
                }
            } while (true);
        }

        public bool PlaceContainers()
        {
            cooledContainers = cooledContainers.OrderByDescending(container => container.Weight).ToList();
            foreach(Container container in cooledContainers)
            {
                if(!PlaceCooledContainer(container))
                {
                    return false;
                }
            }

            normalContainers = normalContainers.OrderByDescending(container => container.Weight).ToList();
            foreach (Container container in normalContainers)
            {
                if(!PlaceNormalContainer(container))
                {
                    return false;
                }
            }

            valuableContainers.OrderByDescending(container => container.Weight);
            foreach(Container container in valuableContainers)
            {
                if(!PlaceValuableContainer(container))
                {
                    return false;
                }
            }

            return true;
        }

        private List<WeightDirectionWrapper> GetOptimalDirections()
        {
            int leftWeight = GetLeftSideWeight();
            int rightWeight = GetRightSideWeight();
            int topWeight = GetTopSideWeight();
            int bottomWeight = GetBottomSideWeight();

            WeightDirectionWrapper topLeftContainer = new WeightDirectionWrapper(leftWeight + topWeight, -1, 1);
            WeightDirectionWrapper topRightContainer = new WeightDirectionWrapper(rightWeight + topWeight, 1, 1);
            WeightDirectionWrapper bottomLeftContainer = new WeightDirectionWrapper(leftWeight + bottomWeight, -1, -1);
            WeightDirectionWrapper bottomRightContainer = new WeightDirectionWrapper(rightWeight + bottomWeight, 1, -1);

            List<WeightDirectionWrapper> weightDirectionContainers = new List<WeightDirectionWrapper>();
            weightDirectionContainers.Add(topLeftContainer);
            weightDirectionContainers.Add(topRightContainer);
            weightDirectionContainers.Add(bottomLeftContainer);
            weightDirectionContainers.Add(bottomRightContainer);

            return weightDirectionContainers.OrderBy(x => x.Weight).ToList();
        }

        private bool IsEven(int value)
        {
            return (value % 2 == 0);
        }

        private int GetLeftSideWeight()
        {
            int leftSideWeight = 0;

            float adjustmentValue = 0.0f;
            if(!IsEven(width))
            {
                adjustmentValue = 1.0f;
            }

            List<ContainerStack> leftSideContainerStacks = containerStacks.FindAll(stack => (float)stack.X < (float)width / 2.0f - adjustmentValue);
            leftSideContainerStacks.ForEach(stack => leftSideWeight += stack.GetStackWeight());
            return leftSideWeight;
        }

        private int GetRightSideWeight()
        {
            int rightSideWeight = 0;

            List<ContainerStack> rightSideContainerStacks = containerStacks.FindAll(stack => (float)stack.X >= (float)width / 2.0f);
            rightSideContainerStacks.ForEach(stack => rightSideWeight += stack.GetStackWeight());
            return rightSideWeight;
        }

        private int GetTopSideWeight()
        {
            int topSideWeight = 0;

            List<ContainerStack> topSideContainerStacks = containerStacks.FindAll(stack => (float)stack.Y >= (float)length / 2.0f);
            topSideContainerStacks.ForEach(stack => topSideWeight += stack.GetStackWeight());
            return topSideWeight;
        }

        private int GetBottomSideWeight()
        {
            int bottomSideWeight = 0;

            float adjustmentValue = 0.0f;
            if (!IsEven(width))
            {
                adjustmentValue = 1.0f;
            }

            List<ContainerStack> bottomSideContainerStacks = containerStacks.FindAll(stack => (float)stack.Y < (float)length / 2.0f - adjustmentValue);
            bottomSideContainerStacks.ForEach(stack => bottomSideWeight += stack.GetStackWeight());
            return bottomSideWeight;
        }
    }
}