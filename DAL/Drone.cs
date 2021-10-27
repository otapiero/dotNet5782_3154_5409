using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWheight{ get; set; }
            public DroneStatuses Status{ get; set; }
            public double Battery { get; set; }
            public Drone(int _id, string _model, WeightCategories _maxWheight, DroneStatuses _status, double _battery)
            {
                Id = _id;
                Model = _model;
                MaxWheight = _maxWheight;
                Status = _status;
                Battery = _battery;
            }
            public override string ToString()
            {

                return "Id: " + Id + "\nName: " + Model + "\nMaxWheight: " + MaxWheight + "\nStatus: " + Status +
                    "\nBattery: " + Battery + "\n";
            }
        }

    }
}
