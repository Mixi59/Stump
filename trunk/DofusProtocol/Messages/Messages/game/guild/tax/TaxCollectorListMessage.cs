

// Generated on 08/11/2013 11:28:48
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class TaxCollectorListMessage : Message
    {
        public const uint Id = 5930;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public sbyte nbcollectorMax;
        public short taxCollectorHireCost;
        public IEnumerable<Types.TaxCollectorInformations> informations;
        public IEnumerable<Types.TaxCollectorFightersInformation> fightersInformations;
        
        public TaxCollectorListMessage()
        {
        }
        
        public TaxCollectorListMessage(sbyte nbcollectorMax, short taxCollectorHireCost, IEnumerable<Types.TaxCollectorInformations> informations, IEnumerable<Types.TaxCollectorFightersInformation> fightersInformations)
        {
            this.nbcollectorMax = nbcollectorMax;
            this.taxCollectorHireCost = taxCollectorHireCost;
            this.informations = informations;
            this.fightersInformations = fightersInformations;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(nbcollectorMax);
            writer.WriteShort(taxCollectorHireCost);
            writer.WriteUShort((ushort)informations.Count());
            foreach (var entry in informations)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
            }
            writer.WriteUShort((ushort)fightersInformations.Count());
            foreach (var entry in fightersInformations)
            {
                 entry.Serialize(writer);
            }
        }
        
        public override void Deserialize(IDataReader reader)
        {
            nbcollectorMax = reader.ReadSByte();
            if (nbcollectorMax < 0)
                throw new Exception("Forbidden value on nbcollectorMax = " + nbcollectorMax + ", it doesn't respect the following condition : nbcollectorMax < 0");
            taxCollectorHireCost = reader.ReadShort();
            if (taxCollectorHireCost < 0)
                throw new Exception("Forbidden value on taxCollectorHireCost = " + taxCollectorHireCost + ", it doesn't respect the following condition : taxCollectorHireCost < 0");
            var limit = reader.ReadUShort();
            informations = new Types.TaxCollectorInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 (informations as Types.TaxCollectorInformations[])[i] = Types.ProtocolTypeManager.GetInstance<Types.TaxCollectorInformations>(reader.ReadShort());
                 (informations as Types.TaxCollectorInformations[])[i].Deserialize(reader);
            }
            limit = reader.ReadUShort();
            fightersInformations = new Types.TaxCollectorFightersInformation[limit];
            for (int i = 0; i < limit; i++)
            {
                 (fightersInformations as Types.TaxCollectorFightersInformation[])[i] = new Types.TaxCollectorFightersInformation();
                 (fightersInformations as Types.TaxCollectorFightersInformation[])[i].Deserialize(reader);
            }
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(sbyte) + sizeof(short) + sizeof(short) + informations.Sum(x => sizeof(short) + x.GetSerializationSize()) + sizeof(short) + fightersInformations.Sum(x => x.GetSerializationSize());
        }
        
    }
    
}