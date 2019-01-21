using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BattleSimulator.Utilities.DataManager;
using static BattleSimulator.Utilities.Utils;

namespace BattleSimulator.Classes.ClassTemplates
{
    public abstract class BaseObject
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public Bitmap Image { get; protected set; }
        

        public BaseObject() { }

        public void Initialize(System.Data.SQLite.SQLiteDataReader data)
        {
            Name = data["Name"].ToString();
            Description = data["Description"].ToString();
            Image = BytesToImage(data, 3);
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
    }
}