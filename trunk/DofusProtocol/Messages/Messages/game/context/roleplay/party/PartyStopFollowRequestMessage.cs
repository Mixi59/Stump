// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PartyStopFollowRequestMessage.xml' the '26/06/2012 18:47:53'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class PartyStopFollowRequestMessage : AbstractPartyMessage
	{
		public const uint Id = 5574;
		public override uint MessageId
		{
			get
			{
				return 5574;
			}
		}
		
		
		public PartyStopFollowRequestMessage()
		{
		}
		
		public PartyStopFollowRequestMessage(int partyId)
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
