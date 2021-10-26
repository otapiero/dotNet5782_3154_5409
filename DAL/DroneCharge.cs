using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct DroneCharge
        {
            public int DroneId { get; set; }
            public int StationId { get; set; }
            public DroneCharge(int _droneId, int _stationId)
            {
                DroneId = _droneId;
                StationId = _stationId;
            }
            public override string ToString()
            {
                return "Drone id: " + DroneId + "\nStation id: " + StationId;
            }
        }
    }
}
