using System.Collections.Generic;
using System.Windows;
using Database.Utilities;
using static Database.TableTemplates.BattleEnemyTool;

namespace Database.TableTemplates.Helpers
{
    public partial class BattleEnemyToolAI : Window
    {
        private ComboBoxInputData ElementData;
        private ComboBoxInputData StateData;
        private List<string> StatsOptions = new List<string> { "None", "MaxHP", "Luck", "Attack", "Defense", "Magic Power", "Magic Resistance", "Speed", "Technique" };
        private List<string> ValueOptions = new List<string> { "Immune", "Very Low" , "Low", "Average", "High", "Very High" };
        private List<string> GateOptions = new List<string> { "is", "not" };
        private List<string> StatValueOptions = new List<string> { "Low-Bounded", "High-Bounded" };
        public bool Stored { get; private set; }

        public BattleEnemyToolAI()
        {
            InitializeComponent();
            AllyConditionInput.ItemsSource = new List<string> { "None", "No allies", "One ally", "All allies" };
            FoeConditionInput.ItemsSource = new List<string> { "None", "No foes", "One foe", "All foes" };
            UserConditionInput.ItemsSource = new List<string> { "None", "Cannot meet", "Must meet" };
            ElementData = new ComboBoxInputData("List_ID", "Name", "TypesLists", "List_Type = 'Elements'", "List_ID", ComboBoxInputData.ADD_NULL_INPUT);
            StateData = new ComboBoxInputData("State_ID", "Name", "BaseObject JOIN State", "BaseObjectID = BaseObject_ID", "Name", ComboBoxInputData.ADD_NULL_INPUT);
            ElementRateInput.ItemsSource = ElementData.OptionsListNames;
            State1Input.ItemsSource = StateData.OptionsListNames;
            State2Input.ItemsSource = StateData.OptionsListNames;
            Stat1Input.ItemsSource = StatsOptions;
            Stat2Input.ItemsSource = StatsOptions;
            Gate1Input.ItemsSource = GateOptions;
            Gate2Input.ItemsSource = GateOptions;
            Gate3Input.ItemsSource = GateOptions;
            Gate4Input.ItemsSource = GateOptions;
            Gate5Input.ItemsSource = GateOptions;
            Value1Input.ItemsSource = ValueOptions;
            Value2Input.ItemsSource = ValueOptions;
            Value3Input.ItemsSource = ValueOptions;
            Value4Input.ItemsSource = StatValueOptions;
            Value5Input.ItemsSource = StatValueOptions;
            ToolElementInput.ItemsSource = ElementData.OptionsListNames;
        }
        
        public void SetInformation(ToolAI selectedBETool)
        {
            PriorityInput.Text = selectedBETool.Priority.ToString();
            QuantityInput.Text = selectedBETool.Quantity.ToString();
            HPLowInput.Text = selectedBETool.HPLow.ToString();
            HPHighInput.Text = selectedBETool.HPHigh.ToString();
            SPLowInput.Text = selectedBETool.SPLow.ToString();
            SPHighInput.Text = selectedBETool.SPHigh.ToString();
            ActiveState1Input.Text = selectedBETool.ActiveState1.ToString();
            ActiveState2Input.Text = selectedBETool.ActiveState2.ToString();
            InactiveState1Input.Text = selectedBETool.InactiveState1.ToString();
            InactiveState2Input.Text = selectedBETool.InactiveState2.ToString();
            AllyConditionInput.SelectedIndex = selectedBETool.AllyCondition;
            FoeConditionInput.SelectedIndex = selectedBETool.FoeCondition;
            UserConditionInput.SelectedIndex = selectedBETool.UserCondition;
            if (selectedBETool.TargetElementRate == null) return;
            string[] elementRate = selectedBETool.TargetElementRate.Split('_');
            string[] stateRates = selectedBETool.TargetStateRates.Split('_');
            string[] statConditions = selectedBETool.TargetStatConditions.Split('_');
            ElementRateInput.SelectedIndex = int.Parse(elementRate[0]);
            Gate1Input.SelectedIndex = int.Parse(elementRate[1]);
            Value1Input.SelectedIndex = int.Parse(elementRate[2]);
            State1Input.SelectedIndex = int.Parse(stateRates[0]);
            Gate2Input.SelectedIndex = int.Parse(stateRates[1]);
            Value2Input.SelectedIndex = int.Parse(stateRates[2]);
            State2Input.SelectedIndex = int.Parse(stateRates[3]);
            Gate3Input.SelectedIndex = int.Parse(stateRates[4]);
            Value3Input.SelectedIndex = int.Parse(stateRates[5]);
            Stat1Input.SelectedIndex = int.Parse(statConditions[0]);
            Gate4Input.SelectedIndex = int.Parse(statConditions[1]);
            Value4Input.SelectedIndex = int.Parse(statConditions[2]);
            Stat2Input.SelectedIndex = int.Parse(statConditions[3]);
            Gate5Input.SelectedIndex = int.Parse(statConditions[4]);
            Value5Input.SelectedIndex = int.Parse(statConditions[5]);
            ToolElementInput.SelectedIndex = selectedBETool.TargetToolElement;
        }

        public string ValidateInputs()
        {
            string err = "";
            if (!Utils.PosInt(PriorityInput.Text)) err += "Priority must be a positive integer\n";
            if (!Utils.PosInt(QuantityInput.Text)) err += "Quantity must be a positive integer\n";
            if (!Utils.NumberBetween(HPLowInput.Text, 0, 100)) err += "HP Low must be a number between 0 and 100\n";
            if (!Utils.NumberBetween(HPHighInput.Text, 0, 100)) err += "HP High must be a number between 0 and 100\n";
            if (!Utils.NumberBetween(SPLowInput.Text, 0, 100)) err += "SP Low must be a number between 0 and 100\n";
            if (!Utils.NumberBetween(SPHighInput.Text, 0, 100)) err += "SP High must be a number between 0 and 100\n";
            if (!Utils.PosInt(ActiveState1Input.Text)) err += "Active State 1 must be a positive integer\n";
            if (!Utils.PosInt(ActiveState2Input.Text)) err += "Active State 2 must be a positive integer\n";
            if (!Utils.PosInt(InactiveState1Input.Text)) err += "Inactive State 1 must be a positive integer\n";
            if (!Utils.PosInt(InactiveState2Input.Text)) err += "Inactive State 2 must be a positive integer\n";
            if (State1Input.SelectedIndex != State2Input.SelectedIndex) err += "State Rates must be unique\n";
            if (Stat1Input.SelectedIndex != Stat2Input.SelectedIndex) err += "Stat Conditions must be unique\n";
            return err;
        }

        private void CancelClicked(object sender, RoutedEventArgs e) { CloseWindow(false); }
        private void OKClicked(object sender, RoutedEventArgs e) { CloseWindow(true); }
        private void CloseWindow(bool save)
        {
            if (save)
            {
                string err = ValidateInputs();
                if (err != "")
                {
                    MessageBox.Show("Could not confirm changes:\n\n" + err);
                    return;
                }
            }
            Stored = save;
            Close();
        }

        public ToolAI GetInformation()
        {
            ToolAI toolAi = new ToolAI();
            toolAi.Priority = int.Parse(PriorityInput.Text);
            toolAi.Quantity = int.Parse(QuantityInput.Text);
            toolAi.HPLow = int.Parse(HPLowInput.Text);
            toolAi.HPHigh = int.Parse(HPHighInput.Text);
            toolAi.SPLow = int.Parse(SPLowInput.Text);
            toolAi.SPHigh = int.Parse(SPHighInput.Text);
            toolAi.ActiveState1 = int.Parse(ActiveState1Input.Text);
            toolAi.ActiveState2 = int.Parse(ActiveState2Input.Text);
            toolAi.InactiveState1 = int.Parse(InactiveState1Input.Text);
            toolAi.InactiveState2 = int.Parse(InactiveState2Input.Text);
            toolAi.AllyCondition = AllyConditionInput.SelectedIndex;
            toolAi.FoeCondition = FoeConditionInput.SelectedIndex;
            toolAi.UserCondition = UserConditionInput.SelectedIndex;
            toolAi.TargetElementRate = ElementRateInput.SelectedIndex + "_" + Gate1Input.SelectedIndex + "_" + Value1Input.SelectedIndex;
            string state1 = State1Input.SelectedIndex + "_" + Gate2Input.SelectedIndex + "_" + Value2Input.SelectedIndex;
            string state2 = State2Input.SelectedIndex + "_" + Gate3Input.SelectedIndex + "_" + Value3Input.SelectedIndex;
            string stat1 = Stat1Input.SelectedIndex + "_" + Gate4Input.SelectedIndex + "_" + Value4Input.SelectedIndex;
            string stat2 = Stat2Input.SelectedIndex + "_" + Gate5Input.SelectedIndex + "_" + Value5Input.SelectedIndex;
            toolAi.TargetStateRates = state1 + "_" + state2;
            toolAi.TargetStatConditions = stat1 + "_" + stat2;
            toolAi.TargetToolElement = ToolElementInput.SelectedIndex;
            return toolAi;
        }
    }
}
