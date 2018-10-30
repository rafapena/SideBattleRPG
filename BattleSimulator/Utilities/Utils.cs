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
        private static readonly Random randomNumber = new Random();

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

        public static bool Chance(int value)
        {
            return value > RandInt(0, 100);
        }

        public static int RandInt(int low, int high)
        {
            return randomNumber.Next(low, high);
        }
    }
}
