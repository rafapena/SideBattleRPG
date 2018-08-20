﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
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

namespace Database.ClassesUnstructured
{
    public partial class OtherLists : _ClassUnstructuredOperations
    {
        public OtherLists()
        {
            InitializeComponent();
            InitializeNew();
        }
        
        public void SetupTables()
        {
            List<string> columnNames = new List<string> { "Name" };
            Elements.Setup("", "TypesLists", "Elements", columnNames, 200);
            WeaponTypes.Setup("", "TypesLists", "Weapon Types", columnNames, 200);
            ToolFormulas.Setup("", "TypesLists", "Tool Formulas", columnNames, 200);
        }

        protected override void OnInitializeNew()
        {
            SetupTables();
            Elements.InitializeNew();
            WeaponTypes.InitializeNew();
            ToolFormulas.InitializeNew();
            Elements.Read();
            WeaponTypes.Read();
            ToolFormulas.Read();
        }

        public override string ValidateInputs()
        {
            string err = Elements.ValidateInputs();
            err += WeaponTypes.ValidateInputs();
            err += ToolFormulas.ValidateInputs();
            return err;
        }

        public override void ParameterizeInputs()
        {
            Elements.ParameterizeInputs();
            WeaponTypes.ParameterizeInputs();
            ToolFormulas.ParameterizeInputs();
        }
        
        protected override void OnCreate(SQLiteConnection conn)
        {
            Elements.Create(conn);
            WeaponTypes.Create(conn);
            ToolFormulas.Create(conn);
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Elements.Read();
            WeaponTypes.Read();
            ToolFormulas.Read();
        }

        protected override void OnDelete(SQLiteConnection conn)
        {
            Elements.Delete(conn);
            WeaponTypes.Delete(conn);
            ToolFormulas.Delete(conn);
        }

        private void Updated(object sender, RoutedEventArgs e)
        {
            Update();
        }
        protected override void OnUpdate(SQLiteConnection conn)
        {
            Elements.Update(conn);
            WeaponTypes.Update(conn);
            ToolFormulas.Update(conn);
        }

        protected override void OnClone(SQLiteConnection conn)
        {
            Elements.Clone(conn);
            WeaponTypes.Clone(conn);
            ToolFormulas.Clone(conn);
        }
    }
}
