using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DO
{
    ///<summary>struct of Parcel</summary>
    public struct Parcel
    {

       public bool Availble { get; set; }
        public int Id { get; set; }
        public int Sender { get; set; }
        public int TargetId { get; set; }
        public WeightCategories Wheight { get; set; }
        public Priorities Priority { get; set; }
        public DateTime? Requsted { get; set; }
        public int DroneId { get; set; }
        public DateTime? Scheduled { get; set; }
        public DateTime? PickedUp { get; set; }
        public DateTime? Delivered { get; set; }




        ///<summary>The function print detail of the object</summary>
        public override string ToString()
        {
            //The function print detail of the object
            return "Id: " + Id + "\nSender: " + Sender + "\nTargetId: " + TargetId + "\nWheight: " + Wheight +
                "\nPriority: " + Priority + "\nRequsted: " + Requsted + "\nDroneId: " + DroneId + "\nScheduled: " +
                Scheduled + "\nPickedUp: " + PickedUp + "\nDelivered: " + Delivered;
        }
    }

}

