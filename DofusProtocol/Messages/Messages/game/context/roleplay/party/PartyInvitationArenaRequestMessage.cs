

// Generated on 09/01/2015 10:48:13
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class PartyInvitationArenaRequestMessage : PartyInvitationRequestMessage
    {
        public const uint Id = 6283;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        
        public PartyInvitationArenaRequestMessage()
        {
        }
        
        public PartyInvitationArenaRequestMessage(string name)
         : base(name)
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }
        
    }
    
}