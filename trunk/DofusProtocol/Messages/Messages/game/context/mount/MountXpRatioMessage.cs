// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'MountXpRatioMessage.xml' the '26/06/2012 18:47:49'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class MountXpRatioMessage : Message
	{
		public const uint Id = 5970;
		public override uint MessageId
		{
			get
			{
				return 5970;
			}
		}
		
		public sbyte ratio;
		
		public MountXpRatioMessage()
		{
		}
		
		public MountXpRatioMessage(sbyte ratio)
		{
			this.ratio = ratio;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(ratio);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			ratio = reader.ReadSByte();
			if ( ratio < 0 )
			{
				throw new Exception("Forbidden value on ratio = " + ratio + ", it doesn't respect the following condition : ratio < 0");
			}
		}
	}
}
