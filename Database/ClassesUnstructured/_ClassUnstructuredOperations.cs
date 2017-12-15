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
using Database.BaseControls;
using Database.Utilities;

/*
 * OtherLists
 * Skill/Tool Animations
 * Skill/Tools Animation Combos
 * State Animations
 * Event Control Flow
*/

namespace Database.ClassesUnstructured
{
    public abstract class _ClassUnstructuredOperations : Page, ObjectOperations
    {
        protected abstract void OnInitializeNew();
        public void InitializeNew()
        {
            //SQLDB.CurrentId = SQLDB.GetMaxIdFromTable(SQLDB.CurrentTable, SQLDB.CurrentClass);
            OnInitializeNew();
        }


        public abstract void Automate();
        public abstract string ValidateInputs();
        public abstract void ParameterizeInputs();


        protected abstract void OnCreate();
        public void Create()
        {
            string err = ValidateInputs();
            if (err != "") MessageBox.Show("Could not update due to the following:\n\n" + err);
            else
            {
                OnCreate();
                MessageBox.Show("Creating successful");
            }
        }
        protected void SQLCreate(string[] text)
        {
            ParameterizeInputs();
            SQLDB.Command("INSERT INTO " + SQLDB.CurrentTable + " (" + text[0] + ") VALUES (" + text[1] + ");");
            SQLDB.Inputs = null;
        }


        protected abstract void OnRead(SQLiteDataReader reader);
        public void Read()
        {
            using (var conn = SQLDB.DB())
            {
                conn.Open();
                using (var reader = SQLDB.Retrieve("SELECT * FROM " + SQLDB.CurrentTable + " WHERE " + SQLDB.CurrentClass + "_ID = " + SQLDB.CurrentId.ToString(), conn))
                {
                    reader.Read();
                    OnRead(reader);
                }
                conn.Close();
            }
        }


        protected abstract void OnUpdate();
        public void Update()
        {
            string err = ValidateInputs();
            if (err != "") MessageBox.Show("Could not update due to the following:\n\n" + err);
            else
            {
                OnUpdate();
                MessageBox.Show("Updating successful");
                SQLDB.Inputs = null;
            }
        }
        protected void SQLUpdate(string input)
        {
            ParameterizeInputs();
            SQLDB.Command("UPDATE " + SQLDB.CurrentTable + " SET " + input + " WHERE " + SQLDB.CurrentClass + "_ID = " + SQLDB.CurrentId.ToString() + ";");
            SQLDB.Inputs = null;
        }


        protected abstract void OnDelete();
        public void Delete()
        {
            if (!Utils.Confirm("Are you sure?", "Deleting " + SQLDB.CurrentClass)) return;
            OnDelete();
            MessageBox.Show("Deleting successful");
            InitializeNew();
        }


        protected abstract void OnClone();
        public void Clone()
        {
            if (!Utils.Confirm("Are you sure?\nAny un-updated changes will be discarded", "Cloning over to new " + SQLDB.CurrentClass)) return;
            SQLDB.CurrentId = SQLDB.GetMaxIdFromTable(SQLDB.CurrentTable, SQLDB.CurrentClass);
            OnClone();
            MessageBox.Show("The contents have been cloned to a new\n" + SQLDB.CurrentClass + " and can now be created");
        }
    }
}
