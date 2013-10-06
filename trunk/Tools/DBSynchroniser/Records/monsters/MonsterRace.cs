 


// Generated on 10/06/2013 18:02:18
using System;
using System.Collections.Generic;
using Stump.Core.IO;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;

namespace DBSynchroniser.Records
{
    [TableName("MonsterRaces")]
    [D2OClass("MonsterRace", "com.ankamagames.dofus.datacenter.monsters")]
    public class MonsterRaceRecord : ID2ORecord
    {
        int ID2ORecord.Id
        {
            get { return (int)Id; }
        }
        private const String MODULE = "MonsterRaces";
        public int id;
        public int superRaceId;
        public uint nameId;

        [D2OIgnore]
        [PrimaryKey("Id", false)]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [D2OIgnore]
        public int SuperRaceId
        {
            get { return superRaceId; }
            set { superRaceId = value; }
        }

        [D2OIgnore]
        public uint NameId
        {
            get { return nameId; }
            set { nameId = value; }
        }

        public virtual void AssignFields(object obj)
        {
            var castedObj = (MonsterRace)obj;
            
            Id = castedObj.id;
            SuperRaceId = castedObj.superRaceId;
            NameId = castedObj.nameId;
        }
        
        public virtual object CreateObject(object parent = null)
        {
            
            var obj = parent != null ? (MonsterRace)parent : new MonsterRace();
            obj.id = Id;
            obj.superRaceId = SuperRaceId;
            obj.nameId = NameId;
            return obj;
        
        }
    }
}