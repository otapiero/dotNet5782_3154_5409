using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{

    public partial class BL
    {
        public BO.BaseStation SearchStation(int id)
        {
            BO.BaseStation temp = new();
            IDAL.DO.Station y = idal.SearchStation(id);
            if (y.Equals(new IDAL.DO.Station()))
            {
                //  excaption station dose not exsit
            }
            temp.Location = new(y.Longitude, y.Lattitude);
            temp.Name = y.Name;
            temp.Id = y.Id;
            temp.NumAvilableChargeStation = y.ChargeSlots;//
            

            return temp;
        }
        public BO.CustomerBl SearchCostumer(int id)
        {
            BO.CustomerBl temp = new();
            return temp;
        }
        public BO.DroneBL SearchDrone(int id)
        {
            BO.DroneBL temp = new();
            return temp;
        }
        public BO.ParcelBl SearchParcel(int id)
        {
            BO.ParcelBl temp = new();
            return temp;
        }

    }
}
