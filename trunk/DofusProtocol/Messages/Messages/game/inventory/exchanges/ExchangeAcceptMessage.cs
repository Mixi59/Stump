// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeAcceptMessage.xml' the '26/06/2012 18:47:56'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeAcceptMessage : Message
	{
		public const uint Id = 5508;
		public override uint MessageId
		{
			get
			{
				return 5508;
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
