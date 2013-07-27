

// Generated on 07/26/2013 22:50:49
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class AchievementDetailsMessage : Message
    {
        public const uint Id = 6378;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public Types.Achievement achievement;
        
        public AchievementDetailsMessage()
        {
        }
        
        public AchievementDetailsMessage(Types.Achievement achievement)
        {
            this.achievement = achievement;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            achievement.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            achievement = new Types.Achievement();
            achievement.Deserialize(reader);
        }
        
        public override int GetSerializationSize()
        {
            return achievement.GetSerializationSize();
        }
        
    }
    
}