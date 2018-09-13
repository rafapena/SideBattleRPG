using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Data.SQLite;
using Database.Utilities;

namespace Database.ClassTemplates
{
    public partial class ToolStates : _ClassTemplateOperations
    {
        public ToolStates()
        {
            InitializeComponent();
            ClassTemplateTable = "Tool";
        }

        protected override void SetupTableData()
        {
            List<string> cols = new List<string> { "State", "%" };
            StatesGive.Setup("Tool", "State", "Inflicts", cols);
            StatesReceive.Setup("Tool", "State", "Receives", cols);
            StatesGive.AttributeName = "Chance";
            StatesReceive.AttributeName = "Chance";
            StatesGive.TableIdentifier = "_Give";
            StatesReceive.TableIdentifier = "_Receive";
        }

        protected override void OnInitializeNew() { }

        public override string ValidateInputs()
        {
            string err = "";
            err += StatesGive.ValidateInputs();
            err += StatesReceive.ValidateInputs();
            return err;
        }

        public override void ParameterizeAttributes()
        {
            StatesGive.ParameterizeAttributes();
            StatesReceive.ParameterizeAttributes();
        }

        protected override string[] OnCreate(SQLiteConnection conn)
        {
            StatesGive.Create(conn);
            StatesReceive.Create(conn);
            return null;
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            StatesGive.Read();
            StatesReceive.Read();
        }

        protected override string OnUpdate(SQLiteConnection conn)
        {
            StatesGive.Update(conn);
            StatesReceive.Update(conn);
            return "";
        }

        protected override void OnDelete(SQLiteConnection conn)
        {
            StatesGive.Delete(conn);
            StatesReceive.Delete(conn);
        }

        protected override void OnClone(SQLiteConnection conn)
        {
            StatesGive.Clone(conn);
            StatesReceive.Clone(conn);
        }
    }
}