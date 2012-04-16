// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeItemGoldAddAsPaymentMessage.xml' the '04/04/2012 14:27:32'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeItemGoldAddAsPaymentMessage : Message
	{
		public const uint Id = 5770;
		public override uint MessageId
		{
			get
			{
				return 5770;
			}
		}
		
		public sbyte paymentType;
		public int quantity;
		
		public ExchangeItemGoldAddAsPaymentMessage()
		{
		}
		
		public ExchangeItemGoldAddAsPaymentMessage(sbyte paymentType, int quantity)
		{
			this.paymentType = paymentType;
			this.quantity = quantity;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(paymentType);
			writer.WriteInt(quantity);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			paymentType = reader.ReadSByte();
			quantity = reader.ReadInt();
			if ( quantity < 0 )
			{
				throw new Exception("Forbidden value on quantity = " + quantity + ", it doesn't respect the following condition : quantity < 0");
			}
		}
	}
}
