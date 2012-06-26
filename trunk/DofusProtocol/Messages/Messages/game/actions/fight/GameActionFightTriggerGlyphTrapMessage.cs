// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameActionFightTriggerGlyphTrapMessage.xml' the '26/06/2012 18:47:45'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameActionFightTriggerGlyphTrapMessage : AbstractGameActionMessage
	{
		public const uint Id = 5741;
		public override uint MessageId
		{
			get
			{
				return 5741;
			}
		}
		
		public short markId;
		public int triggeringCharacterId;
		public short triggeredSpellId;
		
		public GameActionFightTriggerGlyphTrapMessage()
		{
		}
		
		public GameActionFightTriggerGlyphTrapMessage(short actionId, int sourceId, short markId, int triggeringCharacterId, short triggeredSpellId)
			 : base(actionId, sourceId)
		{
			this.markId = markId;
			this.triggeringCharacterId = triggeringCharacterId;
			this.triggeredSpellId = triggeredSpellId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteShort(markId);
			writer.WriteInt(triggeringCharacterId);
			writer.WriteShort(triggeredSpellId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			markId = reader.ReadShort();
			triggeringCharacterId = reader.ReadInt();
			triggeredSpellId = reader.ReadShort();
			if ( triggeredSpellId < 0 )
			{
				throw new Exception("Forbidden value on triggeredSpellId = " + triggeredSpellId + ", it doesn't respect the following condition : triggeredSpellId < 0");
			}
		}
	}
}
