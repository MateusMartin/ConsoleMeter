using ConsoleMeter.Clas;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ConsoleMeter
{
    class Startup
    {
       //Set Controller as global variable
       static ControllerMetter Controller;

        static void Main(string[] args)
        {
            Controller = new ControllerMetter();

            bool showMenu = true;
            while (showMenu)
            {
                try
                { 
                Console.Clear();            
                showMenu = DrawnMainMenu();
                } catch(Exception ex) 
                {
                    Console.Write(ex.ToString());
                    Console.ReadKey();
                }
            }

        }


        private static bool DrawnMainMenu() 
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("--------------Wellcome------------");
            Console.WriteLine(" 1) Insert a new Metter ");
            Console.WriteLine(" 2) Edit an existing Metter");
            Console.WriteLine(" 3) Delete an existing Metter");
            Console.WriteLine(" 4) List all Metter");
            Console.WriteLine(" 5) Find a Metter");
            Console.WriteLine(" 6) Exit");
            Console.WriteLine("----------------------------------");         
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine().Trim())
            {
                case "1":
                    InsertMetter();
                    return true;
                case "2":

                    EditMetter();

                    return true;
                case "3":
                    
                    DeleteMetter();

                    return true;
                case "4":
                    Console.Clear();
                    Console.WriteLine("-------- Listing all Metters -------- \n");
                    Controller.ListAllMetter();
                    Console.WriteLine("\n Press any key to continue...");
                    Console.ReadKey();
                    return true;
                case "5":
                    bool loop = true;
                    var serialNumber = "";
                    while (loop)
                    {
                        Console.Write("Inform Serial Number: ");
                        serialNumber = Console.ReadLine().Trim();
                        if (string.IsNullOrEmpty(serialNumber))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid input, please write at least 1 character");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Controller.FindBySerialNumber(serialNumber);                     
                            loop = false;
                        }
                    }

                    Console.WriteLine("\n Press any key to continue...");
                    Console.ReadKey();
                    return true;
                case "6":
                    return false;
                default:

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n Invalid input Press any key to continue......");
                    Console.ReadKey();
                    return true;
            }

        }

        private static void EditMetter()
        {

            bool loop = true;
            var serialNumber = "";
            while (loop)
            {
                Console.Write("Inform Serial Number: ");
                serialNumber = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(serialNumber))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input, please write at least 1 character");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {

                    bool existSerial = Controller.VerifyIfExistBySerialNumber(serialNumber);
                    if (existSerial == true) 
                    {


                        bool loopSwitch = true;
                        var input = "";
                        int switchState = 0;
                        while (loopSwitch)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Found Metter {0} \n ", serialNumber);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("Input new Switch State: ");
                            input = Console.ReadLine();
                            if (int.TryParse(input, out switchState))
                            {
                                if (switchState < 0 || switchState > 2)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Invalid input, please write an interger between 0 and 2");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    
                                }
                                else
                                {
                                    bool edited = Controller.EditMetter(serialNumber,switchState);

                                    if (edited == true)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("Succeed in Edit Metter ");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Failed in Edit Metter ");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }

                                    loopSwitch = false;
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid input, please write an interger between 0 and 2");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }

                    }
                    else 
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error Not Found Any Metter With Serial: " + serialNumber);
                        Console.ForegroundColor = ConsoleColor.White;                        
                    }           

                    loop = false;
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
        }


        private static void DeleteMetter() 
        {
            bool loop = true;
            var serialNumber = "";
            while (loop)
            {
                Console.Write("Inform Serial Number: ");
                serialNumber = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(serialNumber))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input, please write at least 1 character");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {

                    bool existSerial = Controller.VerifyIfExistBySerialNumber(serialNumber);
                    if (existSerial == true) 
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Are you sure in delete metter {0} (y/n) ? ", serialNumber);
                        Console.ForegroundColor = ConsoleColor.White;
                        bool loopConfirm = true;
                        char ansawer;
                        while (loopConfirm)
                        {
                            ansawer = Console.ReadKey().KeyChar;
                            if (ansawer.ToString().ToLower() == "y")
                            {
                                bool deleted = Controller.DeleteMetter(serialNumber);

                                if (deleted == true)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("\nSucceed in Delete Metter ");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                else 
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Fail in Delete Metter");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                loopConfirm = false;
                            }
                            else if (ansawer.ToString().ToLower() == "n")
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\n Operation Cancelled ");
                                Console.ForegroundColor = ConsoleColor.White;
                                loopConfirm = false;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid input, please write y or n");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }

                    }
                    else 
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error Not Found any Metter With Serial: " + serialNumber);
                        Console.ForegroundColor = ConsoleColor.White;
                    }                    
                
                    loop = false;
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
        }

        private static void InsertMetter() 
        {
            bool insertLoop = true; 

            while (insertLoop)
            {
                Console.Clear();
                Console.WriteLine("--------------Inserting New Metter------------\n");

                var serialNumber = "";
                int modelId = 0;
                int number = 0;
                string firmwareVersion = "";
                int switchState = 0;

                bool loop = true;
                var input = "";                
                

                while (loop) 
                {
                    Console.Write("Inform Serial Number: ");
                     serialNumber = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(serialNumber))
                    {                        
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input, please write at least 1 character");
                        Console.ForegroundColor = ConsoleColor.White;                
                    }
                    else
                    {
                        loop = false;
                    } 
                }

                loop = true;
                while (loop) 
                {
                    Console.Write("Model Id: ");
                    input = Console.ReadLine();
                   
                    if(int.TryParse(input,out modelId))
                    {                    
                        if(modelId < 16 || modelId > 19)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid input, please write an interger between 16 and 19");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else 
                        {
                            loop = false;
                        }
                    }                   
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input, please write an interger between 16 and 19");
                        Console.ForegroundColor = ConsoleColor.White;                        
                    } 
                }

                loop = true;
                while (loop)
                {
                    Console.Write("Number: ");
                    input = Console.ReadLine();
                    if (!int.TryParse(input, out number))
                    {                       
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input, please write an interger");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        loop = false;
                    }
                }
                loop = true;

                while (loop)
                {
                    Console.Write("Firmware Version: ");
                    firmwareVersion = Console.ReadLine();
                    if (string.IsNullOrEmpty(firmwareVersion))
                    {                        
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input, please write at least 1 character");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        loop = false;
                    }
                }
                loop = true;

                while (loop)
                {
                    Console.Write("Switch State: ");
                    input = Console.ReadLine();
                    if (int.TryParse(input, out switchState))
                    {
                        if(switchState < 0 || switchState > 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid input, please write an interger between 0 and 2");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            loop = false;
                        }                        
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input, please write an interger between 0 and 2");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }

                Metter Novo = new Metter(serialNumber, modelId, number, firmwareVersion, switchState);

                bool inserido =  Controller.InsertMetter(Novo);

                if(inserido == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Succeed in insert a new Metter ");
                    Console.ForegroundColor = ConsoleColor.White;         
                }


                insertLoop = false;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
        }

    }
}
