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

namespace Database.ClassTemplates
{
    public abstract class _ClassTemplateOperations : UserControl, ObjectOperations
    {
        protected string ClassTemplateTable { get; set; }
        protected string ClassTemplateType { get; set; }
        public int ClassTemplateId { get; protected set; }

        public abstract void Automate();
        public abstract string ValidateInputs();


        protected abstract void OnInitializeNew();
        public void InitializeNew()
        {
            ClassTemplateId = SQLDB.GetMaxIdFromTable(ClassTemplateTable, ClassTemplateType);
            OnInitializeNew();
        }


        protected abstract string[] OnCreate();
        public void Create()
        {
            string[] text = OnCreate();
            SQLDB.Command("INSERT INTO " + ClassTemplateTable + " (" + text[0] + ") VALUES (" + text[1] + ");");
        }


        protected abstract void OnRead(SQLiteDataReader reader);
        public void Read(SQLiteDataReader reader)
        {
            ClassTemplateId = int.Parse(reader[ClassTemplateType + "ID"].ToString());
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
                    OnRead(reader);
                }
                conn.Close();
            }
        }


        protected abstract string OnUpdate();
        public void Update()
        {
            SQLDB.Command("UPDATE " + ClassTemplateTable + " SET " + OnUpdate() + " WHERE " + ClassTemplateType + "_ID = " + ClassTemplateId.ToString() + ";");
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
