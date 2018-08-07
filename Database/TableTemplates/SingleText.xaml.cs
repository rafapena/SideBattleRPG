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
    /// <summary>
    /// Interaction logic for GenericTable.xaml
    /// </summary>
    public partial class SingleText : _TableTemplateOperations
    {
        public string ListType { get; set; }

        public SingleText()
        {
            InitializeComponent();
            TableTemplateTable = "TypesLists";
        }

        public new void AddRow(object sender, RoutedEventArgs e)
        {
            if (Inputs == null) return;
            base.AddRow(sender, e);
            InputElements[Count - 1].Add(TextBox(Inputs[0] + Count, "", Count, 1));
            AddRangeToTable();
        }

        // InitializeNew() applies different conventions to TableTemplateOperations
        public override void InitializeNew()
        {
            OnInitializeNew();
            TableSetup(Table, Columns);
        }
        protected override void OnInitializeNew()
        {
            Title.Text = TableTitle;
            Scroller.Height = ScrollerHeight;
            Table = TableList;
        }

        public override void Automate()
        {
            for (int i = 0; i < Count; i++)
            {
                string text = Utils.CutSpaces(((TextBox)InputElements[i][0]).Text);
                if (text == "") ((TextBox)InputElements[i][0]).Text = (i + Count*2).ToString();
            }
        }

        public override string ValidateInputs()
        {
            string err = "";
            for (int i = 0; i < Count; i++)
            {
                if (Utils.CutSpaces(((TextBox)InputElements[i][0]).Text) != "") continue;
                err += "Inputs in " + TableTitle + " cannot be empty\n";
                break;
            }
            return err;
        }

        public override void ParameterizeInputs()
        {
            SQLDB.Inputs = new SQLiteParameter[Count*Inputs.Count];
            for (int i = 0; i < Count; i++) SQLDB.Inputs[i] = new SQLiteParameter("@Name"+i, ((TextBox)InputElements[i][0]).Text);
        }

        protected override void OnCreate()
        {   /* Does nothing: only exists to follow the abstract function protocol */ }

        // Read() applies different conventions to TableTemplateOperations
        public override void Read()
        {
            using (var conn = SQLDB.DB())
            {
                conn.Open();
                using (var reader = SQLDB.Retrieve("SELECT * FROM " + TableTemplateTable + " WHERE ListType = " + ListType + " ORDER BY SingleListID ASC", conn))
                {
                    while (reader.Read()) OnRead(reader);
                }
                conn.Close();
            }
        }
        protected override void OnRead(SQLiteDataReader reader)
        {
            if (Inputs == null) return;
            base.AddRow(null, null);
            InputElements[Count - 1].Add(TextBox(Inputs[0] + Count, reader.GetString(3), Count, 1));
            AddRangeToTable();
        }

        protected override void OnUpdate()
        {
            int prevCount = SQLDB.GetScalar("SELECT COUNT(*) FROM " + TableTemplateTable + " WHERE ListType = " + ListType);
            if (Count > prevCount) // Add undercharge
            {
                for (int i = prevCount; i < Count; i++)
                    SQLDB.Command("INSERT INTO " + TableTemplateTable + " (ListType, SingleListID, Name) VALUES (" + ListType + ", " + i.ToString() + ", @Name" + i.ToString() + ");");
            }
            else if (Count < prevCount) // Delete overcharge
            {
                SQLDB.Command("DELETE FROM " + TableTemplateTable + " WHERE ListType = " + ListType + " AND SingleListID >= " + Count.ToString());
            }
            for (int i = 0; i < Count; i++) // Update rows that are still intact
                SQLDB.Command("UPDATE " + TableTemplateTable + " SET Name = @Name" + i.ToString() + " WHERE ListType = " + ListType + " AND SingleListID = " + i.ToString() + ";");
        }
    }
}