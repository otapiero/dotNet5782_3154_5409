﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class BaseStation
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Location Location { get; set; }
            public int NumAvilableChargeStation { get; set; }
            public List<DroneInCharge> dronesInCharges { get; set; }
        }
    }
}