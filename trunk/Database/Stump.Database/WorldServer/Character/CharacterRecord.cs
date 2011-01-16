﻿// /*************************************************************************
//  *
//  *  Copyright (C) 2010 - 2011 Stump Team
//  *
//  *  This program is free software: you can redistribute it and/or modify
//  *  it under the terms of the GNU General Public License as published by
//  *  the Free Software Foundation, either version 3 of the License, or
//  *  (at your option) any later version.
//  *
//  *  This program is distributed in the hope that it will be useful,
//  *  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  *  GNU General Public License for more details.
//  *
//  *  You should have received a copy of the GNU General Public License
//  *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//  *
//  *************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Castle.ActiveRecord;
using NHibernate.Criterion;
using NLog;
using Stump.DofusProtocol.Classes;
using Stump.DofusProtocol.Classes.Extensions;
using Stump.DofusProtocol.Enums;

namespace Stump.Database
{
    [AttributeDatabase(DatabaseService.WorldServer)]
    [ActiveRecord("characters"), JoinedBase]
    public class CharacterRecord : ActiveRecordBase<CharacterRecord>
    {
        private IList<SpellRecord> m_spells;
        private IList<ZaapRecord> m_zaaps;
        private IList<QuestRecord> m_quests;
        private IList<JobRecord> m_jobs;

        #region Base

        [PrimaryKey(PrimaryKeyType.Identity, "Id")]
        public int Id
        {
            get;
            set;
        }

        [Property("Name", Length = 18, NotNull = true)]
        public string Name
        {
            get;
            set;
        }

        [Property("Breed", NotNull = true)]
        public int Breed
        {
            get;
            set;
        }

        [Property("BaseHealth", NotNull = true)]
        public int BaseHealth
        {
            get;
            set;
        }

        [Property("DamageTaken", NotNull = true)]
        public int DamageTaken
        {
            get;
            set;
        }

        #endregion

        #region Look

        [Property("Sex", NotNull = true)]
        public SexTypeEnum Sex
        {
            get;
            set;
        }

        [Property("BonesId", NotNull = true, Default = "1")]
        private uint BonesId
        {
            get;
            set;
        }

        [Property("Skins", NotNull = true)]
        private string Skins
        {
            get;
            set;
        }

        [Property("Color1", NotNull = true)]
        private int Color1
        {
            get;
            set;
        }

        [Property("Color2", NotNull = true)]
        private int Color2
        {
            get;
            set;
        }

        [Property("Color3", NotNull = true)]
        private int Color3
        {
            get;
            set;
        }

        [Property("Color4", NotNull = true)]
        private int Color4
        {
            get;
            set;
        }

        [Property("Color5", NotNull = true)]
        private int Color5
        {
            get;
            set;
        }

        [Property("Height", NotNull = true)]
        private int Height
        {
            get;
            set;
        }

        [Property("Width", NotNull = true)]
        private int Width
        {
            get;
            set;
        }

        public EntityLook BaseLook
        {
            get
            {
                List<uint> skins = Skins.Split(',').ToList().ConvertAll(s => uint.Parse(s));
                return new EntityLook(BonesId, skins, new List<int>(5) { Color1, Color2, Color3, Color4, Color5 }, new List<int>(2) { Height, Width }, new List<SubEntity>());
            }
            set
            {
                BonesId = value.bonesId;
                Skins = string.Join(",", value.skins);
                Color1 = value.indexedColors[0];
                Color2 = value.indexedColors[1];
                Color3 = value.indexedColors[2];
                Color4 = value.indexedColors[3];
                Color5 = value.indexedColors[4];
                if (value.scales.Count == 2)
                {
                    Height = value.scales[0];
                    Width = value.scales[1];
                }
                else
                {
                    Height = value.scales[0];
                    Width = value.scales[0];
                }

            }
        }

        [Property("HasRecolor", NotNull = true, Default = "0")]
        public bool Recolor
        {
            get;
            set;
        }

        [Property("HasRename", NotNull = true, Default = "0")]
        public bool Rename
        {
            get;
            set;
        }

        #endregion

        #region Position

        [Property("MapId", NotNull = true)]
        public int MapId
        {
            get;
            set;
        }


        [Property("CellId", NotNull = true)]
        public ushort CellId
        {
            get;
            set;
        }

        [Property("Direction", NotNull = true)]
        public DirectionsEnum Direction
        {
            get;
            set;
        }

        #endregion

        #region Stats

        [Property("Strength", NotNull = true)]
        public int Strength
        {
            get;
            set;
        }

        [Property("Chance", NotNull = true)]
        public int Chance
        {
            get;
            set;
        }

        [Property("Vitality", NotNull = true)]
        public int Vitality
        {
            get;
            set;
        }

        [Property("Wisdom", NotNull = true)]
        public int Wisdom
        {
            get;
            set;
        }

        [Property("Intelligence", NotNull = true)]
        public int Intelligence
        {
            get;
            set;
        }

        [Property("Agility", NotNull = true)]
        public int Agility
        {
            get;
            set;
        }

        #endregion

        #region Points

        [Property("Experience", NotNull = true, Default = "0")]
        public long Experience
        {
            get;
            set;
        }

        [Property("EnergyMax", NotNull = true, Default = "10000")]
        public uint EnergyMax
        {
            get;
            set;
        }

        [Property("Energy", NotNull = true, Default = "10000")]
        public uint Energy
        {
            get;
            set;
        }

        [Property("StatsPoints", NotNull = true, Default = "0")]
        public int StatsPoints
        {
            get;
            set;
        }

        [Property("SpellsPoints", NotNull = true, Default = "0")]
        public int SpellsPoints
        {
            get;
            set;
        }

        #endregion

        #region Inventory

        [BelongsTo("InventoryId", NotNull = true, Cascade = CascadeEnum.Delete)]
        public InventoryRecord Inventory
        {
            get;
            set;
        }

        #endregion

        #region Spell

        [HasMany(typeof(SpellRecord), Cascade = ManyRelationCascadeEnum.Delete)]
        public IList<SpellRecord> Spells
        {
            get { return m_spells ?? new List<SpellRecord>(); }
            set { m_spells = value; }
        }

        public IDictionary<uint, SpellRecord> SpellsDictionnary
        {
            get { return Spells.ToDictionary(s => s.SpellId); }
            set { Spells = value.Select(k => k.Value).ToList(); }
        }

        #endregion

        #region Zaaps

        [HasMany(typeof(ZaapRecord))]
        public IList<ZaapRecord> Zaaps
        {
            get { return m_zaaps ?? new List<ZaapRecord>(); }
            set { m_zaaps = value; }
        }

        #endregion

        #region Quests

        [HasMany(typeof(QuestRecord))]
        public IList<QuestRecord> Quests
        {
            get { return m_quests ?? new List<QuestRecord>(); }
            set { m_quests = value; }
        }

        #endregion

        #region Jobs

        [HasMany(typeof(JobRecord))]
        public IList<JobRecord> Jobs
        {
            get { return m_jobs ?? new List<JobRecord>(); }
            set { m_jobs = value; }
        }

        #endregion

        #region Alignment

        [OneToOne(Cascade = CascadeEnum.Delete)]
        public AlignmentRecord Alignment
        {
            get;
            set;
        }

        #endregion

        #region Guild

        [BelongsTo("GuildId", NotNull = false)]
        public GuildRecord Guild
        {
            get;
            set;
        }

        #endregion

        public static CharacterRecord FindCharacterById(int characterId)
        {
            return FindByPrimaryKey(characterId);
        }

        public static CharacterRecord FindCharacterByName(string characterName)
        {
            return FindOne(Restrictions.Eq("Name", characterName));
        }

        public static bool IsNameExists(string name)
        {
            return Exists(Restrictions.Eq("Name", name));
        }

        public static int GetCount()
        {
            return Count();
        }

    }
}