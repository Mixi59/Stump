 


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
    [TableName("TitleCategories")]
    [D2OClass("TitleCategory", "com.ankamagames.dofus.datacenter.appearance")]
    public class TitleCategoryRecord : ID2ORecord
    {
        int ID2ORecord.Id
        {
            get { return (int)Id; }
        }
        private const String MODULE = "TitleCategories";
        public int id;
        public uint nameId;

        [D2OIgnore]
        [PrimaryKey("Id", false)]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [D2OIgnore]
        public uint NameId
        {
            get { return nameId; }
            set { nameId = value; }
        }

        public virtual void AssignFields(object obj)
        {
            var castedObj = (TitleCategory)obj;
            
            Id = castedObj.id;
            NameId = castedObj.nameId;
        }
        
        public virtual object CreateObject(object parent = null)
        {
            
            var obj = parent != null ? (TitleCategory)parent : new TitleCategory();
            obj.id = Id;
            obj.nameId = NameId;
            return obj;
        
        }
    }
}