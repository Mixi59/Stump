// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'CharacterDeletionRequestMessage.xml' the '04/04/2012 14:27:22'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class CharacterDeletionRequestMessage : Message
	{
		public const uint Id = 165;
		public override uint MessageId
		{
			get
			{
				return 165;
			}
		}
		
		public int characterId;
		public string secretAnswerHash;
		
		public CharacterDeletionRequestMessage()
		{
		}
		
		public CharacterDeletionRequestMessage(int characterId, string secretAnswerHash)
		{
			this.characterId = characterId;
			this.secretAnswerHash = secretAnswerHash;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(characterId);
			writer.WriteUTF(secretAnswerHash);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			characterId = reader.ReadInt();
			if ( characterId < 0 )
			{
				throw new Exception("Forbidden value on characterId = " + characterId + ", it doesn't respect the following condition : characterId < 0");
			}
			secretAnswerHash = reader.ReadUTF();
		}
	}
}
