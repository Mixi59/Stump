

// Generated on 03/06/2014 18:50:18
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class GuildMemberSetWarnOnConnectionMessage : Message
    {
        public const uint Id = 6159;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public bool enable;
        
        public GuildMemberSetWarnOnConnectionMessage()
        {
        }
        
        public GuildMemberSetWarnOnConnectionMessage(bool enable)
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