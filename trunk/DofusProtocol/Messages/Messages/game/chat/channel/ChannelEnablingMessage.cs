// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ChannelEnablingMessage.xml' the '26/06/2012 18:47:47'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ChannelEnablingMessage : Message
	{
		public const uint Id = 890;
		public override uint MessageId
		{
			get
			{
				return 890;
			}
		}
		
		public sbyte channel;
		public bool enable;
		
		public ChannelEnablingMessage()
		{
		}
		
		public ChannelEnablingMessage(sbyte channel, bool enable)
		{
			this.channel = channel;
			this.enable = enable;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(channel);
			writer.WriteBoolean(enable);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			channel = reader.ReadSByte();
			if ( channel < 0 )
			{
				throw new Exception("Forbidden value on channel = " + channel + ", it doesn't respect the following condition : channel < 0");
			}
			enable = reader.ReadBoolean();
		}
	}
}
