

// Generated on 02/19/2015 12:10:41
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class PaddockAbandonnedInformations : PaddockBuyableInformations
    {
        public const short Id = 133;
        public override short TypeId
        {
            get { return Id; }
        }
        
        public int guildId;
        
        public PaddockAbandonnedInformations()
        {
        }
        
        public PaddockAbandonnedInformations(short maxOutdoorMount, short maxItems, int price, bool locked, int guildId)
         : base(maxOutdoorMount, maxItems, price, locked)
        {
            this.guildId = guildId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(guildId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            guildId = reader.ReadInt();
        }
        
        
    }
    
}