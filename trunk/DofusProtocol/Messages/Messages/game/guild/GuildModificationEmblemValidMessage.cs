// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GuildModificationEmblemValidMessage.xml' the '26/06/2012 18:47:55'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GuildModificationEmblemValidMessage : Message
	{
		public const uint Id = 6328;
		public override uint MessageId
		{
			get
			{
				return 6328;
			}
		}
		
		public Types.GuildEmblem guildEmblem;
		
		public GuildModificationEmblemValidMessage()
		{
		}
		
		public GuildModificationEmblemValidMessage(Types.GuildEmblem guildEmblem)
		{
			this.guildEmblem = guildEmblem;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			guildEmblem.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			guildEmblem = new Types.GuildEmblem();
			guildEmblem.Deserialize(reader);
		}
	}
}
