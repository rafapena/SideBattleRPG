﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using BattleSimulator.Classes.ClassTemplates;
using static BattleSimulator.Utilities.DataManager;
using static BattleSimulator.Utilities.ListManager;
using System.Windows.Forms;

namespace BattleSimulator.Classes
{
    public class Player : Battler
    {
        public Stats NaturalStats { get; private set; }
        public int Companionship { get; private set; }
        public int SavePartnerRate { get; private set; }
        public int CounterattackRate { get; private set; }
        public int AssistDamageRate { get; private set; }
        public List<BattlerClass> ClassSet { get; private set; }
        public int[] PlayerCompanionships { get; private set; }
        private List<Skill> SkillSet;
        private List<int> SkillSetLevels;

        public List<Skill> ComboSkills { get; private set; }
        public int SelectedLocationX { get; set; }
        public int SelectedLocationZ { get; set; }


        public Player() : base()
        {
            ClassSet = new List<BattlerClass>();
            SkillSet = new List<Skill>();
            SkillSetLevels = new List<int>();
            ComboSkills = new List<Skill>();
        }

        public void Initialize(System.Data.SQLite.SQLiteDataReader data, List<string> elementsData,
            List<State> statesData, List<BattlerClass> classesData, List<Player> playersData, List<Skill> skillsData)
        {
            Initialize(data);
            Id = Int(data["Player_ID"]);
            ElementRates = ReadRatesList(data, "Player", elementsData, "ElementRates");
            StateRates = ReadRatesList(data, "Player", statesData, "Vulnerability");
            NaturalStats = ReadStats(data["NaturalStats"], false);
            Companionship = Int(data["Companionship"]);
            SavePartnerRate = Int(data["SavePartnerRate"]);
            CounterattackRate = Int(data["CounterattackRate"]);
            AssistDamageRate = Int(data["AssistDamageRate"]);
            List<int> classesList = ReadDBList(data, "Player", "BattlerClass");
            for (int i = 0; i < classesList.Count; i++) ClassSet.Add(ReadObj(classesData, classesList[i]));
            PlayerCompanionships = ReadRatesList(data, "Player", playersData, "CompanionshipTo");
            List<int> skillsList = ReadDBList(data, "Player", "Skill", "LevelRequired");
            for (int i = 0; i < skillsList.Count;)
            {
                SkillSet.Add(ReadObj(skillsData, skillsList[i++]));
                SkillSetLevels.Add(skillsList[i++]);
            }
        }

        public Player(Player original) : base(original)
        {
            NaturalStats = Clone(original.NaturalStats, o => new Stats(o));
            Companionship = original.Companionship;
            SavePartnerRate = original.SavePartnerRate;
            CounterattackRate = original.CounterattackRate;
            AssistDamageRate = original.AssistDamageRate;
            ClassSet = Clone(original.ClassSet, o => new BattlerClass(o));
            PlayerCompanionships = Clone(original.PlayerCompanionships);
            SkillSet = Clone(original.SkillSet, o => new Skill(o));
            SkillSetLevels = Clone(original.SkillSetLevels);
            ComboSkills = Clone(original.ComboSkills, o => new Skill(o));
        }


        public new void SetAllStats(int level)
        {
            base.SetAllStats(level);
            if (Class != null) Stats = new Stats(level, Class.BaseStats, NaturalStats);
        }

        public void SetCurrentClass(List<BattlerClass> classesData, int classId)
        {
            if (ValidListInput(classesData, classId)) Class = classesData[classId];
        }

        public void SetRelation(int id, int level)
        {
            PlayerCompanionships[id] = 0;
            for (int i = 1; i <= level; i++) PlayerCompanionships[id] += 10 * i;
        }

        public void AddSkillsFromLevel()
        {
            for (int i = 0; i < SkillSet.Count; i++)
            {
                if (Level < SkillSetLevels[i]) continue;
                if (SkillSet[i].NumberOfUsers > 1) ComboSkills.Add(new Skill(SkillSet[i]));
                else Skills.Add(new Skill(SkillSet[i]));
            }
        }
    }
}