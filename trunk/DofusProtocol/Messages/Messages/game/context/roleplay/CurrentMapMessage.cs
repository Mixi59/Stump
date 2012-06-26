// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'CurrentMapMessage.xml' the '26/06/2012 18:47:49'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class CurrentMapMessage : Message
	{
		public const uint Id = 220;
		public override uint MessageId
		{
			get
			{
				return 220;
			}
		}
		
		public int mapId;
		public string mapKey;
		
		public CurrentMapMessage()
		{
		}
		
		public CurrentMapMessage(int mapId, string mapKey)
		{
			this.mapId = mapId;
			this.mapKey = mapKey;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(mapId);
			writer.WriteUTF(mapKey);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			mapId = reader.ReadInt();
			if ( mapId < 0 )
			{
				throw new Exception("Forbidden value on mapId = " + mapId + ", it doesn't respect the following condition : mapId < 0");
			}
			mapKey = reader.ReadUTF();
		}
	}
}
