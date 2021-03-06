﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using static Database.Utilities.SQLDB;

namespace Database.Utilities
{
    /// <summary>
    /// General table utilities to build a table from scratch. This static class can programmatically call UI elements that are meant to be set up in XAML.
    /// This is so that the table can continuously update on run-time.
    /// </summary>
    public static class TableBuilder
    {
        public delegate void Function(object sender, RoutedEventArgs e);

        // Sets up a grid from scratch (Title and buttons on included)
        public static bool TableSetup(Grid targetGrid, List<string> columnNames=null)
        {
            if (targetGrid == null) return false;
            targetGrid.Children.Clear();
            targetGrid.RowDefinitions.Clear();
            targetGrid.ColumnDefinitions.Clear();
            if (columnNames != null && columnNames.Count > 0)
            {
                for (int i = 0; i < columnNames.Count; i++)
                {
                    ColumnDefinition indexColumn = new ColumnDefinition();
                    targetGrid.ColumnDefinitions.Add(indexColumn);
                    TextBlock t = TextBlock(columnNames[i], 0, i);
                    t.Margin = Margin(2,2,2,2);
                    t.HorizontalAlignment = HorizontalAlignment.Center;
                    targetGrid.Children.Add(t);
                }
                targetGrid.Children.Add(Border("#0000000", 1, 0, 0, 1, columnNames.Count));
                targetGrid.ColumnDefinitions[0].Width = new GridLength(20);
                targetGrid.RowDefinitions.Add(new RowDefinition());
            }
            else targetGrid.ColumnDefinitions.Add(new ColumnDefinition());
            return true;
        }


        /// <summary>
        /// Textblock inputs for tables
        /// </summary>

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
            newTextBlock.Margin = Margin(5, 0, 5, 0);
            newTextBlock.Text = text;
            newTextBlock.HorizontalAlignment = hAlign;
            newTextBlock.VerticalAlignment = VerticalAlignment.Center;
            return newTextBlock;
        }


        /// <summary>
        /// Textbox inputs for tables
        /// </summary>

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


        /// <summary>
        /// Images for tables
        /// </summary>

       /* private static WriteableBitmap loadedImage;
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
        private static async void GetImage(byte[] data, int w, int h)
        {
            //loadedImage = data == null ? null : await BytesToImage(data, w, h);
        }*/


        /// <summary>
        /// Combo box inputs for tables
        /// </summary>

        public static ComboBox ComboBox<L>(string name, List<L> list, int initIndex, int rowNum, int colNum, Function changed = null)
        {
            var newComboBox = ComboBox(name, list, initIndex, changed);
            newComboBox.SetValue(Grid.RowProperty, rowNum);
            newComboBox.SetValue(Grid.ColumnProperty, colNum);
            return newComboBox;
        }
        public static ComboBox ComboBox<L>(string name, List<L> list, int initIndex, Function changed = null)
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


        /// <summary>
        /// Button inputs for tables
        /// </summary>

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


        /// <summary>
        /// Other inputs for tables
        /// </summary>

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
            hex = hex.Replace("#", string.Empty);
            Color c = new Color();
            c.A = (byte)(Convert.ToUInt32("FF", 16));
            c.R = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            c.G = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            c.B = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16)); ;
            return new SolidColorBrush(c);
        }
    }
}
