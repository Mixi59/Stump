// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameActionFightNoSpellCastMessage.xml' the '26/06/2012 18:47:44'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameActionFightNoSpellCastMessage : Message
	{
		public const uint Id = 6132;
		public override uint MessageId
		{
			get
			{
				return 6132;
			}
		}
		
		public int spellLevelId;
		
		public GameActionFightNoSpellCastMessage()
		{
		}
		
		public GameActionFightNoSpellCastMessage(int spellLevelId)
		{
			this.spellLevelId = spellLevelId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(spellLevelId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			spellLevelId = reader.ReadInt();
			if ( spellLevelId < 0 )
			{
				throw new Exception("Forbidden value on spellLevelId = " + spellLevelId + ", it doesn't respect the following condition : spellLevelId < 0");
			}
		}
	}
}
