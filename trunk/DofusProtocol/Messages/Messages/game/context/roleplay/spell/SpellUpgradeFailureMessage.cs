// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'SpellUpgradeFailureMessage.xml' the '26/06/2012 18:47:53'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class SpellUpgradeFailureMessage : Message
	{
		public const uint Id = 1202;
		public override uint MessageId
		{
			get
			{
				return 1202;
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
