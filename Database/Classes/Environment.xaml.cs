using System.Collections.Generic;
using System.Data.SQLite;
using Database.Utilities;
using System.Windows.Media.Imaging;
using System;

namespace Database.Classes
{
    public partial class Environment : _ClassOperations
    {
        public Environment()
        {
            InitializeComponent();
            LinkedTableList = ObjectList;
            LinkedFooter = FooterButtons;
            InitializeNew();
        }

        protected override void SetupTableData() { }

        protected override void OnInitializeNew()
        {
            Base.InitializeNew();
            PassiveEffectAttributes.InitializeNew();
            PassiveEffectRates.InitializeNew();
            MapForegroundInput.Source = null;
            MapBackgroundInput.Source = null;
            BattleForegroundInput.Source = null;
            BattleBackgroundInput.Source = null;
            AccuracyInput.Text = "100";
            EvasionInput.Text = "100";
            CriticalRateInput.Text = "100";
            CritEvadeRateInput.Text = "100";
        }

        public override string ValidateInputs()
        {
            string err = Base.ValidateInputs();
            err += PassiveEffectAttributes.ValidateInputs();
            err += PassiveEffectRates.ValidateInputs();
            if (!Utils.PosInt(AccuracyInput.Text, 1000)) err += "Accuracy must be an integer between 0 to 1000\n";
            if (!Utils.PosInt(EvasionInput.Text, 1000)) err += "Evasion must be an integer between 0 to 1000\n";
            if (!Utils.PosInt(CriticalRateInput.Text, 1000)) err += "Critical Rate must be an integer between 0 and 1000\n";
            if (!Utils.PosInt(CritEvadeRateInput.Text, 1000)) err += "Crit Evade Rate must be an integer between 0 and 1000\n";
            return err;
        }

        public override void ParameterizeAttributes()
        {
            SQLDB.ParameterizeAttribute("@BaseObjectID", Base.ClassTemplateId);
            SQLDB.ParameterizeAttribute("@PassiveEffectID", PassiveEffectAttributes.ClassTemplateId);
            SQLDB.ParameterizeBlobAttribute("@MapForeground", ImageManager.ImageToBytes(MapForegroundInput.Source), (int)MapForegroundInput.Width * (int)MapForegroundInput.Height);
            SQLDB.ParameterizeBlobAttribute("@MapBackground", ImageManager.ImageToBytes(MapBackgroundInput.Source), (int)MapBackgroundInput.Width * (int)MapBackgroundInput.Height);
            SQLDB.ParameterizeBlobAttribute("@BattleForeground", ImageManager.ImageToBytes(BattleForegroundInput.Source), (int)BattleForegroundInput.Width * (int)BattleForegroundInput.Height);
            SQLDB.ParameterizeBlobAttribute("@BattleBackground", ImageManager.ImageToBytes(BattleBackgroundInput.Source), (int)BattleBackgroundInput.Width * (int)BattleBackgroundInput.Height);
            SQLDB.ParameterizeAttribute("@Accuracy", AccuracyInput.Text);
            SQLDB.ParameterizeAttribute("@Evasion", AccuracyInput.Text);
            SQLDB.ParameterizeAttribute("@CriticalRate", AccuracyInput.Text);
            SQLDB.ParameterizeAttribute("@CritEvadeRate", CritEvadeRateInput.Text);
        }

        protected override void OnCreate(SQLiteConnection conn)
        {
            Base.Create(conn);
            PassiveEffectAttributes.Create(conn);
            PassiveEffectRates.Create(conn);
            SQLCreate(conn, "BaseObjectID, PassiveEffectID, MapForeground, MapBackground, BattleForeground, BattleBackground, " +
                "Accuracy, Evasion, CriticalRate, CritEvadeRate",
                "@BaseObjectID, @PassiveEffectID, @MapForeground, @MapBackground, @BattleForeground, @BattleBackground, " +
                "@Accuracy, @Evasion, @CriticalRate, @CritEvadeRate");
        }

        protected override void OnRead(SQLiteDataReader reader)
        {
            Base.Read(reader);
            PassiveEffectAttributes.Read(reader);
            PassiveEffectRates.Read(reader);
            MapForegroundInput.Source = ImageManager.BytesToImage(ImageManager.BlobToBytes(reader, 1));
            MapBackgroundInput.Source = ImageManager.BytesToImage(ImageManager.BlobToBytes(reader, 2));
            BattleForegroundInput.Source = ImageManager.BytesToImage(ImageManager.BlobToBytes(reader, 3));
            BattleBackgroundInput.Source = ImageManager.BytesToImage(ImageManager.BlobToBytes(reader, 4));
            AccuracyInput.Text = reader["Accuracy"].ToString();
            EvasionInput.Text = reader["Evasion"].ToString();
            CriticalRateInput.Text = reader["CriticalRate"].ToString();
            CritEvadeRateInput.Text = reader["CritEvadeRate"].ToString();
        }

        protected override void OnUpdate(SQLiteConnection conn)
        {
            Base.Update(conn);
            PassiveEffectAttributes.Update(conn);
            PassiveEffectRates.Update(conn);
            SQLUpdate(conn, "BaseObjectID=@BaseObjectID, PassiveEffectID=@PassiveEffectID, " +
                "MapForeground=@MapForeground, MapBackground=@MapBackground, BattleForeground=@BattleForeground, BattleBackground=@BattleBackground, " +
                "Accuracy=@Accuracy, Evasion=@Evasion, CriticalRate=@CriticalRate, CritEvadeRate=@CritEvadeRate");
        }

        protected override void OnDelete(SQLiteConnection conn)
        {
            Base.Delete(conn);
            PassiveEffectAttributes.Delete(conn);
            PassiveEffectRates.Delete(conn);
        }

        protected override void OnClone(SQLiteConnection conn)
        {
            Base.Clone(conn);
            PassiveEffectAttributes.Clone(conn);
            PassiveEffectRates.Clone(conn);
        }

        private void SelectMapForeground(object sender, EventArgs e)
        {
            BitmapImage img = ImageManager.SelectImage((int)MapForegroundInput.Width, (int)MapForegroundInput.Height);
            if (img != null) MapForegroundInput.Source = img;
        }
        private void SelectMapBackground(object sender, EventArgs e)
        {
            BitmapImage img = ImageManager.SelectImage((int)MapBackgroundInput.Width, (int)MapBackgroundInput.Height);
            if (img != null) MapBackgroundInput.Source = img;
        }
        private void SelectBattleForeground(object sender, EventArgs e)
        {
            BitmapImage img = ImageManager.SelectImage((int)BattleForegroundInput.Width, (int)BattleForegroundInput.Height);
            if (img != null) BattleForegroundInput.Source = img;
        }
        private void SelectBattleBackground(object sender, EventArgs e)
        {
            BitmapImage img = ImageManager.SelectImage((int)BattleBackgroundInput.Width, (int)BattleBackgroundInput.Height);
            if (img != null) BattleBackgroundInput.Source = img;
        }
    }
}