// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'IgnoredDeleteResultMessage.xml' the '26/06/2012 18:47:54'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class IgnoredDeleteResultMessage : Message
	{
		public const uint Id = 5677;
		public override uint MessageId
		{
			get
			{
				return 5677;
			}
		}
		
		public bool success;
		public bool session;
		public string name;
		
		public IgnoredDeleteResultMessage()
		{
		}
		
		public IgnoredDeleteResultMessage(bool success, bool session, string name)
		{
			this.success = success;
			this.session = session;
			this.name = name;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			byte flag1 = 0;
			flag1 = BooleanByteWrapper.SetFlag(flag1, 0, success);
			flag1 = BooleanByteWrapper.SetFlag(flag1, 1, session);
			writer.WriteByte(flag1);
			writer.WriteUTF(name);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			byte flag1 = reader.ReadByte();
			success = BooleanByteWrapper.GetFlag(flag1, 0);
			session = BooleanByteWrapper.GetFlag(flag1, 1);
			name = reader.ReadUTF();
		}
	}
}
