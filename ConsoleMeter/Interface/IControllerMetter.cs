using ConsoleMeter.Clas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMeter.Interface
{
    interface IControllerMetter
    {

        Boolean InsertMetter(Metter metter);
        Boolean EditMetter(String serialNumber, int switchState);
        Boolean DeleteMetter(String serialNumber);
        void FindBySerialNumber(String serialNumber);
        Boolean ListAllMetter();
        Boolean VerifyIfExistBySerialNumber(String serialNumber);


    }
}
