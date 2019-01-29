using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Data.SQLite;
using Database.Utilities;

namespace Database.ClassTemplates
{
    public partial class ToolAttributes : _ClassTemplateOperations
    {
        private ComboBoxInputData TypeData, FormulaData, ElementData, ClassExclusive1Data, ClassExclusive2Data;
        private List<string> HPSPModOptions = new List<string> { "None", "HP Loss", "SP Loss", "HP Gain", "SP Gain", "HP Drain", "SP Drain" };
        private List<string> ScopeOptions = new List<string> {
            "None", "One Enemy", "Splash Enemies", "Row of Enemies", "Column of Enemies",
            "All Enemies", "User", "One Ally", "All Allies", "Everyone Except User", "Everyone"
        };


        public ToolAttributes()
        {
            InitializeComponent();
            ClassTemplateTable = "Tool";
        }

        protected override void SetupTableData()
        {
            TypeData = new ComboBoxInputData("List_ID", "Name", "TypesLists", "List_Type = 'Tool Types'", "List_ID", ComboBoxInputData.ADD_NULL_INPUT);
            FormulaData = new ComboBoxInputData("List_ID", "Name", "TypesLists", "List_Type = 'Tool Formulas'", "List_ID", ComboBoxInputData.ADD_NULL_INPUT);
            ElementData = new ComboBoxInputData("List_ID", "Name", "TypesLists", "List_Type = 'Elements'", "List_ID", ComboBoxInputData.ADD_NULL_INPUT);
            ClassExclusive1Data = new ComboBoxInputData("BattlerClass_ID", "Name", "BaseObject JOIN BattlerClass", "BaseObjectID = BaseObject_ID", "Name", ComboBoxInputData.ADD_NULL_INPUT);
            ClassExclusive2Data = new ComboBoxInputData("BattlerClass_ID", "Name", "BaseObject JOIN BattlerClass", "BaseObjectID = BaseObject_ID", "Name", ComboBoxInputData.ADD_NULL_INPUT);
            TypeInput.ItemsSource = TypeData.OptionsListNames;
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

        public override string ValidateInputs()
        {
            string err = "";
            if (!Utils.PosInt(HPAmountInput.Text)) err += "HP Amount must be a positive integer\n";
            if (!Utils.PosInt(SPAmountInput.Text)) err += "SP Amount must be a positive integer\n";
            if (!Utils.PosInt(HPPercentInput.Text, 100)) err += "HP % must be an integer between 0 and 100\n";
            if (!Utils.PosInt(SPPercentInput.Text, 100)) err += "SP % must be an integer between 0 and 100\n";
            if (!Utils.PosInt(HPRecoilInput.Text, 100)) err += "HP Recoil % must be an integer between 0 and 100\n";
            if (ClassExclusive1Input.SelectedIndex == ClassExclusive2Input.SelectedIndex && ClassExclusive1Input.SelectedIndex != 0) err += "Class Exclusives cannot be the same\n";
            if (!Utils.PosInt(PowerInput.Text, 1000)) err += "Power must be an integer between 0 and 1000\n";
            if (!Utils.PosInt(AccuracyInput.Text, 1000)) err += "Accuracy % must be an integer between 0 and 1000\n";
            if (!Utils.PosInt(CriticalRateInput.Text, 1000)) err += "Critical Rate % must be an integer between 0 and 1000\n";
            if (!Utils.NumberBetween(PriorityInput.Text, -3, 3)) err += "Priority must be a number between -3 and 3\n";
            if (!Utils.PosInt(ConsecutiveActsInput.Text, 100)) err += "Consecutive Acts must be an integer between 0 and 100\n";
            if (!Utils.PosInt(RandomActsInput.Text, 10)) err += "Random Acts must be an integer between 0 and 10\n";
            return err;
        }

        public override void ParameterizeAttributes()
        {
            SQLDB.ParameterizeAttribute("@Type", TypeData.OptionsListIds[TypeInput.SelectedIndex].ToString());
            SQLDB.ParameterizeAttribute("@Formula", FormulaData.OptionsListIds[FormulaInput.SelectedIndex].ToString());
            SQLDB.ParameterizeAttribute("@HPSPModType", HPSPModInput.SelectedIndex.ToString());
            SQLDB.ParameterizeAttribute("@HPAmount", HPAmountInput.Text);
            SQLDB.ParameterizeAttribute("@SPAmount", SPAmountInput.Text);
            SQLDB.ParameterizeAttribute("@HPPercent", HPPercentInput.Text);
            SQLDB.ParameterizeAttribute("@SPPercent", SPPercentInput.Text);
            SQLDB.ParameterizeAttribute("@HPRecoil", HPRecoilInput.Text);
            SQLDB.ParameterizeAttribute("@ClassExclusive1", ClassExclusive1Data.SelectedInput(ClassExclusive1Input));
            SQLDB.ParameterizeAttribute("@ClassExclusive2", ClassExclusive1Data.SelectedInput(ClassExclusive2Input));
            SQLDB.ParameterizeAttribute("@Element", ElementData.OptionsListIds[ElementInput.SelectedIndex].ToString());
            SQLDB.ParameterizeAttribute("@Power", PowerInput.Text);
            SQLDB.ParameterizeAttribute("@Accuracy", AccuracyInput.Text);
            SQLDB.ParameterizeAttribute("@CriticalRate", CriticalRateInput.Text);
            SQLDB.ParameterizeAttribute("@Priority", PriorityInput.Text);
            SQLDB.ParameterizeAttribute("@Scope", ScopeInput.SelectedIndex.ToString());
            SQLDB.ParameterizeAttribute("@ConsecutiveActs", ConsecutiveActsInput.Text);
            SQLDB.ParameterizeAttribute("@RandomActs", RandomActsInput.Text);
        }

        protected override string[] OnCreate(SQLiteConnection conn)
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
            TypeInput.SelectedIndex = TypeData.FindIndex(reader["Type"]);
            FormulaInput.SelectedIndex = FormulaData.FindIndex(reader["Formula"]);
            HPSPModInput.SelectedIndex = int.Parse(reader["HPSPModType"].ToString());
            HPAmountInput.Text = reader["HPAmount"].ToString();
            SPAmountInput.Text = reader["SPAmount"].ToString();
            HPPercentInput.Text = reader["HPPercent"].ToString();
            SPPercentInput.Text = reader["SPPercent"].ToString();
            HPRecoilInput.Text = reader["HPRecoil"].ToString();
            ClassExclusive1Input.SelectedIndex = ClassExclusive1Data.FindIndex(reader["ClassExclusive1"]);
            ClassExclusive2Input.SelectedIndex = ClassExclusive2Data.FindIndex(reader["ClassExclusive2"]);
            ElementInput.SelectedIndex = ElementData.FindIndex(reader["Element"]);
            PowerInput.Text = reader["Power"].ToString();
            AccuracyInput.Text = reader["Accuracy"].ToString();
            CriticalRateInput.Text = reader["CriticalRate"].ToString();
            PriorityInput.Text = reader["Priority"].ToString();
            ScopeInput.SelectedIndex = int.Parse(reader["Scope"].ToString());
            ConsecutiveActsInput.Text = reader["ConsecutiveActs"].ToString();
            RandomActsInput.Text = reader["RandomActs"].ToString();
        }

        protected override string OnUpdate(SQLiteConnection conn)
        {
            return "Type=@Type, Formula=@Formula, HPSPModType=@HPSPModType, " +
                "HPAmount=@HPAmount, SPAmount=@SPAmount, HPPercent=@HPPercent, SPPercent=@SPPercent, HPRecoil=@HPRecoil, " +
                "ClassExclusive1=@ClassExclusive1, ClassExclusive2=@ClassExclusive2, Element=@Element, Power=@Power, Accuracy=@Accuracy, " +
                "CriticalRate=@CriticalRate, Priority=@Priority, Scope=@Scope, ConsecutiveActs=@ConsecutiveActs, RandomActs=@RandomActs";
        }
    }
}