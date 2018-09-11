using System.Collections.Generic;
using System.Windows.Controls;
using System.Data.SQLite;
using Database.Utilities;

namespace Database.ClassTemplates
{
    public partial class PassiveEffectRates : _ClassTemplateOperations
    {
        public PassiveEffectRates()
        {
            InitializeComponent();
            ClassTemplateTable = "PassiveEffect";
        }

        protected override void SetupTableData()
        {
            StateRates.Setup("PassiveEffect", "State", "State Rates", new List<string> { "State", "%" });
            ElementRates.Setup("PassiveEffect", "TypesLists", "Elements", "Element Rates", new List<string> { "Element", "%" });
            StateRates.AttributeName = "Vulnerability";
            ElementRates.AttributeName = "ElementRates";
        }

        protected override void OnInitializeNew() { }

        public override string ValidateInputs()
        {
            string err = "";
            err += StateRates.ValidateInputs();
            err += ElementRates.ValidateInputs();
            return err;
        }

        public override void ParameterizeInputs()
        {
            StateRates.ParameterizeInputs();
            ElementRates.ParameterizeInputs();
        }

        protected override string[] OnCreate(SQLiteConnection conn)
        {
            StateRates.Create(conn);
            ElementRates.Create(conn);
            SQLDB.Write(conn, "UPDATE PassiveEffect SET ElementRates = '" + ElementRates.StringList + "' WHERE PassiveEffect_ID = " + ClassTemplateId + ";");
            return null;
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            StateRates.Read();
            ElementRates.Read();
        }

        protected override string OnUpdate(SQLiteConnection conn)
        {
            StateRates.Update(conn);
            ElementRates.Update(conn);
            return "";
        }

        protected override void OnDelete(SQLiteConnection conn)
        {
            StateRates.Delete(conn);
            ElementRates.Delete(conn);
        }

        protected override void OnClone(SQLiteConnection conn)
        {
            StateRates.Clone(conn);
            ElementRates.Clone(conn);
        }
    }
}