using System;
using System.Windows.Controls;
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
        public int ClassTemplateId { get; protected set; }
        public string HostTableAttributeName { get; set; }  // The forirgn key attribute of the host table


        protected virtual void SetupTableData() { }
        protected abstract void OnInitializeNew();
        public void InitializeNew()
        {
            HostTableAttributeName = ClassTemplateTable + "ID";
            ClassTemplateId = SQLDB.MaxIdPlusOne(ClassTemplateTable);
            SetupTableData();
            OnInitializeNew();
        }

        public abstract string ValidateInputs();
        public abstract void ParameterizeAttributes();


        protected abstract string[] OnCreate(SQLiteConnection conn);
        public void Create(SQLiteConnection conn)
        {
            SQLDB.ResetParameterizedAttributes();
            ParameterizeAttributes();
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
            using (var conn = AccessDB.Connect())
            {
                conn.Open();
                using (var reader = SQLDB.Read(conn, "SELECT * FROM " + ClassTemplateTable + " WHERE " + ClassTemplateTable + "_ID = " + ClassTemplateId + ";"))
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
            SQLDB.ResetParameterizedAttributes();
            ParameterizeAttributes();
            string updateText = OnUpdate(conn);
            if (updateText == "") return;
            SQLDB.Write(conn, "UPDATE " + ClassTemplateTable + " SET " + updateText + " WHERE " + ClassTemplateTable + "_ID = " + ClassTemplateId + ";");
        }


        protected virtual void OnDelete(SQLiteConnection conn) { }
        public void Delete(SQLiteConnection conn)
        {
            SQLDB.Write(conn, "DELETE FROM " + ClassTemplateTable + " WHERE " + ClassTemplateTable + "_ID = " + ClassTemplateId + ";");
        }


        protected virtual void OnClone(SQLiteConnection conn) { }
        public void Clone(SQLiteConnection conn)
        {
            ClassTemplateId = SQLDB.MaxIdPlusOne(ClassTemplateTable);
        }
    }
}
