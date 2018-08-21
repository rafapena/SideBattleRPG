﻿using System;
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

namespace Database.Classes
{
    public partial class _GenericClass : _ClassOperations
    {
        public _GenericClass()
        {
            InitializeComponent();
            LinkedTableList = ObjectList;
            LinkedFooter = FooterButtons;
            InitializeNew();
        }

        protected override void SetupTableData()
        {
            //table1.Setup(targetType, targetDBTable, title, new List<string> {});
            //table2.Setup(targetType, targetDBTable, title, new List<string> {});
        }

        protected override void OnInitializeNew()
        {
            Base.InitializeNew();
            //attr1Input.Text = "";
            //attr2Image.Source = null;
        }

        public override string ValidateInputs()
        {
            string err = Base.ValidateInputs();
            //if (!Utils.InRequiredLength(Utils.CutSpaces(attr1))) err += "attr1 needs to have 1 to 16 characters";
            return err;
        }

        public override void ParameterizeInputs()
        {
            //SQLDB.ParamterizeInput("@attr1", attr1Input.Text);
            //SQLDB.ParamterizeInput("@attr2", attr2Input.Text);
        }

        protected override void OnCreate(SQLiteConnection conn)
        {
            Base.Create(conn);
            // Create DualInput TypeLists
            SQLCreate(conn, "attr1, attr2, BaseObjectID", "@attr1, @attr2, " + Base.ClassTemplateId.ToString());
            // Create Dual Input Classes
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read(reader);
            //attr1Input.Text = reader.GetInt32(N).ToString();
            //attr2Input.Text = reader.GetString(N);
        }

        protected override void OnUpdate(SQLiteConnection conn)
        {
            Base.Update(conn);
            SQLUpdate(conn, "attr1 = @attr1, attr2 = @attr2");
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