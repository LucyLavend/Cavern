using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CavernCS
{
    public class Map : Item
    {

        public Map()
        {
            this.name = "Map";
        }

        public Map(string name, string description, float weight) : base(name, description, weight)
        {

        }
    }
}
