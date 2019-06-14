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
        protected object objectToUseOn;

        public Item()
        {
            Name = "Item";
            Description = "A generic Item";
            Weight = 1;
        }

        public virtual void use(object o)
        {
            Console.WriteLine(this.Name + " cannot be used on that.");
        }

        public virtual void use()
        {
            Console.WriteLine("object used");
        }

        public Item(string name, string description, float weight, object obj = null)
        {
            Name = name;
            Description = description;
            Weight = weight;
            objectToUseOn = obj;
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
