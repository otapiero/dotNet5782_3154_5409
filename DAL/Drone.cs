﻿using System;
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
          
            
            //constructor
            public Drone(int _id, string _model)
            {
                Id = _id;
                Model = _model;
            
                
            }
            ///<summary>The function print detail of the object</summary>
            public override string ToString()
            {
                //The function print detail of the object
                return "Id: " + Id + "\nName: " + Model + 
                     "\n";
            }
        }

    }
}
