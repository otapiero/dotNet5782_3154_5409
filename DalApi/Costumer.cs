using System;



    namespace DO
    {
        ///<summary>struct of customer</summary>
        public struct Costumer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set; }

            ///<summary>The function print detail of the object</summary>
            public override string ToString()
            {
                //The function print detail of the object
                return "Id: " + Id + "\nName: " + Name + "\nPhone: " + Phone +
                    "\nLongitude: " + DAL.Cordinates.LongitudeDoubleToString(Longitude) +
                    "\nLattitude: " + DAL.Cordinates.LatitudeDoubleToString(Lattitude);
            }



        }
    }

