using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Data.SQLite;
using Database.Utilities;

namespace Database.ClassTemplates
{
    public partial class Stats : _ClassTemplateOperations
    {
        private double Initial, Low, High;

        public Stats()
        {
            InitializeComponent();
            ClassTemplateTable = "Stats";
            Initial = 0;
            Low = -1;
            High = 1;
        }

        public void InitializeNew(int initial)
        {
            Initial = initial;
            InitializeNew();
        }
        protected override void OnInitializeNew()
        {
            string initial = Initial.ToString();
            HPInput.Text = initial;
            LukInput.Text = initial;
            AtkInput.Text = initial;
            DefInput.Text = initial;
            MapInput.Text = initial;
            MarInput.Text = initial;
            SpdInput.Text = initial;
            TecInput.Text = initial;
            AccInput.Text = "100";
            EvaInput.Text = "100";
            CrtInput.Text = "100";
            CevInput.Text = "100";
        }

        public string ValidateInputs(double low, double high)
        {
            Low = low;
            High = high;
            return ValidateInputs();
        }
        public override string ValidateInputs()
        {
            string err = "";
            bool err1 = false;
            bool err2 = false;
            if (!Utils.NumberBetween(HPInput.Text, Low, High)) err1 = true;
            else if (!Utils.NumberBetween(LukInput.Text, Low, High)) err1 = true;
            else if (!Utils.NumberBetween(AtkInput.Text, Low, High)) err1 = true;
            else if (!Utils.NumberBetween(DefInput.Text, Low, High)) err1 = true;
            else if (!Utils.NumberBetween(MapInput.Text, Low, High)) err1 = true;
            else if (!Utils.NumberBetween(MarInput.Text, Low, High)) err1 = true;
            else if (!Utils.NumberBetween(SpdInput.Text, Low, High)) err1 = true;
            else if (!Utils.NumberBetween(TecInput.Text, Low, High)) err1 = true;
            if (!Utils.NumberBetween(AccInput.Text, -100, 100)) err2 = true;
            else if (!Utils.NumberBetween(EvaInput.Text, -100, 100)) err2 = true;
            else if (!Utils.NumberBetween(CrtInput.Text, -100, 100)) err2 = true;
            else if (!Utils.NumberBetween(CevInput.Text, -100, 100)) err2 = true;
            if (err1) err += "All of the 8 stats, on top, must be within " + Low + " and " + High + "\n";
            if (err2) err += "All of the 4 stats, at the bottom, be an integer within -100 and 100\n";
            return err;
        }

        public override void ParameterizeInputs()
        {
            SQLDB.ParameterizeInput("@HP", HPInput.Text);
            SQLDB.ParameterizeInput("@Luk", LukInput.Text);
            SQLDB.ParameterizeInput("@Atk", AtkInput.Text);
            SQLDB.ParameterizeInput("@Def", DefInput.Text);
            SQLDB.ParameterizeInput("@Map", MapInput.Text);
            SQLDB.ParameterizeInput("@Mar", MarInput.Text);
            SQLDB.ParameterizeInput("@Spd", SpdInput.Text);
            SQLDB.ParameterizeInput("@Tec", TecInput.Text);
            SQLDB.ParameterizeInput("@Acc", AccInput.Text);
            SQLDB.ParameterizeInput("@Eva", EvaInput.Text);
            SQLDB.ParameterizeInput("@Crt", CrtInput.Text);
            SQLDB.ParameterizeInput("@Cev", CevInput.Text);
        }

        protected override string[] OnCreate(SQLiteConnection conn)
        {
            return new string[] {
                "HP, Atk, Def, Map, Mar, Spd, Tec, Luk, Acc, Eva, Crt, Cev",
                "@HP, @Atk, @Def, @Map, @Mar, @Spd, @Tec, @Luk, @Acc, @Eva, @Crt, @Cev"
            };
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            HPInput.Text = reader["HP"].ToString();
            AtkInput.Text = reader["Atk"].ToString();
            DefInput.Text = reader["Def"].ToString();
            MapInput.Text = reader["Map"].ToString();
            MarInput.Text = reader["Mar"].ToString();
            SpdInput.Text = reader["Spd"].ToString();
            TecInput.Text = reader["Tec"].ToString();
            LukInput.Text = reader["Luk"].ToString();
            AccInput.Text = reader["Acc"].ToString();
            EvaInput.Text = reader["Eva"].ToString();
            CrtInput.Text = reader["Crt"].ToString();
            CevInput.Text = reader["Cev"].ToString();
        }

        protected override string OnUpdate(SQLiteConnection conn)
        {
            return "HP=@HP, Atk=@Atk, Def=@Def, Map=@Map, Mar=@Mar, Spd=@Spd, Tec=@Tec, Luk=@Luk, Acc=@Acc, Eva=@Eva, Crt=@Crt, Cev=@Cev";
        }
    }
}