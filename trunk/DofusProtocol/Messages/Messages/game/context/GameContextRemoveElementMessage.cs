// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameContextRemoveElementMessage.xml' the '26/06/2012 18:47:47'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameContextRemoveElementMessage : Message
	{
		public const uint Id = 251;
		public override uint MessageId
		{
			get
			{
				return 251;
			}
		}
		
		public int id;
		
		public GameContextRemoveElementMessage()
		{
		}
		
		public GameContextRemoveElementMessage(int id)
		{
			this.id = id;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(id);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			id = reader.ReadInt();
		}
	}
}
