

// Generated on 02/19/2015 12:09:22
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class GameActionFightLifePointsLostMessage : AbstractGameActionMessage
    {
        public const uint Id = 6312;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public int targetId;
        public short loss;
        public short permanentDamages;
        
        public GameActionFightLifePointsLostMessage()
        {
        }
        
        public GameActionFightLifePointsLostMessage(short actionId, int sourceId, int targetId, short loss, short permanentDamages)
         : base(actionId, sourceId)
        {
            this.targetId = targetId;
            this.loss = loss;
            this.permanentDamages = permanentDamages;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(targetId);
            writer.WriteVarShort(loss);
            writer.WriteVarShort(permanentDamages);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            targetId = reader.ReadInt();
            loss = reader.ReadVarShort();
            if (loss < 0)
                throw new Exception("Forbidden value on loss = " + loss + ", it doesn't respect the following condition : loss < 0");
            permanentDamages = reader.ReadVarShort();
            if (permanentDamages < 0)
                throw new Exception("Forbidden value on permanentDamages = " + permanentDamages + ", it doesn't respect the following condition : permanentDamages < 0");
        }
        
    }
    
}