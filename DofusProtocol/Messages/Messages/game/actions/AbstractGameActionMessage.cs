

// Generated on 08/04/2015 00:36:52
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class AbstractGameActionMessage : Message
    {
        public const uint Id = 1000;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public short actionId;
        public int sourceId;
        
        public AbstractGameActionMessage()
        {
        }
        
        public AbstractGameActionMessage(short actionId, int sourceId)
        {
            this.actionId = actionId;
            this.sourceId = sourceId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarShort(actionId);
            writer.WriteInt(sourceId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            actionId = reader.ReadVarShort();
            if (actionId < 0)
                throw new Exception("Forbidden value on actionId = " + actionId + ", it doesn't respect the following condition : actionId < 0");
            sourceId = reader.ReadInt();
        }
        
    }
    
}