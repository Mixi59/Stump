// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'SpellListMessage.xml' the '26/06/2012 18:47:59'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class SpellListMessage : Message
	{
		public const uint Id = 1200;
		public override uint MessageId
		{
			get
			{
				return 1200;
			}
		}
		
		public bool spellPrevisualization;
		public IEnumerable<Types.SpellItem> spells;
		
		public SpellListMessage()
		{
		}
		
		public SpellListMessage(bool spellPrevisualization, IEnumerable<Types.SpellItem> spells)
		{
			this.spellPrevisualization = spellPrevisualization;
			this.spells = spells;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteBoolean(spellPrevisualization);
			writer.WriteUShort((ushort)spells.Count());
			foreach (var entry in spells)
			{
				entry.Serialize(writer);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			spellPrevisualization = reader.ReadBoolean();
			int limit = reader.ReadUShort();
			spells = new Types.SpellItem[limit];
			for (int i = 0; i < limit; i++)
			{
				(spells as Types.SpellItem[])[i] = new Types.SpellItem();
				(spells as Types.SpellItem[])[i].Deserialize(reader);
			}
		}
	}
}
