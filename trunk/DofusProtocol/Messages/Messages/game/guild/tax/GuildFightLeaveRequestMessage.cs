// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GuildFightLeaveRequestMessage.xml' the '26/06/2012 18:47:55'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GuildFightLeaveRequestMessage : Message
	{
		public const uint Id = 5715;
		public override uint MessageId
		{
			get
			{
				return 5715;
			}
		}
		
		public int taxCollectorId;
		public int characterId;
		
		public GuildFightLeaveRequestMessage()
		{
		}
		
		public GuildFightLeaveRequestMessage(int taxCollectorId, int characterId)
		{
			this.taxCollectorId = taxCollectorId;
			this.characterId = characterId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(taxCollectorId);
			writer.WriteInt(characterId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			taxCollectorId = reader.ReadInt();
			characterId = reader.ReadInt();
			if ( characterId < 0 )
			{
				throw new Exception("Forbidden value on characterId = " + characterId + ", it doesn't respect the following condition : characterId < 0");
			}
		}
	}
}
