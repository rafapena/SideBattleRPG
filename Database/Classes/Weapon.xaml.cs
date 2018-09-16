using System.Collections.Generic;
using System.Data.SQLite;
using Database.Utilities;

namespace Database.Classes
{
    public partial class Weapon : _ClassOperations
    {
        private ComboBoxInputData WeaponTypeData;

        public Weapon()
        {
            InitializeComponent();
            LinkedTableList = ObjectList;
            LinkedFooter = FooterButtons;
            InitializeNew();
        }

        protected override void SetupTableData()
        {
            WeaponTypeData = new ComboBoxInputData("List_ID", "Name", "TypesLists", "List_Type = 'Weapon Types'", "List_ID");
            WeaponTypeInput.ItemsSource = WeaponTypeData.OptionsListNames;
        }

        protected override void OnInitializeNew()
        {
            Base.InitializeNew();
            ToolAttributes.InitializeNew();
            ToolStateRates.InitializeNew();
            EquipBoosts.InitializeNew();
            EquipBoosts.HostTableAttributeName = "EquipBoosts";
            WeaponTypeInput.SelectedIndex = 0;
            RangeInput.Text = "2";
            CollideRangeInput.IsChecked = true;
            DefaultPriceInput.Text = "100";
            DefaultQuantityInput.Text = "0";
        }

        public override string ValidateInputs()
        {
            string err = Base.ValidateInputs();
            err += ToolAttributes.ValidateInputs();
            err += ToolStateRates.ValidateInputs();
            err += EquipBoosts.ValidateInputs(0, 500);
            if (!Utils.PosInt(RangeInput.Text)) err += "Range must be a positive integer\n";
            if (!Utils.PosInt(DefaultPriceInput.Text)) err += "Default Price must be a positive integer\n";
            if (!Utils.PosInt(DefaultQuantityInput.Text)) err += "Default Quantity must be a positive integer\n";
            return err;
        }

        public override void ParameterizeAttributes()
        {
            SQLDB.ParameterizeAttribute("@BaseObjectID", Base.ClassTemplateId);
            SQLDB.ParameterizeAttribute("@ToolID", ToolAttributes.ClassTemplateId);
            SQLDB.ParameterizeAttribute("@EquipBoosts", EquipBoosts.ClassTemplateId);
            SQLDB.ParameterizeAttribute("@WeaponType", WeaponTypeData.SelectedInput(WeaponTypeInput));
            SQLDB.ParameterizeAttribute("@Range", RangeInput.Text);
            SQLDB.ParameterizeAttribute("@CollideRange", (bool)CollideRangeInput.IsChecked ? 1 : 0);
            SQLDB.ParameterizeAttribute("@DefaultPrice", DefaultPriceInput.Text);
            SQLDB.ParameterizeAttribute("@DefaultQuantity", DefaultQuantityInput.Text);
        }

        protected override void OnCreate(SQLiteConnection conn)
        {
            Base.Create(conn);
            EquipBoosts.Create(conn);
            ToolAttributes.Create(conn);
            SQLCreate(conn, "BaseObjectID, ToolID, EquipBoosts, WeaponType, Range, CollideRange, DefaultPrice, DefaultQuantity",
                "@BaseObjectID, @ToolID, @EquipBoosts, @WeaponType, @Range, @CollideRange, @DefaultPrice, @DefaultQuantity");
            ToolStateRates.Create(conn);
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read(reader);
            ToolAttributes.Read(reader);
            ToolStateRates.Read(reader);
            EquipBoosts.Read(reader);
            WeaponTypeInput.SelectedIndex = WeaponTypeData.FindIndex(reader["WeaponType"]);
            RangeInput.Text = reader["Range"].ToString();
            CollideRangeInput.IsChecked = reader["CollideRange"].ToString() == "True" ? true : false;
            DefaultPriceInput.Text = reader["DefaultPrice"].ToString();
            DefaultQuantityInput.Text = reader["DefaultQuantity"].ToString();
        }

        protected override void OnUpdate(SQLiteConnection conn)
        {
            Base.Update(conn);
            ToolAttributes.Update(conn);
            ToolStateRates.Update(conn);
            EquipBoosts.Update(conn);
            SQLUpdate(conn, "WeaponType = @WeaponType, Range = @Range, CollideRange = @CollideRange, DefaultPrice = @DefaultPrice, DefaultQuantity = @DefaultQuantity");
        }

        protected override void OnDelete(SQLiteConnection conn)
        {
            Base.Delete(conn);
            ToolAttributes.Delete(conn);
            ToolStateRates.Delete(conn);
            EquipBoosts.Delete(conn);
        }

        protected override void OnClone(SQLiteConnection conn)
        {
            Base.Clone(conn);
            ToolAttributes.Clone(conn);
            ToolStateRates.Clone(conn);
            EquipBoosts.Clone(conn);
        }
    }
}