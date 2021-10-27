using System;

namespace IDAL
{
    namespace DO
    {
        ///<summary>struct of customer</summary>
        public struct Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set;}
            //constructor
            public Customer(int _id, string _Name, string _Phone, double _Longitude, double _Lattitude)
            {
                Id = _id;
                Name = _Name;
                Phone = _Phone;
                Longitude = _Longitude;
                Lattitude = _Lattitude;
            }
           
            ///<summary>The function print detail of the object</summary>
            public override string ToString()
            {
                //The function print detail of the object
                return "Id: " + Id + "\nName: " + Name + "\nPhone: " + Phone +
                    "\nLongitude: " + DalObject.DalObject.LongitudeDoubleToString( Longitude) +
                    "\nLattitude: " + DalObject.DalObject.LatitudeDoubleToString( Lattitude);
            }



        }
    }
}
