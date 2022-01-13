using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [Serializable]
    public class IdDoseNotExist : Exception
    {
        int id;
        string objectType;

        public string ObjectType { get => objectType; }
        public int Id { get => id;  }

        public IdDoseNotExist(string message,string _objectType, int _id) : base(message)
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
        public IdAlredyExist() { }
    }
    [Serializable]
    public class XMLFileLoadCreateException : Exception
    {
        private string xmlFilePath;
        
        public XMLFileLoadCreateException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }

        public string XmlFilePath { get => xmlFilePath;  }

        public override string ToString() => base.ToString() + $", fail to load or create xml file: {XmlFilePath}";
    }
}

