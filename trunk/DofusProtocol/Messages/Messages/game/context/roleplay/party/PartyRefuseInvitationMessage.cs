using System;
using System.Collections.Generic;
using Stump.BaseCore.Framework.Utils;
using Stump.BaseCore.Framework.IO;
using Stump.DofusProtocol.Classes;
namespace Stump.DofusProtocol.Messages
{
	
	public class PartyRefuseInvitationMessage : Message
	{
		public const uint protocolId = 5582;
		
		public PartyRefuseInvitationMessage()
		{
		}
		
		public override uint getMessageId()
		{
			return 5582;
		}
		
		public PartyRefuseInvitationMessage initPartyRefuseInvitationMessage()
		{
			return this;
		}
		
		public override void reset()
		{
		}
		
		public override void pack(BigEndianWriter arg1)
		{
			this.serialize(arg1);
			WritePacket(arg1, this.getMessageId());
		}
		
		public override void unpack(BigEndianReader arg1, uint arg2)
		{
			this.deserialize(arg1);
		}
		
		public virtual void serialize(BigEndianWriter arg1)
		{
		}
		
		public void serializeAs_PartyRefuseInvitationMessage(BigEndianWriter arg1)
		{
		}
		
		public virtual void deserialize(BigEndianReader arg1)
		{
		}
		
		public void deserializeAs_PartyRefuseInvitationMessage(BigEndianReader arg1)
		{
		}
		
	}
}
