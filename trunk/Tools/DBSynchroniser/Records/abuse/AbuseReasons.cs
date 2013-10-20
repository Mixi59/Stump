 


// Generated on 10/19/2013 17:17:42
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Stump.Core.IO;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;

namespace DBSynchroniser.Records
{
    [TableName("AbuseReasons")]
    [D2OClass("AbuseReasons", "com.ankamagames.dofus.datacenter.abuse")]
    public class AbuseReasonsRecord : ID2ORecord
    {
        int ID2ORecord.Id
        {
            get { return (int)Id; }
        }
        private const String MODULE = "AbuseReasons";
        public uint _abuseReasonId;
        public uint _mask;
        public int _reasonTextId;

        [D2OIgnore]
        [PrimaryKey("Id")]
        public int Id
        {
            get;
            set;
        }
        [D2OIgnore]
        public uint AbuseReasonId
        {
            get { return _abuseReasonId; }
            set { _abuseReasonId = value; }
        }

        [D2OIgnore]
        public uint Mask
        {
            get { return _mask; }
            set { _mask = value; }
        }

        [D2OIgnore]
        [I18NField]
        public int ReasonTextId
        {
            get { return _reasonTextId; }
            set { _reasonTextId = value; }
        }

        public virtual void AssignFields(object obj)
        {
            var castedObj = (AbuseReasons)obj;
            
            AbuseReasonId = castedObj._abuseReasonId;
            Mask = castedObj._mask;
            ReasonTextId = castedObj._reasonTextId;
        }
        
        public virtual object CreateObject(object parent = null)
        {
            
            var obj = parent != null ? (AbuseReasons)parent : new AbuseReasons();
            obj._abuseReasonId = AbuseReasonId;
            obj._mask = Mask;
            obj._reasonTextId = ReasonTextId;
            return obj;
        
        }
    }
}