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
        public Ship(int _height, int _length, int _width) { }

        /// <summary>
        /// Adds the given container to the most optimal container stack and returns true. Returns false if unable to place the container.
        /// </summary>
        /// <param name="container">The container to place.</param>
        /// <returns></returns>
        public bool AddContainer(Container container) { throw new NotImplementedException(); }
    }
}
