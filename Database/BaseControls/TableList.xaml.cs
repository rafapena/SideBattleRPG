﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using Database.Classes;
using Database.Utilities;
using static Database.Utilities.TableBuilder;

namespace Database.BaseControls
{
    /// <summary>
    /// Interaction logic for TableList.xaml
    /// </summary>
    public partial class TableList : UserControl
    {
        private Button CurrentlySelected;
        private const string StandardButtonColor = "#dddddd";
        private const string HighlightedButtonColor = "#888888";
        private const string StandardNewColor = "#aaffaa";
        private const string HighlightedNewColor = "#558855";

        public TableList()
        {
            InitializeComponent();
        }

        public void SetupTable(bool keepHighlightedButton)
        {
            AddNew.Background = Color(StandardNewColor);
            Title.Text = SQLDB.CurrentTable;
            CurrentlySelected = null;
            TableSetup(RowsTable);
            int count = 0;
            using (var conn = SQLDB.DB())
            {
                conn.Open();
                string query = "SELECT * FROM BaseObjects JOIN " + SQLDB.CurrentTable + " WHERE BaseObject_ID = BaseObjectID ORDER BY Name";
                using (SQLiteDataReader reader = SQLDB.Retrieve(query, conn))
                {
                    if (keepHighlightedButton) while (reader.Read()) CreateRowWithHighlighted(reader, RowsTable, count++);
                    else while (reader.Read()) CreateRow(reader, RowsTable, count++);
                }
                Count.Text = "Total: " + count;
                conn.Close();
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Row creating operations --
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void CreateRow(SQLiteDataReader reader, Grid table, int rowNum)
        {
            Button b = Button((string)reader["Name"], Read, StandardButtonColor, int.Parse(reader[SQLDB.CurrentClass + "_ID"].ToString()), rowNum, 0);
            CreateRow(table, b);
        }

        private void CreateRowWithHighlighted(SQLiteDataReader reader, Grid table, int rowNum)
        {
            int currId = int.Parse(reader[SQLDB.CurrentClass + "_ID"].ToString());
            Button b = null;
            if (SQLDB.CurrentId == currId)
            {
                b = Button((string)reader["Name"], Read, HighlightedButtonColor, currId, rowNum, 0);
                CurrentlySelected = b;
            }
            else b = Button((string)reader["Name"], Read, StandardButtonColor, currId, rowNum, 0);
            CreateRow(table, b);
        }

        private void CreateRow(Grid table, Button b)
        {
            table.RowDefinitions.Add(new RowDefinition());
            b.HorizontalAlignment = HorizontalAlignment.Left;
            b.HorizontalContentAlignment = HorizontalAlignment.Left;
            b.Width = 100;
            b.Height = 20;
            table.Children.Add(b);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Highlighting buttons --
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void HighlightButton(Button current)
        {
            if (CurrentlySelected != null) CurrentlySelected.Background = Color(StandardButtonColor);
            current.Background = Color(HighlightedButtonColor);
            CurrentlySelected = current;
        }

        public void RemoveButtonHighlight()
        {
            if (CurrentlySelected != null) CurrentlySelected.Background = Color(StandardButtonColor);
            AddNew.Background = Color(HighlightedNewColor);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// -- Trigger Setup and Trigger Page Functions --
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Read(object sender, EventArgs e)
        {
            Button b = sender as Button;
            SQLDB.CurrentId = (int)b.Tag;
            HighlightButton(b);
            AddNew.Background = Color(StandardNewColor);
            switch (SQLDB.CurrentClass)
            {
                case "Achievement": Read<Achievement>(); break;
                case "Player": Read<Player>(); break;
            }
        }

        public void InitializeNew(object sender, EventArgs e)
        {
            switch (SQLDB.CurrentClass)
            {
                case "Achievement": InitializeNew<Achievement>(); break;
                case "Player": InitializeNew<Player>(); break;
            }
        }

        private void Read<P>() where P : _ClassOperations { (Application.Current.MainWindow.Content as P).Read(); }
        private void InitializeNew<P>() where P : _ClassOperations { (Application.Current.MainWindow.Content as P).InitializeNew(); }
    }
}