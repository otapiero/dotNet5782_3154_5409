﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Station
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set; }
            public int ChargeSlots { get; set; }
           public Station(int _id, string _name, double _Longitude, double _Lattitude,int _chargeSlots)
            {
                Id = _id;
                Name = _name;
                Longitude = _Longitude;
                Lattitude = _Lattitude;
                ChargeSlots = _chargeSlots;
            }
          
            public override string ToString()
            {

                return "Id: " + Id + "\nName: " + Name + "\nLongitude: " +
                    DalObject.Cordinates.LongitudeDoubleToString(Longitude) +
                    "\nLattitude: " + DalObject.Cordinates.LatitudeDoubleToString(Lattitude) +
                    "\nChargeSlots: " + ChargeSlots + "\n";
            }
        }

    }
}
