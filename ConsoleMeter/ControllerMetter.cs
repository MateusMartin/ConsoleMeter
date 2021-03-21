using ConsoleMeter.Clas;
using ConsoleMeter.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMeter
{
   public class ControllerMeter : IControllerMeter
    {
        
        private List<Meter> meters;


        //iniciate switch States and fill with default values
        private List<SwitchState> switchStates = new List<SwitchState>()
        {
            new SwitchState
            {
                State = 0,
                Description = "Disconnected"
            },
            new SwitchState
            {
                State = 1,
                Description = "Connected"
            },
            new SwitchState
            {
                State = 2,
                Description = "Armed"
            }
        };
        //iniciate meter models and fill with default values
        private List<MeterModel> meterModels = new List<MeterModel>() 
        {
            new MeterModel
            {
                IdModel = 16,
                Model = "NSX1P2W"
            },
            new MeterModel
            {
                IdModel = 17,
                Model = "NSX1P3W"
            },
            new MeterModel
            {
                IdModel = 18,
                Model = "NSX2P3W"
            },
            new MeterModel
            {
                IdModel = 19,
                Model = "NSX3P4W"
            }
        };  

        public ControllerMeter() 
        {
            //When construct is called iniciates a new list meter;
            meters = new List<Meter>();        
        }

        //DeleteMeter function create by mateus castanho
        public bool DeleteMeter(string serialNumber)
        {
            //iniciates try delete meter
            try
            {
                //trys remove from list a meter with serial informed in function paramenter
                this.meters.RemoveAll((x) => x.SerialNumber == serialNumber);
                //return true
                return true;
            }
            catch (Exception ex) 
            {
                //if fail in delete meter inform user the erro
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: " + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                //return false
                return false;
            }
        }

        //EditMeter function create by mateus castanho
        public bool EditMeter(string serialNumber, int switchState)
        {
            try 
            {        
                //find a mater with serial number informed in parameter and replace switch state with the switch state informed in parameter
                this.meters.Single(c => c.SerialNumber == serialNumber).SwitchState = switchState;
                //return true
                return true;
            } catch(Exception ex) 
            {
                //if fail in edit meter inform user the erro
                Console.ForegroundColor = ConsoleColor.Red;            
                Console.WriteLine("ERROR: " + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                //return false
                return false;
            }
        }

        //FindBySerialNumber function create by mateus castanho
        public void FindBySerialNumber(string serialNumber)
        {
            //stars try
            try
            {

               //try found in list metter the meter with sertial number informed into parameter
                var meters =
                from Meter in this.meters
                //join with meter model where mettermodel.id equals meter.modelid
                join MeterModel in this.meterModels on Meter.ModelId equals MeterModel.IdModel
                //join with switch state where Meter.SwitchState equals SwitchState.State
                join SwitchState in this.switchStates on Meter.SwitchState equals SwitchState.State
                where Meter.SerialNumber == serialNumber
                //create new varibles in result and fill with returned query info
                select new
                {                    
                    SerialNumber = Meter.SerialNumber,
                    Model = MeterModel.Model,
                    Number = Meter.Number,
                    FirmwareVersion = Meter.FirmwareVersion,
                    State = SwitchState.Description
                };

                //verify if return a meter
                if (meters.Count() > 0)
                {
                    //inform user that found a meter
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("------- Found Meter With Serial: " + serialNumber + " -------");
                    Console.ForegroundColor = ConsoleColor.White;
                    foreach (var meter in meters)
                    {
                        //infor user meter info
                        Console.WriteLine("Serial Number: {0} , Model: {1}, Number: {2}, Firmware Version: {3}, State: {4}"
                            , meter.SerialNumber, meter.Model, meter.Number, meter.FirmwareVersion, meter.State);
                    }
                }
                else
                {
                    //infor user that dont have any meter with serial number informed
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("------- Error Not Found Meter With Serial: " + serialNumber + " -------");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            catch (Exception ex)
            {
                //if fail in execute function inform user the error
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: " + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        //InsertMeter function create by mateus castanho
        public bool InsertMeter(Meter meter)
        {
            //inciate try
            try
            {         
               //add to meter list meter object inform into fuction parameters
               meters.Add(meter);
                //return true
               return true;       
            }
            catch (Exception ex) 
            {
                //if fail in execute function inform user the error
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: "+ex.Message);
                Console.ForegroundColor = ConsoleColor.White;           
                return false;
            }
        }

        //ListAllMeter metter function create by mateus castanho
        public bool ListAllMeter()
        {
            //iniciates try
            try
            {

                //query all meter in meter list 
                var meters =
                    from Meter in this.meters
                    //join with meter model where mettermodel.id equals meter.modelid
                    join MeterModel in this.meterModels on Meter.ModelId equals MeterModel.IdModel
                    //join with switch state where Meter.SwitchState equals SwitchState.State
                    join SwitchState in this.switchStates on Meter.SwitchState equals SwitchState.State
                    //create new varibles in result and fill with returned query info
                    select new
                    {
                        SerialNumber = Meter.SerialNumber,
                        Model = MeterModel.Model,
                        Number = Meter.Number,
                        FirmwareVersion = Meter.FirmwareVersion,
                        State = SwitchState.Description
                    };

                //inciates a loop for each meter found in metter list
                foreach (var meter in meters)
                {
                    //inform user meter info
                    Console.WriteLine("Serial Number: {0} , Model: {1}, Number: {2}, Firmware Version: {3}, State: {4}"
                        , meter.SerialNumber, meter.Model, meter.Number, meter.FirmwareVersion, meter.State);
                }
                //return true
                return true;

            }
            catch (Exception ex)
            {
                //if fail in execute function inform user the error
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: " + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                //return false
                return false;
            }
        }

        //VerifyIfExistBySerialNumber function create by mateus castanho
        public bool VerifyIfExistBySerialNumber(String serialNumber)
        {
            //iniciates try
            try
            {              
                //try find in meter list a meter with serial number informed into function paramater
                var meters = this.meters.Where(x => x.SerialNumber == serialNumber);
                //verify if found a meter with serial number informed
                if (meters.Count() > 0)
                {
                    //return true
                    return true;
                }                
                //return fals
                return false;
            }
            catch (Exception ex)
            {
                //if fail in execute function throw a erro to menu;
                throw new Exception(ex.Message);
            }
        }
    }
}
