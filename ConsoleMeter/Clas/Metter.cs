using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMeter.Clas
{
    public class Metter
    {
        public string SerialNumber { get; set; }
        public int ModelId { get; set; }
        public int Number { get; set; }
        public string FirmwareVersion { get; set; }
        public int SwitchState { get; set; }

        public Metter() { }

        public Metter(string SerialNumber,int ModelId, int Number, string FirmwareVersion, int SwitchState) 
        {
            this.SerialNumber = SerialNumber;
            this.ModelId = ModelId;
            this.Number = Number;
            this.FirmwareVersion = FirmwareVersion;
            this.SwitchState = SwitchState;
        }
   
    }
}
