﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Data.SQLite;
using Database.Utilities;
using static Database.Utilities.TableBuilder;


namespace Database.TableTemplates
{
    public abstract class _TableTemplateOperations : UserControl, ObjectTemplateOperations
    {
        // Information for the table template
        protected Grid Table { get; set; }
        protected string TableTitle { get; private set; }
        protected List<string> ColumnNames { get; private set; }
        protected List<List<UIElement>> Elements { get; private set; }
        protected int ScrollerHeight { get; private set; }
        public int Count { get; private set; }
        
        // Database information for the table template
        protected string HostDBTable { get; set; }
        protected string TargetDBTable { get; set; }
        protected int HostId { get; set; }
        public string TableIdentifier { get; set; }     // Only use this to uniquely identify two of the same many-to-many relationship tables


        // Adds a row to the table template
        protected virtual string CheckAddability() { return ""; }
        protected abstract void OnAddRow();
        public void AddRow(object sender, RoutedEventArgs e)
        {
            string cannotAddMessage = CheckAddability();
            if (cannotAddMessage != "")
            {
                MessageBox.Show(cannotAddMessage, "Could not add to " + TableTitle);
                return;
            }
            Count++;
            Table.RowDefinitions.Add(new RowDefinition());
            TextBlock t = TextBlock(Count, Count, 0);
            t.Margin = new Thickness(1, 1, 1, 1);
            t.HorizontalAlignment = HorizontalAlignment.Center;
            Elements.Add(new List<UIElement>() { t });
            Table.Children.Add(t);
            if (sender == null) return;
            OnAddRow();
            AddRangeToTable();
        }

        // Removes the bottom row from the table template
        protected virtual void OnRemoveRow() { }
        protected void RemoveRow(object sender, RoutedEventArgs e)
        {
            if (Count <= 0) return;
            List<UIElement> elmts = Elements[Count - 1];
            for (int i = 0; i < elmts.Count; i++) Table.Children.Remove(elmts[i]);
            Elements.RemoveAt(Count - 1);
            Table.RowDefinitions.RemoveAt(Count);
            OnRemoveRow();
            Count--;
        }

        // Helper method that adds all of the column content to latest row of the table
        protected void AddRangeToTable()
        {
            List<UIElement> elmts = Elements[Count - 1];
            for (int i = 1; i < elmts.Count; i++) Table.Children.Add(elmts[i]);
        }

        // The actual initialization method for table templates (automatically runs before InitializeNew() and Read())
        public void Setup(string hostDBTable, string targetDBTable, string title, List<string> columnNames, int scrollerHeight=100)
        {
            HostDBTable = hostDBTable;
            TargetDBTable = targetDBTable;
            HostId = GetHostId();
            TableTitle = title;
            ScrollerHeight = scrollerHeight;
            if (columnNames.Count > 0)
            {
                ColumnNames = new List<string>();
                ColumnNames.Add("#");
                ColumnNames.AddRange(columnNames);
            }
            InitializeNew();
        }

        // Helper method of Setup: Get Id of the host DB table that matches the current DB table the user is currently viewing
        protected virtual int GetHostId()
        {
            if (SQLDB.CurrentTable == HostDBTable) return SQLDB.CurrentId;
            int id;
            using (var conn = AccessDB.Connect())
            {
                conn.Open();
                string select = "SELECT  " + HostDBTable + "_ID FROM " + SQLDB.CurrentTable + " JOIN " + HostDBTable;
                string where = "WHERE " + HostDBTable + "_ID = " + HostDBTable + "ID AND " + SQLDB.CurrentTable + "_ID = " + SQLDB.CurrentId;
                using (var reader = SQLDB.Read(conn, select + " " + where + ";"))
                {
                    reader.Read();
                    try { id = int.Parse(reader[HostDBTable + "_ID"].ToString()); }
                    catch (InvalidOperationException) { id = SQLDB.CurrentId; }
                }
                conn.Close();
            }
            return id;
        }


        protected abstract void OnInitializeNew();
        public void InitializeNew() // Not too useful: SetupTableData does all of the work, due to naming conventions and interface constistencies
        {
            Elements = new List<List<UIElement>>();
            Count = 0;
            OnInitializeNew();
            TableIdentifier = "";
            TableSetup(Table, ColumnNames);
        }


        protected abstract string OnValidateInputs(int i);
        public string ValidateInputs()
        {
            string err = "";
            for (int i = 0; i < Count; i++)
            {
                err += OnValidateInputs(i);
                if (err != "") break;
            }
            return err;
        }

        protected abstract void OnParameterizeInputs(int i);
        public void ParameterizeAttributes()
        {
            for (int i = 0; i < Count; i++) OnParameterizeInputs(i);
        }


        protected abstract string[] OnCreate();
        protected abstract string OnCreateValues(int i);
        public void Create(SQLiteConnection conn)
        {
            SQLDB.ResetParameterizedAttributes();
            ParameterizeAttributes();
            string[] str = OnCreate();
            for (int i = 0; i < Count; i++) SQLDB.Write(conn, "INSERT INTO " + str[0] + " (" + str[1] + ") VALUES (" + OnCreateValues(i) + ");");
        }


        protected virtual string[] OnReadCommands()
        {
            string targetIdName = (HostDBTable == TargetDBTable ? "Other" : "") + TargetDBTable + "ID";
            string connectorTable = HostDBTable + "_To_" + TargetDBTable + TableIdentifier;
            string tables = connectorTable + " JOIN  BaseObject JOIN " + TargetDBTable;
            string whereSort = "BaseObject_ID = BaseObjectID AND " + TargetDBTable + "_ID = " + targetIdName + " AND " +
                connectorTable + "." + HostDBTable + "ID = " + HostId + " ORDER BY TableIndex";
            return new string[] { tables, whereSort };
        }
        protected abstract void OnRead(SQLiteDataReader reader);
        public void Read()
        {
            if (Table.ColumnDefinitions.Count <= 0) return;
            using (var conn = AccessDB.Connect())
            {
                conn.Open();
                string[] readStr = OnReadCommands();
                using (var reader = SQLDB.Read(conn, "SELECT * FROM " + readStr[0] + " WHERE " + readStr[1] + ";"))
                {
                    while (reader.Read())
                    {
                        AddRow(null, null);
                        OnRead(reader);
                        AddRangeToTable();
                    }
                }
                conn.Close();
            }
        }


        public void Update(SQLiteConnection conn)
        {
            Delete(conn);
            Create(conn);
        }

        protected virtual string[] OnDelete()
        {
            string connectorTable = HostDBTable + "_To_" + TargetDBTable + TableIdentifier;
            return new string[] { connectorTable, HostDBTable + "ID = " + HostId };
        }
        public void Delete(SQLiteConnection conn)
        {
            string[] deleteMsg = OnDelete();
            SQLDB.Write(conn, "DELETE FROM " + deleteMsg[0] + " WHERE " + deleteMsg[1] + ";");
        }
        
        public void Clone(SQLiteConnection conn)
        {
            HostId = SQLDB.MaxIdPlusOne(HostDBTable);
        }
    }
}