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
        public class IdExaption : Exception
        {
           
            public IdExaption(string message) : base(message)
            {
            }

           

            public override string ToString()
            {
                return $"IdNotFoundException: DAL error id\n";
            }

            public static implicit operator string(IdExaption v)
            {
                throw new NotImplementedException();
            }
        }
    }
}
