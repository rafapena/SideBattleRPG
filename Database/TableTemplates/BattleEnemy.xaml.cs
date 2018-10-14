using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Data.SQLite;
using Database.Utilities;
using static Database.Utilities.TableBuilder;

namespace Database.TableTemplates
{
    public partial class BattleEnemy : _TableTemplateOperations
    {
        private ComboBoxInputData EnemyInput;

        public BattleEnemy()
        {
            InitializeComponent();
        }

        protected override void OnAddRow()
        {
            int curr = Count - 1;
            Elements[curr].Add(EnemyInput.CreateInput(Count, 1, 0));
            //PassiveSkills.Setup("BattleEnemy", "PassiveSkill", "Passive Skills", new List<string> { "Name" });
            //Skills.Setup("BattleEnemy", "Tool", "Skills", new List<string> { "Name", "" });
            //Items.Setup("BattleEnemy", "Tool", "Items", new List<string> { "Name", "" });
            //Weapons.Setup("BattleEnemy", "Tool", "Weapons", new List<string> { "Name", "" });
        }

        protected override void OnInitializeNew()
        {
            Title.Text = TableTitle;
            Table = TableList;
            Scroller.Height = ScrollerHeight;
            EnemyInput = new ComboBoxInputData("Enemy_ID", "Name", "BaseObject JOIN Enemy", "BaseObjectID = BaseObject_ID", "BaseObject_ID", ComboBoxInputData.ADD_NULL_INPUT);
        }

        protected override string OnValidateInputs(int i)
        {
            string err = "";
            if (Utils.CutSpaces(((TextBox)Elements[i][0]).Text) == "") err += "Inputs in " + TableTitle + " cannot be empty\n";
            return err;
        }

        protected override void OnParameterizeInputs(int i)
        {
            SQLDB.ParameterizeAttribute("@HostID" + i, HostId.ToString());
            SQLDB.ParameterizeAttribute("@TargetID" + i, EnemyInput.SelectedIds[i].ToString());
            SQLDB.ParameterizeAttribute("@TableIndex" + i, ((TextBox)Elements[i][0]).Text);
            //SQLDB.ParameterizeAttribute("@" + i, ((TextBox)Elements[i][0]).Text);
        }

        protected override string[] OnCreate()
        {
            string targetDBTable = (HostDBTable == TargetDBTable ? "Other" : "") + TargetDBTable;
            string attributes = HostDBTable + "ID, " + targetDBTable + "ID, TableIndex";
            return new string[] { HostDBTable + "_To_" + TargetDBTable, attributes };
        }
        protected override string OnCreateValues(int i)
        {
            return "@HostID" + i + ", " + "@TargetID" + i + ", " + "@TableIndex" + i;
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            //InputElements[Count - 1].Add(TextBox("TB_" + Count, reader.GetString(3), Count, 1));
            //InputElements[Count - 1].Add(TextBox("TB_" + Count, reader.GetString(4), Count, 2));
        }
    }
}