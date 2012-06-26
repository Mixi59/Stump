// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeIsReadyMessage.xml' the '26/06/2012 18:47:56'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeIsReadyMessage : Message
	{
		public const uint Id = 5509;
		public override uint MessageId
		{
			get
			{
				return 5509;
			}
		}
		
		public int id;
		public bool ready;
		
		public ExchangeIsReadyMessage()
		{
		}
		
		public ExchangeIsReadyMessage(int id, bool ready)
		{
			this.id = id;
			this.ready = ready;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(id);
			writer.WriteBoolean(ready);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			id = reader.ReadInt();
			if ( id < 0 )
			{
				throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < 0");
			}
			ready = reader.ReadBoolean();
		}
	}
}
