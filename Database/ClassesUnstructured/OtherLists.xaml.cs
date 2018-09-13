using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;

namespace Database.ClassesUnstructured
{
    public partial class OtherLists : _ClassUnstructuredOperations
    {
        public OtherLists()
        {
            InitializeComponent();
            InitializeNew();
        }
        
        protected override void SetupTableData()
        {
            List<string> columnNames = new List<string> { "Name" };
            Elements.Setup("", "TypesLists", "Elements", columnNames, 200);
            WeaponTypes.Setup("", "TypesLists", "Weapon Types", columnNames, 200);
            ToolTypes.Setup("", "TypesLists", "Tool Types", columnNames, 200);
            ToolFormulas.Setup("", "TypesLists", "Tool Formulas", columnNames, 200);
        }

        protected override void OnInitializeNew()
        {
            Elements.Read();
            WeaponTypes.Read();
            ToolTypes.Read();
            ToolFormulas.Read();
        }

        public override string ValidateInputs()
        {
            string err = Elements.ValidateInputs();
            err += WeaponTypes.ValidateInputs();
            err += ToolTypes.ValidateInputs();
            err += ToolFormulas.ValidateInputs();
            return err;
        }

        public override void ParameterizeAttributes()
        {
            Elements.ParameterizeAttributes();
            WeaponTypes.ParameterizeAttributes();
            ToolTypes.ValidateInputs();
            ToolFormulas.ParameterizeAttributes();
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Elements.Read();
            WeaponTypes.Read();
            ToolTypes.Read();
            ToolFormulas.Read();
        }
        
        private void Updated(object sender, RoutedEventArgs e)
        {
            Update();
        }
        protected override void OnUpdate(SQLiteConnection conn)
        {
            Elements.Update(conn);
            WeaponTypes.Update(conn);
            ToolTypes.Update(conn);
            ToolFormulas.Update(conn);
        }
    }
}
