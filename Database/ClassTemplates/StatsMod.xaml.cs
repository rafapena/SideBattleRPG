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
    /// <summary>
    /// Interaction logic for _StatsMod.xaml
    /// </summary>
    public partial class StatsMod : _ClassTemplateOperations
    {
        public StatsMod()
        {
            InitializeComponent();
            ClassTemplateTable = "Stats";
            ClassTemplateType = "Stats";
        }

        protected override void OnInitializeNew()
        {
            HPInput.Text = "";
            LukInput.Text = "";
            AtkInput.Text = "";
            DefInput.Text = "";
            MapInput.Text = "";
            MarInput.Text = "";
            SpdInput.Text = "";
            TecInput.Text = "";
            AccInput.Text = "";
            EvaInput.Text = "";
            CrtInput.Text = "";
            CevInput.Text = "";
        }

        public override void Automate()
        {
            HPInput.Text = "0";
            LukInput.Text = "0";
            AtkInput.Text = "0";
            DefInput.Text = "0";
            MapInput.Text = "0";
            MarInput.Text = "0";
            SpdInput.Text = "0";
            TecInput.Text = "0";
            AccInput.Text = "100";
            EvaInput.Text = "100";
            CrtInput.Text = "100";
            CevInput.Text = "100";
        }

        public override string ValidateInputs()
        {
            string err = "";
            string message1 = " inputs must be between -3 and 3, inclusively\n";
            string message2 = " inputs must be an integer between -100 and 100, inclusively\n";
            if (!Utils.N3ToP3Float(HPInput.Text)) err += "MaxHP" + message1;
            if (!Utils.N3ToP3Float(LukInput.Text)) err += "Luck" + message1;
            if (!Utils.N3ToP3Float(AtkInput.Text)) err += "Attack" + message1;
            if (!Utils.N3ToP3Float(DefInput.Text)) err += "Defense" + message1;
            if (!Utils.N3ToP3Float(MapInput.Text)) err += "Magic Power" + message1;
            if (!Utils.N3ToP3Float(MarInput.Text)) err += "Magic Resistance" + message1;
            if (!Utils.N3ToP3Float(SpdInput.Text)) err += "Speed" + message1;
            if (!Utils.N3ToP3Float(TecInput.Text)) err += "Technique" + message1;
            if (!Utils.N100ToP100(AccInput.Text)) err += "Accuracy" + message2;
            if (!Utils.N100ToP100(EvaInput.Text)) err += "Evasion" + message2;
            if (!Utils.N100ToP100(CrtInput.Text)) err += "Critical Rate" + message2;
            if (!Utils.N100ToP100(CevInput.Text)) err += "Crit Evade Rate" + message2;
            return err;
        }

        public override void ParameterizeInputs()
        {
            SQLDB.Inputs = new SQLiteParameter[] {
                new SQLiteParameter("@HP", HPInput.Text),
                new SQLiteParameter("@Luk", LukInput.Text),
                new SQLiteParameter("@Atk", AtkInput.Text),
                new SQLiteParameter("@Def", DefInput.Text),
                new SQLiteParameter("@Map", MapInput.Text),
                new SQLiteParameter("@Mar", MarInput.Text),
                new SQLiteParameter("@Spd", SpdInput.Text),
                new SQLiteParameter("@Tec", TecInput.Text),
                new SQLiteParameter("@Acc", AccInput.Text),
                new SQLiteParameter("@Eva", EvaInput.Text),
                new SQLiteParameter("@Crt", CrtInput.Text),
                new SQLiteParameter("@Cev", CevInput.Text)
            };
        }

        protected override string[] OnCreate()
        {
            return new string[] {
                "HP, Atk, Def, Map, Mar, Spd, Tec, Luk, Acc, Eva, Crt, Cev",
                "@HP, @Atk, @Def, @Map, @Mar, @Spd, @Tec, @Luk, @Acc, @Eva, @Crt, @Cev" };
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

        protected override string OnUpdate()
        {
            return "HP=@HP, Atk=@Atk, Def=@Def, Map=@Map, Mar=@Mar, Spd=@Spd, Tec=@Tec, Luk=@Luk, Acc=@Acc, Eva=@Eva, Crt=@Crt, Cev=@Cev";
        }
    }
}