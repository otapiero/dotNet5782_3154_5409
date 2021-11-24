using IDAL.DO;
using System;
namespace IBL
{
    namespace BO
    {
       
        [Serializable]
        public class IBException : Exception
        {
            public IBException(IdExaption x) : base(x) => Mes = x;

            public string Mes;
            public IBException(string message) : base(message)
            {
                Mes = message;
            }
        }

      




    }
}