using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Imaging;
//using System.Windows;

namespace Database.Utilities
{
    public static class TableBuilder
    {
        public delegate void Function(object sender, RoutedEventArgs e);
        public delegate void Function<B>(object sender, RoutedEventArgs e);

        public static bool TableSetup<B>(Grid targetGrid, List<string> columns, Grid targetSorter = null, List<string> sorters = null, int initIndex = 0, Function sortFunc = null)
        {
            //Grid setup
            AddSorter<B>(targetSorter, sorters, initIndex, sortFunc);
            if (targetGrid == null) return false;
            targetGrid.Children.Clear();
            targetGrid.RowDefinitions.Clear();
            targetGrid.ColumnDefinitions.Clear();
            RowDefinition r = new RowDefinition();
            r.Height = new GridLength(50);
            targetGrid.RowDefinitions.Add(r);
            //Title
            targetGrid.ColumnDefinitions.Add(new ColumnDefinition());   //Blank header for indicies
            targetGrid.ColumnDefinitions.Add(new ColumnDefinition());
            var tableName = TextBlock(targetGrid.Name, 0, 1);
            tableName.FontWeight = Windows.UI.Text.FontWeights.Bold;    //tableName.SetValue(Windows.UI.Xaml.Controls.TextBlock.FontWeightProperty, Windows.UI.Text.FontWeights.Bold);
            targetGrid.Children.Add(tableName);
            //Columns
            if (columns.Count <= 0) return true;
            for (int i = 0; i < columns.Count; i++)
            {
                targetGrid.ColumnDefinitions.Add(new ColumnDefinition());
                var t = TextBlock(columns.ElementAt(i), 0, i + 2);
                t.HorizontalAlignment = HorizontalAlignment.Center;
                targetGrid.Children.Add(t);
            }
            targetGrid.Children.Add(Border("#0000000", 3, 0, 2, 1, columns.Count));
            return true;
        }

        private static void AddSorter<B>(Grid targetSorter, List<string> sortOptions, int initIndex, Function sortFunc)
        {
            //Layout
            try
            {
                targetSorter.HorizontalAlignment = HorizontalAlignment.Right;
                if (sortOptions.Count <= 0) return;
            }
            catch (NullReferenceException) { return; }
            targetSorter.Children.Clear();
            targetSorter.ColumnDefinitions.Clear();
            targetSorter.ColumnDefinitions.Add(new ColumnDefinition());
            targetSorter.ColumnDefinitions.Add(new ColumnDefinition());
            targetSorter.ColumnDefinitions.Add(new ColumnDefinition());
            targetSorter.Margin = Margin(0, 0, 10, 15);
            //Content
            List<string> def = new List<string> { "Date Created" };
            def.AddRange(sortOptions);
            targetSorter.Children.Add(TextBlock("Sort by: ", 0, 0));
            targetSorter.Children.Add(ComboBox(targetSorter.Name + "SELECTION", def, initIndex, 0, 1));
            targetSorter.Children.Add(Button("Sort", sortFunc, "#0099bb", typeof(B).Name[0], 0, 2));
        }

        public static void Indexer(Grid g, int index)
        {
            var t = TextBlock(index, index, 0);
            t.Name = "INDEX" + index;
            t.SetValue(Windows.UI.Xaml.Controls.TextBlock.FontWeightProperty, Windows.UI.Text.FontWeights.Bold);
            g.Children.Add(t);
        }

        public static TextBlock TextBlock(string text) { return TextBlock(text, HorizontalAlignment.Left); }
        public static TextBlock TextBlock(int text) { return TextBlock(Convert.ToString(text), HorizontalAlignment.Center); }

        public static TextBlock TextBlock(string text, int rowNum, int colNum)
        { return TextBlock(text, rowNum, colNum, HorizontalAlignment.Left); }

        public static TextBlock TextBlock(int text, int rowNum, int colNum)
        { return TextBlock(Convert.ToString(text), rowNum, colNum, HorizontalAlignment.Center); }

        private static TextBlock TextBlock(string text, int rowNum, int colNum, HorizontalAlignment hAlign)
        {
            var newTextBlock = TextBlock(text, hAlign);
            newTextBlock.SetValue(Grid.RowProperty, rowNum);
            newTextBlock.SetValue(Grid.ColumnProperty, colNum);
            return newTextBlock;
        }
        private static TextBlock TextBlock(string text, HorizontalAlignment hAlign)
        {
            var newTextBlock = new TextBlock();
            newTextBlock.Margin = Margin(10, 0, 10, 0);
            newTextBlock.Text = text;
            newTextBlock.HorizontalAlignment = hAlign;
            newTextBlock.VerticalAlignment = VerticalAlignment.Center;
            return newTextBlock;
        }

        public static TextBox TextBox(string name, string text, int rowNum, int colNum, Function changed = null)
        {
            var newTextBox = TextBox(name, text, changed);
            newTextBox.SetValue(Grid.RowProperty, rowNum);
            newTextBox.SetValue(Grid.ColumnProperty, colNum);
            return newTextBox;
        }
        public static TextBox TextBox(string name, string text, Function changed = null)
        {
            var newTextBox = new TextBox();
            newTextBox.Margin = Margin(10, 5, 10, 0);
            newTextBox.Name = name;
            newTextBox.Text = text;
            if (changed != null) newTextBox.TextChanged += new TextChangedEventHandler(changed);
            return newTextBox;
        }

        private static WriteableBitmap loadedImage;
        public static Image Image(byte[] data, int w, int h, int rowNum, int colNum)
        {
            GetImage(data, w, h);
            WriteableBitmap img = loadedImage;
            Image picture = Image(data, w, h);
            picture.SetValue(Grid.RowProperty, rowNum);
            picture.SetValue(Grid.ColumnProperty, colNum);
            return picture;
        }
        public static Image Image(byte[] data, int w, int h)
        {
            GetImage(data, w, h);
            WriteableBitmap img = loadedImage;
            Image picture = new Image();
            picture.Source = img;
            return picture;
        }
        private static void GetImage(byte[] data, int w, int h)//async void GetImage(byte[] data, int w, int h)
        {
            //loadedImage = data == null ? null : await BytesToImage(data, w, h);
        }

        public static ComboBox ComboBox<B>(string name, List<B> list, int initIndex, int rowNum, int colNum, Function changed = null)
        {
            var newComboBox = ComboBox(name, list, initIndex, changed);
            newComboBox.SetValue(Grid.RowProperty, rowNum);
            newComboBox.SetValue(Grid.ColumnProperty, colNum);
            return newComboBox;
        }
        public static ComboBox ComboBox<B>(string name, List<B> list, int initIndex, Function changed = null)
        {
            if (list == null) return new ComboBox();
            var newComboBox = new ComboBox();
            newComboBox.Name = name;
            newComboBox.ItemsSource = list;
            if (list.Count > 0) newComboBox.SelectedIndex = initIndex;
            if (changed != null) newComboBox.SelectionChanged += new SelectionChangedEventHandler(changed);
            return newComboBox;
        }

        public static CheckBox CheckBox(string name, bool isChecked, string content)
        {
            var newCheckBox = new CheckBox();
            newCheckBox.Name = name;
            newCheckBox.IsChecked = isChecked;
            newCheckBox.Content = content;
            return newCheckBox;
        }

        public static Button Button(string name, Function clicked, string hexColor, int id, int rowNum, int colNum)
        {
            var newButton = Button(name, clicked, hexColor, id);
            newButton.SetValue(Grid.RowProperty, rowNum);
            newButton.SetValue(Grid.ColumnProperty, colNum);
            return newButton;
        }
        public static Button Button(string name, Function clicked, string hexColor, int id)
        {
            var newButton = new Button();
            newButton.Content = name;
            if (clicked != null) newButton.Click += new RoutedEventHandler(clicked);
            newButton.Tag = id;
            newButton.Background = Color(hexColor);
            return newButton;
        }

        public static Border Border(string hexColor, int thickness, int rowNum, int colNum, int rowSpan, int colSpan)
        {
            Border newBorder = new Border();
            newBorder.BorderThickness = new Thickness(thickness);
            newBorder.BorderBrush = Color(hexColor);
            newBorder.SetValue(Grid.RowProperty, rowNum);
            newBorder.SetValue(Grid.ColumnProperty, colNum);
            newBorder.SetValue(Grid.RowSpanProperty, rowSpan);
            newBorder.SetValue(Grid.ColumnSpanProperty, colSpan);
            return newBorder;
        }

        public static Thickness Margin(int left, int top, int right, int bottom)
        {
            Thickness margin = new Thickness();
            margin.Top = top;
            margin.Bottom = bottom;
            margin.Left = left;
            margin.Right = right;
            return margin;
        }

        public static StackPanel StackPanel(string name, HorizontalAlignment h, VerticalAlignment v, Thickness m, int rowNum, int colNum)
        {
            StackPanel newStack = new StackPanel();
            newStack.Name = name;
            newStack.HorizontalAlignment = h;
            newStack.VerticalAlignment = v;
            newStack.Margin = m;
            newStack.SetValue(Grid.RowProperty, rowNum);
            newStack.SetValue(Grid.ColumnProperty, colNum);
            return newStack;
        }

        public static SolidColorBrush Color(string hex)
        {
            /*hex = hex.Replace("#", string.Empty);
            byte a = (byte)(Convert.ToUInt32("FF", 16));
            byte r = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
            return new SolidColorBrush(Windows.UI.Color.FromArgb(a, r, g, b));*/
            return null;
        }

        /*public static async void OpenView<L, P>(L selected) where L : DBTables.Label
        {
            CoreApplicationView newCoreView = CoreApplication.CreateNewView();
            ApplicationView newAppView = null;
            int mainViewId = ApplicationView.GetApplicationViewIdForWindow(CoreApplication.MainView.CoreWindow);
            await newCoreView.Dispatcher.RunAsync(
              CoreDispatcherPriority.Normal,
              () =>
              {
                  selected.ViewMode = 1;
                  selected.AltMode = 1;
                  selected.InitialPivot = 0;
                  newAppView = ApplicationView.GetForCurrentView();
                  Window.Current.Content = new Frame();
                  (Window.Current.Content as Frame).Navigate(typeof(P), selected);
                  Window.Current.Activate();
              });
            await ApplicationViewSwitcher.TryShowAsStandaloneAsync(
              newAppView.Id,
              ViewSizePreference.UseHalf,
              mainViewId,
              ViewSizePreference.UseHalf);
        }*/
    }


    public static class TypeHelper
    {
        public static Type GetTypeByString(string type, Assembly lookIn)
        {
            var types = lookIn.DefinedTypes.Where(t => t.Name == type && t.IsSubclassOf(typeof(Page)));
            if (types.Count() == 0) throw new ArgumentException("The type you were looking for was not found", "type");
            else if (types.Count() > 1) throw new ArgumentException("The type you were looking for was found multiple times.", "type");
            return types.First().AsType();
        }
    }
}
