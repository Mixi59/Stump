// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GuildInvitationStateRecrutedMessage.xml' the '04/04/2012 14:27:31'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GuildInvitationStateRecrutedMessage : Message
	{
		public const uint Id = 5548;
		public override uint MessageId
		{
			get
			{
				return 5548;
			}
		}
		
		public sbyte invitationState;
		
		public GuildInvitationStateRecrutedMessage()
		{
		}
		
		public GuildInvitationStateRecrutedMessage(sbyte invitationState)
		{
			this.invitationState = invitationState;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(invitationState);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			invitationState = reader.ReadSByte();
			if ( invitationState < 0 )
			{
				throw new Exception("Forbidden value on invitationState = " + invitationState + ", it doesn't respect the following condition : invitationState < 0");
			}
		}
	}
}
