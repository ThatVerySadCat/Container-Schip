using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public bool AddContainer(Container container)
        {
            if (container.Type == ContainerType.Cooled)
            {
                AddCooledContainer(container);
            }
            else if(container.Type == ContainerType.Normal)
            {
                AddNormalContainer(container);
            }

            return true;
        }

        private bool AddCooledContainer(Container container)
        {
            if(container.Type != ContainerType.Cooled)
            {
                return false;
            }

            List<WeightDirectionWrapper> optimalWrappers = GetOptimalDirections();
            int startPosX = 0;
            int startPosY = length - 1;

            if(optimalWrappers[0].X < 0)
            {
                startPosX = width / 2;
            }
            else
            {
                startPosX = width / 2 + 1;
            }

            int currentPosX = startPosX;
            int currentPosY = startPosY;
            
            int horizontalDirectionIndex = 0;
            do
            {
                ContainerStack chosenContainerStack = containerStacks.Find(stack => stack.X == currentPosX && stack.Y == currentPosY);
                if (!chosenContainerStack.CanContainerBePlaced(container))
                {
                    if ((optimalWrappers[horizontalDirectionIndex].X > 0 && currentPosX >= width - 1) || (optimalWrappers[horizontalDirectionIndex].X < 0 && currentPosX <= 0))
                    {
                        horizontalDirectionIndex += 1;
                        currentPosX = startPosX;

                        if (horizontalDirectionIndex > optimalWrappers.Count)
                        {
                            return false;
                        }
                    }

                    currentPosX += optimalWrappers[horizontalDirectionIndex].X;
                }
                else
                {
                    chosenContainerStack.AddContainer(container);
                    break;
                }
            } while (horizontalDirectionIndex < optimalWrappers.Count);

            return true;
        }

        private bool AddNormalContainer(Container container)
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
                startPosY = width / 2;
            }
            else
            {
                startPosY = width / 2 + 1;
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
                        if ((optimalWrappers[directionWrapperIndex].Y > 0 && currentPosY >= height - 1) || (optimalWrappers[directionWrapperIndex].Y < 0 && currentPosY <= 0))
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
                    selectedStack.AddContainer(container);
                    return true;
                }
            } while (true);
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