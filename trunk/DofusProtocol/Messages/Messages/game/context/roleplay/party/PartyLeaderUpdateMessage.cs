// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PartyLeaderUpdateMessage.xml' the '26/06/2012 18:47:52'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class PartyLeaderUpdateMessage : AbstractPartyEventMessage
	{
		public const uint Id = 5578;
		public override uint MessageId
		{
			get
			{
				return 5578;
			}
		}
		
		public int partyLeaderId;
		
		public PartyLeaderUpdateMessage()
		{
		}
		
		public PartyLeaderUpdateMessage(int partyId, int partyLeaderId)
			 : base(partyId)
		{
			this.partyLeaderId = partyLeaderId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(partyLeaderId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			partyLeaderId = reader.ReadInt();
			if ( partyLeaderId < 0 )
			{
				throw new Exception("Forbidden value on partyLeaderId = " + partyLeaderId + ", it doesn't respect the following condition : partyLeaderId < 0");
			}
		}
	}
}
