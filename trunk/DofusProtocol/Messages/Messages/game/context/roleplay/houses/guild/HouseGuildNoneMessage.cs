// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'HouseGuildNoneMessage.xml' the '26/06/2012 18:47:50'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class HouseGuildNoneMessage : Message
	{
		public const uint Id = 5701;
		public override uint MessageId
		{
			get
			{
				return 5701;
			}
		}
		
		public short houseId;
		
		public HouseGuildNoneMessage()
		{
		}
		
		public HouseGuildNoneMessage(short houseId)
		{
			this.houseId = houseId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(houseId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			houseId = reader.ReadShort();
			if ( houseId < 0 )
			{
				throw new Exception("Forbidden value on houseId = " + houseId + ", it doesn't respect the following condition : houseId < 0");
			}
		}
	}
}
