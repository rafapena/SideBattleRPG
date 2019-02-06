using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Database.Utilities;

namespace BattleSimulator.Utilities
{
    public static class ListManager
    {
        public static bool ValidListInput<T>(List<T> objList, int i) where T : class
        {
            return objList != null && objList[i] != null && i >= 0 && i < objList.Count;
        }

        public delegate T NewCaller<T>(T o);
        public static T Clone<T>(T original, NewCaller<T> newObject) where T : class
        {
            return original == null ? null : newObject(original);
        }
        public static List<T> Clone<T>(List<T> original, NewCaller<T> newObject) where T : class
        {
            if (original == null) return null;
            List<T> cloned = new List<T>();
            for (int i = 0; i < original.Count; i++) cloned.Add(newObject(original[i]));
            return cloned;
        }
        public static T[] Clone<T>(T[] original, NewCaller<T> newObject) where T : class
        {
            if (original == null) return null;
            T[] cloned = new T[original.Length];
            for (int i = 0; i < original.Length; i++) cloned[i] = newObject(original[i]);
            return cloned;
        }

        public static List<T> Clone<T>(List<T> original)
        {
            if (original == null) return null;
            List<T> cloned = new List<T>();
            for (int i = 0; i < original.Count; i++) cloned.Add(original[i]);
            return cloned;
        }
        public static T[] Clone<T>(T[] original)
        {
            if (original == null) return null;
            T[] cloned = new T[original.Length];
            for (int i = 0; i < original.Length; i++) cloned[i] = original[i];
            return cloned;
        }

        public static void Print<T>(List<T> list)
        {
            string listValues = "";
            foreach (T elmnt in list) listValues += elmnt + " ";
            MessageBox.Show(listValues);
        }
        public static void Print<T>(T[] list)
        {
            string listValues = "";
            foreach (T elmnt in list) listValues += elmnt + " ";
            MessageBox.Show(listValues);
        }
    }
}
