using System.Collections.Generic;
using System.Data.SQLite;
using Database.Utilities;

namespace Database.Classes
{
    public partial class Item : _ClassOperations
    {
        private ComboBoxInputData TurnsIntoData;

        public Item()
        {
            InitializeComponent();
            LinkedTableList = ObjectList;
            LinkedFooter = FooterButtons;
            InitializeNew();
        }

        protected override void SetupTableData()
        {
            TurnsIntoData = new ComboBoxInputData("Item_ID", "Name", "BaseObject JOIN Item", "BaseObjectID = BaseObject_ID", "Name", ComboBoxInputData.ADD_NULL_INPUT);
            TurnsIntoInput.ItemsSource = TurnsIntoData.OptionsListNames;
        }

        protected override void OnInitializeNew()
        {
            Base.InitializeNew();
            ToolAttributes.InitializeNew();
            ToolStateRates.InitializeNew();
            PermanentStatMods.InitializeNew();
            PermanentStatMods.HostTableAttributeName = "PermStatMods";
            DefaultPriceInput.Text = "100";
            ConsumableInput.IsChecked = true;
            TurnsIntoInput.SelectedIndex = 0;
        }

        public override string ValidateInputs()
        {
            string err = Base.ValidateInputs();
            err += ToolAttributes.ValidateInputs();
            err += ToolStateRates.ValidateInputs();
            err += PermanentStatMods.ValidateInputs(0, 100);
            if (!Utils.PosInt(DefaultPriceInput.Text)) err += "Default price must be a positive integer\n";
            return err;
        }

        public override void ParameterizeAttributes()
        {
            SQLDB.ParameterizeAttribute("@BaseObjectID", Base.ClassTemplateId);
            SQLDB.ParameterizeAttribute("@ToolID", ToolAttributes.ClassTemplateId);
            SQLDB.ParameterizeAttribute("@PermStatMods", PermanentStatMods.ClassTemplateId);
            SQLDB.ParameterizeAttribute("@DefaultPrice", DefaultPriceInput.Text);
            SQLDB.ParameterizeAttribute("@Consumable", (bool)ConsumableInput.IsChecked ? 1 : 0);
            SQLDB.ParameterizeAttribute("@TurnsInto", TurnsIntoData.SelectedInput(TurnsIntoInput));
        }

        protected override void OnCreate(SQLiteConnection conn)
        {
            Base.Create(conn);
            PermanentStatMods.Create(conn);
            ToolAttributes.Create(conn);
            SQLCreate(conn, "BaseObjectID, ToolID, PermStatMods, DefaultPrice, Consumable, TurnsInto",
                "@BaseObjectID, @ToolID, @PermStatMods, @DefaultPrice, @Consumable, @TurnsInto");
            ToolStateRates.Create(conn);
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read(reader);
            ToolAttributes.Read(reader);
            ToolStateRates.Read(reader);
            PermanentStatMods.Read(reader);
            DefaultPriceInput.Text = reader["DefaultPrice"].ToString();
            ConsumableInput.IsChecked = reader["Consumable"].ToString() == "True" ? true : false;
            TurnsIntoInput.SelectedIndex = TurnsIntoData.FindIndex(reader["TurnsInto"]);
        }

        protected override void OnUpdate(SQLiteConnection conn)
        {
            Base.Update(conn);
            ToolAttributes.Update(conn);
            ToolStateRates.Update(conn);
            PermanentStatMods.Update(conn);
            SQLUpdate(conn, "DefaultPrice = @DefaultPrice, Consumable = @Consumable, TurnsInto = @TurnsInto");
        }

        protected override void OnDelete(SQLiteConnection conn)
        {
            Base.Delete(conn);
            ToolAttributes.Delete(conn);
            ToolStateRates.Delete(conn);
            PermanentStatMods.Delete(conn);
        }

        protected override void OnClone(SQLiteConnection conn)
        {
            Base.Clone(conn);
            ToolAttributes.Clone(conn);
            ToolStateRates.Clone(conn);
            PermanentStatMods.Clone(conn);
        }
    }
}