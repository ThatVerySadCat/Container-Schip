using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container_Schip
{
    public class ContainerStack
    {
        /// <summary>
        /// The maximum height of the stack, in containers.
        /// </summary>
        private int maxHeight;
        /// <summary>
        /// The X position of the stack on the ship, in containers.
        /// </summary>
        private int x;
        /// <summary>
        /// The Y position of the stack on the ship, in containers.
        /// </summary>
        private int y;
        /// <summary>
        /// A list containing the containers on the stack.
        /// </summary>
        private List<Container> containers;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxHeight">The maximum allowed height of the container stack, in containers.</param>
        public ContainerStack(int maxHeight) { }

        /// <summary>
        /// Adds a container to the stack and returns true, if possible. Returns false otherwise.
        /// </summary>
        /// <param name="container">The container to place.</param>
        /// <returns></returns>
        public bool AddContainer(Container container) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns true if the given container can be placed on the stack. Returns false otherwise.
        /// </summary>
        /// <param name="container">The container to check.</param>
        /// <returns></returns>
        public bool CanContainerBePlaced(Container container) { throw new NotImplementedException(); }
    }
}
