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
        public int Height
        {
            get;
            private set;
        }
        /// <summary>
        /// The length of the ship, in containers.
        /// </summary>
        public int Length
        {
            get;
            private set;
        }
        /// <summary>
        /// The weight of the ship, in kg.
        /// </summary>
        public int Weight
        {
            get;
            private set;
        }
        /// <summary>
        /// The width of the ship, in containers.
        /// </summary>
        public int Width
        {
            get;
            private set;
        }
        /// <summary>
        /// A ReadOnlyList containing all of the container stacks that are a part of the ship.
        /// </summary>
        public IReadOnlyList<ContainerStack> iContainerStacks
        {
            get
            {
                return containerStacks;
            }
        }
        
        /// <summary>
        /// Half the length of the ship, rounded down, in containers.
        /// </summary>
        private int halfLength = 0;
        /// <summary>
        /// Half the width of the ship, rounded down, in containers.
        /// </summary>
        private int halfWidth = 0;

        /// <summary>
        /// The maximum possible amount of containers that can be kept on the ship.
        /// </summary>
        private int maxContainers = 0;
        /// <summary>
        /// The maximum possible amount of cooled containers that can be kept on the ship.
        /// </summary>
        private int maxCooledContainers = 0;

        /// <summary>
        /// The X position at which the top and right sides start, in containers.
        /// </summary>
        private int positiveStartX = 0;
        /// <summary>
        /// The X position at which the bottom and left sides start, in containers.
        /// </summary>
        private int negativeStartX = 0;
        /// <summary>
        /// The Y position at which the top and right sides start, in containers.
        /// </summary>
        private int positiveStartY = 0;
        /// <summary>
        /// The Y position at which the bottom and left sides start, in containers.
        /// </summary>
        private int negativeStartY = 0;

        /// <summary>
        /// A list filled with containers of type normal.
        /// </summary>
        private List<Container> normalContainers = new List<Container>();
        /// <summary>
        /// A list filled with containers of type cooled.
        /// </summary>
        private List<Container> cooledContainers = new List<Container>();
        /// <summary>
        /// A list filled with containers of type valuable.
        /// </summary>
        private List<Container> valuableContainers = new List<Container>();
        /// <summary>
        /// A list containg container stacks which hold the containers.
        /// </summary>
        private List<ContainerStack> containerStacks;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_height">The height of the ship, in containers.</param>
        /// <param name="_length">The length of the ship, in containers.</param>
        /// <param name="_width">The width of the ship, in containers.</param>
        public Ship(int _height, int _length, int _weight, int _width)
        {
            Height = _height;
            Length = _length;
            Weight = _weight;
            Width = _width;
            
            halfLength = Length / 2;
            halfWidth = Width / 2;

            maxContainers = Height * Length * Width;
            maxCooledContainers = Height * Width;

            positiveStartX = Width / 2;
            negativeStartX = Width / 2;
            if (IsEven(Width))
            {
                negativeStartX -= 1;
            }

            positiveStartY = Length / 2;
            negativeStartY = Length / 2;
            if(IsEven(Length))
            {
                negativeStartY -= 1;
            }

            containerStacks = new List<ContainerStack>(Length * Width);
            for(int x = 0; x < Width; x++)
            {
                for(int y = 0; y < Length; y++)
                {
                    containerStacks.Add(new ContainerStack(Height, x, y));
                }
            }
        }

        /// <summary>
        /// Place the containers within the ship on the correct positions and return true. Returns false if not all containers could be placed.
        /// </summary>
        /// <returns></returns>
        public bool PlaceContainers()
        {
            if(GetTotalCargoWeight() < Weight / 2.0f)
            {
                return false;
            }

            cooledContainers = cooledContainers.OrderByDescending(container => container.Weight).ToList();
            foreach (Container container in cooledContainers)
            {
                if (!PlaceCooledContainer(container))
                {
                    return false;
                }
            }

            normalContainers = normalContainers.OrderByDescending(container => container.Weight).ToList();
            foreach (Container container in normalContainers)
            {
                if (!PlaceNormalContainer(container))
                {
                    return false;
                }
            }

            valuableContainers.OrderByDescending(container => container.Weight);
            foreach (Container container in valuableContainers)
            {
                if (!PlaceValuableContainer(container))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Returns the total weight of the stacks on the bottom side of the ship, in kg.
        /// </summary>
        /// <returns></returns>
        public int GetBottomSideWeight()
        {
            int bottomSideWeight = 0;

            List<ContainerStack> bottomSideContainerStacks = containerStacks.FindAll(stack => stack.Y <= negativeStartY);
            bottomSideContainerStacks.ForEach(stack => bottomSideWeight += stack.GetStackWeight());
            return bottomSideWeight;
        }

        /// <summary>
        /// Returns the total weight of the stacks on the left side of the ship, in kg.
        /// </summary>
        /// <returns></returns>
        public int GetLeftSideWeight()
        {
            int leftSideWeight = 0;

            List<ContainerStack> leftSideContainerStacks = containerStacks.FindAll(stack => stack.X <= negativeStartX);
            leftSideContainerStacks.ForEach(stack => leftSideWeight += stack.GetStackWeight());
            return leftSideWeight;
        }

        /// <summary>
        /// Returns the total weight of the stacks on the right side of the ship, in kg.
        /// </summary>
        /// <returns></returns>
        public int GetRightSideWeight()
        {
            int rightSideWeight = 0;

            List<ContainerStack> rightSideContainerStacks = containerStacks.FindAll(stack => stack.X >= positiveStartX);
            rightSideContainerStacks.ForEach(stack => rightSideWeight += stack.GetStackWeight());
            return rightSideWeight;
        }

        /// <summary>
        /// Returns the total weight of the stacks on the top side of the ship, in kg.
        /// </summary>
        /// <returns></returns>
        public int GetTopSideWeight()
        {
            int topSideWeight = 0;

            List<ContainerStack> topSideContainerStacks = containerStacks.FindAll(stack => stack.Y >= positiveStartY);
            topSideContainerStacks.ForEach(stack => topSideWeight += stack.GetStackWeight());
            return topSideWeight;
        }

        /// <summary>
        /// Adds the given container to the correct list and returns true if succesfull. Returns false otherwise.
        /// </summary>
        /// <param name="container">The container to add.</param>
        public bool AddContainer(Container container)
        {
            if (cooledContainers.Count + normalContainers.Count + valuableContainers.Count + 1 <= maxContainers)
            {
                if (container.Type == ContainerType.Cooled && cooledContainers.Count < maxCooledContainers)
                {
                    cooledContainers.Add(container);
                    return true;
                }
                else if (container.Type == ContainerType.Normal)
                {
                    normalContainers.Add(container);
                    return true;
                }
                else if (container.Type == ContainerType.Valuable)
                {
                    valuableContainers.Add(container);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Adds the given list of containers to the correct list and returns true if succesfull. Returns false otherwise.
        /// </summary>
        /// <param name="containers">The containers to add.</param>
        public bool AddContainers(List<Container> containers)
        {
            foreach(Container container in containers)
            {
                if(!AddContainer(container))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Is the given value even?
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns></returns>
        private bool IsEven(int value)
        {
            return (value % 2 == 0);
        }

        /// <summary>
        /// Places the given container, which must be cooled, in an appropriate container stack and returns true. Returns false if the container couldn't be placed.
        /// </summary>
        /// <param name="container">The container to place.</param>
        /// <returns></returns>
        private bool PlaceCooledContainer(Container container)
        {
            if (container.Type != ContainerType.Cooled)
            {
                return false;
            }

            List<WeightDirectionWrapper> optimalWrappers = GetOptimalDirections();

            int startPosX = 0;
            int startPosY = Length - 1;

            if (optimalWrappers[0].X < 0)
            {
                startPosX = halfWidth;
            }
            else
            {
                startPosX = halfWidth + 1;
            }

            int currentPosX = startPosX;

            int directionWrapperIndex = 0;
            do
            {
                ContainerStack selectedStack = containerStacks.Find(stack => stack.X == currentPosX && stack.Y == startPosY);
                if (!selectedStack.CanContainerBePlaced(container))
                {
                    if ((optimalWrappers[directionWrapperIndex].X > 0 && currentPosX >= Width - 1) || (optimalWrappers[directionWrapperIndex].X < 0 && currentPosX <= 0))
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

        /// <summary>
        /// Places the given container, which must be normal, in an appropriate container stack and returns true. Returns false if the container could not be placed.
        /// </summary>
        /// <param name="container">The container to place.</param>
        /// <returns></returns>
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
                startPosX = halfWidth;
            }
            else
            {
                startPosX = halfWidth + 1;
            }

            if (optimalWrappers[0].Y < 0)
            {
                startPosY = halfLength;
            }
            else
            {
                startPosY = halfLength + 1;
            }

            int currentPosX = startPosX;
            int currentPosY = startPosY;

            int directionWrapperIndex = 0;
            do
            {
                ContainerStack selectedStack = containerStacks.Find(stack => stack.X == currentPosX && stack.Y == currentPosY);
                if (!selectedStack.CanContainerBePlaced(container))
                {
                    if ((optimalWrappers[directionWrapperIndex].X > 0 && currentPosX >= Width - 1) || (optimalWrappers[directionWrapperIndex].X < 0 && currentPosX <= 0))
                    {
                        if ((optimalWrappers[directionWrapperIndex].Y > 0 && currentPosY >= Length - 1) || (optimalWrappers[directionWrapperIndex].Y < 0 && currentPosY <= 0))
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

        /// <summary>
        /// Places the given container, which must be valuable, in an appropriate container stack and returns true. Returns false if the container could not be placed.
        /// </summary>
        /// <param name="container">The container to place.</param>
        /// <returns></returns>
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
                startPosX = halfWidth;
            }
            else
            {
                startPosX = halfWidth + 1;
            }

            if (optimalWrappers[0].Y < 0)
            {
                startPosY = halfLength / 2;
            }
            else
            {
                startPosY = halfLength / 2 + 1;
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
                if (currentPosY + 1 <= Length - 1)
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
                    if ((optimalWrappers[directionWrapperIndex].X > 0 && currentPosX >= Width - 1) || (optimalWrappers[directionWrapperIndex].X < 0 && currentPosX <= 0))
                    {
                        if ((optimalWrappers[directionWrapperIndex].Y > 0 && currentPosY >= Length - 1) || (optimalWrappers[directionWrapperIndex].Y < 0 && currentPosY <= 0))
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

        /// <summary>
        /// Returns the total weight of all containers in all three lists, in kg.
        /// </summary>
        /// <returns></returns>
        private int GetTotalCargoWeight()
        {
            int totalWeight = 0;

            cooledContainers.ForEach(container => totalWeight += container.Weight);
            normalContainers.ForEach(container => totalWeight += container.Weight);
            valuableContainers.ForEach(container => totalWeight += container.Weight);

            return totalWeight;
        }

        /// <summary>
        /// Returns a list of WeightDirectionWrappers which are ordered in the most optimal order, starting with the most optimal.
        /// </summary>
        /// <returns></returns>
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
    }
}