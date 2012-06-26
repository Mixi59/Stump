// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeStartedWithStorageMessage.xml' the '26/06/2012 18:47:57'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeStartedWithStorageMessage : ExchangeStartedMessage
	{
		public const uint Id = 6236;
		public override uint MessageId
		{
			get
			{
				return 6236;
			}
		}
		
		public int storageMaxSlot;
		
		public ExchangeStartedWithStorageMessage()
		{
		}
		
		public ExchangeStartedWithStorageMessage(sbyte exchangeType, int storageMaxSlot)
			 : base(exchangeType)
		{
			this.storageMaxSlot = storageMaxSlot;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(storageMaxSlot);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			storageMaxSlot = reader.ReadInt();
			if ( storageMaxSlot < 0 )
			{
				throw new Exception("Forbidden value on storageMaxSlot = " + storageMaxSlot + ", it doesn't respect the following condition : storageMaxSlot < 0");
			}
		}
	}
}
