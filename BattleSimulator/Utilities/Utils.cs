using System;
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
    public static class Utils
    {
        private static Random randomNumber = new Random();
        public static bool Chance(int value)
        {
            return value >= RandInt(1, 100);
        }
        public static int RandInt(int low, int high)
        {
            return randomNumber.Next(low, high + 1);
        }


        public static int Int(object o)
        {
            string res = o.ToString();
            return res == "" ? 0 : int.Parse(res);
        }
        public static double Dbl(object o)
        {
            return o.ToString() == "" ? 0 : (double)o;
        }


        public static bool ValidListInput<T>(List<T> objList, int i) where T : class
        {
            return objList != null && objList[i] != null && i >= 0 && i < objList.Count;
        }


        public delegate T NewCaller<T>(T o);
        public static T Clone<T>(T original, NewCaller<T> newObject) where T : class
        {
            return  original == null ? null : newObject(original);
        }
        public static List<T> Clone<T>(List<T> original, NewCaller<T> newObject) where T : class
        {
            if (original == null) return null;
            List<T> cloned = new List<T>();
            for (int i = 0; i < original.Count; i++) cloned.Add(newObject(original[i]));
            return cloned;
        }
        public static List<T> Clone<T>(List<T> original)
        {
            if (original == null) return null;
            List<T> cloned = new List<T>();
            for (int i = 0; i < original.Count; i++) cloned.Add(original[i]);
            return cloned;
        }
        

        public static Bitmap BytesToImage(SQLiteDataReader reader, int attributeIndex)
        {
            byte[] blob = ImageManager.BlobToBytes(reader, attributeIndex);
            if (blob == null) return null;
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }
    }
}
