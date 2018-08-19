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

/*
 * Tools <-- Superclass for skills, items, and weapons
 * States <-- Superclass for passive skills
*/

namespace Database.ClassTemplates
{
    public abstract class _ClassTemplateOperations : UserControl, ObjectOperations
    {
        protected string ClassTemplateTable { get; set; }
        protected string ClassTemplateType { get; set; }
        public string HostTableAttributeName { get; set; }
        public int ClassTemplateId { get; protected set; }


        protected virtual void SetupTableData() { }
        protected abstract void OnInitializeNew();
        public void InitializeNew()
        {
            HostTableAttributeName = ClassTemplateType + "ID";
            ClassTemplateId = SQLDB.GetMaxIdFromTable(ClassTemplateTable, ClassTemplateType);
            SetupTableData();
            OnInitializeNew();
        }


        public abstract void Automate();
        public abstract string ValidateInputs();
        public abstract void ParameterizeInputs();
        public void ParameterizeInput(string parameterized, string input)
        {
            SQLDB.Inputs.Add(new SQLiteParameter(parameterized, input));
        }


        protected abstract string[] OnCreate();
        public void Create()
        {
            SQLDB.Inputs = new List<SQLiteParameter>();
            ParameterizeInputs();
            string[] createText = OnCreate();
            if (createText != null)
                SQLDB.Command("INSERT INTO " + ClassTemplateTable + " (" + createText[0] + ") VALUES (" + createText[1] + ");");
            SQLDB.Inputs = null;
        }


        protected abstract void OnRead(SQLiteDataReader reader);
        public void Read(SQLiteDataReader reader)
        {
            ClassTemplateId = int.Parse(reader[HostTableAttributeName].ToString());
            Read();
        }
        public void Read()
        {
            using (var conn = SQLDB.DB())
            {
                conn.Open();
                using (var reader = SQLDB.Retrieve("SELECT * FROM " + ClassTemplateTable + " WHERE " + ClassTemplateType + "_ID = " + ClassTemplateId.ToString(), conn))
                {
                    reader.Read();
                    ClassTemplateId = reader.GetInt32(0);
                    SetupTableData();
                    OnRead(reader);
                }
                conn.Close();
            }
        }


        protected abstract string OnUpdate();
        public void Update()
        {
            SQLDB.Inputs = new List<SQLiteParameter>();
            ParameterizeInputs();
            string updateText = OnUpdate();
            if (updateText != "")
                SQLDB.Command(
                    "UPDATE " + ClassTemplateTable + " SET " + updateText + " " +
                    "WHERE " + ClassTemplateType + "_ID = " + ClassTemplateId.ToString() + ";");
            SQLDB.Inputs = null;
        }


        public void Delete()
        {
            SQLDB.Command("DELETE FROM " + ClassTemplateTable + " WHERE " + ClassTemplateType + "_ID = " + ClassTemplateId.ToString() + ";");
        }


        public void Clone()
        {
            ClassTemplateId = SQLDB.GetMaxIdFromTable(ClassTemplateTable, ClassTemplateType);
        }
    }
}
