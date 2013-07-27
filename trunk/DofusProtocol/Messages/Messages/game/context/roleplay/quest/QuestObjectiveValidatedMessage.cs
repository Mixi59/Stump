

// Generated on 07/26/2013 22:51:00
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class QuestObjectiveValidatedMessage : Message
    {
        public const uint Id = 6098;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public ushort questId;
        public ushort objectiveId;
        
        public QuestObjectiveValidatedMessage()
        {
        }
        
        public QuestObjectiveValidatedMessage(ushort questId, ushort objectiveId)
        {
            this.questId = questId;
            this.objectiveId = objectiveId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUShort(questId);
            writer.WriteUShort(objectiveId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            questId = reader.ReadUShort();
            if (questId < 0 || questId > 65535)
                throw new Exception("Forbidden value on questId = " + questId + ", it doesn't respect the following condition : questId < 0 || questId > 65535");
            objectiveId = reader.ReadUShort();
            if (objectiveId < 0 || objectiveId > 65535)
                throw new Exception("Forbidden value on objectiveId = " + objectiveId + ", it doesn't respect the following condition : objectiveId < 0 || objectiveId > 65535");
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(ushort) + sizeof(ushort);
        }
        
    }
    
}