using IDAL.DO;
using System;
namespace IBL
{
    namespace BO
    {

        [Serializable]
        public class IBException : Exception
        {

            public string Mes;
            public IBException(string message) : base(message)
            {
                Mes = message;
            }

            public static explicit operator string(IBException v)
            {
                return (string)v;
            }
        }






    }
}