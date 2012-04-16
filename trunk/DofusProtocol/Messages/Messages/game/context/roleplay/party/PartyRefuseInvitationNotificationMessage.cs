// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PartyRefuseInvitationNotificationMessage.xml' the '04/04/2012 14:27:29'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class PartyRefuseInvitationNotificationMessage : AbstractPartyEventMessage
	{
		public const uint Id = 5596;
		public override uint MessageId
		{
			get
			{
				return 5596;
			}
		}
		
		public int guestId;
		
		public PartyRefuseInvitationNotificationMessage()
		{
		}
		
		public PartyRefuseInvitationNotificationMessage(int partyId, int guestId)
			 : base(partyId)
		{
			this.guestId = guestId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(guestId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			guestId = reader.ReadInt();
			if ( guestId < 0 )
			{
				throw new Exception("Forbidden value on guestId = " + guestId + ", it doesn't respect the following condition : guestId < 0");
			}
		}
	}
}
