using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        ///<summary>struct of Station</summary>
        public struct Station
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set; }
            public int ChargeSlots { get; set; }
            //constructor
            public Station(int _id, string _name, double _Longitude, double _Lattitude,int _chargeSlots)
            {
                Id = _id;
                Name = _name;
                Longitude = _Longitude;
                Lattitude = _Lattitude;
                ChargeSlots = _chargeSlots;
            }

            ///<summary>The function print detail of the object</summary>
            public override string ToString()
            {
                //The function print detail of the object
                return "Id: " + Id + "\nName: " + Name + "\nLongitude: " +
                    DalObject.DalObject.LongitudeDoubleToString(Longitude) +
                    "\nLattitude: " + DalObject.DalObject.LatitudeDoubleToString(Lattitude) +
                    "\nChargeSlots: " + ChargeSlots + "\n";
            }
        }

    }
}
