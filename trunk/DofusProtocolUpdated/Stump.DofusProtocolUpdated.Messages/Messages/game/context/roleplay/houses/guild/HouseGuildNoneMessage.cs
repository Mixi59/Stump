

// Generated on 12/12/2013 16:57:02
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class HouseGuildNoneMessage : Message
    {
        public const uint Id = 5701;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public short houseId;
        
        public HouseGuildNoneMessage()
        {
        }
        
        public HouseGuildNoneMessage(short houseId)
        {
            this.houseId = houseId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(houseId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            houseId = reader.ReadShort();
            if (houseId < 0)
                throw new Exception("Forbidden value on houseId = " + houseId + ", it doesn't respect the following condition : houseId < 0");
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(short);
        }
        
    }
    
}