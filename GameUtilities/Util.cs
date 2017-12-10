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
        public static Random random = new Random();

        public static int RandInt(int low, int high) { return random.Next(low, high); }
        public static double Rand(double low, double high) { return low + (random.NextDouble() * (high - low)); }
        public static bool Chance(int probability) { return probability > random.Next(0, 100); }
        public static double Noise(double scale) { return Rand(-1, 1) * scale; }

        public static async void Alert(string message)
        {
            var dialog = new MessageDialog(message);
            dialog.Commands.Add(new UICommand("OK"));
            await dialog.ShowAsync();
        }

        public static async void Confirm(string header, string message, Action yes, Action no = null, Action cancel = null)
        {
            var dialog = new MessageDialog(message, header);
            dialog.Commands.Add(new UICommand("Yes") { Id = 0 });
            dialog.Commands.Add(new UICommand("No") { Id = 1 });

            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Cancel") { Id = 2 });
            }
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            var result = await dialog.ShowAsync();
            if (result.Label == "Yes") yes?.Invoke();
            else if (result.Label == "No") no?.Invoke();
            else cancel?.Invoke();
        }

        private static Regex PNOneHundred = new Regex(@"^[-]?(100|\d{1,2})$", RegexOptions.IgnoreCase);
        private static Regex oneHundred = new Regex(@"^(100|\d{1,2})$", RegexOptions.IgnoreCase);
        private static Regex positiveInt = new Regex(@"^\d{1,9}$", RegexOptions.IgnoreCase);
        private static Regex badlySpaced = new Regex(@"^\s+$", RegexOptions.IgnoreCase);

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

        public static bool IsSameType(object comparee, Type target) { return comparee.GetType() == target; }
    }
}