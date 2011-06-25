﻿
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Classes;
using Stump.DofusProtocol.Classes.Custom;
using Stump.DofusProtocol.Classes.Extensions;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Data;
using Stump.Server.BaseServer.Initializing;
using Stump.Server.WorldServer.Groups;
using MonsterGradeTemplate = Stump.DofusProtocol.D2oClasses.MonsterGrade;
using MonsterRaceTemplate = Stump.DofusProtocol.D2oClasses.MonsterRace;
using MonsterTemplate = Stump.DofusProtocol.D2oClasses.Monster;

namespace Stump.Server.WorldServer.Entities
{
    public class Monster : LivingEntity
    {
        #region Fields

        /// <summary>
        ///   Our global array containing every monsters of our world.
        /// </summary>
        public static List<Monster> Monsters = new List<Monster>();

        private readonly List<int> m_levelsRange = new List<int>();
        private readonly List<MapIdEnum> m_mapIds = new List<MapIdEnum>();

        #endregion

        public Monster(long id)
            : base((int)id)
        {
            Race = MonsterRaceIdEnum.NotListed; // default race if not defined later.
            Grades = new List<MonsterGrade>();
        }

        public Monster(long id, MonsterRaceIdEnum monsterRace)
            : base((int)id)
        {
            Race = monsterRace;
            Grades = new List<MonsterGrade>();
        }

        #region Loading

        [StageStep(Stages.Two, "Loaded Monsters")]
        public static void LoadAll()
        {
            IEnumerable<MonsterTemplate> monstertemplates = DataLoader.LoadData<MonsterTemplate>();
            IEnumerable<MonsterRaceTemplate> monsterRaces = DataLoader.LoadData<MonsterRaceTemplate>();

            foreach (MonsterTemplate monstertemplate in monstertemplates)
            {
                var monster = new Monster(monstertemplate.id)
                    {
                        GfxId = monstertemplate.gfxId,
                        Race = (MonsterRaceIdEnum)monstertemplate.race,
                        Look = !string.IsNullOrEmpty(monstertemplate.look) ? new ExtendedLook(monstertemplate.look.ToEntityLook()) : null,
                    };


                // todo : this is totally wrong !
                MonsterRaceTemplate race = monsterRaces.SingleOrDefault(monsterRace => monsterRace.id == monster.Id);
                if (race != null)
                    monster.ParentMonsterId = (MonsterRaceIdEnum)race.superRaceId;

                foreach (MonsterGradeTemplate grade in monstertemplate.grades)
                {
                    var mg = new MonsterGrade
                        {
                            ActionPoints = grade.actionPoints,
                            AirResistance = grade.airResistance,
                            EarthResistance = grade.earthResistance,
                            FireResistance = grade.fireResistance,
                            Grade = grade.grade,
                            Level = grade.level,
                            LifePoints = grade.lifePoints,
                            MonsterId = grade.monsterId,
                            MovementPoints = grade.movementPoints,
                            NeutralResistance = grade.neutralResistance,
                            PaDodge = grade.paDodge,
                            PmDodge = grade.pmDodge,
                            WaterResistance = grade.waterResistance
                        };

                    monster.Grades.Add(mg);
                }

                Monsters.Add(monster);
            }

            LoadSpawnData();
        }


        public static void LoadSpawnData()
        {
            // todo
        }

        #endregion

        #region Generation

//        public static MonsterGroup GenerateGroup(MapIdEnum mapid)
//        {
//            var group = new MonsterGroup();
//
            // 1) Get how munch monsters will join this group.
//            var random = new AsyncRandom();
//            int number = random.NextInt(1, 8);
//
            // 2) Get succeptibles monsters to land on this map
//            IEnumerable<Monster> monsters = (from entry in Monsters
//                                             where entry.m_mapIds.Contains(mapid)
//                                             select entry);
//            IEnumerator<Monster> enumerator = monsters.GetEnumerator();
//            enumerator.Reset();
//            int i = 0;
//            while (enumerator.MoveNext() && i < number)
//            {
//                group.AddMember(enumerator.Current);
//                i++;
//            }
//
//            MonsterGroup = group;
//            return group;
//        }

        #endregion

        #region Properties

        public uint GfxId
        {
            get;
            set;
        }

        public MonsterRaceIdEnum Race
        {
            get;
            set;
        }

        public List<MonsterGrade> Grades
        {
            get;
            set;
        }

        public MonsterRaceIdEnum ParentMonsterId
        {
            get;
            set;
        }

//        public static MonsterGroup MonsterGroup
//        {
//            get;
//            set;
//        }

        #endregion

        public override string ToString()
        {
            return String.Format("Monster \"{0}\" <Id:{1}>", Enum.GetName(typeof(MonsterIdEnum), Id), Id);
        }

        public override FightTeamMemberInformations ToNetworkTeamMember()
        {
            throw new NotImplementedException();
        }

        public override GameFightFighterInformations ToNetworkFighter()
        {
            throw new NotImplementedException();
        }
    }
}