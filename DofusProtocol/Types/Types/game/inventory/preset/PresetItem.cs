

// Generated on 09/01/2014 15:52:53
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class PresetItem
    {
        public const short Id = 354;
        public virtual short TypeId
        {
            get { return Id; }
        }
        
        public byte position;
        public int objGid;
        public int objUid;
        
        public PresetItem()
        {
        }
        
        public PresetItem(byte position, int objGid, int objUid)
        {
            this.position = position;
            this.objGid = objGid;
            this.objUid = objUid;
        }
        
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteByte(position);
            writer.WriteInt(objGid);
            writer.WriteInt(objUid);
        }
        
        public virtual void Deserialize(IDataReader reader)
        {
            position = reader.ReadByte();
            if (position < 0 || position > 255)
                throw new Exception("Forbidden value on position = " + position + ", it doesn't respect the following condition : position < 0 || position > 255");
            objGid = reader.ReadInt();
            if (objGid < 0)
                throw new Exception("Forbidden value on objGid = " + objGid + ", it doesn't respect the following condition : objGid < 0");
            objUid = reader.ReadInt();
            if (objUid < 0)
                throw new Exception("Forbidden value on objUid = " + objUid + ", it doesn't respect the following condition : objUid < 0");
        }
        
        public virtual int GetSerializationSize()
        {
            return sizeof(byte) + sizeof(int) + sizeof(int);
        }
        
    }
    
}