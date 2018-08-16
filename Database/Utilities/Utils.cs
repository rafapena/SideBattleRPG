using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Database.Utilities
{
    class Utils
    {
        public static bool Confirm(string title, string message)
        {
            return MessageBox.Show(title, message, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes;
        }

        private static Regex isNumber = new Regex(@"^[-]?(\d)+(.(\d)+)?$", RegexOptions.IgnoreCase);
        public static bool NumberBetween(string inputText, double low, double high, bool lowInclusive = true, bool highInclusive = true)
        {
            if (!isNumber.Match(inputText).Success) return false;
            double n;
            try { n = double.Parse(inputText); }
            catch (OverflowException) { return false; }
            bool aboveLow = lowInclusive ? low <= n : low < n;
            bool belowHigh = highInclusive ? high >= n : high > n;
            return aboveLow && belowHigh;
        }

        private static Regex isPosInt = new Regex(@"^(\d)+$", RegexOptions.IgnoreCase);
        public static bool PosInt(string inputText)
        {
            if (!isPosInt.Match(inputText).Success) return false;
            int n;
            try { n = int.Parse(inputText); }
            catch (OverflowException) { return false; }
            return n >= 0;
        }

        private static Regex badlySpaced = new Regex(@"^\s+$", RegexOptions.IgnoreCase);
        public static string CutSpaces(string inputText) { return badlySpaced.Match(inputText).Success ? "" : inputText; }

        public static bool InRequiredLength(string inputText)
        {
            inputText = CutSpaces(inputText);
            return inputText.Length > 0 && inputText.Length <= 16;
        }

        public static void Print(string message) { System.Diagnostics.Debug.WriteLine(message); }
        public static void Print() { Print("00000"); }
    }
}
