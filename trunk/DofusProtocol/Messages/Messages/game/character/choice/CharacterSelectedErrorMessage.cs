// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'CharacterSelectedErrorMessage.xml' the '26/06/2012 18:47:46'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class CharacterSelectedErrorMessage : Message
	{
		public const uint Id = 5836;
		public override uint MessageId
		{
			get
			{
				return 5836;
			}
		}
		
		
		public override void Serialize(IDataWriter writer)
		{
		}
		
		public override void Deserialize(IDataReader reader)
		{
		}
	}
}
