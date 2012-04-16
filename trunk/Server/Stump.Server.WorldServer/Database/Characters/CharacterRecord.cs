using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.DofusProtocol.Types.Extensions;
using Stump.Server.WorldServer.Database.Breeds;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Game.Maps;
using Shortcut = Stump.Server.WorldServer.Database.Shortcuts.Shortcut;

namespace Stump.Server.WorldServer.Database.Characters
{
    [ActiveRecord("characters", Access = PropertyAccess.Property)]
    public class CharacterRecord : WorldBaseRecord<CharacterRecord>
    {
        private EntityLook m_entityLook;
        private string m_lookAsString;

        public CharacterRecord()
        {
            TitleParam = string.Empty; // why doesn't it work with Attribute ? dunno :x
        }

        public CharacterRecord(Breed breed)
            : this()
        {
            Breed = (PlayableBreedEnum) breed.Id;

            BaseHealth = breed.StartHealthPoint;
            AP = breed.StartActionPoints;
            MP = breed.StartMovementPoints;
            Prospection = breed.StartProspection;
            SpellsPoints = breed.StartSpellsPoints;
            StatsPoints = breed.StartStatsPoints;
            Strength = breed.StartStrength;
            Vitality = breed.StartVitality;
            Wisdom = breed.StartWisdom;
            Chance = breed.StartChance;
            Intelligence = breed.StartIntelligence;
            Agility = breed.StartAgility;

            MapId = breed.StartMap;
            CellId = breed.StartCell;
            Direction = breed.StartDirection;

            SpellsPoints = breed.StartLevel;
            StatsPoints = (ushort) (breed.StartLevel * 5);
            Kamas = breed.StartKamas;
        }

        [PrimaryKey(PrimaryKeyType.Native)]
        public int Id
        {
            get;
            set;
        }

        [Property]
        public DateTime CreationDate
        {
            get;
            set;
        }

        [Property]
        public DateTime LastUsage
        {
            get;
            set;
        }

        [Property("Name", Length = 24, NotNull = true)]
        public string Name
        {
            get;
            set;
        }

        [Property("Breed", NotNull = true)]
        public PlayableBreedEnum Breed
        {
            get;
            set;
        }

        [Property("Sex", NotNull = true)]
        public SexTypeEnum Sex
        {
            get;
            set;
        }

        [Property("EntityLook", ColumnType = "StringClob", SqlType = "Text")]
        private string LookAsString
        {
            get
            {
                if (EntityLook == null)
                    return string.Empty;

                if (string.IsNullOrEmpty(m_lookAsString))
                    m_lookAsString = EntityLook.ConvertToString();

                return m_lookAsString;
            }
            set
            {
                m_lookAsString = value;

                if (value != null)
                    m_entityLook = m_lookAsString.ToEntityLook();
            }
        }

        public EntityLook EntityLook
        {
            get { return m_entityLook; }
            set
            {
                m_entityLook = value;

                if (value != null)
                    m_lookAsString = value.ConvertToString();
            }
        }

        [Property("TitleId", NotNull = true, Default = "0")]
        public uint TitleId
        {
            get;
            set;
        }

        [Property("TitleParam", NotNull = true, Default = "", ColumnType = "StringClob", SqlType = "Text")]
        public string TitleParam
        {
            get;
            set;
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

        #region Restrictions

        [Property]
        public bool CantBeAggressed
        {
            get;
            set;
        }

        [Property]
        public bool CantBeChallenged
        {
            get;
            set;
        }

        [Property]
        public bool CantTrade
        {
            get;
            set;
        }

        [Property]
        public bool CantBeAttackedByMutant
        {
            get;
            set;
        }

        [Property]
        public bool CantRun
        {
            get;
            set;
        }

        [Property]
        public bool ForceSlowWalk
        {
            get;
            set;
        }

        [Property]
        public bool CantMinimize
        {
            get;
            set;
        }

        [Property]
        public bool CantMove
        {
            get;
            set;
        }

        [Property]
        public bool CantAggress
        {
            get;
            set;
        }

        [Property]
        public bool CantChallenge
        {
            get;
            set;
        }

        [Property]
        public bool CantExchange
        {
            get;
            set;
        }

        [Property]
        public bool CantAttack
        {
            get;
            set;
        }

        [Property]
        public bool CantChat
        {
            get;
            set;
        }

        [Property]
        public bool CantBeMerchant
        {
            get;
            set;
        }

        [Property]
        public bool CantUseObject
        {
            get;
            set;
        }

        [Property]
        public bool CantUseTaxCollector
        {
            get;
            set;
        }

        [Property]
        public bool CantUseInteractive
        {
            get;
            set;
        }

        [Property]
        public bool CantSpeakToNpc
        {
            get;
            set;
        }

        [Property]
        public bool CantChangeZone
        {
            get;
            set;
        }

        [Property]
        public bool CantAttackMonster
        {
            get;
            set;
        }

        [Property]
        public bool CantWalk8Directions
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
        public short CellId
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

        [Property("BaseHealth", NotNull = true)]
        public ushort BaseHealth
        {
            get;
            set;
        }

        [Property("DamageTaken", NotNull = true)]
        public ushort DamageTaken
        {
            get;
            set;
        }

        [Property("AP", NotNull = true)]
        public ushort AP
        {
            get;
            set;
        }

        [Property("MP", NotNull = true)]
        public ushort MP
        {
            get;
            set;
        }

        [Property("Prospection", NotNull = true)]
        public ushort Prospection
        {
            get;
            set;
        }

        [Property("Strength", NotNull = true)]
        public short Strength
        {
            get;
            set;
        }

        [Property("Chance", NotNull = true)]
        public short Chance
        {
            get;
            set;
        }

        [Property("Vitality", NotNull = true)]
        public short Vitality
        {
            get;
            set;
        }

        [Property("Wisdom", NotNull = true)]
        public short Wisdom
        {
            get;
            set;
        }

        [Property("Intelligence", NotNull = true)]
        public short Intelligence
        {
            get;
            set;
        }

        [Property("Agility", NotNull = true)]
        public short Agility
        {
            get;
            set;
        }

        [Property("Kamas", NotNull = true, Default = "0")]
        public int Kamas
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
        public short EnergyMax
        {
            get;
            set;
        }

        [Property("Energy", NotNull = true, Default = "10000")]
        public short Energy
        {
            get;
            set;
        }

        [Property("StatsPoints", NotNull = true, Default = "0")]
        public ushort StatsPoints
        {
            get;
            set;
        }

        [Property("SpellsPoints", NotNull = true, Default = "0")]
        public ushort SpellsPoints
        {
            get;
            set;
        }

        #endregion

        #region Alignement

        [Property]
        public AlignmentSideEnum AlignmentSide
        {
            get;
            set;
        }

        [Property]
        public sbyte AlignmentValue
        {
            get;
            set;
        }

        [Property]
        public ushort Honor
        {
            get;
            set;
        }

        [Property]
        public ushort Dishonor
        {
            get;
            set;
        }

        [Property]
        public bool PvPEnabled
        {
            get;
            set;
        }

        #endregion

        #region Zaaps

        private byte[] m_serializedZaaps = new byte[0];

        [Property("KnownZaaps", NotNull = true)]
        private byte[] SerializedZaaps
        {
            get { return m_serializedZaaps; }
            set
            {
                m_serializedZaaps = value;
                m_knownZaaps = UnSerializeZaaps(m_serializedZaaps);
            }
        }

        private List<Map> m_knownZaaps = new List<Map>();

        public List<Map> KnownZaaps
        {
            get { return m_knownZaaps; }
            set
            {
                m_knownZaaps = value;
                m_serializedZaaps = SerializeZaaps(m_knownZaaps);
            }
        }

        private byte[] SerializeZaaps(List<Map> knownZaaps)
        {
            var result = new byte[knownZaaps.Count*4];

            for (int i = 0; i < knownZaaps.Count; i++)
            {
                result[i*4] = (byte) (knownZaaps[i].Id >> 24);
                result[i*4 + 1] = (byte) ((knownZaaps[i].Id >> 16) & 0xFF);
                result[i*4 + 2] = (byte) ((knownZaaps[i].Id >> 8) & 0xFF);
                result[i*4 + 3] = (byte) (knownZaaps[i].Id & 0xFF);
            }

            return result;
        }

        private List<Map> UnSerializeZaaps(byte[] serialized)
        {
            var result = new List<Map>();

            for (int i = 0; i < serialized.Length; i += 4)
            {
                int id = serialized[i] << 24 | serialized[i + 1] << 16 | serialized[i + 2] << 8 | serialized[i + 3];

                var map = Game.World.Instance.GetMap(id);

                if (map == null)
                    throw new Exception("Map " + id + " not found");

                result.Add(map);
            }

            return result;
        }

        private int? m_spawnMapId;

        [Property("SpawnMap")]
        public int? SpawnMapId
        {
            get { return m_spawnMapId; }
            set
            {
                m_spawnMapId = value;
                m_spawnMap = null;
            }
        }

        private Map m_spawnMap;

        public Map SpawnMap
        {
            get
            {
                if (!SpawnMapId.HasValue)
                    return null;

                return m_spawnMap ?? (m_spawnMap = Game.World.Instance.GetMap(SpawnMapId.Value));
            }
            set
            {
                m_spawnMap = value;

                if (value == null)
                    SpawnMapId = null;
                else
                    SpawnMapId = value.Id;
            }
        }

        #endregion

        #region Friends

        [Property]
        public bool WarnOnConnection
        {
            get;
            set;
        }

        [Property]
        public bool WarnOnLevel
        {
            get;
            set;
        }

        #endregion

        protected override void OnDelete()
        {
            PlayerItemRecord.DeleteAll("OwnerId = " + Id);
            CharacterSpellRecord.DeleteAll("OwnerId = " + Id);
            Shortcut.DeleteAll("OwnerId = " + Id);

            base.OnDelete();
        }

        protected override bool BeforeSave(System.Collections.IDictionary state)
        {
            m_serializedZaaps = SerializeZaaps(m_knownZaaps);

            return base.BeforeSave(state);
        }

        public static CharacterRecord FindById(int characterId)
        {
            return FindByPrimaryKey(characterId);
        }

        public static CharacterRecord FindByName(string characterName)
        {
            return FindOne(NHibernate.Criterion.Restrictions.Eq("Name", characterName));
        }

        public static bool DoesNameExists(string name)
        {
            return Exists(NHibernate.Criterion.Restrictions.Eq("Name", name));
        }

        public static int GetCount()
        {
            return Count();
        }
    }
}