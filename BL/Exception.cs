using IDAL.DO;
using System;
namespace IBL
{
   /* namespace BO
    {
        [Serializable]
        public class ExistIdException : Exception
        {
            private int iD;
            public ExistIdException(string message, Exception innerException) :
                base(message, innerException) => ID = ((BadIdException)innerException).ID;

            public int ID { get => iD; set => iD = value; }

            public override string ToString() => base.ToString() + $",bad id:{ID}";
        }

        [Serializable]
        public class DoubleIdException : Exception
        {

            private int iD;
            public DoubleIdException(string message, Exception innerException) :
            base(message, innerException)
            { ID = ((BadIdException)innerException).ID; }

            public int ID { get => iD; set => iD = value; }

            public override string ToString() => base.ToString() + $"Thre is a double id:{ID}";
        }

        [Serializable]
        public class NagatineIDException : Exception
        {
            private int iD;
            public NagatineIDException(int id, string message) :
                base(message) => ID = ID=id;
            public int ID { get => iD; set => iD = value; }
        }

        [Serializable]
        public class LongLattException : Exception
        {
            public double LongLatt;
            public LongLattException(double longLatt) : base() => LongLatt = longLatt;

            public LongLattException(double longLatt, string message) : base(message) => LongLatt = longLatt;

        }
        [Serializable]
        public class MinosOneException : Exception
        {
            public int ID;
            public MinosOneException(int id) : base() => ID = id;

            public MinosOneException(int id, string message) : base(message) => ID = id;

        }

        [Serializable]
        public class CollectedException : Exception
        {

            public string Mes;
            public CollectedException(string message) : base(message) => Mes = message;
        }

        [Serializable]
        public class ProvidedException : Exception
        {

            public string Mes;
            public ProvidedException(string message) : base(message) => Mes = message;
        }


        [Serializable]
        public class BusyException : Exception
        {

            public string Mes;
            public BusyException(string message) : base(message) => Mes = message;
        }

        [Serializable]
        public class ChargeException : Exception
        {

            public string Mes;
            public ChargeException(string message) : base(message) => Mes = message;
        }


        [Serializable]
        public class AvailableException : Exception
        {
            public string Mes;
            public AvailableException(string message) : base(message) => Mes = message;


        }







    }*/
}