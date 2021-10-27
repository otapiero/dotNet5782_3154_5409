using System;

namespace IDAL
{
    namespace DO
    {
        public struct Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set;}
            public Customer(int _id, string _Name, string _Phone, double _Longitude, double _Lattitude)
            {
                Id = _id;
                Name = _Name;
                Phone = _Phone;
                Longitude = _Longitude;
                Lattitude = _Lattitude;
            }
           
            public override string ToString()
            {
                return "Id: " + Id + "\nName: " + Name + "\nPhone: " + Phone +
                    "\nLongitude: " + DalObject.DalObject.LongitudeDoubleToString( Longitude) +
                    "\nLattitude: " + DalObject.DalObject.LatitudeDoubleToString( Lattitude);
            }



        }
    }
}
