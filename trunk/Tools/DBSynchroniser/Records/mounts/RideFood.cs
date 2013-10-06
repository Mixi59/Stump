 


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
    [TableName("RideFood")]
    [D2OClass("RideFood", "com.ankamagames.dofus.datacenter.mounts")]
    public class RideFoodRecord : ID2ORecord
    {
        int ID2ORecord.Id
        {
            get { return (int)Id; }
        }
        public uint gid;
        public uint typeId;
        public String MODULE = "RideFood";

        [D2OIgnore]
        [PrimaryKey("Id")]
        public int Id
        {
            get;
            set;
        }
        [D2OIgnore]
        public uint Gid
        {
            get { return gid; }
            set { gid = value; }
        }

        [D2OIgnore]
        public uint TypeId
        {
            get { return typeId; }
            set { typeId = value; }
        }

        public virtual void AssignFields(object obj)
        {
            var castedObj = (RideFood)obj;
            
            Gid = castedObj.gid;
            TypeId = castedObj.typeId;
        }
        
        public virtual object CreateObject(object parent = null)
        {
            
            var obj = parent != null ? (RideFood)parent : new RideFood();
            obj.gid = Gid;
            obj.typeId = TypeId;
            return obj;
        
        }
    }
}