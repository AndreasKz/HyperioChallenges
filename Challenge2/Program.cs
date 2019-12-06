using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge2
{
    class Program
    {
        private static List<Ship> ships = new List<Ship>();
        static void Main()
        {
            // init
            Initialise();

            // main start
            main:
            Console.WriteLine("==========================");
            Console.WriteLine("====== Instructions ======");
            Console.WriteLine("==========================");
            Console.WriteLine("To add a ship type 'ship details'");
            Console.WriteLine("To add a ship type 'ship add'");
            Console.WriteLine("To transfer cargos between ships type 'cargo transfer'");
            Console.WriteLine("To exit type 'exit'");
            Console.WriteLine();
            var input = Console.ReadLine();
            switch(input)
            {
                case "ship add":
                    AddShip();               
                    Console.WriteLine();
                    goto main;
                // break;
                case "ship details":
                    shipDetails();
                    Console.WriteLine();
                    goto main;
                // break;
                case "cargo transfer":
                    transferCargo();
                    Console.WriteLine();
                    goto main;
                    // break
                case "exit":
                    break;
                default:
                    goto main;
                    // break
            }
        }

        private static void Initialise()
        {
            Console.WriteLine("Initialising...");
            // get ships
            string[] filePaths = Directory.GetFiles(Environment.CurrentDirectory + "/Ships/", "*.txt");
            foreach (string shipPath in filePaths)
            {
                using (StreamReader r = new StreamReader(shipPath))
                {
                    string json = r.ReadToEnd();
                    ships.Add(JsonConvert.DeserializeObject<Ship>(json));
                }
            }
            Console.WriteLine("Found " + ships.Count + " ships");
            Console.WriteLine();
        }

        private static void AddShip()
        {
            // needs validations
            Console.Write("Ship name: ");
            var name = Console.ReadLine();
            Console.Write("Max capacity: ");
            var cap = Console.ReadLine();

            // cargo
            Console.WriteLine("Cargo (press 'esc' to finish): ");
            ConsoleKeyInfo info = Console.ReadKey(true);
            List<string> cargo = new List<string>();
            StringBuilder buffer = new StringBuilder();
            while (info.Key != ConsoleKey.Escape)
            {
                Console.Write(info.KeyChar);
                buffer.Append(info.KeyChar);
                if (info.Key == ConsoleKey.Enter)
                {
                    // replacing carriage return
                    cargo.Add(buffer.ToString().Replace("\r",""));
                    // new line so we do not overwrite current text and confuse user
                    Console.WriteLine();
                    buffer.Clear();
                    // check if we exceeded cap
                    if (cargo.Count >= Convert.ToUInt32(cap, 16))
                    {
                        break;
                    }
                }
                info = Console.ReadKey(true);
            }

            Console.WriteLine("Creating new ship...");
            Ship newShip = new Ship(name, Convert.ToUInt32(cap,16), cargo);
            newShip.createShip();
            ships.Add(newShip);
        }

        private static void shipDetails()
        {
            foreach(Ship ship in ships)
            {
                Console.WriteLine();
                Console.WriteLine("Ship: " + ship.name);
                Console.WriteLine("-----------------------");
                Console.WriteLine("Max Capacity: " + ship.maxLoad);
                Console.WriteLine("Cargo: " + String.Join(", ", ship.currentLoad));
            }
        }

        private static void transferCargo()
        {
            // transfering cargo from one ship to another
            // todo create another class called ship management and add this method in there (and all other methods to do with ships)

            // from
            ship1fx:
            Console.Write("Transfer from ship (name): ");
            var ship1Name = Console.ReadLine();
            if (ships.FindIndex(ss=>ss.name == ship1Name) == -1)
            {
                Console.WriteLine("Ship not found.");
                goto ship1fx;
            }
            Ship ship1 = ships.Find(ss => ss.name == ship1Name);
            Console.WriteLine("Ship " + ship1.name + " has maximum capacity " + ship1.maxLoad);
            Console.WriteLine("Ship's cargo: " + String.Join(", ", ship1.currentLoad));

        // to
        ship2fx:
            Console.Write("Transfer to ship (name): ");
            var ship2Name = Console.ReadLine();
            if (ships.FindIndex(ss => ss.name == ship2Name) == -1)
            {
                Console.WriteLine("Ship not found.");
                goto ship2fx;
            }
            Ship ship2 = ships.Find(ss => ss.name == ship2Name);
            Console.WriteLine("Ship " + ship2.name + " has maximum capacity " + ship2.maxLoad);
            Console.WriteLine("Ship's cargo: " + String.Join(", ", ship2.currentLoad));
            // i removed the next few line as i added validations inside the ship class (the correct way, where they need to be)
            //if (ship2.maxLoad >= ship2.currentLoad.Length)
            //{
            //    Console.WriteLine("Ship is full. Please select another ship");
            //    goto ship2fx;
            //}

        // payload
        payload:
            Console.Write("Transfer item's name: ");
            var item = Console.ReadLine();
            if (!ship1.currentLoad.Contains(item))
            {
                Console.WriteLine("Item not found.");
                goto payload;
            }


            // transfering
            // first we add the item to ship2 to see if the ship can receive the item (i.e. ship not full)
            if (ship2.addCargo(item) == 0) // no errors proceed to transfering
            {
                if (ship1.removeCargo(item) != 0)
                {
                    goto payload;
                }
            } 
            else
            {
                goto ship2fx;
            }

            Console.WriteLine("The transfer was successful!");
        }
    }
}


// NOTE: I should have created a ShipsManagement class as well
// which would manage the creation of ships and the items transfer
// and everything else it may be needed in the future so we do not spam this main file