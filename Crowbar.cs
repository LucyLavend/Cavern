using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CavernCS
{
    public class Crowbar : Item
    {

        public Crowbar()
        {
            this.name = "Crowbar";
        }

        public Crowbar(string name, string description, float weight) : base(name, description, weight)
        {

        }
    }
}
