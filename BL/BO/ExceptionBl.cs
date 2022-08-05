using DO;
using System;

namespace BO
{

    /// <summary>
    /// Exception of Bl 
    /// </summary>
    [Serializable]
    public class IBException : Exception
    {

        public string Mes;
        /// <summary>
        /// Initializes a new instance of the <see cref="IBException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
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
    /// <summary>
    /// The battery exaption.
    /// </summary>
    [Serializable]
    public class BatteryExaption : Exception
    {

        double minumumBattery=0;
        double timeReqested=0;

        public double MinumumBattery { get => MinumumBattery; }
        public double TimeReqested { get => timeReqested; }

        public BatteryExaption(string message, double battery) : base(message+$"need at least {battery} for this") => minumumBattery=battery;
        public BatteryExaption(string message, double battery, double time) : base(message+$"need at least {battery} for this.\n" +
            $"charge the drone {time} time for it.")
        {
            minumumBattery=battery;
            timeReqested=time;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BatteryExaption"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerexaption">The innerexaption.</param>
        public BatteryExaption(string message, BatteryExaption innerexaption) : base(message,innerexaption)
        {
            minumumBattery=innerexaption.MinumumBattery;
            timeReqested=innerexaption.timeReqested;
        }
        public static explicit operator string(BatteryExaption v)
        {
            return (string)v;

        }
        public override string ToString()
        {
            return Message;
        }
    }
    /// <summary>
    /// The num of charge slots.
    /// </summary>
    [Serializable]
    public class NumOfChargeSlots : Exception
    {
        int numOfCharge;
        public int NumOfCharge { get => numOfCharge; }
        public NumOfChargeSlots(int _numOfCharge) : base("not enough charge slots for a station") => numOfCharge=_numOfCharge;
        public NumOfChargeSlots(string message, int _numOfCharge) : base(message+$"need at least {_numOfCharge} charge slots") => numOfCharge=_numOfCharge;
        public static explicit operator string(NumOfChargeSlots v)
        {
            return (string)v;
        }
        public override string ToString()
        {
            return Message;
        }
    }
    /// <summary>
    /// Excaption for  no station available.
    /// </summary>
    [Serializable]
    public class NoStationAvailable : Exception
    {
        string objectType;
        public string ObjectType { get => objectType; }
        int id;
        public int Id { get => id; }
        /// <summary>
        /// Initializes a new instance of the <see cref="NoStationAvailable"/> class.
        /// </summary>
        /// <param name="_objectType">The _object type.</param>
        /// <param name="_id">The _id.</param>
        public NoStationAvailable(string _objectType, int _id) : base($"no station available! can not do this action")
        {
            objectType=_objectType;
            id=_id;
        }
        public static explicit operator string(NoStationAvailable v)
        {
            return (string)v;
        }
        public override string ToString()
        {
            return Message;
        }
    }
    /// <summary>
    /// Excaption for wrong status object.
    /// </summary>
    [Serializable]
    public class WrongStatusObject : Exception
    {
        string objectType;
        public string ObjectType { get => objectType; }
        int id;
        public int Id { get => id; }
        string error;
        public string Error { get => error; }
        /// <summary>
        /// Initializes a new instance of the <see cref="WrongStatusObject"/> class.
        /// </summary>
        /// <param name="_objectType">The _object type.</param>
        /// <param name="_id">The _id.</param>
        /// <param name="_error">The _error.</param>
        public WrongStatusObject(string _objectType, int _id, string _error) : base($"the {_objectType} with id: {_id} is "+_error)
        {
            objectType=_objectType;
            id=_id;
            error=_error;
        }
        public static explicit operator string(WrongStatusObject v)
        {
            return (string)v;
        }
        public override string ToString()
        {
            return Message;
        }
    }
    /// <summary>
    /// Excaption for the name model not valid.
    /// </summary>
    [Serializable]
    public class TheNameModelNotValid : Exception
    {

        public TheNameModelNotValid() : base("The name model are invalid, please enter a valid name") { }
        public static explicit operator string(TheNameModelNotValid v)
        {
            return (string)v;
        }
        public override string ToString()
        {
            return Message;
        }
    }
    /// <summary>
    /// Excaption for no parcel avilable.
    /// </summary>
    [Serializable]
    public class NoParcelAvilable : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="NoParcelAvilable"/> class.
        /// </summary>
        public NoParcelAvilable() : base("There is 0 parcel to deliver.\nPlease wait for new parcels") { }
        public static explicit operator string(NoParcelAvilable v)
        {
            return (string)v;
        }
        public override string ToString()
        {
            return Message;
        }
    }
    /// <summary>
    /// Excaption for the phone and name input are incorrect.
    /// </summary>
    public class thePhoneAndNameInputAreIncorrect : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="thePhoneAndNameInputAreIncorrect"/> class.
        /// </summary>
        public thePhoneAndNameInputAreIncorrect() : base("Invalid input ,need any input for atleastg one of name and phone.") { }
        public static explicit operator string(thePhoneAndNameInputAreIncorrect v)
        {
            return (string)v;
        }
        public override string ToString()
        {
            return Message;
        }
    }
    /// <summary>
    /// Excaption wrong input priorities.
    /// </summary>
    [Serializable]
    public class WrongInputPriorities : Exception
    {
        int num;
        public int Num { get => num; }
        /// <summary>
        /// Initializes a new instance of the <see cref="WrongInputPriorities"/> class.
        /// </summary>
        /// <param name="_num">The _num.</param>
        public WrongInputPriorities(int _num) : base($"The input for Priorities must be beetween 0-2 ,the number {_num} is invalide") => num=_num;

        public override string ToString()
        {
            return Message;
        }
    }
    /// <summary>
    /// Excaption for wrong input wheight.
    /// </summary>
    [Serializable]
    public class WrongInputWheight : Exception
    {
        int num;
        public int Num { get => num; }
        /// <summary>
        /// Initializes a new instance of the <see cref="WrongInputWheight"/> class.
        /// </summary>
        /// <param name="_num">The _num.</param>
        public WrongInputWheight(int _num) : base($"The input for wheigt must be beetween 0-2 ,the number {_num} is invalide") => num=_num;

        public override string ToString()
        {
            return Message;
        }
    }
    /// <summary>
    /// Excaption for wrong input battery.
    /// </summary>
    [Serializable]
    public class WrongInputBattery : Exception
    {
        double num;
        public double Num { get => num; }
        /// <summary>
        /// Initializes a new instance of the <see cref="WrongInputBattery"/> class.
        /// </summary>
        /// <param name="_num">The _num.</param>
        public WrongInputBattery(double _num) : base($"The input for battery must be beetween 0-100 ,the number {_num} is invalide") => num=_num;

        public override string ToString()
        {
            return Message;
        }
    }
    /// <summary>
    /// Excaption for id dose not exist.
    /// </summary>
    [Serializable]
    public class IdDoseNotExist : Exception
    {
        int id;
        string objectType;

        public string ObjectType { get => objectType; }
        public int Id { get => id; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdDoseNotExist"/> class.
        /// </summary>
        /// <param name="_objectType">The _object type.</param>
        /// <param name="_id">The _id.</param>
        /// <param name="innerExeptiont">The inner exeptiont.</param>
        public IdDoseNotExist(string _objectType, int _id, Exception innerExeptiont) : base(innerExeptiont.Message, innerExeptiont)
        {
            id = _id;
            objectType = _objectType;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="IdDoseNotExist"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="_objectType">The _object type.</param>
        /// <param name="_id">The _id.</param>
        public IdDoseNotExist(string message, string _objectType, int _id) : base(message)
        {
            id = _id;
            objectType = _objectType;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="IdDoseNotExist"/> class.
        /// </summary>
        public IdDoseNotExist() { }
    }
    /// <summary>
    /// Excaption for id alredy exist.
    /// </summary>
    [Serializable]
    public class IdAlredyExist : Exception
    {
        int id;

        public int Id { get => id; }

        string objectType;

        public string ObjectType { get => objectType; }
        /// <summary>
        /// Initializes a new instance of the <see cref="IdAlredyExist"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="_objectType">The _object type.</param>
        /// <param name="_id">The _id.</param>
        public IdAlredyExist(string message, string _objectType, int _id) : base(message)
        {
            id = _id;
            objectType = _objectType;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="IdAlredyExist"/> class.
        /// </summary>
        /// <param name="_objectType">The _object type.</param>
        /// <param name="_id">The _id.</param>
        /// <param name="innerExeptiont">The inner exeptiont.</param>
        public IdAlredyExist(string _objectType, int _id, Exception innerExeptiont) : base(innerExeptiont.Message, innerExeptiont)
        {
            id = _id;
            objectType = _objectType;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="IdAlredyExist"/> class.
        /// </summary>
        public IdAlredyExist() { }
    }
    /// <summary>
    /// Excaption for xml file load create exception.
    /// </summary>
    [Serializable]
    public class XMLFileLoadCreateException : Exception
    {
        private string xmlFilePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="XMLFileLoadCreateException"/> class.
        /// </summary>
        /// <param name="xmlPath">The xml path.</param>
        public XMLFileLoadCreateException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        /// <summary>
        /// Initializes a new instance of the <see cref="XMLFileLoadCreateException"/> class.
        /// </summary>
        /// <param name="xmlPath">The xml path.</param>
        /// <param name="message">The message.</param>
        public XMLFileLoadCreateException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        /// <summary>
        /// Initializes a new instance of the <see cref="XMLFileLoadCreateException"/> class.
        /// </summary>
        /// <param name="xmlPath">The xml path.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public XMLFileLoadCreateException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }

        /// <summary>
        /// Gets the xml file path.
        /// </summary>
        public string XmlFilePath { get => xmlFilePath; }

        public override string ToString() => base.ToString() + $", fail to load or create xml file: {XmlFilePath}";
    }
}