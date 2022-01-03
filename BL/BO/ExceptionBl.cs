using DO;
using System;

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
        public override string ToString()
        {
            return Message;
        }
    }
    [Serializable]
    public class NumOfChargeSlots : Exception
    {
        int numOfCharge;
        public int NumOfCharge { get => numOfCharge;  }
        public NumOfChargeSlots(string message, int _numOfCharge) : base(message) => numOfCharge=_numOfCharge;
        public static explicit operator string(NumOfChargeSlots v)
        {
            return (string)v;
        }
        public override string ToString()
        {
            return Message;
        }
    }
    [Serializable]
    public class WrongInputPriorities : Exception
    {
        int num;
        public int Num { get => num; }
        public WrongInputPriorities( int _num) : base($"The input for Priorities must be beetween 0-2 the number {_num} is invalide") => num=_num;
        
        public override string ToString()
        {
            return Message;
        }
    }
    [Serializable]
    public class WrongInputWheigt : Exception
    {
        int num;
        public int Num { get => num; }
        public WrongInputWheigt(int _num) : base($"The input for wheigt must be beetween 0-2 the number {_num} is invalide") => num=_num;

        public override string ToString()
        {
            return Message;
        }
    }
    [Serializable]
    public class IdDoseNotExist : Exception
    {
        int id;
        string objectType;

        public string ObjectType { get => objectType; }
        public int Id { get => id; }

        public IdDoseNotExist(string message, string _objectType, int _id, Exception innerExeptiont) : base(message, innerExeptiont)
        { 
            id = _id;  
            objectType = _objectType;
        }
        public IdDoseNotExist(string message, string _objectType, int _id) : base(message)
        {
            id = _id;
            objectType = _objectType;
        }
        public IdDoseNotExist() { }
    }
    [Serializable]
    public class IdAlredyExist : Exception
    {
        int id;

        public int Id { get => id; }

        string objectType;

        public string ObjectType { get => objectType; }
        public IdAlredyExist(string message, string _objectType, int _id) : base(message)
        {
            id = _id;
            objectType = _objectType;
        }
        public IdAlredyExist(string message, string _objectType, int _id, Exception innerExeptiont) : base(message,innerExeptiont)
        {
            id = _id;
            objectType = _objectType;
        }
        public IdAlredyExist() { }
    }
}