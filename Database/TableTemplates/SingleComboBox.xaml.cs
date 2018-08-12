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
    public partial class SingleComboBox : _TableTemplateOperations
    {
        private List<string> SelectedIds { get; set; }
        private List<string> OptionsListIds { get; set; }
        private List<string> OptionsListNames { get; set; }

        public SingleComboBox()
        {
            InitializeComponent();
        }

        protected override void OnAddRow()
        {
            string name = TableTemplateTable + "_" + SelectedIds.Count;
            InputElements[Count - 1].Add(ComboBox(name, OptionsListNames, 0, Count, 1, UpdateSelectedIds));
            SelectedIds.Add(OptionsListIds[0]);
        }

        protected override void OnInitializeNew()
        {
            Title.Text = TableTitle;
            Table = TableList;
            Scroller.Height = ScrollerHeight;
            SelectedIds = new List<string>();
            OptionsListIds = getFromQuery(TargetTable, "BaseObject_ID");
            OptionsListNames = getFromQuery(TargetTable, "Name");
        }

        protected override void OnAutomate(int i) { }
        protected override string OnValidateInputs(int i)
        {
            string err = "";
            for (int j = i+1; j < Count; j++)
            {
                if (SelectedIds[i] != SelectedIds[j]) continue;
                err += "All rows in " + Title.Text + " must be unique";
                break;
            }
            return err;
        }
        protected override void OnParameterizeInputs(int i) { }


        protected override void OnRead(SQLiteDataReader reader)
        {
            int landingIndex = Convert.ToInt32( OptionsListIds.FindIndex(a => a == reader["BaseObject_ID"].ToString()) );
            string name = TableTemplateTable + "_" + SelectedIds.Count;
            InputElements[Count - 1].Add(ComboBox(name, OptionsListNames, landingIndex, Count, 1, UpdateSelectedIds));
            SelectedIds.Add(OptionsListIds[landingIndex]);
        }

        protected override string[] OnUpdateAddRow(int i)
        {
            return new string[] {
                SQLDB.CurrentClass + "ID, " + TargetClass + "ID",
                SQLDB.CurrentId.ToString() + ", " + SelectedIds[i] };
        }
        protected override string OnUpdateRemovedRowCondition()
        {
            return SQLDB.CurrentClass + "ID = " + SQLDB.CurrentId.ToString();
        }
        protected override string[] OnUpdateRow(int i)
        {
            return new string[] {
                TargetClass + "ID = " + SelectedIds[i],
                SQLDB.CurrentClass + "ID = " + SQLDB.CurrentId.ToString() + " AND " + TargetClass + "ID = " + OptionsListIds[i] };
        }


        private void UpdateSelectedIds(object sender, EventArgs e)
        {
            int getIdThroughName = Convert.ToInt32( ((ComboBox)sender).Name.Split('_').Last() );
            SelectedIds[getIdThroughName] = OptionsListIds[((ComboBox)sender).SelectedIndex];
        }
    }
}
