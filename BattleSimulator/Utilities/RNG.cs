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
    public static class RNG
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

        public static T RandList<T>(List<T> list)
        {
            return list[randomNumber.Next(0, list.Count)];
        }

        public static T RandList<T>(T[] list)
        {
            return list[randomNumber.Next(0, list.Length)];
        }
    }
}
