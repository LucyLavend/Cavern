using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CavernCS
{
    public class Arrowhead : Item
    {

        public Arrowhead()
        {
            this.name = "Arrowhead";
        }

        public Arrowhead(string name, string description, float weight, object objectToUseOn) : base(name, description, weight, objectToUseOn)
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
                Console.WriteLine("You fiddled aroud with the arrowhead");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You accidentally cut yourself!");
                p.damage(5);
                Console.WriteLine("Your health is now " + p.getHealth());
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
