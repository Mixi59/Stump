// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'DebugHighlightCellsMessage.xml' the '26/06/2012 18:47:44'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class DebugHighlightCellsMessage : Message
	{
		public const uint Id = 2001;
		public override uint MessageId
		{
			get
			{
				return 2001;
			}
		}
		
		public int color;
		public IEnumerable<short> cells;
		
		public DebugHighlightCellsMessage()
		{
		}
		
		public DebugHighlightCellsMessage(int color, IEnumerable<short> cells)
		{
			this.color = color;
			this.cells = cells;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(color);
			writer.WriteUShort((ushort)cells.Count());
			foreach (var entry in cells)
			{
				writer.WriteShort(entry);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			color = reader.ReadInt();
			int limit = reader.ReadUShort();
			cells = new short[limit];
			for (int i = 0; i < limit; i++)
			{
				(cells as short[])[i] = reader.ReadShort();
			}
		}
	}
}
