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
            string dalType = DalConfig.DalName;
            string dalPkg = DalConfig.DalPackages[dalType];
            if (dalPkg == null) throw new DalConfigException("e");
            try { Assembly.Load(dalPkg); }
            catch (Exception h) { throw h; }
            Type type = Type.GetType($"DAL.{dalPkg}, {dalPkg}");
            if (type == null) throw new DalConfigException("g");
            IDal dal = (IDal)type.GetProperty("Instance",
            BindingFlags.Public | BindingFlags.Static).GetValue(null);
            if (dal == null) throw new DalConfigException("h");
            return dal;
        }
    }
}

