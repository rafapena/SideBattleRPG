using System;
using System.Collections.Generic;
using System.Data.SQLite;
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

namespace Database.ClassesUnstructured
{
    public partial class OtherLists : _ClassUnstructuredOperations
    {
        public OtherLists()
        {
            InitializeComponent();
            InitializeNew();
        }
        
        public void SetupTables()
        {
            List<string> columnNames = new List<string> { "Name" };
            Elements.SetupTableData("", "TypesLists", "Elements", columnNames, 200);
            WeaponTypes.SetupTableData("", "TypesLists", "Weapon Types", columnNames, 200);
            SkillTypes.SetupTableData("", "TypesLists", "Skill Types", columnNames, 200);
            ToolFormulas.SetupTableData("", "TypesLists", "Tool Formulas", columnNames, 200);
        }

        protected override void OnInitializeNew()
        {
            SetupTables();
            Elements.InitializeNew();
            WeaponTypes.InitializeNew();
            SkillTypes.InitializeNew();
            ToolFormulas.InitializeNew();
            Elements.Read();
            WeaponTypes.Read();
            SkillTypes.Read();
            ToolFormulas.Read();
        }

        private void Automated(object sender, RoutedEventArgs e)
        {
            Automate();
        }
        public override void Automate()
        {
            Elements.Automate();
            WeaponTypes.Automate();
            SkillTypes.Automate();
            ToolFormulas.Automate();
        }

        public override string ValidateInputs()
        {
            string err = Elements.ValidateInputs();
            err += WeaponTypes.ValidateInputs();
            err += SkillTypes.ValidateInputs();
            err += ToolFormulas.ValidateInputs();
            return err;
        }

        public override void ParameterizeInputs()
        {
            Elements.ParameterizeInputs();
            WeaponTypes.ParameterizeInputs();
            SkillTypes.ParameterizeInputs();
            ToolFormulas.ParameterizeInputs();
        }
        
        protected override void OnCreate()
        {
            Elements.Create();
            WeaponTypes.Create();
            SkillTypes.Create();
            ToolFormulas.Create();
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Elements.Read();
            WeaponTypes.Read();
            SkillTypes.Read();
            ToolFormulas.Read();
        }

        protected override void OnDelete()
        {
            Elements.Delete();
            WeaponTypes.Delete();
            SkillTypes.Delete();
            ToolFormulas.Delete();
        }

        private void Updated(object sender, RoutedEventArgs e)
        {
            Update();
        }
        protected override void OnUpdate()
        {
            Elements.Update();
            WeaponTypes.Update();
            SkillTypes.Update();
            ToolFormulas.Update();
        }

        protected override void OnClone()
        {
            Elements.Clone();
            WeaponTypes.Clone();
            SkillTypes.Clone();
            ToolFormulas.Clone();
        }
    }
}
