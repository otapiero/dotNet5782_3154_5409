using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public class FactoeySingletonDl
    {
        static IDAL.IDal Instance =null;
        private FactoeySingletonDl()
        {

        }
        public static IDAL.IDal GetDal()
        {
            if (Instance == null)
                Instance= new DalObject.DalObject();

            return Instance;
        }
    }
}
