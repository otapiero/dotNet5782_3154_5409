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


            public string Mes;
            public IdExaption(string message) : base(message)
            {
                Mes = message;
            }
        }
    }
}
