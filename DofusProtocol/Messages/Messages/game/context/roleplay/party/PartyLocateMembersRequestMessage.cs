

// Generated on 10/30/2016 16:20:34
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class PartyLocateMembersRequestMessage : AbstractPartyMessage
    {
        public const uint Id = 5587;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        
        public PartyLocateMembersRequestMessage()
        {
        }
        
        public PartyLocateMembersRequestMessage(int partyId)
         : base(partyId)
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