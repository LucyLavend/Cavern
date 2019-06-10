using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CavernCS
{
    public class Ring : Item
    {

        public Ring()
        {
            this.Name = "Ring";
        }

        public Ring(string name, string description = "", float weight = 1f) : base(name, description, weight)
        {

        }
    }
}
