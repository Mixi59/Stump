// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'FriendDeleteResultMessage.xml' the '04/04/2012 14:27:30'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class FriendDeleteResultMessage : Message
	{
		public const uint Id = 5601;
		public override uint MessageId
		{
			get
			{
				return 5601;
			}
		}
		
		public bool success;
		public string name;
		
		public FriendDeleteResultMessage()
		{
		}
		
		public FriendDeleteResultMessage(bool success, string name)
		{
			this.success = success;
			this.name = name;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteBoolean(success);
			writer.WriteUTF(name);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			success = reader.ReadBoolean();
			name = reader.ReadUTF();
		}
	}
}
