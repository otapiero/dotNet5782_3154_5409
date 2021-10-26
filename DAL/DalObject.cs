using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public class DalObject
    {
       


        public DalObject()
        {
            DataSource.Initialize();
        }

        public IDAL.DO.Parcel Search(int _id)
        {
            IDAL.DO.Parcel finded = new();

            finded = DataSource.parcels.Find(x => x.Id.Equals(_id));

            return finded;
        }
    }
}
