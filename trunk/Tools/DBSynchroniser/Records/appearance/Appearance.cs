 


// Generated on 10/06/2013 18:02:16
using System;
using System.Collections.Generic;
using Stump.Core.IO;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;

namespace DBSynchroniser.Records
{
    [TableName("Appearances")]
    [D2OClass("Appearance", "com.ankamagames.dofus.datacenter.appearance")]
    public class AppearanceRecord : ID2ORecord
    {
        int ID2ORecord.Id
        {
            get { return (int)Id; }
        }
        public const String MODULE = "Appearances";
        public uint id;
        public uint type;
        public String data;

        [D2OIgnore]
        [PrimaryKey("Id", false)]
        public uint Id
        {
            get { return id; }
            set { id = value; }
        }

        [D2OIgnore]
        public uint Type
        {
            get { return type; }
            set { type = value; }
        }

        [D2OIgnore]
        [NullString]
        public String Data
        {
            get { return data; }
            set { data = value; }
        }

        public virtual void AssignFields(object obj)
        {
            var castedObj = (Appearance)obj;
            
            Id = castedObj.id;
            Type = castedObj.type;
            Data = castedObj.data;
        }
        
        public virtual object CreateObject(object parent = null)
        {
            
            var obj = parent != null ? (Appearance)parent : new Appearance();
            obj.id = Id;
            obj.type = Type;
            obj.data = Data;
            return obj;
        
        }
    }
}