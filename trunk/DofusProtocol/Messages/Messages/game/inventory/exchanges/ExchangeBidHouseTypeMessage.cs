// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeBidHouseTypeMessage.xml' the '26/06/2012 18:47:56'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeBidHouseTypeMessage : Message
	{
		public const uint Id = 5803;
		public override uint MessageId
		{
			get
			{
				return 5803;
			}
		}
		
		public int type;
		
		public ExchangeBidHouseTypeMessage()
		{
		}
		
		public ExchangeBidHouseTypeMessage(int type)
		{
			this.type = type;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(type);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			type = reader.ReadInt();
			if ( type < 0 )
			{
				throw new Exception("Forbidden value on type = " + type + ", it doesn't respect the following condition : type < 0");
			}
		}
	}
}
