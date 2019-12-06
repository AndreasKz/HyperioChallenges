using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge2
{
    /* Tasks
   1: Bug: something odd happens when transfering the last container
   2: As a user I want the load of my ships to be persisted so that I can restart the program and continue from where I left (txt-files is enough)
   3: As a user I want to restrict the load pr ship to a max of 4 containers because ship 1 and 2 have a max capacity of 4
   4: As a user I want to register an unlimitted number of ships and make load transfers between them, so that I can manage my whole fleet
   5: As a user I want to specify individual capacities per ship, so that I can handle ships with diferent capacities
   6: As a user I want to also manage trucks, so that I can use the program to manage load of my trucks as well
   7: As a user I want the offloading from trucks to be restricted to the last load, so that it is not possible to unload unreachable goods
   */

    class ProgramOld
    {
        // Data
        static string containerA = "Container A";
        static string containerB = "Container B";
        static string containerC = "Container C";

        static string containerD = "Container D";
        static string containerE = "Container E";

        static string[] ship1Load = new string[] { containerA, containerB, containerC };
        static string[] ship2Load = new string[] { containerD, containerE };

        static void MainOld(string[] args)
        {
            Console.WriteLine("Current load:");
            PrintLoad();

            again:
            // data entry
            Console.Write("Load:");
            var load = Console.ReadLine();

            // logic

            if (load == "")
                return;

            bool found = false;
            for (int i = 0; i < ship1Load.Length; i++)
            {
                if (ship1Load[i] == load)
                    found = true;
            }
            if (!found)
            {
                Console.WriteLine("Load " + load + " was not found on ship 1!");
                goto again;
            }

            string[] newLoadShip1 = new string[ship1Load.Length - 1];
            bool after = false;
            for (int index = 0; index < ship1Load.Length; index++)
            {
                if (after)
                {
                    newLoadShip1[index - 1] = ship1Load[index];
                }
                else
                {
                    newLoadShip1[index] = ship1Load[index];
                    after = ship1Load[index] == load;
                }
            }

            ship1Load = newLoadShip1;

            string[] newLoadShip2 = new string[ship2Load.Length + 1];
            for (int index = 0; index < ship2Load.Length; index++)
            {
                newLoadShip2[index] = ship2Load[index];
            }
            newLoadShip2[newLoadShip2.Length - 1] = load;

            ship2Load = newLoadShip2;

            // Ausgabe
            Console.WriteLine(load + " was transfered from ship 1 to ship 2!");
            PrintLoad();

            goto again;
        }

        private static void PrintLoad()
        {
            Console.WriteLine("Ship 1: " + String.Join(", ", ship1Load));
            Console.WriteLine("Ship 2: " + String.Join(", ", ship2Load));
        }
    }
}
