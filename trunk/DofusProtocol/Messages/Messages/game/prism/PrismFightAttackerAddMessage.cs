// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PrismFightAttackerAddMessage.xml' the '04/04/2012 14:27:35'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class PrismFightAttackerAddMessage : Message
	{
		public const uint Id = 5893;
		public override uint MessageId
		{
			get
			{
				return 5893;
			}
		}
		
		public double fightId;
		public IEnumerable<Types.CharacterMinimalPlusLookAndGradeInformations> charactersDescription;
		
		public PrismFightAttackerAddMessage()
		{
		}
		
		public PrismFightAttackerAddMessage(double fightId, IEnumerable<Types.CharacterMinimalPlusLookAndGradeInformations> charactersDescription)
		{
			this.fightId = fightId;
			this.charactersDescription = charactersDescription;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteDouble(fightId);
			writer.WriteUShort((ushort)charactersDescription.Count());
			foreach (var entry in charactersDescription)
			{
				entry.Serialize(writer);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			fightId = reader.ReadDouble();
			int limit = reader.ReadUShort();
			charactersDescription = new Types.CharacterMinimalPlusLookAndGradeInformations[limit];
			for (int i = 0; i < limit; i++)
			{
				(charactersDescription as Types.CharacterMinimalPlusLookAndGradeInformations[])[i] = new Types.CharacterMinimalPlusLookAndGradeInformations();
				(charactersDescription as Types.CharacterMinimalPlusLookAndGradeInformations[])[i].Deserialize(reader);
			}
		}
	}
}
