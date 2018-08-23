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
    public partial class DualInputDBTable : _TableTemplateOperations
    {
        private ComboBoxInputData CBInputs;
        public string AttributeName { get; set; }   // That name of the fourth attribute on a many-to-many relationship table

        public DualInputDBTable()
        {
            InitializeComponent();
        }


        // <summary>
        /// Same functions as DualInputTypesList below
        /// </summary>
        
        // Checks is a table has that extra textbox input
        public bool isDual() { return Table.ColumnDefinitions.Count == 3; }

        // Adds that extra textbox input to the table, as another column
        private void AddSecondInput(string startingText)
        {
            TextBox tb = TextBox("TB_" + Count, startingText, Count, 2);
            tb.Width = 30;
            Elements[Count - 1].Add(tb);
        }

        protected override string CheckAddability()
        {
            return CBInputs.NoOptions() ? "The table '" + TargetDBTable + "' is currently empty" : "";
        }
        protected override void OnAddRow()
        {
            Elements[Count - 1].Add(CBInputs.CreateInput(Count, 1, 0));
            if (isDual()) AddSecondInput("100");
            CBInputs.AddToSelectedIds(0);
        }
        protected override void OnRemoveRow()
        {
            CBInputs.RemoveFromSelectedIds();
        }

        /// <summary>
        /// Same functions as DualInputTypesList above
        /// </summary>


        protected override void OnInitializeNew()
        {
            Title.Text = TableTitle;
            Table = TableList;
            Scroller.Height = ScrollerHeight;
            string tables = "BaseObject JOIN " + TargetDBTable;
            string dupCond = HostDBTable == TargetDBTable ? "AND " + HostDBTable + "_ID <> " + HostId : "";
            string where = "BaseObject_ID = BaseObjectID " + dupCond;
            CBInputs = new ComboBoxInputData(TargetDBTable + "_ID", "Name", tables, where, "Name");
            AttributeName = "";
        }

        // Same as DualInputTypesList
        protected override string OnValidateInputs(int i)
        {
            string err = "";
            if (isDual() && !Utils.PosInt(((TextBox)Elements[i][2]).Text))
                err += "Input on row " + (i+1) + " for " + TableTitle + " must be a positive integer\n";
            for (int j = i + 1; j < Count; j++)
            {
                if (CBInputs.SelectedIds[i] != CBInputs.SelectedIds[j]) continue;
                err += "All rows in " + TableTitle + " must be unique\n";
                break;
            }
            return err;
        }

        // Same as DualInputTypesList
        protected override void OnParameterizeInputs(int i)
        {
            if (isDual()) SQLDB.ParameterizeInput("@" + AttributeName + i.ToString(), ((TextBox)Elements[i][2]).Text);
        }
        

        protected override string[] OnCreate()
        {
            string connectorTable = HostDBTable + "_To_" + TargetDBTable + TableIdentifier;
            string targetIdName = (HostDBTable == TargetDBTable ? "Other" : "") + TargetDBTable + "ID";
            string attributes = HostDBTable + "ID, " + targetIdName + ", TableIndex";
            if (isDual()) attributes += ", " + AttributeName;
            return new string[] { connectorTable, attributes };
        }
        protected override string OnCreateValues(int i)
        {
            string textAttr = isDual() ? ", @" + AttributeName + i : "";
            return HostId + ", " + CBInputs.SelectedIds[i] + ", " + i + textAttr;
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            int landingIndex = CBInputs.OptionsListIds.FindIndex( a => a == int.Parse(reader[TargetDBTable + "_ID"].ToString()) );
            Elements[Count - 1].Add(CBInputs.CreateInput(Count, 1, landingIndex));
            if (isDual()) AddSecondInput(reader[AttributeName].ToString());
            CBInputs.AddToSelectedIds(landingIndex);
        }
    }
}
