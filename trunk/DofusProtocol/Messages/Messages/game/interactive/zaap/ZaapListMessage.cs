// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ZaapListMessage.xml' the '26/06/2012 18:47:55'
using System;
using Stump.Core.IO;
using System.Collections.Generic;

namespace Stump.DofusProtocol.Messages
{
	public class ZaapListMessage : TeleportDestinationsListMessage
	{
		public const uint Id = 1604;
		public override uint MessageId
		{
			get
			{
				return 1604;
			}
		}
		
		public int spawnMapId;
		
		public ZaapListMessage()
		{
		}
		
		public ZaapListMessage(sbyte teleporterType, IEnumerable<int> mapIds, IEnumerable<short> subAreaIds, IEnumerable<short> costs, int spawnMapId)
			 : base(teleporterType, mapIds, subAreaIds, costs)
		{
			this.spawnMapId = spawnMapId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(spawnMapId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			spawnMapId = reader.ReadInt();
			if ( spawnMapId < 0 )
			{
				throw new Exception("Forbidden value on spawnMapId = " + spawnMapId + ", it doesn't respect the following condition : spawnMapId < 0");
			}
		}
	}
}
