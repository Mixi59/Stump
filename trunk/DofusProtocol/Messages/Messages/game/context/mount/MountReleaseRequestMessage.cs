// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'MountReleaseRequestMessage.xml' the '26/06/2012 18:47:48'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class MountReleaseRequestMessage : Message
	{
		public const uint Id = 5980;
		public override uint MessageId
		{
			get
			{
				return 5980;
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
