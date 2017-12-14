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
    /// <summary>
    /// Interaction logic for Achievement.xaml
    /// </summary>
    public partial class Achievement : _ClassOperations
    {
        public Achievement()
        {
            InitializeComponent();
            LinkedTableList = ObjectList;
            LinkedFooter = FooterButtons;
            InitializeNew();
        }

        protected override void OnInitializeNew()
        {
            Base.InitializeNew();
            LevelInput.Text = "";
            HiddenMessageInput.Text = "";
        }

        public override void Automate()
        {
            Base.Automate();
            LevelInput.Text = "1";
            HiddenMessageInput.Text = "Insert Text Here";
        }

        public override string ValidateInputs()
        {
            string err = Base.ValidateInputs();
            if (!Utils.PosInt(LevelInput.Text)) err += "Level must be a positive integer\n";
            if (Utils.CutSpaces(HiddenMessageInput.Text) == "") HiddenMessageInput.Text = "N/A";
            return err;
        }

        public override void ParameterizeInputs()
        {
            SQLDB.Inputs = new SQLiteParameter[] {
                new SQLiteParameter("@Level", LevelInput.Text),
                new SQLiteParameter("@HiddenMessage", HiddenMessageInput.Text)
            };
        }

        protected override void OnCreate()
        {
            Base.Create();
            SQLCreate(new string[] { "Level, HiddenMessage, BaseObjectID", "@Level, @HiddenMessage, " + Base.ClassTemplateId });
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read(reader);
            LevelInput.Text = reader.GetInt32(1).ToString();
            HiddenMessageInput.Text = reader.GetString(2);
        }

        protected override void OnUpdate()
        {
            Base.Update();
            SQLUpdate("Level = @Level, HiddenMessage = @HiddenMessage");
        }

        protected override void OnDelete()
        {
            Base.Delete();
        }

        protected override void OnClone()
        {
            Base.Clone();
        }
    }
}