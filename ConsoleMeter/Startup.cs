using ConsoleMeter.Clas;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ConsoleMeter
{
    class Startup
    {
        //Set varvariable Controller as global variable.
        private static ControllerMeter Controller;

        static void Main(string[] args)
        {     
            //Iniciates controller
            Controller = new ControllerMeter();
            
            bool showMenu = true;
            //loop main menu while var is true
            while (showMenu)
            {
                try
                { 
                    Console.Clear();          

                    //Calls function DrawnMainMenu and put return into var show menu.
                    //when function returns true clear console and write menu again.
                    showMenu = DrawnMainMenu();
                } 
                //If Apllication Trows a exeption print error in console
                catch(Exception ex) 
                {
                    Console.Write(ex.ToString());
                    Console.ReadKey();
                    //wait user input any kay to continue.
                }
            }

        }

        //DrawnMainMenu function created by mateus castanho
        private static bool DrawnMainMenu() 
        {
            //Drawn the menu
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("--------------Wellcome------------");
            Console.WriteLine(" 1) Insert a new Meter ");
            Console.WriteLine(" 2) Edit an existing Meter");
            Console.WriteLine(" 3) Delete an existing Meter");
            Console.WriteLine(" 4) List all Meter");
            Console.WriteLine(" 5) Find a Meter");
            Console.WriteLine(" 6) Exit");
            Console.WriteLine("----------------------------------");         
            Console.Write("\r\nSelect an option: ");
            //await user input a option
            switch (Console.ReadLine().Trim())
            {
                //if option is 1 calls insert function
                case "1":
                    InsertMeter();
                    return true;
                //if option is 2 calls edit function
                case "2":

                    EditMeter();

                    return true;
                //if option is 3 calls delete function
                case "3":
                    
                    DeleteMeter();

                    return true;
                //if option is 4 calls List All function
                case "4":

                    ListAllMeter();
                    return true;
                //if option is 5 calls function Find
                case "5":
                    FindMeter();
                    return true;
                //if option is 6 calls exit function
                case "6":
                    Exit();
                    return true;
                //if user input a non valid options print error in console
                default:

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n Invalid input Press any key to continue......");
                    Console.ReadKey();
                    //user press any key and re write menu;
                    return true;
            }

        }


        //Exit function created by mateus castanho
        private static void Exit() 
        {

            //ask if user is sure in exit aplication
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Are you sure in exit application (y/n) ? ");
            Console.ForegroundColor = ConsoleColor.White;
            //iniciate a boolean variable  
            bool loopConfirm = true;
            //iniciates new char variable
            char ansawer;
            //iniciates a loop when user puts a valid input the loop will stop
            while (loopConfirm)
            {
                //put the user input into ansawer var
                ansawer = Console.ReadKey().KeyChar;
                //verify if its a valid input "y" or "n"               
                if (ansawer.ToString().ToLower() == "y")
                {
                    //if input was "y" close application
                    Environment.Exit(-1);
                } 
                else if (ansawer.ToString().ToLower() == "n") 
                {

                    //if input was "n" exites loop and return to menu
                    loopConfirm = false;
                }  
                else
                {
                    //inform user that his input is invalid
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input, please write y or n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                //back to start of the loop
            }
        }


        //ListAllMeter function created by mateus castanho

        private static void ListAllMeter() 
        {
            
            Console.Clear();
            //print listing all in console.
            Console.WriteLine("-------- Listing all Meters -------- \n");
            //Calls function ListAllMeter in controller class.
            Controller.ListAllMeter();
            //print in console and wait the user press a key to return to menu
            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
        }

        //Find meter function created by mateus castanho
        private static void FindMeter() 
        {
            //iniciates a boolean variable
            bool loop = true;
            //iniciates serial number var
            var serialNumber = "";
            //iniciates a loop when user puts a valid input the loop will stop       
            while (loop)
            {
                //ask user to infor the serial number
                Console.Write("Inform Serial Number: ");
                //put user input into serial number variable
                serialNumber = Console.ReadLine().Trim();
                //verify if user input is null or empty 
                if (string.IsNullOrEmpty(serialNumber))
                {
                    //if input is null or empty
                    //infor user that his input was invalid
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input, please write at least 1 character");
                    Console.ForegroundColor = ConsoleColor.White;
                    //continue the loop
                }
                else
                {
                    //calls controller function FindBySerialNumber and pass var serial number as parameter
                    Controller.FindBySerialNumber(serialNumber);
                    //stops the loop
                    loop = false;
                }
            }
                       
            Console.WriteLine("\n Press any key to continue...");
            //wait user input a key to return to menu
            Console.ReadKey();
        }


        //EditMeter function created by mateus castanho
        private static void EditMeter()
        {
            //iniciates boolean variable
            bool loop = true;
            //iniciates serial number var
            var serialNumber = "";
            //iniciates a loop when user puts a valid input the loop will stop
            while (loop)
            {
                //ask user to inform serial number
                Console.Write("Inform Serial Number: ");
                //puts user input into serial number variable
                serialNumber = Console.ReadLine().Trim();
                //verify if user input is null or empty
                if (string.IsNullOrEmpty(serialNumber))
                {
                    //if user input was invalid inform it on console and return to loop start
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input, please write at least 1 character");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    //calls controller function VerifyIfExistBySerialNumber pass serial number var as parameter
                    //puts return into boolean var
                    //verify is exist a meter registred with serial number
                    bool existSerial = Controller.VerifyIfExistBySerialNumber(serialNumber);
                    // verify the return
                    if (existSerial == true) 
                    {

                        //iniciates new loop boolean var
                        bool loopSwitch = true;
                        //iniciates boolean var
                        var input = "";
                        //iniciates switch state var
                        int switchState = 0;
                        //iniciates new loop when user puts a valid input loop stop
                        while (loopSwitch)
                        {
                            //inform user that serial input was found
                            Console.ForegroundColor = ConsoleColor.Green;                           
                            Console.WriteLine("Found Meter {0} \n ", serialNumber);
                            Console.ForegroundColor = ConsoleColor.White;
                            //ask user input a new switch state
                            Console.Write("Input new Switch State: ");
                            //put user input into input variable
                            input = Console.ReadLine();
                            //verify if user input was a integer
                            //if is interger put input value into switchstate var
                            if (int.TryParse(input, out switchState))
                            {
                                //verify if user input is between 0 and 2
                                if (switchState < 0 || switchState > 2)
                                {
                                    //if not between ask to user put a input a value between 0 and 2
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Invalid input, please write an interger between 0 and 2");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    //return to loop start
                                }
                                else
                                {
                                    //if is between 0 and 2 calls function EditMeter and pass serial number and switch states as variables
                                    bool edited = Controller.EditMeter(serialNumber,switchState);
                                    // verify if function return true of false
                                    if (edited == true)
                                    {
                                        //if return was true inform user that mater was edited
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("Succeed in Edit Meter ");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    else
                                    {
                                        //if return was false informe the user that fail in edit the meter
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Failed in Edit Meter ");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    //end loop
                                    loopSwitch = false;
                                }
                            }
                            else
                            {
                                //inform user that his input is invalid
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid input, please write an interger between 0 and 2");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }

                    }
                    else 
                    {
                        //inform user that not exist any meter registered with the serial that was informed
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error Not Found Any Meter With Serial: " + serialNumber);
                        Console.ForegroundColor = ConsoleColor.White;                        
                    }           
                    //end the loop
                    loop = false;
                }
            }

            //wait user input any key to return to the menu 
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
        }

        //DeleteMeter function created by mateus castanho
        private static void DeleteMeter()
        {    
            
            //iniciates boolean variable
            bool loop = true;
            //iniciates serial number var
            var serialNumber = "";
            //iniciates new loop when user puts a valid serial number loop stop
            while (loop)
            {
                //ask user to input a serial number
                Console.Write("Inform Serial Number: ");
                //put user input into serial number var
                serialNumber = Console.ReadLine().Trim();
                //verify is user input is null or empty
                if (string.IsNullOrEmpty(serialNumber))
                {
                    //inform user that his input was invalid
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input, please write at least 1 character");
                    Console.ForegroundColor = ConsoleColor.White;
                    //back to loop start
                }
                else
                {
                    //calls controller function VerifyIfExistBySerialNumber pass serial number var as parameter
                    //puts return into boolean var
                    //verify is exist a meter registred with serial number
                    bool existSerial = Controller.VerifyIfExistBySerialNumber(serialNumber);
                    //verfify return
                    if (existSerial == true) 
                    {
                        //if meter exist ask if user are sure in delete meter
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Are you sure in delete meter {0} (y/n) ? ", serialNumber);
                        Console.ForegroundColor = ConsoleColor.White;
                        //iniciates new loop variable
                        bool loopConfirm = true;
                        //creates a ansawer char variable
                        char ansawer;
                        //iniciates a new loop
                        while (loopConfirm)
                        {
                            //await user pres a key
                            ansawer = Console.ReadKey().KeyChar;
                            //verify if user press key "y"
                            if (ansawer.ToString().ToLower() == "y")
                            {
                                //calls function DeleteMeter and pass serial number var as parameter
                                bool deleted = Controller.DeleteMeter(serialNumber);
                                //verify return of delete meter class
                                if (deleted == true)
                                {
                                    //if retourn was true inform user that delete successfuly the meter
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("\nSucceed in Delete Meter ");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                else 
                                {
                                    //if retourn was false inform user that fail in delete meter
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Fail in Delete Meter");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                //stops the loop
                                loopConfirm = false;
                            }
                            //verify if user input was "n"
                            else if (ansawer.ToString().ToLower() == "n")
                            {
                                //inform user that operation was cancelled
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\n Operation Cancelled ");
                                Console.ForegroundColor = ConsoleColor.White;
                                //stops the loop
                                loopConfirm = false;
                            }
                            else
                            {
                                //inform user his input was invalid
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid input, please write y or n");
                                Console.ForegroundColor = ConsoleColor.White;
                                // back to start of loop
                            }
                        }

                    }
                    else 
                    {
                        //inform user that not exist any meter registered with the serial that was informed
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error Not Found any Meter With Serial: " + serialNumber);
                        Console.ForegroundColor = ConsoleColor.White;
                    }                    
                
                    loop = false;
                }
            }

            //wait user input any key to return to the menu 
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
        }

        //Insert meter function created by mateus castanho
        private static void InsertMeter() 
        {

            //iniciates boolen var
            bool insertLoop = true; 
            //iniciates new loop
            while (insertLoop)
            {
                //inform user that his is insert new meter
                Console.Clear();
                Console.WriteLine("--------------Inserting New Meter------------\n");
                //iniciantes var serial number
                var serialNumber = "";
                //iniciates integer variable model id
                int modelId = 0;
                //iniciates integer variable number
                int number = 0;
                //iniciates string variable firmware version
                string firmwareVersion = "";
                //iniciates integer variable switch State
                int switchState = 0;

                //iniciates new loop boolean variable
                bool loop = true;
                //iniciates input variable
                var input = "";                
                
                //iniciates new loop when user input is valid loop stop
                while (loop) 
                {
                    //ask user to inform the serial number
                    Console.Write("Inform Serial Number: ");
                    //put user input into serial number variable 
                    serialNumber = Console.ReadLine().Trim();
                    //verify if user input was null or empty
                    if (string.IsNullOrEmpty(serialNumber))
                    {                        
                        //inform user that his input was invalid
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input, please write at least 1 character");
                        Console.ForegroundColor = ConsoleColor.White;                
                        //return to loop star
                    }
                    else
                    {
                        //calls controller function VerifyIfExistBySerialNumber pass serial number var as parameter
                        //puts return into boolean var
                        bool serialExist = Controller.VerifyIfExistBySerialNumber(serialNumber);
                        //verify is exist a meter registred with serial number
                        if (serialExist == true) 
                        {
                            //iform user that serial number is already registered
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Fail this serial number already registered");
                            Console.ForegroundColor = ConsoleColor.White;     
                            //return to loop start
                        }
                        else 
                        {
                            //stop the loop
                            loop = false;
                        }                     
                    } 
                }
                //reset loop var
                loop = true;

                //iniciates new loop when user input is valid loop stop
                while (loop) 
                {
                    //ask user to input model id
                    Console.Write("Model Id: ");
                    //put user input into input varible
                    input = Console.ReadLine();
                    //verify if user input was a integer
                    //if is interger put input value into model id var
                    if (int.TryParse(input,out modelId))
                    {                    
                        //verify is model id var is between 16 and 19
                        if(modelId < 16 || modelId > 19)
                        {
                            //infor user that his input was invalid
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid input, please write an interger between 16 and 19");
                            Console.ForegroundColor = ConsoleColor.White;
                            //return to loop start
                        }
                        else 
                        {
                            //ends the loop
                            loop = false;
                        }
                    }                   
                    else
                    {
                        //infor user that his input was invalid
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input, please write an interger between 16 and 19");
                        Console.ForegroundColor = ConsoleColor.White;                        
                    } 
                }
                //reset loop variable
                loop = true;
                //iniciates new loop when user input is valid loop stop
                while (loop)
                {  
                    //ask user to input number
                    Console.Write("Number: ");
                    input = Console.ReadLine();
                    //verify if user input was a integer
                    //if is interger put input value into number var
                    if (!int.TryParse(input, out number))
                    {       
                        //inform user that his input was invalid
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input, please write an interger");
                        Console.ForegroundColor = ConsoleColor.White;
                        //return to loop start
                    }
                    else
                    {
                        //ends loop
                        loop = false;
                    }
                }
                //reset loop var
                loop = true;
                //iniciates new loop when user input is valid loop stop
                while (loop)
                {
                    //ask user to input firmware vertion
                    Console.Write("Firmware Version: ");
                    //put user input into firmware version variable
                    firmwareVersion = Console.ReadLine().Trim();
                    //verify if user input is null or invalid
                    if (string.IsNullOrEmpty(firmwareVersion))
                    {                        
                        //inform user that his input was invalid
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input, please write at least 1 character");
                        Console.ForegroundColor = ConsoleColor.White;
                        //return to loop start
                    }
                    else
                    {
                        //ends loop
                        loop = false;
                    }
                }
                //reset loop variable
                loop = true;
                //iniciates new loop when user input is valid loop stop
                while (loop)
                {
                    //ask user switch state
                    Console.Write("Switch State: ");
                    //verify if user input was a integer
                    //if is interger put input value into switch state var
                    input = Console.ReadLine();
                    if (int.TryParse(input, out switchState))
                    {
                        //verify is user input is between 0 and 2
                        if(switchState < 0 || switchState > 2)
                        {

                            //inform user that his input was invalid
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid input, please write an interger between 0 and 2");
                            Console.ForegroundColor = ConsoleColor.White;
                            //return to loop start
                        }
                        else
                        {
                            //ends loop
                            loop = false;
                        }                        
                    }
                    else
                    {
                        //inform user that his input was invalid
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input, please write an interger between 0 and 2");
                        Console.ForegroundColor = ConsoleColor.White;
                        //return to loop start
                    }
                }

                //creates new object meter and pass to controler the input variable as parameter
                Meter Novo = new Meter(serialNumber, modelId, number, firmwareVersion, switchState);
                //calls controller InsertMeter function and pass object novo as parameter
                //functions trys insert new meter
                bool inserido =  Controller.InsertMeter(Novo);
                //verify is return was true
                if(inserido == true)
                {
                    //inform user succed in insert new meter
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Succeed in insert a new Meter ");
                    Console.ForegroundColor = ConsoleColor.White;         
                }
                else 
                {
                    //inform user succed in insert new meter
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed in insert a new Meter ");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                //ends loop
                insertLoop = false;
            }
            //wait user press any key and return to menu;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
          
        }

    }
}
