

// Generated on 12/12/2013 16:56:53
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class PlayerStatusUpdateRequestMessage : Message
    {
        public const uint Id = 6387;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public Types.PlayerStatus status;
        
        public PlayerStatusUpdateRequestMessage()
        {
        }
        
        public PlayerStatusUpdateRequestMessage(Types.PlayerStatus status)
        {
            this.status = status;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(status.TypeId);
            status.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            status = Types.ProtocolTypeManager.GetInstance<Types.PlayerStatus>(reader.ReadShort());
            status.Deserialize(reader);
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(short) + status.GetSerializationSize();
        }
        
    }
    
}