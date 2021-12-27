using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
   public class FactoeySingletonBl
    {
        static BL Instance = null;
        private FactoeySingletonBl()
        {

        }
        public static BL GetBl()
        {
            if (Instance == null)
                Instance = new BL();

            return Instance;
        }
    }
}
