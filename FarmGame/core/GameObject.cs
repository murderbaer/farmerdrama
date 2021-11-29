using System;
using System.Collections.Generic;
using System.Linq;

namespace FarmGame
{
    public class GameObject
    {
        public GameObject(string name = "")
        {
            Name = name;
        }

        public string Name { get; set; }

        public List<IComponent> Components { get; } = new List<IComponent>();

        public TYPE GetComponent<TYPE>()
        {
            var component = Components.OfType<TYPE>().FirstOrDefault();
            if (component == null)
            {
                throw new ArgumentException($"Component of type {typeof(TYPE).Name} not found");
            }

            return (TYPE)component;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}