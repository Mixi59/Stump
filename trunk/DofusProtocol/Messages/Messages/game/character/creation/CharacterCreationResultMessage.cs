// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'CharacterCreationResultMessage.xml' the '04/04/2012 14:27:22'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class CharacterCreationResultMessage : Message
	{
		public const uint Id = 161;
		public override uint MessageId
		{
			get
			{
				return 161;
			}
		}
		
		public sbyte result;
		
		public CharacterCreationResultMessage()
		{
		}
		
		public CharacterCreationResultMessage(sbyte result)
		{
			this.result = result;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(result);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			result = reader.ReadSByte();
			if ( result < 0 )
			{
				throw new Exception("Forbidden value on result = " + result + ", it doesn't respect the following condition : result < 0");
			}
		}
	}
}
