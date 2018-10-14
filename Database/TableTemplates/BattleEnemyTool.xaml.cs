using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Data.SQLite;
using Database.Utilities;
using static Database.Utilities.TableBuilder;

namespace Database.TableTemplates
{
    public partial class BattleEnemyTool: _TableTemplateOperations
    {
        private ComboBoxInputData CBInputs;

        public BattleEnemyTool()
        {
            InitializeComponent();
        }

        protected override string CheckAddability()
        {
            return CBInputs.NoOptions() ? "The table '" + TargetDBTable + "' is currently empty" : "";
        }
        protected override void OnAddRow()
        {
            Elements[Count - 1].Add(CBInputs.CreateInput(Count, 1, 0));
            //Elements[Count - 1].Add(Button("Edit", , "#999999", 1));
        }
        protected override void OnRemoveRow()
        {
            CBInputs.RemoveFromSelectedIds();
        }


        protected override void OnInitializeNew()
        {
            Title.Text = TableTitle;
            Table = TableList;
            Scroller.Height = ScrollerHeight;
            string tables = "BaseObject JOIN " + TargetDBTable;
            string dupCond = HostDBTable == TargetDBTable ? "AND " + HostDBTable + "_ID <> " + HostId : "";
            string where = "BaseObject_ID = BaseObjectID " + dupCond;
            CBInputs = new ComboBoxInputData(TargetDBTable + "_ID", "Name", tables, where, "Name");
            // ADD HERE
        }

        protected override string OnValidateInputs(int i)
        {
            string err = "";
            // ADD HERE
            for (int j = i + 1; j < Count; j++)
            {
                if (CBInputs.SelectedIds[i] != CBInputs.SelectedIds[j]) continue;
                err += "All rows in " + TableTitle + " must be unique\n";
                break;
            }
            return err;
        }
        
        protected override void OnParameterizeInputs(int i) { }
        

        protected override string[] OnCreate()
        {
            string connectorTable = HostDBTable + "_To_" + TargetDBTable + TableIdentifier;
            string targetIdName = (HostDBTable == TargetDBTable ? "Other" : "") + TargetDBTable + "ID";
            string attributes = HostDBTable + "ID, " + targetIdName + ", TableIndex";
            //attributes += ;
            return new string[] { connectorTable, attributes };
        }
        protected override string OnCreateValues(int i)
        {
            //string textAttr = ;
            //return HostId + ", " + CBInputs.SelectedIds[i] + ", " + i + textAttr;
            return "";
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            int landingIndex = CBInputs.OptionsListIds.FindIndex( a => a == int.Parse(reader[TargetDBTable + "_ID"].ToString()) );
            Elements[Count - 1].Add(CBInputs.CreateInput(Count, 1, landingIndex));
            // ADD HERE
        }
    }
}
