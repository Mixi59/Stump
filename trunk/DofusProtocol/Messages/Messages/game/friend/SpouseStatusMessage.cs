// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'SpouseStatusMessage.xml' the '04/04/2012 14:27:30'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class SpouseStatusMessage : Message
	{
		public const uint Id = 6265;
		public override uint MessageId
		{
			get
			{
				return 6265;
			}
		}
		
		public bool hasSpouse;
		
		public SpouseStatusMessage()
		{
		}
		
		public SpouseStatusMessage(bool hasSpouse)
		{
			this.hasSpouse = hasSpouse;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteBoolean(hasSpouse);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			hasSpouse = reader.ReadBoolean();
		}
	}
}
