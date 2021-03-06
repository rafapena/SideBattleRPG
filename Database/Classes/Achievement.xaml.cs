﻿using System.Collections.Generic;
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
            if (!Utils.PosInt(LevelInput.Text, 10)) err += "Level must be an integer between 0 and 10\n";
            if (Utils.CutSpaces(HintInput.Text) == "") HintInput.Text = "N/A";
            return err;
        }

        public override void ParameterizeAttributes()
        {
            SQLDB.ParameterizeAttribute("@BaseObjectID", Base.ClassTemplateId);
            SQLDB.ParameterizeAttribute("@Level", LevelInput.Text);
            SQLDB.ParameterizeAttribute("@Hint", HintInput.Text);
        }

        protected override void OnCreate(SQLiteConnection conn)
        {
            Base.Create(conn);
            SQLCreate(conn, "BaseObjectID, Level, Hint", "@BaseObjectID, @Level, @Hint");
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read(reader);
            LevelInput.Text = reader["Level"].ToString();
            HintInput.Text = reader["Hint"].ToString();
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