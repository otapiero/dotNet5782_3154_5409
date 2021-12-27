using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public static class DalFactory
    {
        static DalApi.IDal Instance =null;
       
        public static IDal GetDal()
        {
            if (Instance == null)
                Instance = new DalObject.DalObject.Instance;

            return Instance;
        }
    }
}
