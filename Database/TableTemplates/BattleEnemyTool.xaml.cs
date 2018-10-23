using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Data.SQLite;
using Database.Utilities;
using Database.TableTemplates.Helpers;
using static Database.Utilities.TableBuilder;

namespace Database.TableTemplates
{
    public partial class BattleEnemyTool : _TableTemplateOperations
    {
        public class ToolAI
        {
            public int Priority { get; set; }
            public int Quantity { get; set; }
            public int HPLow { get; set; }
            public int HPHigh { get; set; }
            public int SPLow { get; set; }
            public int SPHigh { get; set; }
            public int ActiveState1 { get; set; }
            public int ActiveState2 { get; set; }
            public int InactiveState1 { get; set; }
            public int InactiveState2 { get; set; }
            public int AllyCondition { get; set; }
            public int FoeCondition { get; set; }
            public int UserCondition { get; set; }
            public string TargetElementRate { get; set; }
            public string TargetStateRates { get; set; }
            public string TargetStatConditions { get; set; }
            public int TargetToolElement { get; set; }
        }


        private string TargetToolTable;
        private ComboBoxInputData CBInputs;
        private List<ToolAI> ToolAIs;
        public string AttributeName { get; set; }   // The name of the fourth attribute on a many-to-many relationship table
        private string BaseTableAttributeName { get; set; } // Name of the template in the base, that hosts this table table (i.e. Battle, Player, etc.)

        public BattleEnemyTool()
        {
            InitializeComponent();
        }
        
        // Same functions as DualInput tables
        protected override string CheckAddability()
        {
            return CBInputs.NoOptions() ? "The table '" + TargetToolTable + "' is currently empty" : "";
        }

        protected override void OnAddRow()
        {
            int count = Count - 1;
            Elements[count].Add(CBInputs.CreateInput(Count, 1, 0));
            Elements[count].Add(Button("Edit", EditAI, "#CCCCCC", Count, Count, 2));
            ToolAI toolAi = new ToolAI();
            toolAi.Priority = 10;
            toolAi.Quantity = 0;
            toolAi.HPLow = 0;
            toolAi.HPHigh = 100;
            toolAi.SPLow = 0;
            toolAi.SPHigh = 100;
            toolAi.ActiveState1 = 0;
            toolAi.ActiveState2 = 0;
            toolAi.InactiveState1 = 0;
            toolAi.InactiveState2 = 0;
            toolAi.AllyCondition = 0;
            toolAi.FoeCondition = 0;
            toolAi.UserCondition = 2;
            toolAi.TargetElementRate = "0_0_3";
            toolAi.TargetStateRates = "0_0_3_0_0_3";
            toolAi.TargetStatConditions = "0_0_0_0_0_0";
            toolAi.TargetToolElement = 0;
            ToolAIs.Add(toolAi);
        }
        protected override void OnRemoveRow()
        {
            CBInputs.RemoveFromSelectedIds();
            ToolAIs.RemoveAt(ToolAIs.Count - 1);
        }

        public void Setup(string baseTableAttributeName, string hostDBTable, string targetDBTable, string title, List<string> columnNames, int scrollerHeight = 100)
        {
            ToolAIs = new List<ToolAI>();
            BaseTableAttributeName = baseTableAttributeName;
            TargetToolTable = targetDBTable;
            Setup(hostDBTable, "Tool", title, columnNames, scrollerHeight);
        }

        // Helper method of Setup: Get Id of the host DB table that matches the current DB table the user is currently viewing
        protected override int GetHostId()
        {
            if (SQLDB.CurrentTable == HostDBTable) return SQLDB.CurrentId;
            int id;
            using (var conn = AccessDB.Connect())
            {
                conn.Open();
                string select = "SELECT  " + HostDBTable + "_ID FROM " + SQLDB.CurrentTable + " JOIN " + HostDBTable;
                string where = "WHERE " + HostDBTable + "_ID = " + BaseTableAttributeName + " AND " + SQLDB.CurrentTable + "_ID = " + SQLDB.CurrentId;
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
        
        protected override void OnInitializeNew()
        {
            Title.Text = TableTitle;
            Table = TableList;
            Scroller.Height = ScrollerHeight;
            string tables = "BaseObject JOIN " + TargetToolTable;
            string where = "BaseObject_ID = BaseObjectID ";
            CBInputs = new ComboBoxInputData(TargetToolTable + "_ID", "Name", tables, where, TargetToolTable + "_ID");
            AttributeName = "";
        }

        // Same as DualInputTypesList
        protected override string OnValidateInputs(int i)
        {
            for (int j = i + 1; j < Count; j++) if (CBInputs.SelectedIds[i] == CBInputs.SelectedIds[j]) return "All rows in " + TableTitle + " must be unique\n";
            return "";
        }

        // Same as DualInputTypesList
        protected override void OnParameterizeInputs(int i)
        {
            SQLDB.ParameterizeAttribute("@HostID" + i, HostId);
            SQLDB.ParameterizeAttribute("@ToolID" + i, CBInputs.SelectedIds[i]);
            SQLDB.ParameterizeAttribute("@TableIndex" + i, i.ToString());
            SQLDB.ParameterizeAttribute("@ToolTable" + i, TargetToolTable);
            SQLDB.ParameterizeAttribute("@Priority" + i, ToolAIs[i].Priority);
            SQLDB.ParameterizeAttribute("@Quantity" + i, ToolAIs[i].Quantity);
            SQLDB.ParameterizeAttribute("@HPLow" + i, ToolAIs[i].HPLow);
            SQLDB.ParameterizeAttribute("@HPHigh" + i, ToolAIs[i].HPHigh);
            SQLDB.ParameterizeAttribute("@SPLow" + i, ToolAIs[i].SPLow);
            SQLDB.ParameterizeAttribute("@SPHigh" + i, ToolAIs[i].SPHigh);
            SQLDB.ParameterizeAttribute("@ActiveState1" + i, ToolAIs[i].ActiveState1);
            SQLDB.ParameterizeAttribute("@ActiveState2" + i, ToolAIs[i].ActiveState2);
            SQLDB.ParameterizeAttribute("@InactiveState1" + i, ToolAIs[i].InactiveState1);
            SQLDB.ParameterizeAttribute("@InactiveState2" + i, ToolAIs[i].InactiveState2);
            SQLDB.ParameterizeAttribute("@AllyCondition" + i, ToolAIs[i].AllyCondition);
            SQLDB.ParameterizeAttribute("@FoeCondition" + i, ToolAIs[i].FoeCondition);
            SQLDB.ParameterizeAttribute("@UserCondition" + i, ToolAIs[i].UserCondition);
            SQLDB.ParameterizeAttribute("@TargetElementRate" + i, ToolAIs[i].TargetElementRate);
            SQLDB.ParameterizeAttribute("@TargetStateRates" + i, ToolAIs[i].TargetStateRates);
            SQLDB.ParameterizeAttribute("@TargetStatConditions" + i, ToolAIs[i].TargetStatConditions);
            SQLDB.ParameterizeAttribute("@TargetToolElement" + i, ToolAIs[i].TargetToolElement);
        }

        protected override string[] OnCreate()
        {
            string connectorTable = HostDBTable + "_To_Tool";
            string attributes = HostDBTable + "ID, ToolID, TableIndex, ToolTable, " +
                "Priority, Quantity, HPLow, HPHigh, SPLow, SPHigh, ActiveState1, ActiveState2, InactiveState1, InactiveState2, " +
                "AllyCondition, FoeCondition, UserCondition, TargetElementRate, TargetStateRates, TargetStatConditions, TargetToolElement";
            return new string[] { connectorTable, attributes };
        }
        protected override string OnCreateValues(int i)
        {
            return "@HostID" + i + ", @ToolID" + i + ", @TableIndex" + i + ", @ToolTable" + i +
                ", @Priority" + i + ", @Quantity" + i + ", @HPLow" + i + ", @HPHigh" + i + ", @SPLow" + i + ", @SPHigh" + i + ", @ActiveState1" + i + ", @ActiveState2" + i +
                ", @InactiveState2" + i + ", @InactiveState2" + i + ", @AllyCondition" + i + ", @FoeCondition" + i + ", @UserCondition" + i +
                ", @TargetElementRate" + i + ", @TargetStateRates" + i + ", @TargetStatConditions" + i + ", @TargetToolElement" + i;
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            int landingIndex = CBInputs.FindIndex(reader[TargetToolTable + "_ID"]);
            Elements[Count - 1].Add(CBInputs.CreateInput(Count, 1, landingIndex));
            Elements[Count - 1].Add(Button("Edit", EditAI, "#CCCCCC", Count, Count, 2));
            ToolAI toolAi = new ToolAI();
            toolAi.Priority = int.Parse(reader["Priority"].ToString());
            toolAi.Quantity = int.Parse(reader["Quantity"].ToString());
            toolAi.HPLow = int.Parse(reader["HPLow"].ToString());
            toolAi.HPHigh = int.Parse(reader["HPHigh"].ToString());
            toolAi.SPLow = int.Parse(reader["SPLow"].ToString());
            toolAi.SPHigh = int.Parse(reader["SPHigh"].ToString());
            toolAi.ActiveState1 = int.Parse(reader["ActiveState1"].ToString());
            toolAi.ActiveState2 = int.Parse(reader["ActiveState2"].ToString());
            toolAi.InactiveState1 = int.Parse(reader["InactiveState1"].ToString());
            toolAi.InactiveState2 = int.Parse(reader["InactiveState2"].ToString());
            toolAi.AllyCondition = int.Parse(reader["AllyCondition"].ToString());
            toolAi.FoeCondition = int.Parse(reader["FoeCondition"].ToString());
            toolAi.UserCondition = int.Parse(reader["UserCondition"].ToString());
            toolAi.TargetElementRate = reader["TargetElementRate"].ToString();
            toolAi.TargetStateRates = reader["TargetStateRates"].ToString();
            toolAi.TargetStatConditions = reader["TargetStatConditions"].ToString();
            toolAi.TargetToolElement = int.Parse(reader["TargetToolElement"].ToString());
            ToolAIs.Add(toolAi);
        }
        protected override string[] OnReadCommands()
        {
            string select = HostDBTable + "_To_Tool JOIN BaseObject JOIN " + TargetToolTable;
            string where = "BaseObject_ID = BaseObjectID AND " + TargetToolTable + "_ID = " + TargetToolTable + ".ToolID AND " + HostDBTable + "ID = " + HostId + " AND ToolTable = '" + TargetToolTable + "'";
            return new string[] { select, where + " ORDER BY TableIndex" };
        }

        private void EditAI(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            BattleEnemyToolAI toolAiPage = new BattleEnemyToolAI();
            int index = (int)btn.Tag - 1;
            btn.Background = Color("#666666");
            toolAiPage.SetInformation(ToolAIs[index]);
            toolAiPage.ShowDialog();
            if (toolAiPage.Stored) ToolAIs[index] = toolAiPage.GetInformation();
            btn.Background = Color("#CCCCCC");
        }
    }
}
