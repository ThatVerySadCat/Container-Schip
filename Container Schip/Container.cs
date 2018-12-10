using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container_Schip
{
    public class Container
    {
        /// <summary>
        /// The type the container is.
        /// </summary>
        public ContainerType Type
        {
            get;
            private set;
        }
        /// <summary>
        /// The weight of the container, in kg.
        /// </summary>
        public int Weight
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_type">The type of the container.</param>
        /// <param name="_weight">The weight of the container, in kg.</param>
        public Container(ContainerType _type, int _weight) { }
    }
}
