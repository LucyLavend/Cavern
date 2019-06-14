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

        public Arrowhead(string name, string description, float weight) : base(name, description, weight)
        {

        }

        public override void use(Object o)
        {
            if (o.GetType() == typeof(Player))
            {
                Player p = (Player)o; // must cast
                p.damage(5);
            }
            else
            {
                // Object o is not a Person
                System.Console.WriteLine("Can't use Object on " + o.GetType());
            }
        }
    }
}
