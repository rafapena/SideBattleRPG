using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSimulator.Classes.ClassTemplates
{
    public abstract class BaseObject
    {
        private int Id;
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Bitmap Image { get; private set; }

        protected BaseObject() { }
        public BaseObject(int id, string name, string description, Bitmap image = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Image = image;
        }

        public BaseObject(BaseObject original)
        {
            Id = original.Id;
            Name = original.Name;
            Description = original.Description;
            Image = original.Image;
        }

        public bool Equals(BaseObject other)
        {
            return Id == other.Id;
        }

        public List<T> ClonePrimitiveList<T>(List<T> original)
        {
            List<T> cloned = new List<T>();
            for (int i = 0; i < original.Count; i++) cloned[i] = original[i];
            return cloned;
        }

        public delegate T GenericFunction<T>(T o);
        public List<T> CloneObjectList<T>(List<T> original, GenericFunction<T> newObject)
        {
            if (original == null) return null;
            List<T> cloned = new List<T>();
            for (int i = 0; i < original.Count; i++) cloned.Add(newObject(original[i]));
            return cloned;
        }
    }
}