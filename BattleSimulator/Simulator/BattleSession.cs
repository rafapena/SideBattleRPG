using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleSimulator.Classes;
using BattleSimulator.Classes.ClassTemplates;
using static BattleSimulator.Utilities.Utils;
using static BattleSimulator.Utilities.FileHelper;

namespace BattleSimulator.Simulator
{
    public class BattleSession : BaseObject
    {
        public string PartyName { get; private set; }
        public int TurnNumber { get; private set; }
        public Classes.Environment Environment { get; private set; }

        public List<Player> Players { get; private set; }
        public List<Enemy> Enemies { get; private set; }

        public BattleSession(string prtyFilenameWithPath)
        {
            Players = new List<Player>();
            Enemies = new List<Enemy>();
            TurnNumber = 0;
            using (var file = File.Open(prtyFilenameWithPath, FileMode.Open, FileAccess.Read))
            {
                PartyName = ReadText(file);
                int numberOfPlayers = ReadByte(file);
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    Player battlePlayer = new Player(AllData.Players[ReadByte(file)]);
                    battlePlayer.SetCurrentClass(ReadByte(file));
                    battlePlayer.SetAllStats(ReadByte(file));
                    battlePlayer.MoveToPosition(ReadByte(file), ReadByte(file));
                    int[] Ids = new int[9];
                    for (int j = 0; j < 9; j++) Ids[j] = ReadShort(file);
                    for (int j = 0; j < 4; j++) if (Ids[j] > 0) battlePlayer.AddItem(AllData.Items, Ids[j]);
                    for (int j = 4; j < 7; j++) if (Ids[j] > 0) battlePlayer.AddWeapon(AllData.Weapons, Ids[j]);
                    for (int j = 7; j < 9; j++) if (Ids[j] > 0) battlePlayer.AddPassiveSkill(AllData.PassiveSkills, Ids[j]);
                    Players.Add(battlePlayer);
                }
                for (int i = 0; i < 4; i++) for (int j = i + 1; j < 4; j++) PlayerSetRelationHelper(file, i, j);
                AllData.SetupBattle(ReadShort(file));
            }
        }
        private void PlayerSetRelationHelper(FileStream file, int i, int j)
        {
            int value = ReadByte(file);
            if (i >= Players.Count || j >= Players.Count) return;
            Players[i].SetRelation(Players[j].Id, value);
            Players[j].SetRelation(Players[i].Id, value);
        }
    }
}
