// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GuildInformations.xml' the '24/06/2011 12:04:58'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class GuildInformations : BasicGuildInformations
	{
		public const uint Id = 127;
		public override short TypeId
		{
			get
			{
				return 127;
			}
		}
		
		public Types.GuildEmblem guildEmblem;
		
		public GuildInformations()
		{
		}
		
		public GuildInformations(int guildId, string guildName, Types.GuildEmblem guildEmblem)
			 : base(guildId, guildName)
		{
			this.guildEmblem = guildEmblem;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			guildEmblem.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			guildEmblem = new Types.GuildEmblem();
			guildEmblem.Deserialize(reader);
		}
	}
}