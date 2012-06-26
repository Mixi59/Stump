// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'CharacterReplayRequestMessage.xml' the '26/06/2012 18:47:46'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class CharacterReplayRequestMessage : Message
	{
		public const uint Id = 167;
		public override uint MessageId
		{
			get
			{
				return 167;
			}
		}
		
		public int characterId;
		
		public CharacterReplayRequestMessage()
		{
		}
		
		public CharacterReplayRequestMessage(int characterId)
		{
			this.characterId = characterId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(characterId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			characterId = reader.ReadInt();
			if ( characterId < 0 )
			{
				throw new Exception("Forbidden value on characterId = " + characterId + ", it doesn't respect the following condition : characterId < 0");
			}
		}
	}
}
