

// Generated on 07/26/2013 22:51:02
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class GuildInformationsMemberUpdateMessage : Message
    {
        public const uint Id = 5597;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public Types.GuildMember member;
        
        public GuildInformationsMemberUpdateMessage()
        {
        }
        
        public GuildInformationsMemberUpdateMessage(Types.GuildMember member)
        {
            this.member = member;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            member.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            member = new Types.GuildMember();
            member.Deserialize(reader);
        }
        
        public override int GetSerializationSize()
        {
            return member.GetSerializationSize();
        }
        
    }
    
}