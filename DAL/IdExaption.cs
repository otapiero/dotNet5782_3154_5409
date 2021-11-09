using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        [Serializable]
        class IdExaption : Exception
        {

            public IdExaption() : base("Id is incorrect")
            {
            }

            public IdExaption(string name) : base(name)
            {

            }
            override public string ToString()
            {
                return "IdNotFoundException: DAL error id\n" + Message;
            }
        }
    }
}
