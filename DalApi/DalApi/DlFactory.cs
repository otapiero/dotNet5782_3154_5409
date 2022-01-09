using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection;

namespace DalApi
{


    public static class DalFactory
    {
        public static IDal GetDal()
        {
            // get dal implementation name from config.xml according to <data> element
            string dalType = DalConfig.DalName;
            // bring package name (dll file name) for the dal name (above) from the list of packages in config.xml
            string dalPkg = DalConfig.DalPackages[dalType];
            try // get dal package info according to <dal> element value in config file
            {
                if (dalPkg == null) throw new DalConfigException("e");
            }
            catch (KeyNotFoundException ex)
            {
                // if package name is not found in the list - there is a problem in config.xml
                throw new DalConfigException($"Wrong Dal type: {dalType}", ex);
            }
            try // Load into CLR the dal implementation assembly according to dll file name (taken above)
            {
                Assembly.Load(dalPkg);
            }
            catch (Exception ex)
            {
                throw new DalConfigException($"Failed loading {dalPkg}.dll", ex);
            }
            // Get concrete Dal implementation's class metadata object
            // 1st element in the list inside the string is full class name:
            //    namespace = "Dal" or as specified in the "namespace" attribute in the config file,
            //    class name = package name or as specified in the "class" attribute in the config file
            //    the last requirement (class name = package name) is not mandatory in general - but this is the way it
            //    is configured per the implementation here, otherwise we'd need to add class name in addition to package
            //    name in the config.xml file - which is clearly a good option.
            //    NB: the class may not be public - it will still be found... Our approach that the implemntation class
            //        should hold "internal" access permission (which is actually the default access permission)
            // 2nd element is the package name = assembly name (as above)
            Type type;
            try
            {
                type = Type.GetType($"DAL.{dalPkg}, {dalPkg}");
            }
            catch (Exception ex)
            { // If the type is not found - the implementation is not correct - it looks like the class name is wrong...
                throw new DalConfigException($"Class not found due to a wrong namespace or/and class name: {dalPkg}, {dalPkg}", ex);
            }
            // *** Get concrete Dal implementation's Instance
            // Get property info for public static property named "Instance" (in the dal implementation class- taken above)
            // If the property is not found or it's not public or not static then it is not properly implemented
            // as a Singleton...
            // Get the value of the property Instance (get function is automatically called by the system)
            // Since the property is static - the object parameter is irrelevant for the GetValue() function and we can use null
            try
            {
                IDal dal = type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static).GetValue(null) as IDal;
                // If the instance property is not initialized (i.e. it does not hold a real instance reference)...
                if (dal == null)
                    throw new DalConfigException($"Class {dalPkg} instance is not initialized");
                // now it looks like we have appropriate dal implementation instance :-)
                return dal;
            }
            catch (NullReferenceException ex)
            {
                throw new DalConfigException($"Class {dalPkg} is not a singleton", ex);
            }

        }
    }
}

