﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    /// <summary>
    /// The base station for list.
    /// </summary>
    public class BaseStationToList
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int NumAvilableChargeStation { get; set; }
        public int NumNotAvilableChargeStation { get; set; }
        public BaseStationToList(int id, string name, int numAvaiable, int numNotAvailable)
        {
            Id=id;
            Name=name;
            NumAvilableChargeStation=numAvaiable;
            NumNotAvilableChargeStation=numNotAvailable;
        }

        public override string ToString()
        {
            return "Id: " +Id+"\nName: "+Name+"\nnumber of avilabl charge spolts: "+NumAvilableChargeStation+
                "\nnumber of not avilable charge spolts: "+NumNotAvilableChargeStation+"\n";
        }
    }
}