using System.Collections.Generic;
using System.Data.SQLite;
using Database.Utilities;

namespace Database.Classes
{
    public partial class Battle : _ClassOperations
    {
        public Battle()
        {
            InitializeComponent();
            LinkedTableList = ObjectList;
            LinkedFooter = FooterButtons;
            InitializeNew();
        }

        protected override void SetupTableData()
        {
            Enemies.Setup("Battle", "Enemy", "Enemies", new List<string> { "Enemy", "Position", "Mods", "Tools" }, 300);
        }

        protected override void OnInitializeNew()
        {
            Base.InitializeNew();
            Enemies.InitializeNew();
            //attr1Input.Text = "";
            //attr2Image.Source = null;
        }

        public override string ValidateInputs()
        {
            string err = Base.ValidateInputs();
            err += Enemies.ValidateInputs();
            //if (!Utils.InRequiredLength(Utils.CutSpaces(attr1Input.Text))) err += "attr1 must have 1 to 16 characters";
            //if (!Utils.PosInt(attr2Input.Text)) err += "attr2 must be a positive integer";
            //if (!Utils.NumberBetween(attr3Input.Text, 1, 100)) err += "attr3 must be a number between 1 and 100";
            return err;
        }

        public override void ParameterizeAttributes()
        {
            SQLDB.ParameterizeAttribute("@BaseObjectID", Base.ClassTemplateId);
            //SQLDB.ParameterizeAttribute("@attr1", attr1Input.Text);
            //SQLDB.ParameterizeAttribute("@attr2", attr2Input.Text);
        }

        protected override void OnCreate(SQLiteConnection conn)
        {
            Base.Create(conn);
            // Create DualInput TypeLists
            SQLCreate(conn, "BaseObjectID, attr1, attr2", "@BaseObjectID, @attr1, @attr2");
            Enemies.Create(conn);
            // Create Dual Input Classes
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read(reader);
            Enemies.Read();
            //attr1Input.Text = int.Parse(reader["IntegerAttr"].ToString());
            //attr2Input.Text = reader["StringAttr"].ToString();
        }

        protected override void OnUpdate(SQLiteConnection conn)
        {
            Base.Update(conn);
            Enemies.Update(conn);
            SQLUpdate(conn, "attr1 = @attr1, attr2 = @attr2");
        }

        protected override void OnDelete(SQLiteConnection conn)
        {
            Base.Delete(conn);
            Enemies.Delete(conn);
        }

        protected override void OnClone(SQLiteConnection conn)
        {
            Base.Clone(conn);
            Enemies.Clone(conn);
        }
    }
}