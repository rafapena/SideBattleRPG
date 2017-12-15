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
            Elements.InitializeNew("Elements");
            WeaponTypes.InitializeNew("Weapon Types");
            SkillTypes.InitializeNew("Skill Types");
        }

        public override void Automate()
        {
            Elements.Automate();
            WeaponTypes.Automate();
            SkillTypes.Automate();
        }

        public override string ValidateInputs()
        {
            string err = Elements.ValidateInputs();
            err += WeaponTypes.ValidateInputs();
            err += SkillTypes.ValidateInputs();
            return err;
        }

        public override void ParameterizeInputs()
        {
            Elements.ParameterizeInputs();
            WeaponTypes.ParameterizeInputs();
            SkillTypes.ParameterizeInputs();
        }
        
        protected override void OnCreate()
        {
            Elements.Create();
            WeaponTypes.Create();
            SkillTypes.Create();
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Elements.Read(reader);
            WeaponTypes.Read(reader);
            SkillTypes.Read(reader);
        }

        protected override void OnDelete()
        {
            Elements.Delete();
            WeaponTypes.Delete();
            SkillTypes.Delete();
        }

        protected override void OnUpdate()
        {
            Elements.Update();
            WeaponTypes.Update();
            SkillTypes.Update();
        }

        protected override void OnClone()
        {
            Elements.Clone();
            WeaponTypes.Clone();
            SkillTypes.Clone();
        }
    }
}
