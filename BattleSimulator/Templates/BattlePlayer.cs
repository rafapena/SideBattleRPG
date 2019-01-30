using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BattleSimulator.Utilities.FileHelper;
using BattleSimulator.Utilities;
using Database.Utilities;
using System.IO;

namespace BattleSimulator.Templates
{
    public partial class BattlePlayer : UserControl
    {
        private ComboBoxInputData PlayerData, BattlerClassData, ItemData, WeaponData, PassiveSkillData;
        private string[] ZFormationOptions = new string[] { "Front", "Center", "Back" };
        private string[] XFormationOptions = new string[] { "Left", "Center", "Right" };

        public BattlePlayer()
        {
            InitializeComponent();
        }

        public string Position()
        {
            return ZFormation.SelectedIndex + "" + XFormation.SelectedIndex;
        }
        public bool InSamePositionAs(BattlePlayer other)
        {
            return Position() == other.Position();
        }

        public void SetupData()
        {
            PlayerData = new ComboBoxInputData("Player_ID", "Name", "BaseObject JOIN Player", "BaseObjectID = BaseObject_ID", "Player_ID");
            ItemData = new ComboBoxInputData("Item_ID", "Name", "BaseObject JOIN Item", "BaseObjectID = BaseObject_ID", "Name", ComboBoxInputData.ADD_NULL_INPUT);
            WeaponData = new ComboBoxInputData("Weapon_ID", "Name", "BaseObject JOIN Weapon", "BaseObjectID = BaseObject_ID", "Name", ComboBoxInputData.ADD_NULL_INPUT);
            PassiveSkillData = new ComboBoxInputData("PassiveSkill_ID", "Name", "BaseObject JOIN PassiveSkill", "BaseObjectID = BaseObject_ID", "Name", ComboBoxInputData.ADD_NULL_INPUT);
            Player.Items.AddRange(PlayerData.OptionsListNames.ToArray());
            ZFormation.Items.AddRange(ZFormationOptions);
            XFormation.Items.AddRange(XFormationOptions);
            string[] items = ItemData.OptionsListNames.ToArray();
            string[] weapons = WeaponData.OptionsListNames.ToArray();
            string[] passiveSkills = PassiveSkillData.OptionsListNames.ToArray();
            Item1.Items.AddRange(items);
            Item2.Items.AddRange(items);
            Item3.Items.AddRange(items);
            Item4.Items.AddRange(items);
            Weapon1.Items.AddRange(weapons);
            Weapon2.Items.AddRange(weapons);
            Weapon3.Items.AddRange(weapons);
            PassiveSkill1.Items.AddRange(passiveSkills);
            PassiveSkill2.Items.AddRange(passiveSkills);
            Player.SelectedIndexChanged += new EventHandler(CBChangedPlayer);
        }

        private void CBChangedPlayer(object sender, EventArgs e)
        {
            int playerId = PlayerData.OptionsListIds[Player.SelectedIndex];
            using (var conn = Database.AccessDB.Connect())
            {
                conn.Open();
                using (var reader = SQLDB.Read(conn, "SELECT Image FROM Player JOIN BaseObject WHERE BaseObject_ID = BaseObjectID AND Player_ID = " + playerId + ";"))
                {
                    reader.Read();
                    try { PlayerImage.Image = Conversion.BytesToImage(reader, 0); }
                    catch (Exception) { PlayerImage.Image = null; }
                }
                conn.Close();
            }
            string where = "BaseObject_ID = BaseObjectID AND BattlerClass_ID = BattlerClassID AND PlayerID = " + playerId;
            BattlerClassData = new ComboBoxInputData("BattlerClassID", "Name", "Player_To_BattlerClass JOIN BaseObject JOIN BattlerClass", where, "BattlerClass_ID");
            Class.Items.AddRange(BattlerClassData.OptionsListNames.ToArray());
        }


        public void Initialize()
        {
            Player.SelectedIndex = 0;
            Class.SelectedIndex = 0;
            LevelInput.Text = "1";
            ZFormation.SelectedIndex = 0;
            XFormation.SelectedIndex = 1;
            Item1.SelectedIndex = 0;
            Item2.SelectedIndex = 0;
            Item3.SelectedIndex = 0;
            Item4.SelectedIndex = 0;
            Weapon1.SelectedIndex = 0;
            Weapon2.SelectedIndex = 0;
            Weapon3.SelectedIndex = 0;
            PassiveSkill1.SelectedIndex = 0;
            PassiveSkill2.SelectedIndex = 0;
        }

        public void Read(FileStream file)
        {
            Player.SelectedIndex = PlayerData.FindIndex(ReadByte(file));
            Class.SelectedIndex = BattlerClassData.FindIndex(ReadByte(file));
            LevelInput.Text = ReadByte(file).ToString();
            ZFormation.SelectedIndex = ReadByte(file);
            XFormation.SelectedIndex = ReadByte(file);
            Item1.SelectedIndex = ItemData.FindIndex(ReadShort(file));
            Item2.SelectedIndex = ItemData.FindIndex(ReadShort(file));
            Item3.SelectedIndex = ItemData.FindIndex(ReadShort(file));
            Item4.SelectedIndex = ItemData.FindIndex(ReadShort(file));
            Weapon1.SelectedIndex = WeaponData.FindIndex(ReadShort(file));
            Weapon2.SelectedIndex = WeaponData.FindIndex(ReadShort(file));
            Weapon3.SelectedIndex = WeaponData.FindIndex(ReadShort(file));
            PassiveSkill1.SelectedIndex = PassiveSkillData.FindIndex(ReadShort(file));
            PassiveSkill2.SelectedIndex = PassiveSkillData.FindIndex(ReadShort(file));
        }

        public string ValidateInputs()
        {
            string err = "";
            if (!Database.Utilities.Utils.PosInt(LevelInput.Text) || int.Parse(LevelInput.Text) < 1 || int.Parse(LevelInput.Text) > 100)
                err += "Level must be a positive integer between 1 and 100\n";
            if (PassiveSkill1.SelectedIndex == PassiveSkill2.SelectedIndex && PassiveSkill1.SelectedIndex > 0) err += "Passive skills must be unique\n";
            return err;
        }

        public bool UnsavedChanges(FileStream file)
        {
            if (Player.SelectedIndex != PlayerData.FindIndex(ReadByte(file))) return true;
            if (Class.SelectedIndex != BattlerClassData.FindIndex(ReadByte(file))) return true;
            if (LevelInput.Text != ReadByte(file).ToString()) return true;
            if (ZFormation.SelectedIndex != ReadByte(file)) return true;
            if (XFormation.SelectedIndex != ReadByte(file)) return true;
            if (Item1.SelectedIndex != ItemData.FindIndex(ReadShort(file))) return true;
            if (Item2.SelectedIndex != ItemData.FindIndex(ReadShort(file))) return true;
            if (Item3.SelectedIndex != ItemData.FindIndex(ReadShort(file))) return true;
            if (Item4.SelectedIndex != ItemData.FindIndex(ReadShort(file))) return true;
            if (Weapon1.SelectedIndex != WeaponData.FindIndex(ReadShort(file))) return true;
            if (Weapon2.SelectedIndex != WeaponData.FindIndex(ReadShort(file))) return true;
            if (Weapon3.SelectedIndex != WeaponData.FindIndex(ReadShort(file))) return true;
            if (PassiveSkill1.SelectedIndex != PassiveSkillData.FindIndex(ReadShort(file))) return true;
            if (PassiveSkill2.SelectedIndex != PassiveSkillData.FindIndex(ReadShort(file))) return true;
            return false;
        }

        public void Write(FileStream file)
        {
            WriteByte(file, PlayerData.OptionsListIds[Player.SelectedIndex]);
            WriteByte(file, BattlerClassData.OptionsListIds[Class.SelectedIndex]);
            WriteByte(file, int.Parse(LevelInput.Text));
            WriteByte(file, ZFormation.SelectedIndex);
            WriteByte(file, XFormation.SelectedIndex);
            WriteShort(file, ItemData.OptionsListIds[Item1.SelectedIndex]);
            WriteShort(file, ItemData.OptionsListIds[Item2.SelectedIndex]);
            WriteShort(file, ItemData.OptionsListIds[Item3.SelectedIndex]);
            WriteShort(file, ItemData.OptionsListIds[Item4.SelectedIndex]);
            WriteShort(file, WeaponData.OptionsListIds[Weapon1.SelectedIndex]);
            WriteShort(file, WeaponData.OptionsListIds[Weapon2.SelectedIndex]);
            WriteShort(file, WeaponData.OptionsListIds[Weapon3.SelectedIndex]);
            WriteShort(file, PassiveSkillData.OptionsListIds[PassiveSkill1.SelectedIndex]);
            WriteShort(file, PassiveSkillData.OptionsListIds[PassiveSkill2.SelectedIndex]);
        }
    }
}
