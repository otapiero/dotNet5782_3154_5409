﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        ///<summary>struct of Parcel</summary>
        public struct Parcel
        {

            public int Id { get; set; }
            public int Sender { get; set; }
            public int TargetId { get; set; }
            public WeightCategories Wheight { get; set; }
            public Priorities Priority { get; set; }
            public DateTime Requsted { get; set; }
            public int DroneId { get; set ; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }

            //constructor
            public Parcel(int _Id, int _Sender, int _TargetId, WeightCategories _Wheight, Priorities _Priority, DateTime _Requsted, int _DroneId, DateTime _Scheduled, DateTime _PickedUp, DateTime _Delivered)
            {
                Id = _Id;
                Sender = _Sender;
                TargetId = _TargetId;
                Wheight = _Wheight;
                Priority = _Priority;
                Requsted = _Requsted;
                DroneId = _DroneId;
                Scheduled = _Scheduled;
                PickedUp = _PickedUp;
                Delivered = _Delivered;
            }
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
}
