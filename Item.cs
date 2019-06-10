using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CavernCS
{
    public class Item
    {
        protected string name;
        protected string description;
        protected float weight;

        public Item()
        {
            Name = "Item";
            Description = "A generic Item";
            Weight = 1;
        }

        public Item(string name, string description, float weight)
        {
            Name = name;
            Description = description;
            Weight = weight;
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public float Weight
        {
            get { return this.weight; }
            set { this.weight = value > 0 ? value : 0; }
        }
    }
}
