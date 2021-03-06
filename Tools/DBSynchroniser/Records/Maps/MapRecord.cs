﻿using System;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.D2oClasses.Tools.Dlm;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.WorldServer;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Database.World.Maps;
using Stump.DofusProtocol.D2oClasses.Tools.Ele;
using Stump.DofusProtocol.D2oClasses.Tools.Ele.Datas;

namespace DBSynchroniser.Records.Maps
{    
    public class MapRelator
    {
        public static string FetchQuery = "SELECT * FROM maps";
    }

    [TableName("maps")]
    public class MapRecord : IAutoGeneratedRecord, ISaveIntercepter
    {
        private short[] m_blueCells;
        private byte[] m_compressedCells;
        private byte[] m_compressedElements;
        private short[] m_redCells;

        public MapRecord()
        {
            
        }

        public MapRecord(DlmMap map)
        {
            Id = map.Id;
            
            Id = map.Id;
            Version = map.Version;
            RelativeId = map.RelativeId;
            MapType = map.MapType;
            SubAreaId = map.SubAreaId;
            ClientTopNeighbourId = map.TopNeighbourId;
            ClientBottomNeighbourId = map.BottomNeighbourId;
            ClientLeftNeighbourId = map.LeftNeighbourId;
            ClientRightNeighbourId = map.RightNeighbourId;
            ShadowBonusOnEntities = map.ShadowBonusOnEntities;
            UseLowpassFilter = map.UseLowPassFilter;
            UseReverb = map.UseReverb;
            PresetId = map.PresetId;
            Cells =
                map.Cells.Select(
                    x =>
                        new Cell
                        {
                            Id = x.Id,
                            Floor = x.Floor,
                            Data = x.Data,
                            MapChangeData = x.MapChangeData,
                            MoveZone = x.MoveZone,
                            Speed = x.Speed
                        }).ToArray();
            bool any = Cells.Any(x => x.Walkable);
            BeforeSave(false);
        }

        [PrimaryKey("Id", false)]
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        ///   Map version of this map.
        /// </summary>
        public uint Version
        {
            get;
            set;
        }

        /// <summary>
        ///   Relative id of this map.
        /// </summary>
        public uint RelativeId
        {
            get;
            set;
        }

        /// <summary>
        ///   Type of this map.
        /// </summary>
        public int MapType
        {
            get;
            set;
        }

        /// <summary>
        ///   Zone Id which owns this map.
        /// </summary>
        public int SubAreaId
        {
            get;
            set;
        }

        public int TopNeighbourId
        {
            get;
            set;
        }

        public int BottomNeighbourId
        {
            get;
            set;
        }

        public int LeftNeighbourId
        {
            get;
            set;
        }

        public int RightNeighbourId
        {
            get;
            set;
        }

        [DefaultSetting(-1)]
        public short TopNeighbourCellId
        {
            get;
            set;
        }

        [DefaultSetting(-1)]
        public short BottomNeighbourCellId
        {
            get;
            set;
        }

        [DefaultSetting(-1)]
        public short RightNeighbourCellId
        {
            get;
            set;
        }

        [DefaultSetting(-1)]
        public short LeftNeighbourCellId
        {
            get;
            set;
        }

        public int ClientTopNeighbourId
        {
            get;
            set;
        }

        public int ClientBottomNeighbourId
        {
            get;
            set;
        }

        public int ClientLeftNeighbourId
        {
            get;
            set;
        }

        public int ClientRightNeighbourId
        {
            get;
            set;
        }

        public int ShadowBonusOnEntities
        {
            get;
            set;
        }

        public bool UseLowpassFilter
        {
            get;
            set;
        }

        public bool UseReverb
        {
            get;
            set;
        }

        public int PresetId
        {
            get;
            set;
        }

        public byte[] BlueCellsBin
        {
            get;
            set;
        }

        public byte[] RedCellsBin
        {
            get;
            set;
        }

        [Ignore]
        public short[] BlueFightCells
        {
            get
            {
                return BlueCellsBin == null
                           ? new short[0]
                           : (m_blueCells ?? (m_blueCells = DeserializeFightCells(BlueCellsBin)));
            }
            set
            {
                m_blueCells = value;

                BlueCellsBin = value != null ? SerializeFightCells(value) : null;
            }
        }

        [Ignore]
        public short[] RedFightCells
        {
            get
            {
                return RedCellsBin == null
                           ? new short[0]
                           : (m_redCells ?? (m_redCells = DeserializeFightCells(RedCellsBin)));
            }
            set
            {
                m_redCells = value;
                RedCellsBin = value != null ? SerializeFightCells(value) : null;
            }
        }

        public byte[] CompressedCells
        {
            get { return m_compressedCells; }
            set
            {
                m_compressedCells = value;
                byte[] uncompressedCells = ZipHelper.Uncompress(m_compressedCells);

                Cells = new Cell[uncompressedCells.Length/Cell.StructSize];
                for (int i = 0, j = 0; i < uncompressedCells.Length; i += Cell.StructSize, j++)
                {
                    Cells[j] = new Cell();
                    Cells[j].Deserialize(uncompressedCells, i);
                }
            }
        }

        [Ignore]
        public Cell[] Cells
        {
            get;
            set;
        }

        public bool SpawnDisabled
        {
            get;
            set;
        }

        #region ISaveIntercepter Members

        public void BeforeSave(bool insert)
        {
            m_compressedCells = new byte[Cells.Length*Cell.StructSize];

            for (int i = 0; i < Cells.Length; i++)
            {
                Array.Copy(Cells[i].Serialize(), 0, m_compressedCells, i*Cell.StructSize, Cell.StructSize);
            }

            m_compressedCells = ZipHelper.Compress(m_compressedCells);
        }

        #endregion

        public static byte[] SerializeFightCells(short[] cells)
        {
            var bytes = new byte[cells.Length*2];

            for (int i = 0, l = 0; i < cells.Length; i++, l += 2)
            {
                bytes[l] = (byte) ((cells[i] & 0xFF00) >> 8);
                bytes[l + 1] = (byte) (cells[i] & 0xFF);
            }

            return bytes;
        }

        public static short[] DeserializeFightCells(byte[] bytes)
        {
            if ((bytes.Length%2) != 0)
                throw new ArgumentException("bytes.Length % 2 != 0");

            var cells = new short[bytes.Length/2];

            for (int i = 0, j = 0; i < bytes.Length; i += 2, j++)
                cells[j] = (short) (bytes[i] << 8 | bytes[i + 1]);

            return cells;
        }

        public Stump.Server.WorldServer.Database.World.Maps.MapRecord GetWorldRecord()
        {
            var record = new Stump.Server.WorldServer.Database.World.Maps.MapRecord
            {
                Id = Id,
                Version = Version,
                RelativeId = RelativeId,
                MapType = MapType,
                SubAreaId = SubAreaId,
                TopNeighbourId = TopNeighbourId,
                BottomNeighbourId = BottomNeighbourId,
                LeftNeighbourId = LeftNeighbourId,
                RightNeighbourId = RightNeighbourId,
                TopNeighbourCellId = TopNeighbourCellId,
                BottomNeighbourCellId = BottomNeighbourCellId,
                LeftNeighbourCellId = LeftNeighbourCellId,
                RightNeighbourCellId = RightNeighbourCellId,
                ClientTopNeighbourId = ClientTopNeighbourId,
                ClientBottomNeighbourId = ClientBottomNeighbourId,
                ClientLeftNeighbourId = ClientLeftNeighbourId,
                ClientRightNeighbourId = ClientRightNeighbourId,
                ShadowBonusOnEntities = ShadowBonusOnEntities,
                UseLowpassFilter = UseLowpassFilter,
                UseReverb = UseReverb,
                PresetId = PresetId,
                BlueFightCells = BlueFightCells,
                RedFightCells = RedFightCells,
                CompressedCells = CompressedCells,
                SpawnDisabled = SpawnDisabled
            };

            return record;
        }
    }
}