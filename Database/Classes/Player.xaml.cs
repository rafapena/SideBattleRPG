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
            ObjectList.SetupTable(FooterButtons);
            InitializeNew();
        }

        public override void InitializeNew()
        {
            Base.InitializeNew();
        }

        public override void Automate()
        {
            Base.Automate();
        }

        public override string ValidateInputs()
        {
            SQLDB.AddParameters(new SQLiteParameter[] { });
            string err = Base.ValidateInputs();
            //Insert Here
            return err;
        }

        protected override void OnCreate()
        {
            Base.Create();
        }

        protected override void OnDelete()
        {
            Base.Delete();
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read();
        }

        protected override void OnUpdate()
        {
            Base.Update();
        }

        protected override void OnClone()
        {
            Base.Clone();
        }
    }
}