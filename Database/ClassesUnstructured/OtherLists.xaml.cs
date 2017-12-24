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
    /// <summary>
    /// Interaction logic for OtherLists.xaml
    /// </summary>
    public partial class OtherLists : _ClassUnstructuredOperations
    {
        public OtherLists()
        {
            InitializeComponent();
            InitializeNew();
        }

        protected override void OnInitializeNew()
        {
            List<string> cols = new List<string> { "Name" };
            List<string> inputs = new List<string> { "Name" };
            Elements.InitializeNew("Elements", cols, inputs, 200);
            WeaponTypes.InitializeNew("Weapon Types", cols, inputs, 200);
            SkillTypes.InitializeNew("Skill Types", cols, inputs, 150);
            ToolFormulas.InitializeNew("Tool Formulas", cols, inputs, 150);
            Elements.ListType = "0";
            WeaponTypes.ListType = "1";
            SkillTypes.ListType = "2";
            ToolFormulas.ListType = "3";
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
            Elements.Read(reader);
            WeaponTypes.Read(reader);
            SkillTypes.Read(reader);
            ToolFormulas.Read(reader);
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
