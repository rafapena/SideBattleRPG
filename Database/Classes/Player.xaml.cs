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
    /// Interaction logic for Player.xaml
    /// </summary>
    public partial class Player : _ClassOperations
    {
        public Player()
        {
            InitializeComponent();
            LinkedTableList = ObjectList;
            LinkedFooter = FooterButtons;
            InitializeNew();
        }

        protected override void OnInitializeNew()
        {
            Base.InitializeNew();
            //Rates.InitializeNew();
        }

        public override void Automate()
        {
            Base.Automate();
            //Rates.Automate();
        }

        public override string ValidateInputs()
        {
            string err = Base.ValidateInputs();
            //err += Rates.ValidateInputs();
            return err;
        }

        public override void ParameterizeInputs()
        {
            SQLDB.Inputs = null;
        }

        protected override void OnCreate()
        {
            Base.Create();
            //Rates.Create();
            SQLCreate(new string[] { "BaseObjectID", Base.ClassTemplateId.ToString() });
            // -- RATES
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read(reader);
            //Rates.Read(reader);
        }

        protected override void OnUpdate()
        {
            Base.Update();
            //Rates.Update();
        }

        protected override void OnDelete()
        {
            Base.Delete();
            //Rates.Delete();
        }

        protected override void OnClone()
        {
            Base.Clone();
            //Rates.Clone();
        }
    }
}