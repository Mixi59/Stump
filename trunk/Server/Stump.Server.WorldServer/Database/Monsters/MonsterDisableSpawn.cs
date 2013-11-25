﻿using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.WorldServer.Game.Maps;

namespace Stump.Server.WorldServer.Database.Monsters
{
    public class MonsterDisableSpawnRelator
    {
        public static string FetchQuery = "SELECT * FROM monsters_disable_spawns";
    }

    [TableName("monsters_disable_spawns")]
    public class MonsterDisableSpawn : IAutoGeneratedRecord
    {
        private SubArea m_subArea;

        [PrimaryKey("Id")]
        public int Id
        {
            get;
            set;
        }

        public int? SubAreaId
        {
            get;
            set;
        }

        [Ignore]
        public SubArea SubArea
        {
            get
            {
                if (!SubAreaId.HasValue)
                    return null;

                return m_subArea ?? (m_subArea = Game.World.Instance.GetSubArea(SubAreaId.Value));
            }
            set
            {
                m_subArea = value;

                if (value == null)
                    SubAreaId = null;
                else
                    SubAreaId = value.Id;
            }
        }

        public int MonsterId
        {
            get;
            set;
        }
    }
}
