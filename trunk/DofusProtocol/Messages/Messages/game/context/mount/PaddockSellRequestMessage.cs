// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PaddockSellRequestMessage.xml' the '26/06/2012 18:47:49'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class PaddockSellRequestMessage : Message
	{
		public const uint Id = 5953;
		public override uint MessageId
		{
			get
			{
				return 5953;
			}
		}
		
		public int price;
		
		public PaddockSellRequestMessage()
		{
		}
		
		public PaddockSellRequestMessage(int price)
		{
			this.price = price;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(price);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			price = reader.ReadInt();
			if ( price < 0 )
			{
				throw new Exception("Forbidden value on price = " + price + ", it doesn't respect the following condition : price < 0");
			}
		}
	}
}
