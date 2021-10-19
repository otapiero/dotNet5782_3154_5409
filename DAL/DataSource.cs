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
        internal void Initialize()
        {

            int numDrones = 5;
            int numCustomers = 10;
            int numParcels = 10;
            int numStations = 2;
            for (int i = 0; i < numDrones; i++)
            {
                drones[i] = new();
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
