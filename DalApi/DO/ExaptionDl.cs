using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    ///Exaption for type error The id dose not exist.
    /// </summary>
    [Serializable]
    public class IdDoseNotExist : Exception
    {
        int id;
        string objectType;

        public string ObjectType { get => objectType; }
        public int Id { get => id;  }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdDoseNotExist"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="_objectType">The _object type.</param>
        /// <param name="_id">The _id.</param>
        public IdDoseNotExist(string message,string _objectType, int _id) : base(message)
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
    /// Exaption for type error The id alredy exist.
    /// </summary>
    [Serializable]
    public class IdAlredyExist : Exception
    {
        int id;

        public int Id { get => id; }

        string objectType;

        /// <summary>
        /// Gets the object type.
        /// </summary>
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
        public IdAlredyExist() { }
    }
    /// <summary>
    /// Exaption for type error xml file load create exception.
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

        /// <summary>
        /// convert to string string.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString() => base.ToString() + $", fail to load or create xml file: {XmlFilePath}";
    }
}

