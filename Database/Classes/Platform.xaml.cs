using System.Collections.Generic;
using System.Data.SQLite;
using Database.Utilities;

namespace Database.Classes
{
    public partial class Platform : _ClassOperations
    {
        private List<string> ForceDirectionOptions = new List<string> { "None", "Up", "Down", "Left", "Right" };

        public Platform()
        {
            InitializeComponent();
            LinkedTableList = ObjectList;
            LinkedFooter = FooterButtons;
            InitializeNew();
        }

        protected override void SetupTableData()
        {
            ForceDirectionInput.ItemsSource = ForceDirectionOptions;
        }

        protected override void OnInitializeNew()
        {
            Base.InitializeNew();
            ForceDirectionInput.SelectedIndex = 0;
            JumpDistanceInput.Text = "1";
            BounceVelocityInput.Text = "0";
            SlipperinessInput.Text = "1";
            HPLossInput.Text = "0";
        }

        public override string ValidateInputs()
        {
            string err = Base.ValidateInputs();
            if (!Utils.PosInt(JumpDistanceInput.Text)) err += "Jump Distance must be a positive integer\n";
            if (!Utils.NumberBetween(BounceVelocityInput.Text, 0, 5)) err += "Bounce Velocity must be a number within 0 to 5\n";
            if (!Utils.NumberBetween(SlipperinessInput.Text, 0, 5)) err += "Slipperiness must be a number within 0 to 5\n";
            if (!Utils.PosInt(HPLossInput.Text)) err += "HP Loss must be a positive integer\n";
            return err;
        }

        public override void ParameterizeAttributes()
        {
            SQLDB.ParameterizeAttribute("@BaseObjectID", Base.ClassTemplateId);
            SQLDB.ParameterizeAttribute("@ForceDirection", ForceDirectionOptions[ForceDirectionInput.SelectedIndex].ToString());
            SQLDB.ParameterizeAttribute("@JumpDistance", JumpDistanceInput.Text);
            SQLDB.ParameterizeAttribute("@BounceVelocity", BounceVelocityInput.Text);
            SQLDB.ParameterizeAttribute("@Slipperiness", SlipperinessInput.Text);
            SQLDB.ParameterizeAttribute("@HPLoss", HPLossInput.Text);
        }

        protected override void OnCreate(SQLiteConnection conn)
        {
            Base.Create(conn);
            SQLCreate(conn, "BaseObjectID, ForceDirection, JumpDistance, BounceVelocity, Slipperiness, HPLoss",
                "@BaseObjectID, @ForceDirection, @JumpDistance, @BounceVelocity, @Slipperiness, @HPLoss");
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read(reader);
            ForceDirectionInput.SelectedIndex = int.Parse(reader["ForceDirection"].ToString());
            JumpDistanceInput.Text = reader["JumpDistance"].ToString();
            BounceVelocityInput.Text = reader["BounceVelocity"].ToString();
            SlipperinessInput.Text = reader["Slipperiness"].ToString();
            HPLossInput.Text = reader["HPLoss"].ToString();
        }

        protected override void OnUpdate(SQLiteConnection conn)
        {
            Base.Update(conn);
            SQLUpdate(conn, "ForceDirection=@ForceDirection, JumpDistance=@JumpDistance, BounceVelocity=@BounceVelocity, Slipperiness=@Slipperiness, HPLoss=@HPLoss");
        }

        protected override void OnDelete(SQLiteConnection conn)
        {
            Base.Delete(conn);
        }

        protected override void OnClone(SQLiteConnection conn)
        {
            Base.Clone(conn);
        }
    }
}