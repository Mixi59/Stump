// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'IgnoredGetListMessage.xml' the '04/04/2012 14:27:30'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class IgnoredGetListMessage : Message
	{
		public const uint Id = 5676;
		public override uint MessageId
		{
			get
			{
				return 5676;
			}
		}
		
		
		public override void Serialize(IDataWriter writer)
		{
		}
		
		public override void Deserialize(IDataReader reader)
		{
		}
	}
}
