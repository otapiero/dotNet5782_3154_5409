using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
   internal class DataSource
    {
        IDAL.DO.Drone[] drones = new IDAL.DO.Drone[10];
        IDAL.DO.Station[] stations = new IDAL.DO.Station[5];
        IDAL.DO.Customer[] customers = new IDAL.DO.Customer[100];
        IDAL.DO.Parcel[] parcels = new IDAL.DO.Parcel[1000];
        static Random r = new();
        internal void Initialize()
        {
           
            int numDrones = r.Next(5,10) ;
            int numCustomers = r.Next(10, 100);
            int numParcels = r.Next(10, 1000);
            int numStations = r.Next(2, 5);
            for (int i = 0; i < numDrones; i++)
            {
                drones[i] = new(
                    r.Next(0, 1000),
                  ( r.Next(0, 25))+'A',
                   ,
                    r.NextDouble( ),
                     r.Next(0, 100));
                Config.dronesIndex++;
            }
           
            for (int i = 0; i < numCustomers; i++)
            {
                customers[i] = new();
            }
            for (int i = 0; i < numParcels; i++)
            {
                parcels[i] = new();
            }
            for (int i = 0; i <  numStations; i++)
            {
                stations[i] = new();
            }
           
            static T RandomEnumValue<T>()
            {
                var v = Enum.GetValues(typeof(T));
                return (T)v.GetValue(r.Next(v.Length));
            }
        }

        internal class Config
        {
            internal static int dronesIndex = 0;
            internal static int stationsIndex = 0;
            internal static int customersIndex = 0;
            internal static int parcelsIndex = 0;
            internal static int IdDefault = 0;
        }
    }
}
