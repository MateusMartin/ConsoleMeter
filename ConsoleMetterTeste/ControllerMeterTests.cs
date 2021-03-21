using ConsoleMeter;
using ConsoleMeter.Clas;
using ConsoleMeter.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoqExpression;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleMetterTeste
{

    //unit tests class meter test create by mateus castanho
    [TestClass]
    public class ControllerMeterTests
    {

        
        [TestMethod]
        //test method ListAllMeterTest created bt mateus castanho
        public void ListAllMeterTest()
        {
            //put interface IControllerMeter in new mock var
            var mock = new Mock<IControllerMeter>();          


            //put return of getfakematers function into meter variable
            var meter = GetFakeMeters();
            //put return of GetFakeMeterModels into meter model variable
            var meterModels = GetFakeMeterModels();
            //put return of GetFakeSwitchState into meter model variable
            var switchStates = GetFakeSwitchState();

                var meters = from Meter in meter                           
                            //join with meter model where mettermodel.id equals meter.modelid
                            join MeterModel in meterModels on Meter.ModelId equals MeterModel.IdModel
                            //join with switch state where Meter.SwitchState equals SwitchState.State
                            join SwitchState in switchStates on Meter.SwitchState equals SwitchState.State
                            //create new varibles in result and fill with returned query info                         
                            select new
                            {
                                SerialNumber = Meter.SerialNumber,
                                Model = MeterModel.Model,
                                Number = Meter.Number,
                                FirmwareVersion = Meter.FirmwareVersion,
                                State = SwitchState.Description
                            };
        
                    //loop for each meter retourned
                    foreach (var met in meters)
                    {
                        //write in console meter info
                        Console.WriteLine("Serial Number: {0} , Model: {1}, Number: {2}, Firmware Version: {3}, State: {4}"
                        , met.SerialNumber, met.Model, met.Number, met.FirmwareVersion, met.State);
                    }
                    //mock exection of ListAllMeter function in controllermeter clas 
                    mock.Setup(x => x.ListAllMeter());
             
            
        }

        [TestMethod]
        //test method FindBySerialNumber created bt mateus castanho
        public void FindBySerialNumber()
        {
            //put interface IControllerMeter in new mock var
            var mock = new Mock<IControllerMeter>();
            //iniciates serial var and fill with default values
            string[] serials = { "123", "1234", "1235" };


            //put return of getfakematers function into meter variable
            var meter = GetFakeMeters();
            //put return of GetFakeMeterModels into meter model variable
            var meterModels = GetFakeMeterModels();
            //put return of GetFakeSwitchState into meter model variable
            var switchStates = GetFakeSwitchState();

            //execute a loop for each value in serials varible 
            foreach (string serial in serials)
            {
                //try query a meter with a serial informed in loop paramater
                var exist = from Meter in meter
                            //join with meter model where mettermodel.id equals meter.modelid
                            join MeterModel in meterModels on Meter.ModelId equals MeterModel.IdModel
                            //join with switch state where Meter.SwitchState equals SwitchState.State
                            join SwitchState in switchStates on Meter.SwitchState equals SwitchState.State
                            where Meter.SerialNumber == serial
                            //create new varibles in result and fill with returned query info   
                            select new
                            {
                                SerialNumber = Meter.SerialNumber,
                                Model = MeterModel.Model,
                                Number = Meter.Number,
                                FirmwareVersion = Meter.FirmwareVersion,
                                State = SwitchState.Description
                            };

                //verify if found a meter with serial informed
                if (exist.Count() > 0)
                {
                    //inciates a loop for each meter found in metter list
                    foreach (var met in exist) 
                    {
                        //print in console meter info
                        Console.WriteLine("Serial Number: {0} , Model: {1}, Number: {2}, Firmware Version: {3}, State: {4}"
                        , met.SerialNumber, met.Model, met.Number, met.FirmwareVersion, met.State);
                    }
                    //mock exection of FindBySerialNumber function in controllermeter class 
                    mock.Setup(x => x.FindBySerialNumber(serial));
                }
                else
                {
                    //print not found in console
                    Console.WriteLine("Not Found");
                    //mock exection of FindBySerialNumber function in controllermeter class
                    mock.Setup(x => x.FindBySerialNumber(serial));
                }
            }
        }

        [TestMethod]
        //test method DeleteMeterTest created bt mateus castanho
        public void DeleteMeterTest() 
        {
            //put interface IControllerMeter in new mock var
            var mock = new Mock<IControllerMeter>();
            //iniciates serial var and fill with default values
            string[] serials = { "123", "1234", "1235" };

            //put return of getfakematers function into meter variable
            var meter = GetFakeMeters();

            //execute a loop for each value in serials varible 
            foreach (string serial in serials)
            {

                //verify if exist any meter with serialnumber informed into parameter
                bool exist = meter.Any(x => x.SerialNumber == serial);
                //verify if retourn is true
                if (exist == true)
                {
                    
                    //remove from list meter with serial nubmer informed
                    meter.RemoveAll((x) => x.SerialNumber == serial);
                    //print in console deleted meter
                    Console.WriteLine("Deleted");

                    //mock exection of DeleteMeter function in controllermeter class and return true
                    mock.Setup(c => c.DeleteMeter(serial)).Returns(true);
                }
                else
                {
                    //print in console not found
                    Console.WriteLine("Not Found");
                    //mock exection of DeleteMeter function in controllermeter class and return false
                    mock.Setup(c => c.DeleteMeter(serial)).Returns(false);
                }
            }
        }

        [TestMethod]
        //test method insertMeterTest created bt mateus castanho
        public void insertMeterTest() 
        {
            //put interface IControllerMeter in new mock var
            var mock = new Mock<IControllerMeter>();
            //iniciates lis meter and fill with return of GetFakeMeters function     
            List<Meter> meters = GetFakeMeters();

            //create new object meter and fill with defoul values
            Meter meter = new Meter()
            {
                SerialNumber = "123",
                ModelId = 16,
                Number = 1,
                FirmwareVersion = "1.1.2",
                SwitchState = 0
            };

            //add to meter lis the meter object
            meters.Add(meter);
            //mock exection of InsertMeter function in controllermeter class returning true
            mock.Setup(x => x.InsertMeter(meter)).Returns(true);

        }

        [TestMethod]
        //test method insertMeterTest created bt mateus castanho
        public void EditMeterTest()
        {
            //put interface IControllerMeter in new mock var
            var mock = new Mock<IControllerMeter>();
            //iniciates serial var and fill with default values
            string[] serials = { "123", "1234", "1235" };
            //iniciates state var and fill with default value
            int State = 2;

            //iniciates variable meters and fill with return of GetFakeMeters function   
            var meter = GetFakeMeters();

            //execute a loop for each value in serials varible 
            foreach (string serial in serials) 
            {

                //verify if exist any meter with serialnumber informed into parameter
                bool exist = meter.Any(x => x.SerialNumber == serial);
                //verify if retourn is true
                if (exist == true)
                {
                    //print in console edited
                    Console.WriteLine("Edited");
                    //edit in list meter with serial number informed replacind switch state with state variable value
                    meter.Single(c => c.SerialNumber == serial).SwitchState = State;
                    //mock exection of EditMeter function in controllermeter class returning true
                    mock.Setup(c => c.EditMeter(serial, State)).Returns(true);
                }
                else
                {
                    //print not found in console
                    Console.WriteLine("Not Found");
                    //mock exection of EditMeter function in controllermeter class returning false
                    mock.Setup(c => c.EditMeter(serial, State)).Returns(false);
                }
            }
        }


        [TestMethod]
        public void VerifyIfExistBySerialNumber() 
        {
            //put interface IControllerMeter in new mock var
            var mock = new Mock<IControllerMeter>();
            //iniciates serial var and fill with default value
            var serial = "123";

            //iniciates variable meters and fill with return of GetFakeMeters function
            var meters = GetFakeMeters();
            //try find meter in lis with serialnumber informed in serial var
            //put return into ret var
            bool ret = meters.Any(x => x.SerialNumber == serial);
            //mock exection of VerifyIfExistBySerialNumber function in controllermeter class returning ret var result
            mock.Setup(x => x.VerifyIfExistBySerialNumber(serial)).Returns(ret);
               
        }


        //mock list of MeterModel
        private List<MeterModel> GetFakeMeterModels()
        {
            return new List<MeterModel>
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
        }
        //mock list of SwitchState
        private List<SwitchState> GetFakeSwitchState()
        {
            return new List<SwitchState>
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
        }
        //mock list of Meter
        private List<Meter> GetFakeMeters() 
        {
            return new List<Meter>
            {
                new Meter
                {
                    SerialNumber = "123",
                    ModelId = 16,
                    Number = 1,
                    FirmwareVersion = "1.1.2",
                    SwitchState = 0                    
                },
                new Meter
                {
                    SerialNumber = "124",
                    ModelId = 17,
                    Number = 1,
                    FirmwareVersion = "1.1.2",
                    SwitchState = 1
                },
                new Meter
                {
                    SerialNumber = "133",
                    ModelId = 17,
                    Number = 1,
                    FirmwareVersion = "1.1.2",
                    SwitchState = 2
                },
                new Meter
                {
                    SerialNumber = "112",
                    ModelId = 17,
                    Number = 1,
                    FirmwareVersion = "1.1.2",
                    SwitchState = 3
                },
            };
        }

    }
}
