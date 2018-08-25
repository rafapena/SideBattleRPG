using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;

namespace Database.Utilities
{
    /// <summary>
    /// Everything else that is too short to be properly categorized.
    /// Most of these validate string inputs. Others are confirm and print messages.
    /// Add anything else here that might be convenient.
    /// </summary>
    public static class Utils
    {
        // Confirm message
        public static bool Confirm(string title, string message)
        {
            return MessageBox.Show(title, message, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes;
        }

        // Checks if a string input if a number is in between low and high
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

        // Checks if a string input is a positive integer
        private static Regex isPosInt = new Regex(@"^(\d)+$", RegexOptions.IgnoreCase);
        public static bool PosInt(string inputText)
        {
            if (!isPosInt.Match(inputText).Success) return false;
            int n;
            try { n = int.Parse(inputText); }
            catch (OverflowException)
            {
                MessageBox.Show("Input overflow detected: This does not count as a positive integer");
                return false;
            }
            return n >= 0;
        }

        // Returns an empty string, if it only has spaces
        private static Regex badlySpaced = new Regex(@"^\s+$", RegexOptions.IgnoreCase);
        public static string CutSpaces(string inputText) { return badlySpaced.Match(inputText).Success ? "" : inputText; }

        // Standard length of certain strings inputs should be between 1 and maxLength, inclusively
        public static bool InRequiredLength(string inputText, int maxLength)
        {
            inputText = CutSpaces(inputText);
            return inputText.Length > 0 && inputText.Length <= maxLength;
        }

        // Debug statements
        public static void Print(string message) { System.Diagnostics.Debug.WriteLine(message); }
        public static void Print() { Print("00000"); }
    }
}
