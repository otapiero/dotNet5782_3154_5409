using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public partial class BL
    {
        public void UpdateDroneModel(int id ,string model)
        {
            
            try
            {

                idal.UpdateDroneModel(id, model);
            }
            catch
            {
                //dorone id not exist
            }
        }
        public void UpdateStation(int id,string name,int chargeSlots)
        {
            try
            {
                if (name && chargeSlots)
                {
                    int numOfDronesInCharge =idal.AllDronesIncharge().ToList().Count(w => w.StationId == id);


                    if (chargeSlots >= numOfDronesInCharge)
                    {
                        idal.AllStation().ToList()
                            .Where(w => w.id == id)
                            .select(s => { s.name = name; s.chargeSlots = chargeSlots; });
                    }
                    else else throw ... "chargeSlots lees than numOfDronesInCharge "
                }

                else throw "name or chargeslot is empty";
            }
            catch
            {
                //input chargeSlots or name empty
            }

        }
        public void UpdateCostumer(int id, string name,string phone)
        {
            try
            {
                if (name && phone)
                {

                    idal.AllCustomers().ToList()
                        .Where(w => w.Id == id)
                        .select(s => { s.name = name; s.phone = phone; });
                }
                else throw ...
            }
            catch
            {
                //input phone or name empty
            }
        }
        public void SendDroneToCharge(int id)
        {
            try
            {
                if (name && phone)
                {

                    idal.AllCustomers().ToList()
                        .Where(w => w.id === id)
                        .select(s => { s.name = name; s.phone = phone; });
                }
                else throw ...
            }
            catch
            {
                //input phone or name empty
            }
        }
        public void RelesaeDroneFromCharge(int id,double time)
        {
            DronesBl. 
        }
        public void AssignPackageToDrone(int id)
        {

        }
        public void CollectPackage(int id)
        {

        }
        public void DeliverPackage(int id)
        {

        }
    }
}
