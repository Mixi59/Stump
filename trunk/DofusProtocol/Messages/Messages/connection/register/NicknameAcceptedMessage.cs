// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'NicknameAcceptedMessage.xml' the '04/04/2012 14:27:19'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class NicknameAcceptedMessage : Message
	{
		public const uint Id = 5641;
		public override uint MessageId
		{
			get
			{
				return 5641;
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
