using System.Collections.Generic;
using System.Data.SQLite;
using Database.Utilities;

namespace Database.Classes
{
    public partial class Skill : _ClassOperations
    {
        private List<string> NumberOfUsersOptions = new List<string> { "1", "2", "3", "4", "5" };

        public Skill()
        {
            InitializeComponent();
            LinkedTableList = ObjectList;
            LinkedFooter = FooterButtons;
            InitializeNew();
        }

        protected override void SetupTableData()
        {
            PlayerSummons.Setup("Skill", "Player", "Player Summons", new List<string> { "Name", "%" }, 80);
            EnemySummons.Setup("Skill", "Enemy", "Enemy Summons", new List<string> { "Name", "%" }, 80);
            PlayerSummons.AttributeName = "Response";
            EnemySummons.AttributeName = "Response";
            NumberOfUsersInput.ItemsSource = NumberOfUsersOptions;
        }

        protected override void OnInitializeNew()
        {
            Base.InitializeNew();
            ToolAttributes.InitializeNew();
            ToolStateRates.InitializeNew();
            SPConsumeInput.Text = "0";
            NumberOfUsersInput.SelectedIndex = 0;
            ChargeInput.Text = "0";
            WarmUpInput.Text = "0";
            CoolDownInput.Text = "0";
            StealInput.IsChecked = false;
        }

        public override string ValidateInputs()
        {
            string err = Base.ValidateInputs();
            err += ToolAttributes.ValidateInputs();
            err += ToolStateRates.ValidateInputs();
            if (!Utils.NumberBetween(SPConsumeInput.Text, 0, 100)) err += "SP Consume must be a number between 0 and 100\n";
            if (!Utils.PosInt(ChargeInput.Text)) err += "Charge Turns must be a positive integer\n";
            if (!Utils.PosInt(WarmUpInput.Text)) err += "Warmup Turns must be a positive integer\n";
            if (!Utils.PosInt(CoolDownInput.Text)) err += "Cooldown Turns must be a positive integer\n";
            if (PlayerSummons.Count > 5) err += "The number of summoned players must be less than 6";
            if (EnemySummons.Count > 5) err += "The number of summoned enemies must be less than 6";
            return err;
        }

        public override void ParameterizeInputs()
        {
            SQLDB.ParameterizeInput("@SPConsume", SPConsumeInput.Text);
            SQLDB.ParameterizeInput("@NumberOfUsers", NumberOfUsersOptions[NumberOfUsersInput.SelectedIndex]);
            SQLDB.ParameterizeInput("@Charge", ChargeInput.Text);
            SQLDB.ParameterizeInput("@WarmUp", WarmUpInput.Text);
            SQLDB.ParameterizeInput("@CoolDown", CoolDownInput.Text);
            SQLDB.ParameterizeInput("@Steal", (bool)StealInput.IsChecked ? 1 : 0);
            SQLDB.ParameterizeInput("@ToolID", ToolAttributes.ClassTemplateId);
            SQLDB.ParameterizeInput("@BaseObjectID", Base.ClassTemplateId);
        }

        protected override void OnCreate(SQLiteConnection conn)
        {
            Base.Create(conn);
            ToolAttributes.Create(conn);
            SQLCreate(conn, "SPConsume, NumberOfUsers, Charge, WarmUp, CoolDown, Steal, ToolID, BaseObjectID",
                "@SPConsume, @NumberOfUsers, @Charge, @WarmUp, @CoolDown, @Steal, @ToolID, @BaseObjectID");
            ToolStateRates.Create(conn);
            PlayerSummons.Create(conn);
            EnemySummons.Create(conn);
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read(reader);
            ToolAttributes.Read(reader);
            ToolStateRates.Read(reader);
            PlayerSummons.Read();
            EnemySummons.Read();
            SPConsumeInput.Text = reader["SPConsume"].ToString();
            NumberOfUsersInput.SelectedIndex = NumberOfUsersOptions.FindIndex(a => a == reader["NumberOfUsers"].ToString());
            ChargeInput.Text = reader["Charge"].ToString();
            WarmUpInput.Text = reader["WarmUp"].ToString();
            CoolDownInput.Text = reader["CoolDown"].ToString();
            StealInput.IsChecked = reader["Steal"].ToString() == "True" ? true : false;
        }

        protected override void OnUpdate(SQLiteConnection conn)
        {
            Base.Update(conn);
            ToolAttributes.Update(conn);
            ToolStateRates.Update(conn);
            SQLUpdate(conn, "SPConsume = @SPConsume, NumberOfUsers = @NumberOfUsers, Charge = @Charge, WarmUp = @WarmUp, CoolDown = @CoolDown, Steal = @Steal");
            PlayerSummons.Update(conn);
            EnemySummons.Update(conn);
        }

        protected override void OnDelete(SQLiteConnection conn)
        {
            Base.Delete(conn);
            ToolAttributes.Delete(conn);
            ToolStateRates.Delete(conn);
            PlayerSummons.Delete(conn);
            EnemySummons.Delete(conn);
        }

        protected override void OnClone(SQLiteConnection conn)
        {
            Base.Clone(conn);
            ToolAttributes.Clone(conn);
            ToolStateRates.Clone(conn);
            PlayerSummons.Clone(conn);
            EnemySummons.Clone(conn);
        }
    }
}