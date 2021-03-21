using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMeter.Clas
{
    //Meter class created by mateus castanho 03-19-2020
    public class Meter
    {
        public string SerialNumber { get; set; }
        public int ModelId { get; set; }
        public int Number { get; set; }
        public string FirmwareVersion { get; set; }
        public int SwitchState { get; set; }

        //empty constructer
        public Meter() { }

        //constructer with parameters fill all the variables with value
        public Meter(string SerialNumber,int ModelId, int Number, string FirmwareVersion, int SwitchState) 
        {
            this.SerialNumber = SerialNumber;
            this.ModelId = ModelId;
            this.Number = Number;
            this.FirmwareVersion = FirmwareVersion;
            this.SwitchState = SwitchState;
        }
   
    }
}
