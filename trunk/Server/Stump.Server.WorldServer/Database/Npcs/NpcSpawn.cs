using System;
using Castle.ActiveRecord;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.DofusProtocol.Types.Extensions;
using Stump.Server.WorldServer.Worlds.Maps.Cells;

namespace Stump.Server.WorldServer.Database.Npcs
{
    [ActiveRecord("npcs_spawns")]
    public class NpcSpawn : WorldBaseRecord<NpcSpawn>
    {
        [PrimaryKey(PrimaryKeyType.Native)]
        public uint Id
        {
            get;
            set;
        }

        [BelongsTo("TemplateId", Cascade = CascadeEnum.Delete)]
        public NpcTemplate Template
        {
            get;
            set;
        }

        [Property(NotNull = true)]
        public int MapId
        {
            get;
            set;
        }

        [Property(NotNull = true)]
        public int CellId
        {
            get;
            set;
        }

        [Property(NotNull = true)]
        public DirectionsEnum Direction
        {
            get;
            set;
        }


        private string m_lookAsString;
        private EntityLook m_entityLook;

        [Property("Look", NotNull = false)]
        private string LookAsString
        {
            get
            {
                if (m_entityLook == null)
                    return string.Empty;

                if (string.IsNullOrEmpty(m_lookAsString))
                    m_lookAsString = Look.ConvertToString();

                return m_lookAsString;
            }
            set
            {
                m_lookAsString = value;

                if (value != null)
                    m_entityLook = m_lookAsString.ToEntityLook();
            }
        }

        public EntityLook Look
        {
            get
            {
                return m_entityLook ?? Template.Look;
            }
            set
            {
                m_entityLook = value;

                if (value != null)
                    m_lookAsString = value.ConvertToString();
            }
        }

        public ObjectPosition GetPosition()
        {
            var map = Worlds.World.Instance.GetMap(MapId);

            if (map == null)
                throw new Exception(string.Format("Cannot load NpcSpawn id={0}, map {1} isn't found", Id, MapId));

            var cell = map.Cells[CellId];

            return new ObjectPosition(map, cell, Direction);
        }
    }
}