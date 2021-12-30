using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace DO
    {
        ///<summary>struct of Drone</summary>
        public struct Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }



            ///<summary>The function print detail of the object</summary>
            public override string ToString()
            {
                //The function print detail of the object
                return "Id: " + Id + "\nName: " + Model +
                     "\n";
            }
        }

    }

