using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container_Schip
{
    public class WeightDirectionWrapper
    {
        /// <summary>
        /// The ascociated weight, in kg.
        /// </summary>
        public int Weight
        {
            get;
            private set;
        }
        /// <summary>
        /// The X direction.
        /// </summary>
        public int X
        {
            get;
            private set;
        }
        /// <summary>
        /// The Y direction.
        /// </summary>
        public int Y
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_weight">The ascociated weight, in kg.</param>
        /// <param name="_x">The X direction.</param>
        /// <param name="_y">The Y direction.</param>
        public WeightDirectionWrapper(int _weight, int _x, int _y)
        {
            Weight = _weight;
            X = _x;
            Y = _y;
        }
    }
}
