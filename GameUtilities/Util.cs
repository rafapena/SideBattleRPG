using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Popups;

namespace GameUtilities
{
    public static class Util
    {
        /*public static Random random = new Random();

        public static int RandInt(int low, int high) { return random.Next(low, high); }
        public static double Rand(double low, double high) { return low + (random.NextDouble() * (high - low)); }
        public static bool Chance(int probability) { return probability > random.Next(0, 100); }
        public static double Noise(double scale) { return Rand(-1, 1) * scale; }

        public static void Print<B>(B[] arr)
        {
            string p = "ARRAY => ";
            foreach (var x in arr) p += Convert.ToString(x) + " ";
            Print(p);
        }
        public static void Print<B>(List<B> arr)
        {
            string p = "LIST => ";
            foreach (var x in arr) p += Convert.ToString(x) + " ";
            Print(p);
        }

        public static List<B> CloneList<B>(List<B> original)
        {
            List<B> clone = new List<B>(original.Count);
            foreach (B elmt in original) clone.Add(elmt);
            clone.TrimExcess();
            return clone;
        }

        public static bool IsSameType(object comparee, Type target) { return comparee.GetType() == target; }*/
    }
}