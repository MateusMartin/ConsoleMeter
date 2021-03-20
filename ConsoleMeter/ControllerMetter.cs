using ConsoleMeter.Clas;
using ConsoleMeter.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMeter
{
    class ControllerMetter : IControllerMetter
    {
        protected List<Metter> metters;

        protected List<SwitchState> switchStates = new List<SwitchState>()
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

        protected List<MetterModel> metterModels = new List<MetterModel>() 
        {
            new MetterModel
            {
                IdModel = 16,
                Model = "NSX1P2W"
            },
            new MetterModel
            {
                IdModel = 17,
                Model = "NSX1P3W"
            },
            new MetterModel
            {
                IdModel = 18,
                Model = "NSX2P3W"
            },
            new MetterModel
            {
                IdModel = 19,
                Model = "NSX3P4W"
            }
        };

        public ControllerMetter() 
        {
            //When construct is called iniciates a new list metter;
            metters = new List<Metter>();        
        }


        public bool DeleteMetter(string serialNumber)
        {
            try
            {
                this.metters.RemoveAll((x) => x.SerialNumber == serialNumber);
                return true;
            }
            catch (Exception ex) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: " + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;

                return false;
            }
        }

        public bool EditMetter(string serialNumber, int switchState)
        {
            try 
            {        
                this.metters.Single(c => c.SerialNumber == serialNumber).SwitchState = switchState;                                
                return true;
            } catch(Exception ex) 
            {
                Console.ForegroundColor = ConsoleColor.Red;            
                Console.WriteLine("ERROR: " + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;

                return false;
            }
        }

        public void FindBySerialNumber(string serialNumber)
        {
            try
            {
                var metters =
                from Metter in this.metters
                join MetterModel in this.metterModels on Metter.ModelId equals MetterModel.IdModel
                join SwitchState in this.switchStates on Metter.SwitchState equals SwitchState.State
                where Metter.SerialNumber == serialNumber
                select new
                {
                    SerialNumber = Metter.SerialNumber,
                    Model = MetterModel.Model,
                    Number = Metter.Number,
                    FirmwareVersion = Metter.FirmwareVersion,
                    State = SwitchState.Description
                };

                if (metters.Count() > 0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("------- Found Metter With Serial: " + serialNumber + " -------");
                    Console.ForegroundColor = ConsoleColor.White;
                    foreach (var metter in metters)
                    {
                        Console.WriteLine("Serial Number: {0} , Model: {1}, Number: {2}, Firmware Version: {3}, State: {4}"
                            , metter.SerialNumber, metter.Model, metter.Number, metter.FirmwareVersion, metter.State);
                    }
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("------- Error Not Found Metter With Serial: " + serialNumber + " -------");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: " + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }


        public bool InsertMetter(Metter metter)
        {
            try
            {
                bool existSerial = VerifyIfExistBySerialNumber(metter.SerialNumber);
                if (existSerial == false)
                {
                    metters.Add(metter);
                    return true;
                }
                else 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Fail in insert serial number already registered");
                    Console.ForegroundColor = ConsoleColor.White;
                    return false;
                }

            }
            catch (Exception ex) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: "+ex.Message);
                Console.ForegroundColor = ConsoleColor.White;           
                return false;
            }
        }

        public bool ListAllMetter()
        {
            try
            {
                var metters =
                    from Metter in this.metters
                    join MetterModel in this.metterModels on Metter.ModelId equals MetterModel.IdModel
                    join SwitchState in this.switchStates on Metter.SwitchState equals SwitchState.State
                    select new
                    {
                        SerialNumber = Metter.SerialNumber,
                        Model = MetterModel.Model,
                        Number = Metter.Number,
                        FirmwareVersion = Metter.FirmwareVersion,
                        State = SwitchState.Description
                    };

                foreach (var metter in metters)
                {
                    Console.WriteLine("Serial Number: {0} , Model: {1}, Number: {2}, Firmware Version: {3}, State: {4}"
                        , metter.SerialNumber, metter.Model, metter.Number, metter.FirmwareVersion, metter.State);
                }

                return true;

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: " + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
        }

        public bool VerifyIfExistBySerialNumber(String serialNumber)
        {
            try
            {              
                var metters = this.metters.Where(x => x.SerialNumber == serialNumber);

                if (metters.Count() > 0)
                {
                    return true;
                }                
                return false;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }
    }
}
