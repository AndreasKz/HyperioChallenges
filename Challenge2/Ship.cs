using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge2
{
    class Ship
    {
        public String name { get; set; }
        public UInt32 maxLoad { get; set; }
        public List<String> currentLoad { get; set; }

        public Ship(String sName, UInt32 sLoad, List<String> sCurrentLoad = null)
        {
            this.name = sName;
            this.maxLoad = sLoad;
            this.currentLoad = sCurrentLoad;
        }

        public void createShip()
        {
            // check if ship exists
            String path = Environment.CurrentDirectory + "/Ships/" + this.name + ".txt";
            if (File.Exists(path))
            {
                Console.Write("Ship already exists. To overwrite type 'yes': ");
                var input = Console.ReadLine();
                if (input != "yes") return;
            }
            // save the ship
            this.saveShip();
            Console.WriteLine("Ship created!");
        }

        public int addCargo(string cargo)
        {
            if (this.currentLoad.Count >= this.maxLoad)
            {
                Console.WriteLine("Ship's capacity reached. Item has not been transfered");
                return 1;
            }
            this.currentLoad.Add(cargo);
            // save changes
            this.saveShip();
            return 0;
        }

        public int removeCargo(string cargo)
        {
            int idx = this.currentLoad.IndexOf(cargo);
            // check if item exists
            if (idx == -1)
            {
                Console.WriteLine("Item not found");
                return 1;
            }
            // remove the item
            this.currentLoad.RemoveAt(idx);
            // save changes
            this.saveShip();
            return 0;
        }

        private void saveShip()
        {
            // saves the ship as json format in /Ships
            // convert object to json and write to file
            String path = Environment.CurrentDirectory + "/Ships/" + this.name + ".txt";
            String jSon = JsonConvert.SerializeObject(this);
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, this);
            }
        }
    }
}
