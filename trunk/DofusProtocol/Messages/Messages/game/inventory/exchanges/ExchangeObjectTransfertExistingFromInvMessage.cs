// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeObjectTransfertExistingFromInvMessage.xml' the '26/06/2012 18:47:57'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeObjectTransfertExistingFromInvMessage : Message
	{
		public const uint Id = 6325;
		public override uint MessageId
		{
			get
			{
				return 6325;
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
