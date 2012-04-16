// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameActionFightCastRequestMessage.xml' the '04/04/2012 14:27:19'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameActionFightCastRequestMessage : Message
	{
		public const uint Id = 1005;
		public override uint MessageId
		{
			get
			{
				return 1005;
			}
		}
		
		public short spellId;
		public short cellId;
		
		public GameActionFightCastRequestMessage()
		{
		}
		
		public GameActionFightCastRequestMessage(short spellId, short cellId)
		{
			this.spellId = spellId;
			this.cellId = cellId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(spellId);
			writer.WriteShort(cellId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			spellId = reader.ReadShort();
			if ( spellId < 0 )
			{
				throw new Exception("Forbidden value on spellId = " + spellId + ", it doesn't respect the following condition : spellId < 0");
			}
			cellId = reader.ReadShort();
			if ( cellId < -1 || cellId > 559 )
			{
				throw new Exception("Forbidden value on cellId = " + cellId + ", it doesn't respect the following condition : cellId < -1 || cellId > 559");
			}
		}
	}
}
