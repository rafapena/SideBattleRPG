using System;
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
using Database.Utilities;
using static Database.Utilities.TableBuilder;

namespace Database.TableTemplates
{
    /// <summary>
    /// Interaction logic for GenericTable.xaml
    /// </summary>
    public partial class _GenericTable : _TableTemplateOperations
    {
        public _GenericTable()
        {
            InitializeComponent();
            TableTemplateTable = "_GenericTables";
            TableTemplateType = "_GenericTable";
        }

        public new void AddRow(object sender, RoutedEventArgs e)
        {
            if (Inputs == null) return;
            base.AddRow(sender, e);
            //InputElements[Count - 1].Add(TextBox(Inputs[0] + Count, "", Count, 1));
            AddRangeToTable();
        }

        protected override void OnInitializeNew()
        {
            Title.Text = TableTitle;
            Table = TableList;
            Scroller.Height = ScrollerHeight;
        }

        public override void Automate()
        {
            for (int i = 0; i < Count; i++)
            {
                // Insert and modify
                string text = Utils.CutSpaces(((TextBox)InputElements[i][0]).Text);
                if (text == "") ((TextBox)InputElements[i][0]).Text = (i + Count * 2).ToString();
            }
        }

        public override string ValidateInputs()
        {
            string err = "";
            for (int i = 0; i < Count; i++)
            {
                // Insert and modify
                if (Utils.CutSpaces(((TextBox)InputElements[i][0]).Text) != "") continue;
                err += "Inputs in " + TableTitle + " cannot be empty\n";
                break;
            }
            return err;
        }

        public override void ParameterizeInputs()
        {
            SQLDB.Inputs = new SQLiteParameter[Count*Inputs.Count];
            for (int i = 0; i < Count; i++)
            {
                // Insert and modify
                SQLDB.Inputs[i] = new SQLiteParameter("@attr1" + i, ((TextBox)InputElements[i][0]).Text);
            }
        }

        protected override void OnCreate()
        {
            int prevCount = SQLDB.GetScalar("SELECT COUNT(*) FROM " + TableTemplateTable);
            for (int i = prevCount; i < Count; i++)
                SQLDB.Command("INSERT INTO " + TableTemplateTable + " () VALUES () ");
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            InputElements[Count - 1].Add(TextBox(Inputs[0] + Count, reader.GetString(3), Count, 1));
        }

        protected override void OnUpdate()
        {
            int prevCount = SQLDB.GetScalar("SELECT COUNT(*) FROM " + TableTemplateTable + " WHERE " + SQLDB.CurrentClass + "ID = " + SQLDB.CurrentId);
            if (Count > prevCount) // Add undercharge
            {
                for (int i = prevCount; i < Count; i++)
                    SQLDB.Command("INSERT INTO " + TableTemplateTable + " (" + SQLDB.CurrentClass + "ID) VALUES (" + SQLDB.CurrentId + ");");
            }
            else if (Count < prevCount) // Delete overcharge
            {
                SQLDB.Command("DELETE FROM " + TableTemplateTable + " WHERE " + SQLDB.CurrentClass + "ID = " + SQLDB.CurrentId + " AND ;");
            }
            for (int i = 0; i < Count; i++) // Update rows that are still intact
                SQLDB.Command("UPDATE " + TableTemplateTable + " SET attr1 = @attr1" + i.ToString() + " WHERE " + SQLDB.CurrentClass + "ID = " + SQLDB.CurrentId + " AND ;");
        }
    }
}
