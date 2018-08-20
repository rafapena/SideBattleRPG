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

namespace Database.ClassTemplates
{
    public partial class ToolStates : _ClassTemplateOperations
    {
        public ToolStates()
        {
            InitializeComponent();
            ClassTemplateTable = "Tools";
            ClassTemplateType = "Tool";
        }

        protected override void SetupTableData()
        {
            List<string> cols = new List<string> { "State", "%" };
            StatesGive.Setup("Tool", "Tools", "State", "States", "Inflicts", cols);
            StatesReceive.Setup("Tool", "Tools", "State", "States", "Receives", cols);
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

        public override void ParameterizeInputs()
        {
            StatesGive.ParameterizeInputs();
            StatesReceive.ParameterizeInputs();
        }

        protected override string[] OnCreate()
        {
            StatesGive.Create();
            StatesReceive.Create();
            return null;
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            StatesGive.Read();
            StatesReceive.Read();
        }

        protected override string OnUpdate()
        {
            StatesGive.Update();
            StatesReceive.Update();
            return "";
        }
    }
}