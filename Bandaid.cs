using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CavernCS
{
    public class Bandaid : Item
    {

        public Bandaid()
        {
            this.name = "Bandaid";
        }

        public Bandaid(string name, string description, float weight, object objectToUseOn) : base(name, description, weight, objectToUseOn)
        {

        }

        public override void use()
        {
            use(objectToUseOn);
        }

        public override void use(Object o)
        {
            if (o.GetType() == typeof(Player))
            {
                Player p = (Player)o; // must cast
                if (p.IsBleeding) {
                    Console.WriteLine("You used the bandaid");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("It stopped the bleeding!");
                    p.IsBleeding = false;
                    Console.WriteLine("Your health is " + p.getHealth());
                    p.inventory.removeItem(this);
                }
                else
                {
                    Console.WriteLine("You grabbed the bandaid");
                    Console.WriteLine("You don't have any wounds to put it on");
                }
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                // Object o is not a Person
                System.Console.WriteLine("Can't use Object on " + o.GetType());
            }
        }
    }
}
