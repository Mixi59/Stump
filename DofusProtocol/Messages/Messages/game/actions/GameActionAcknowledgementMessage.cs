

// Generated on 09/01/2014 15:51:49
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class GameActionAcknowledgementMessage : Message
    {
        public const uint Id = 957;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public bool valid;
        public sbyte actionId;
        
        public GameActionAcknowledgementMessage()
        {
        }
        
        public GameActionAcknowledgementMessage(bool valid, sbyte actionId)
        {
            this.valid = valid;
            this.actionId = actionId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(valid);
            writer.WriteSByte(actionId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            valid = reader.ReadBoolean();
            actionId = reader.ReadSByte();
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(bool) + sizeof(sbyte);
        }
        
    }
    
}