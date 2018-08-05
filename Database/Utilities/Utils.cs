using System;
using System.Collections.Generic;
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

        private static Regex PNThree = new Regex(@"^[-]?(((0|1|2)([.]\d+)?)|3)$", RegexOptions.IgnoreCase);
        private static Regex PNOneHundred = new Regex(@"^[-]?(100|\d{1,2})$", RegexOptions.IgnoreCase);
        private static Regex oneHundred = new Regex(@"^(100|\d{1,2})$", RegexOptions.IgnoreCase);
        private static Regex positiveInt = new Regex(@"^\d{1,9}$", RegexOptions.IgnoreCase);
        private static Regex badlySpaced = new Regex(@"^\s+$", RegexOptions.IgnoreCase);

        public static bool N3ToP3Float(string inputText) { return PNThree.Match(inputText).Success; }
        public static bool N100ToP100(string inputText) { return PNOneHundred.Match(inputText).Success; }
        public static bool ZeroToOneHundred(string inputText) { return oneHundred.Match(inputText).Success; }
        public static bool PosInt(string inputText) { return positiveInt.Match(inputText).Success; }
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
