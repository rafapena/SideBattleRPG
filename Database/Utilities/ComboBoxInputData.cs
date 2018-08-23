﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Data.SQLite;
using System.Windows;

namespace Database.Utilities
{
    public class ComboBoxInputData
    {
        // Ensures that there is a value called "None" in the combo box
        public const bool ADD_NULL_INPUT = true;

        // Really only exists for handling a table of combo boxes
        public List<int> SelectedIds { get; private set; }

        // The list of Ids which map to the list of names, which map to the combobox
        public List<int> OptionsListIds { get; private set; }
        public List<string> OptionsListNames { get; private set; }


        // Sets up the list that will be added to the combobox input
        public ComboBoxInputData(string idAttribute, string nameAttribute, string queryTables,
            string queryCondition, string sortAttributes, bool addNullInput=false)
        {
            SelectedIds = new List<int>();
            OptionsListIds = addNullInput ? new List<int> { -1 } : new List<int>();
            OptionsListNames = addNullInput ? new List<string> { "None" } : new List<string>();
            using (var conn = SQLDB.DB())
            {
                conn.Open();
                string select = "SELECT " + idAttribute + ", " + nameAttribute + " FROM " + queryTables;
                using (var reader = SQLDB.Read(conn, select + " WHERE " + queryCondition + " ORDER BY " + sortAttributes + " ASC;"))
                {
                    while (reader.Read())
                    {
                        OptionsListIds.Add(int.Parse(reader[idAttribute].ToString()));
                        OptionsListNames.Add(reader[nameAttribute].ToString());
                    }
                }
                conn.Close();
            }
        }

        public bool NoOptions() { return OptionsListNames.Count <= 0; }
        
        public int FindIndex(object targetData)
        {
            if (targetData == null || targetData.ToString() == "") return 0;
            int targetIndex = OptionsListIds.FindIndex(a => a.ToString() == targetData.ToString());
            return targetIndex < 0 ? 0 : targetIndex;
        }


        // The four functions below are only for tables of combo boxes
        public void AddToSelectedIds(int i) { SelectedIds.Add(OptionsListIds[i]); }
        public void RemoveFromSelectedIds() { SelectedIds.RemoveAt(SelectedIds.Count - 1); }

        public ComboBox CreateInput(int row, int col, int landingIndex)
        {
            if (OptionsListNames == null && landingIndex >= OptionsListNames.Count) return null;
            return TableBuilder.ComboBox("CB_" + SelectedIds.Count, OptionsListNames, landingIndex, row, col, UpdateSelectedIds);
        }
        private void UpdateSelectedIds(object sender, EventArgs e)
        {
            int getIdThroughName = int.Parse(((ComboBox)sender).Name.Split('_').Last());
            SelectedIds[getIdThroughName] = OptionsListIds[((ComboBox)sender).SelectedIndex];
        }
    }
}
