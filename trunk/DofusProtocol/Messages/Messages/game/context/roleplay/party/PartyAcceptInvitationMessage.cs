// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PartyAcceptInvitationMessage.xml' the '26/06/2012 18:47:51'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class PartyAcceptInvitationMessage : AbstractPartyMessage
	{
		public const uint Id = 5580;
		public override uint MessageId
		{
			get
			{
				return 5580;
			}
		}
		
		
		public PartyAcceptInvitationMessage()
		{
		}
		
		public PartyAcceptInvitationMessage(int partyId)
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
