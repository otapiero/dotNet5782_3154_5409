using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace v
{
    class Simulator
    {
        public Simulator(BL.BL b, int id ,Action v, Func<bool> k)
        {
            while(k.Invoke()==false)
            {

            }
        }
    }
}
