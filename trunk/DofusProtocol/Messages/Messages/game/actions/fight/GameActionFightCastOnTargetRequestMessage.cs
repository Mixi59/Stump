// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameActionFightCastOnTargetRequestMessage.xml' the '26/06/2012 18:47:44'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameActionFightCastOnTargetRequestMessage : Message
	{
		public const uint Id = 6330;
		public override uint MessageId
		{
			get
			{
				return 6330;
			}
		}
		
		public short spellId;
		public int targetId;
		
		public GameActionFightCastOnTargetRequestMessage()
		{
		}
		
		public GameActionFightCastOnTargetRequestMessage(short spellId, int targetId)
		{
			this.spellId = spellId;
			this.targetId = targetId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(spellId);
			writer.WriteInt(targetId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			spellId = reader.ReadShort();
			if ( spellId < 0 )
			{
				throw new Exception("Forbidden value on spellId = " + spellId + ", it doesn't respect the following condition : spellId < 0");
			}
			targetId = reader.ReadInt();
		}
	}
}
