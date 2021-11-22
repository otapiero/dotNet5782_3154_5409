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
            DronesBl
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
            {//להוסיף ערך ברירת מחדל במקרה של ערך אחד
                if (name.Length()>0 && chargeSlots>0)
                {
                    int numOfDronesInCharge = 0;
                    idal.AllDronesIncharge().ToList()
                        .where(w => w.StationId == id)
                        .ForEach(numOfDronesInCharge++);


                    if (chargeSlots >= numOfDronesInCharge)
                    {
                        idal.UpdateStation(id, name,chargeSlots);
                      
                    }
                    else throw ... "chargeSlots lees than numOfDronesInCharge "
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
                //מקרה של שדה אחד מעודכן
                if (name.Length()>0 && phone.Length()>0)
                {
                    idal.UpdateCostumer( id, name,phone);
                }
                else throw ..."name or phone not vaild"
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
                


                if (IBL.BL.DronesBl.Exists(x => x.Id == id && x.status == BO.DroneStatuses.Available))
                {

                    
                }
                else throw ..."the drone isnt available"
            }
            catch
            {
                //input phone or name empty
            }
        }
        public void RelesaeDroneFromCharge(int id,double time)
        {
            try
            {

                

                if (IBL.BL.DronesBl.Exists(x => x.Id == id && x.status == BO.DroneStatuses.Maintenace))
                {
                    idal.UpdateCostumer(id);

                }
                else throw ..."the drone isnt in Maintenace"
            }
            catch
            {
                //input phone or name empty
            }
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
