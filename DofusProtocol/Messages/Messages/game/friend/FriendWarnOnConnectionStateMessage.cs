

// Generated on 09/01/2014 15:52:05
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class FriendWarnOnConnectionStateMessage : Message
    {
        public const uint Id = 5630;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public bool enable;
        
        public FriendWarnOnConnectionStateMessage()
        {
        }
        
        public FriendWarnOnConnectionStateMessage(bool enable)
        {
            this.enable = enable;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(enable);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            enable = reader.ReadBoolean();
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(bool);
        }
        
    }
    
}