// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ItemNoMoreAvailableMessage.xml' the '26/06/2012 18:47:58'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ItemNoMoreAvailableMessage : Message
	{
		public const uint Id = 5769;
		public override uint MessageId
		{
			get
			{
				return 5769;
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
