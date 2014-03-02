

// Generated on 03/02/2014 20:42:50
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class ExchangeCraftSlotCountIncreasedMessage : Message
    {
        public const uint Id = 6125;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public sbyte newMaxSlot;
        
        public ExchangeCraftSlotCountIncreasedMessage()
        {
        }
        
        public ExchangeCraftSlotCountIncreasedMessage(sbyte newMaxSlot)
        {
            this.newMaxSlot = newMaxSlot;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(newMaxSlot);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            newMaxSlot = reader.ReadSByte();
            if (newMaxSlot < 0)
                throw new Exception("Forbidden value on newMaxSlot = " + newMaxSlot + ", it doesn't respect the following condition : newMaxSlot < 0");
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(sbyte);
        }
        
    }
    
}