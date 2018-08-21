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

/*
 * Tools <-- Superclass for skills, items, and weapons
 * States <-- Superclass for passive skills
*/

namespace Database.ClassTemplates
{
    public abstract class _ClassTemplateOperations : UserControl, ObjectTemplateOperations
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
            ClassTemplateId = SQLDB.MaxIdPlusOne(ClassTemplateTable, ClassTemplateType);
            SetupTableData();
            OnInitializeNew();
        }

        public abstract string ValidateInputs();
        public abstract void ParameterizeInputs();


        protected abstract string[] OnCreate(SQLiteConnection conn);
        public void Create(SQLiteConnection conn)
        {
            SQLDB.ResetParameterizedInputs();
            ParameterizeInputs();
            string[] createText = OnCreate(conn);
            if (createText != null) SQLDB.Write(conn, "INSERT INTO " + ClassTemplateTable + " (" + createText[0] + ") VALUES (" + createText[1] + ");");
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
                using (var reader = SQLDB.Read(conn,
                    "SELECT * FROM " + ClassTemplateTable + " WHERE " + ClassTemplateType + "_ID = " + ClassTemplateId.ToString()))
                {
                    reader.Read();
                    ClassTemplateId = reader.GetInt32(0);
                    SetupTableData();
                    OnRead(reader);
                }
                conn.Close();
            }
        }


        protected abstract string OnUpdate(SQLiteConnection conn);
        public void Update(SQLiteConnection conn)
        {
            SQLDB.ResetParameterizedInputs();
            ParameterizeInputs();
            string updateText = OnUpdate(conn);
            if (updateText != "")
                SQLDB.Write(conn,
                    "UPDATE " + ClassTemplateTable + " SET " + updateText + " " +
                    "WHERE " + ClassTemplateType + "_ID = " + ClassTemplateId.ToString() + ";");
        }


        protected virtual void OnDelete(SQLiteConnection conn) { }
        public void Delete(SQLiteConnection conn)
        {
            SQLDB.Write(conn, "DELETE FROM " + ClassTemplateTable + " WHERE " + ClassTemplateType + "_ID = " + ClassTemplateId.ToString() + ";");
        }


        protected virtual void OnClone(SQLiteConnection conn) { }
        public void Clone(SQLiteConnection conn)
        {
            ClassTemplateId = SQLDB.MaxIdPlusOne(ClassTemplateTable, ClassTemplateType);
        }
    }
}
