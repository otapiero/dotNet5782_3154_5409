using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DO
{
    ///<summary>struct of DroneCharge </summary>
    public struct DroneCharge
    {
        public int DroneId { get; set; }
        public int StationId { get; set; }
        //constructor
        public DroneCharge(int _droneId, int _stationId)
        {
            DroneId = _droneId;
            StationId = _stationId;
        }
        ///<summary>The function print detail of the object</summary>
        public override string ToString()
        {
            //The function print detail of the object
            return "Drone id: " + DroneId + "\nStation id: " + StationId;
        }
    }
}

