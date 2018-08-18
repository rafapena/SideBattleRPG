using System;
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
        public List<int> SelectedIds { get; set; }
        public List<int> OptionsListIds { get; set; }
        public List<string> OptionsListNames { get; set; }

        public ComboBoxInputData(string idAttribute, string nameAttribute, string queryTables, string queryConditionAndSorter)
        {
            SelectedIds = new List<int>();
            OptionsListIds = new List<int>();
            OptionsListNames = new List<string>();
            using (var conn = SQLDB.DB())
            {
                conn.Open();
                using (var reader = SQLDB.Retrieve("SELECT * FROM " + queryTables + " WHERE " + queryConditionAndSorter + ";", conn))
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
        public void AddToSelectedIds(int i) { SelectedIds.Add(OptionsListIds[i]); }
        public void RemoveFromSelectedIds() { SelectedIds.RemoveAt(SelectedIds.Count - 1); }

        public ComboBox CreateInput(int landingIndex)
        {
            if (OptionsListNames == null && landingIndex >= OptionsListNames.Count) return null;
            return TableBuilder.ComboBox("CB_" + SelectedIds.Count, OptionsListNames, landingIndex, UpdateSelectedIds);
        }
        public ComboBox CreateInput(int row, int col, int landingIndex)
        {
            if (OptionsListNames == null && landingIndex >= OptionsListNames.Count) return null;
            return TableBuilder.ComboBox("CB_" + SelectedIds.Count, OptionsListNames, landingIndex, row, col, UpdateSelectedIds);
        }

        private void UpdateSelectedIds(object sender, EventArgs e)
        {
            int getIdThroughName = Convert.ToInt32(((ComboBox)sender).Name.Split('_').Last());
            SelectedIds[getIdThroughName] = OptionsListIds[((ComboBox)sender).SelectedIndex];
        }
    }
}
