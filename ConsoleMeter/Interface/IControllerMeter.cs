using ConsoleMeter.Clas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMeter.Interface
{
    //controller interface created by mateus castanho 03-19-2020
    public interface IControllerMeter
    {

        Boolean InsertMeter(Meter meter);
        Boolean EditMeter(String serialNumber, int switchState);
        Boolean DeleteMeter(String serialNumber);
        void FindBySerialNumber(String serialNumber);
        Boolean ListAllMeter();
        Boolean VerifyIfExistBySerialNumber(String serialNumber);


    }
}
