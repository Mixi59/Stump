// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameActionAcknowledgementMessage.xml' the '04/04/2012 14:27:19'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameActionAcknowledgementMessage : Message
	{
		public const uint Id = 957;
		public override uint MessageId
		{
			get
			{
				return 957;
			}
		}
		
		public bool valid;
		public sbyte actionId;
		
		public GameActionAcknowledgementMessage()
		{
		}
		
		public GameActionAcknowledgementMessage(bool valid, sbyte actionId)
		{
			this.valid = valid;
			this.actionId = actionId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteBoolean(valid);
			writer.WriteSByte(actionId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			valid = reader.ReadBoolean();
			actionId = reader.ReadSByte();
		}
	}
}
