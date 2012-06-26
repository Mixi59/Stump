// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PartyFollowMemberRequestMessage.xml' the '26/06/2012 18:47:51'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class PartyFollowMemberRequestMessage : AbstractPartyMessage
	{
		public const uint Id = 5577;
		public override uint MessageId
		{
			get
			{
				return 5577;
			}
		}
		
		public int playerId;
		
		public PartyFollowMemberRequestMessage()
		{
		}
		
		public PartyFollowMemberRequestMessage(int partyId, int playerId)
			 : base(partyId)
		{
			this.playerId = playerId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(playerId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			playerId = reader.ReadInt();
			if ( playerId < 0 )
			{
				throw new Exception("Forbidden value on playerId = " + playerId + ", it doesn't respect the following condition : playerId < 0");
			}
		}
	}
}
