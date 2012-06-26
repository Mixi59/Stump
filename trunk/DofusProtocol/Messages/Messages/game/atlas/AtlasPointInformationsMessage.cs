// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'AtlasPointInformationsMessage.xml' the '26/06/2012 18:47:45'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class AtlasPointInformationsMessage : Message
	{
		public const uint Id = 5956;
		public override uint MessageId
		{
			get
			{
				return 5956;
			}
		}
		
		public Types.AtlasPointsInformations type;
		
		public AtlasPointInformationsMessage()
		{
		}
		
		public AtlasPointInformationsMessage(Types.AtlasPointsInformations type)
		{
			this.type = type;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			type.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			type = new Types.AtlasPointsInformations();
			type.Deserialize(reader);
		}
	}
}
