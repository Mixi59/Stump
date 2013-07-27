

// Generated on 07/26/2013 22:50:50
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class GameActionFightStateChangeMessage : AbstractGameActionMessage
    {
        public const uint Id = 5569;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public int targetId;
        public short stateId;
        public bool active;
        
        public GameActionFightStateChangeMessage()
        {
        }
        
        public GameActionFightStateChangeMessage(short actionId, int sourceId, int targetId, short stateId, bool active)
         : base(actionId, sourceId)
        {
            this.targetId = targetId;
            this.stateId = stateId;
            this.active = active;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(targetId);
            writer.WriteShort(stateId);
            writer.WriteBoolean(active);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            targetId = reader.ReadInt();
            stateId = reader.ReadShort();
            active = reader.ReadBoolean();
        }
        
        public override int GetSerializationSize()
        {
            return base.GetSerializationSize() + sizeof(int) + sizeof(short) + sizeof(bool);
        }
        
    }
    
}