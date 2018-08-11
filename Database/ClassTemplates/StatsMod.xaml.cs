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
            bool err1 = false;
            bool err2 = false;
            if (!Utils.N3ToP3Float(HPInput.Text)) err1 = true;
            else if (!Utils.N3ToP3Float(LukInput.Text)) err1 = true;
            else if (!Utils.N3ToP3Float(AtkInput.Text)) err1 = true;
            else if (!Utils.N3ToP3Float(DefInput.Text)) err1 = true;
            else if (!Utils.N3ToP3Float(MapInput.Text)) err1 = true;
            else if (!Utils.N3ToP3Float(MarInput.Text)) err1 = true;
            else if (!Utils.N3ToP3Float(SpdInput.Text)) err1 = true;
            else if (!Utils.N3ToP3Float(TecInput.Text)) err1 = true;
            if (!Utils.N100ToP100(AccInput.Text)) err2 = true;
            else if (!Utils.N100ToP100(EvaInput.Text)) err2 = true;
            else if (!Utils.N100ToP100(CrtInput.Text)) err2 = true;
            else if (!Utils.N100ToP100(CevInput.Text)) err2 = true;
            if (err1) err += "All of the 8 stats, on top, must be within -3 and 3\n";
            if (err2) err += "All of the 4 stats, at the bottom, be an integer within -100 and 100\n";
            return err;
        }

        public override void ParameterizeInputs()
        {
            ParameterizeInput("@HP", HPInput.Text);
            ParameterizeInput("@Luk", LukInput.Text);
            ParameterizeInput("@Atk", AtkInput.Text);
            ParameterizeInput("@Def", DefInput.Text);
            ParameterizeInput("@Map", MapInput.Text);
            ParameterizeInput("@Mar", MarInput.Text);
            ParameterizeInput("@Spd", SpdInput.Text);
            ParameterizeInput("@Tec", TecInput.Text);
            ParameterizeInput("@Acc", AccInput.Text);
            ParameterizeInput("@Eva", EvaInput.Text);
            ParameterizeInput("@Crt", CrtInput.Text);
            ParameterizeInput("@Cev", CevInput.Text);
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