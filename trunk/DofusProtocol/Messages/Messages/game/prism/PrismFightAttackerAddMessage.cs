

// Generated on 08/11/2013 11:29:03
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class PrismFightAttackerAddMessage : Message
    {
        public const uint Id = 5893;
        public override uint MessageId
        {
            get { return Id; }
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
            var limit = reader.ReadUShort();
            charactersDescription = new Types.CharacterMinimalPlusLookAndGradeInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 (charactersDescription as Types.CharacterMinimalPlusLookAndGradeInformations[])[i] = new Types.CharacterMinimalPlusLookAndGradeInformations();
                 (charactersDescription as Types.CharacterMinimalPlusLookAndGradeInformations[])[i].Deserialize(reader);
            }
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(double) + sizeof(short) + charactersDescription.Sum(x => x.GetSerializationSize());
        }
        
    }
    
}