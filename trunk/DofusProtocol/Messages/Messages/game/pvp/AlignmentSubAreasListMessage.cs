// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'AlignmentSubAreasListMessage.xml' the '26/06/2012 18:48:00'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class AlignmentSubAreasListMessage : Message
	{
		public const uint Id = 6059;
		public override uint MessageId
		{
			get
			{
				return 6059;
			}
		}
		
		public IEnumerable<short> angelsSubAreas;
		public IEnumerable<short> evilsSubAreas;
		
		public AlignmentSubAreasListMessage()
		{
		}
		
		public AlignmentSubAreasListMessage(IEnumerable<short> angelsSubAreas, IEnumerable<short> evilsSubAreas)
		{
			this.angelsSubAreas = angelsSubAreas;
			this.evilsSubAreas = evilsSubAreas;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)angelsSubAreas.Count());
			foreach (var entry in angelsSubAreas)
			{
				writer.WriteShort(entry);
			}
			writer.WriteUShort((ushort)evilsSubAreas.Count());
			foreach (var entry in evilsSubAreas)
			{
				writer.WriteShort(entry);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			angelsSubAreas = new short[limit];
			for (int i = 0; i < limit; i++)
			{
				(angelsSubAreas as short[])[i] = reader.ReadShort();
			}
			limit = reader.ReadUShort();
			evilsSubAreas = new short[limit];
			for (int i = 0; i < limit; i++)
			{
				(evilsSubAreas as short[])[i] = reader.ReadShort();
			}
		}
	}
}
