// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GuildMemberSetWarnOnConnectionMessage.xml' the '26/06/2012 18:47:54'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GuildMemberSetWarnOnConnectionMessage : Message
	{
		public const uint Id = 6159;
		public override uint MessageId
		{
			get
			{
				return 6159;
			}
		}
		
		public bool enable;
		
		public GuildMemberSetWarnOnConnectionMessage()
		{
		}
		
		public GuildMemberSetWarnOnConnectionMessage(bool enable)
		{
			this.enable = enable;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteBoolean(enable);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			enable = reader.ReadBoolean();
		}
	}
}
