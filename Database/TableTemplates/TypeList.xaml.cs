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
using static Database.Utilities.TableBuilder;

namespace Database.TableTemplates
{
    public partial class TypeList : _TableTemplateOperations
    {
        public TypeList()
        {
            InitializeComponent();
        }

        protected override void OnAddRow()
        {
            Elements[Count - 1].Add(TextBox("TB_" + Count, "Text", Count, 1));
        }
        
        protected override void OnInitializeNew()
        {
            Title.Text = TableTitle;
            Scroller.Height = ScrollerHeight;
            Table = TableList;
        }

        protected override string OnValidateInputs(int i)
        {
            string input = ((TextBox)Elements[i][1]).Text;
            string text = "";
            if (Utils.CutSpaces(input) == "") text += "Inputs in " + TableTitle + " cannot be empty\n";
            if (input.Length >= 20) text += "Inputs must have 20 characters or less";
            return text;
        }

        protected override void OnParameterizeInputs(int i)
        {
            ParameterizeInput("@Name" + i, ((TextBox)Elements[i][1]).Text);
        }

        protected override string[] OnCreate()
        {
            return new string[] { TargetDBTable, "List_Type, List_ID, Name" };
        }
        protected override string OnCreateValues(int i)
        {
            return "'" + TableTitle + "', " + i.ToString() + ", @Name" + i.ToString();
        }

        protected override string[] OnReadCommands()
        {
            return new string[] { TargetDBTable, "List_Type = '" + TableTitle + "' ORDER BY List_ID ASC" };
        }
        protected override void OnRead(SQLiteDataReader reader)
        {
            Elements[Count - 1].Add(TextBox("TB_" + Count, reader.GetString(2), Count, 1));
        }

        public new void Update(SQLiteConnection conn)
        {
            SQLDB.Inputs = new List<SQLiteParameter>();
            ParameterizeInputs();
            int prevCount = SQLDB.GetScalar("SELECT COUNT(*) FROM " + TargetDBTable + " WHERE List_Type = '" + TableTitle + "';");
            if (prevCount < Count)
            {
                for (int i = prevCount; i < Count; i++)
                {
                    string vals = "'" + TableTitle + "', " + i + ", @Name" + i;
                    SQLDB.Command(conn, "INSERT INTO " + TargetDBTable + " (List_Type, List_ID, Name) VALUES (" + vals + ");");
                }
            }
            else if (prevCount > Count) SQLDB.Command(conn, "DELETE FROM " + TargetDBTable + " WHERE List_ID >= " + Count + " AND List_Type = '" + TableTitle + "';");
            for (int i = 0; i < Count; i++) SQLDB.Command(conn, "UPDATE " + TargetDBTable + " SET Name = @Name" + i + " WHERE List_ID = " + i + " AND List_Type = '" + TableTitle + "';");
            SQLDB.Inputs = null;
        }

        protected override string[] OnDelete()
        {
            return new string[] { TargetDBTable, "List_Type = '" + TableTitle + "'" };
        }
    }
}