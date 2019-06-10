using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CavernCS
{
    public class Inventory
    {
        private float maxWeigt;
        private Dictionary<string, Item> items = new Dictionary<string, Item>();

        public Inventory()
        {
            maxWeigt = 100;
        }

        public bool addItem(Item item)
        {
            if(item != null){
                if(getTotalWeight() + item.Weight <= maxWeigt)
                {
                    items.Add(item.Name, item);
                    return true;
                }
                else
                {
                    Console.WriteLine("There is no more space for this item!");
                    return false;
                }
            }
            return false;
        }

        public bool removeItem(Item item)
        {
            if (item != null)
            {
                items.Remove(item.Name);
                return true;
            }
            return false;
        }

        private float getTotalWeight()
        {
            float tWeight = 0;
            foreach (KeyValuePair<string, Item> entry in items)
            {
                tWeight += entry.Value.Weight;
            }
            return tWeight;
        }

        public float MaxWeight
        {
            get { return this.maxWeigt; }
            set { this.maxWeigt = value > 0 ? value : 0; }
        }

        public float getItemCount()
        {
            return this.items.Count;
        }

        public void showContents()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
		    Console.WriteLine("-~-----------------------------------------------~-");
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (KeyValuePair<string, Item> entry in items)
		    {
                Console.Write(entry.Key + ": " + entry.Value.Description);
                Console.SetCursorPosition(40, Console.CursorTop);
                Console.WriteLine("Weight: " + entry.Value.Weight);
		    }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
		    Console.WriteLine("-~-----------------------------------------------~-");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
