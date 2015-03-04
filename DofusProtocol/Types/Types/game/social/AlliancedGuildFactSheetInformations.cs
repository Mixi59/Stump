

// Generated on 02/19/2015 12:10:41
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class AlliancedGuildFactSheetInformations : GuildInformations
    {
        public const short Id = 422;
        public override short TypeId
        {
            get { return Id; }
        }
        
        public Types.BasicNamedAllianceInformations allianceInfos;
        
        public AlliancedGuildFactSheetInformations()
        {
        }
        
        public AlliancedGuildFactSheetInformations(int guildId, string guildName, Types.GuildEmblem guildEmblem, Types.BasicNamedAllianceInformations allianceInfos)
         : base(guildId, guildName, guildEmblem)
        {
            this.allianceInfos = allianceInfos;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            allianceInfos.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            allianceInfos = new Types.BasicNamedAllianceInformations();
            allianceInfos.Deserialize(reader);
        }
        
        
    }
    
}