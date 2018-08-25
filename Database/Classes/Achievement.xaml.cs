using System.Collections.Generic;
using System.Data.SQLite;
using Database.Utilities;

namespace Database.Classes
{
    public partial class Achievement : _ClassOperations
    {
        public Achievement()
        {
            InitializeComponent();
            LinkedTableList = ObjectList;
            LinkedFooter = FooterButtons;
            InitializeNew();
        }

        protected override void SetupTableData() { }

        protected override void OnInitializeNew()
        {
            Base.InitializeNew();
            LevelInput.Text = "1";
            HintInput.Text = "????";
        }

        public override string ValidateInputs()
        {
            string err = Base.ValidateInputs();
            if (!Utils.PosInt(LevelInput.Text)) err += "Level must be a positive integer\n";
            if (Utils.CutSpaces(HintInput.Text) == "") HintInput.Text = "N/A";
            return err;
        }

        public override void ParameterizeInputs()
        {
            SQLDB.ParameterizeInput("@Level", LevelInput.Text);
            SQLDB.ParameterizeInput("@Hint", HintInput.Text);
        }

        protected override void OnCreate(SQLiteConnection conn)
        {
            Base.Create(conn);
            SQLCreate(conn, "Level, Hint, BaseObjectID", "@Level, @Hint, " + Base.ClassTemplateId);
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read(reader);
            LevelInput.Text = reader.GetInt32(1).ToString();
            HintInput.Text = reader.GetString(2);
        }

        protected override void OnUpdate(SQLiteConnection conn)
        {
            Base.Update(conn);
            SQLUpdate(conn, "Level = @Level, Hint = @Hint");
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