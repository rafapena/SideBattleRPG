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
    public partial class ToolAttributes : _ClassTemplateOperations
    {
        private ComboBoxInputData FormulaData, ElementData, ClassExclusive1Data, ClassExclusive2Data;
        private List<string> TypeOptions = new List<string> { "None", "Physical Offense", "Physical Defense", "Magic Offense", "Magic Defense", "Offense", "Defense" };
        private List<string> HPSPModOptions = new List<string> { "None", "HP", "SP", "HP Drain", "SP Drain" };
        private List<string> ScopeOptions = new List<string> {
            "None", "One Enemy", "Splash Enemies", "Row of Enemies", "Column of Enemies",
            "All Enemies", "User", "One Ally", "All Allies", "Everyone Except User", "Everyone"
        };


        public ToolAttributes()
        {
            InitializeComponent();
            ClassTemplateTable = "Tools";
            ClassTemplateType = "Tool";
        }

        protected override void SetupTableData()
        {
            FormulaData = new ComboBoxInputData("List_ID", "Name", "TypesLists", "ListType = 'Tool Formulas'", "List_ID");
            ElementData = new ComboBoxInputData("List_ID", "Name", "TypesLists", "ListType = 'Elements'", "List_ID");
            ClassExclusive1Data = new ComboBoxInputData("Class_ID", "Name", "BaseObjects JOIN Classes", "BaseObjectID = BaseObject_ID", "Name");
            ClassExclusive2Data = new ComboBoxInputData("Class_ID", "Name", "BaseObjects JOIN Classes", "BaseObjectID = BaseObject_ID", "Name");
            ClassExclusive1Data.Insert(0, -1, "None");
            ClassExclusive2Data.Insert(0, -1, "None");
            TypeInput.ItemsSource = TypeOptions;
            FormulaInput.ItemsSource = FormulaData.OptionsListNames;
            HPSPModInput.ItemsSource = HPSPModOptions;
            ClassExclusive1Input.ItemsSource = ClassExclusive1Data.OptionsListNames;
            ClassExclusive2Input.ItemsSource = ClassExclusive2Data.OptionsListNames;
            ElementInput.ItemsSource = ElementData.OptionsListNames;
            ScopeInput.ItemsSource = ScopeOptions;
        }

        protected override void OnInitializeNew()
        {
            TypeInput.SelectedIndex = 0;
            FormulaInput.SelectedIndex = 0;
            HPSPModInput.SelectedIndex = 0;
            HPAmountInput.Text = "0";
            SPAmountInput.Text = "0";
            HPPercentInput.Text = "0";
            SPPercentInput.Text = "0";
            HPRecoilInput.Text = "0";
            ClassExclusive1Input.SelectedIndex = 0;
            ClassExclusive2Input.SelectedIndex = 0;
            ElementInput.SelectedIndex = 1;
            PowerInput.Text = "10";
            AccuracyInput.Text = "100";
            CriticalRateInput.Text = "3";
            PriorityInput.Text = "0";
            ScopeInput.SelectedIndex = 1;
            ConsecutiveActsInput.Text = "1";
            RandomActsInput.Text = "0";
        }

        public override void Automate()
        {
            OnInitializeNew();
        }

        public override string ValidateInputs()
        {
            string err = "";
            if (!Utils.NumberBetween(HPAmountInput.Text, -100000, 100000)) err += "HP Amount must be an integer within -100000 and 100000\n";
            if (!Utils.NumberBetween(SPAmountInput.Text, -100000, 100000)) err += "SP Amount must be an integer within -100000 and 100000\n";
            if (!Utils.NumberBetween(HPAmountInput.Text, -100, 100)) err += "HP % must be an integer within -100 and 100\n";
            if (!Utils.NumberBetween(SPAmountInput.Text, -100, 100)) err += "SP % must be an integer within -100 and 100\n";
            if (!Utils.NumberBetween(HPRecoilInput.Text, -100, 100)) err += "HP Recoil % must be an integer within -100 and 100\n";
            if (ClassExclusive1Input.SelectedIndex == ClassExclusive2Input.SelectedIndex && ClassExclusive1Input.SelectedIndex != 0)
                err += "Class Exclusives cannot be the same\n";
            if (!Utils.PosInt(PowerInput.Text)) err += "Power must be a positive integer\n";
            if (!Utils.PosInt(AccuracyInput.Text)) err += "Accuracy % must be a positive integer\n";
            if (!Utils.PosInt(CriticalRateInput.Text)) err += "Critical Rate % must be a positive integer\n";
            if (!Utils.NumberBetween(PriorityInput.Text, -3, 3)) err += "Priority must be an integer within -3 and 3\n";
            if (!Utils.PosInt(ConsecutiveActsInput.Text)) err += "Consecutive Acts must be a positive integer\n";
            if (!Utils.PosInt(RandomActsInput.Text)) err += "Random Acts must be a positive integer\n";
            return err;
        }

        public override void ParameterizeInputs()
        {
            ParameterizeInput("@Type", TypeOptions[TypeInput.SelectedIndex]);
            ParameterizeInput("@Formula", FormulaData.OptionsListIds[FormulaInput.SelectedIndex].ToString());
            ParameterizeInput("@HPSPModType", HPSPModOptions[HPSPModInput.SelectedIndex]);
            ParameterizeInput("@HPAmount", HPAmountInput.Text);
            ParameterizeInput("@SPAmount", SPAmountInput.Text);
            ParameterizeInput("@HPPercent", HPPercentInput.Text);
            ParameterizeInput("@SPPercent", SPPercentInput.Text);
            ParameterizeInput("@HPRecoil", HPRecoilInput.Text);
            ParameterizeInput("@ClassExclusive1", ClassExclusive1Data.OptionsListIds[ClassExclusive1Input.SelectedIndex].ToString());
            ParameterizeInput("@ClassExclusive2", ClassExclusive2Data.OptionsListIds[ClassExclusive2Input.SelectedIndex].ToString());
            ParameterizeInput("@Element", ElementData.OptionsListIds[ElementInput.SelectedIndex].ToString());
            ParameterizeInput("@Power", PowerInput.Text);
            ParameterizeInput("@Accuracy", AccuracyInput.Text);
            ParameterizeInput("@CriticalRate", CriticalRateInput.Text);
            ParameterizeInput("@Priority", PriorityInput.Text);
            ParameterizeInput("@Scope", ScopeOptions[ScopeInput.SelectedIndex]);
            ParameterizeInput("@ConsecutiveActs", ConsecutiveActsInput.Text);
            ParameterizeInput("@RandomActs", RandomActsInput.Text);
        }

        protected override string[] OnCreate()
        {
            return new string[] {
                "Type, Formula, HPSPModType, HPAmount, SPAmount, HPPercent, SPPercent, HPRecoil, ClassExclusive1, ClassExclusive2, " +
                "Element, Power, Accuracy, CriticalRate, Priority, Scope, ConsecutiveActs, RandomActs",
                "@Type, @Formula, @HPSPModType, @HPAmount, @SPAmount, @HPPercent, @SPPercent, @HPRecoil, @ClassExclusive1, @ClassExclusive2, " +
                "@Element, @Power, @Accuracy, @CriticalRate, @Priority, @Scope, @ConsecutiveActs, @RandomActs"
            };
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            TypeInput.SelectedIndex = TypeOptions.FindIndex(a => a == reader["Type"].ToString());
            FormulaInput.SelectedIndex = FormulaData.OptionsListIds.FindIndex(a => a.ToString() == reader["Formula"].ToString());
            HPSPModInput.SelectedIndex = HPSPModOptions.FindIndex(a => a == reader["HPSPModType"].ToString());
            HPAmountInput.Text = reader["HPAmount"].ToString();
            SPAmountInput.Text = reader["SPAmount"].ToString();
            HPPercentInput.Text = reader["HPPercent"].ToString();
            SPPercentInput.Text = reader["SPPercent"].ToString();
            HPRecoilInput.Text = reader["HPRecoil"].ToString();
            ClassExclusive1Input.SelectedIndex = ClassExclusive1Data.OptionsListIds.FindIndex(a => a.ToString() == reader["ClassExclusive1"].ToString());
            ClassExclusive2Input.SelectedIndex = ClassExclusive2Data.OptionsListIds.FindIndex(a => a.ToString() == reader["ClassExclusive2"].ToString());
            ElementInput.SelectedIndex = ElementData.OptionsListIds.FindIndex(a => a.ToString() == reader["Element"].ToString());
            PowerInput.Text = reader["Power"].ToString();
            AccuracyInput.Text = reader["Accuracy"].ToString();
            CriticalRateInput.Text = reader["CriticalRate"].ToString();
            PriorityInput.Text = reader["Priority"].ToString();
            ScopeInput.SelectedIndex = ScopeOptions.FindIndex(a => a == reader["Scope"].ToString());
            ConsecutiveActsInput.Text = reader["ConsecutiveActs"].ToString();
            RandomActsInput.Text = reader["RandomActs"].ToString();
        }

        protected override string OnUpdate()
        {
            return "Type=@Type, Formula=@Formula, HPSPModType=@HPSPModType, " +
                "HPAmount=@HPAmount, SPAmount=@SPAmount, HPPercent=@HPPercent, SPPercent=@SPPercent, HPRecoil=@HPRecoil, " +
                "ClassExclusive1=@ClassExclusive1, ClassExclusive2=@ClassExclusive2, Element=@Element, Power=@Power, Accuracy=@Accuracy, " +
                "CriticalRate=@CriticalRate, Priority=@Priority, Scope=@Scope, ConsecutiveActs=@ConsecutiveActs, RandomActs=@RandomActs";
        }
    }
}