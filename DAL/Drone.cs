using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        ///<summary>struct of Drone</summary>
        public struct Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
          
            public double Battery { get; set;}
            //constructor
            public Drone(int _id, string _model, double _battery)
            {
                Id = _id;
                Model = _model;
              
                Battery = _battery;
            }
            ///<summary>The function print detail of the object</summary>
            public override string ToString()
            {
                //The function print detail of the object
                return "Id: " + Id + "\nName: " + Model + 
                    "\nBattery: " + Battery + "\n";
            }
        }

    }
}
