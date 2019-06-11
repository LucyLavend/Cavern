using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CavernCS
{
    public class Inventory
    {
        private string title;
        private float maxWeigt;
        private Dictionary<string, Item> items = new Dictionary<string, Item>();

        public Inventory(string title = "")
        {
            this.title = title;
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

        public float getTotalWeight()
        {
            float tWeight = 0;
            foreach (KeyValuePair<string, Item> entry in items)
            {
                tWeight += entry.Value.Weight;
            }
            return tWeight;
        }

        public bool swapItem(Inventory other, Item item)
        {
            if (item != null)
            {
                if (addItem(item))
                {
                    other.removeItem(item);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
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

        public Item findItem(string item)
        {
            Item retrunItem;
            items.TryGetValue(item, out retrunItem);
            return retrunItem;
        }

        public void showContents()
        {
            string line = "-~-----------------------------------------------~-";
            int titleDistance = Math.Abs(line.Length /2 - title.Length / 2);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(line);
            Console.SetCursorPosition(titleDistance, Console.CursorTop -1);
            Console.WriteLine(title);
            Console.ForegroundColor = ConsoleColor.Cyan;
            if(items.Count != 0)
            {
                foreach (KeyValuePair<string, Item> entry in items)
		        {
                    Console.Write(entry.Key + ": " + entry.Value.Description);
                    Console.SetCursorPosition(40, Console.CursorTop);
                    Console.WriteLine("Weight: " + entry.Value.Weight);
		        }
            }
            else
            {
                Console.WriteLine("No items.");
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(line);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
