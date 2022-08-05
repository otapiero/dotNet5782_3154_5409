using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    /// <summary>
    /// The drone in charge.
    /// </summary>
    public class DroneInCharge
    {
        public int Id { get; set; }
        public double Battery { get; set; }

        public override string ToString()
        {
            var temp = Battery.ToString().Length < 5 ? Battery.ToString() : Battery.ToString().Substring(0, 5);
            return "\n  ID: "+Id+"\n  Battery: "+temp+"\n";
        }
    }
}