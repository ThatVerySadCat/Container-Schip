using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container_Schip
{
    public class WeightDirectionWrapper
    {
        public int Weight
        {
            get;
            private set;
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

        public WeightDirectionWrapper(int _weight, int _x, int _y)
        {
            Weight = _weight;
            X = _x;
            Y = _y;
        }
    }
}
