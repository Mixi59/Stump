

// Generated on 09/01/2014 15:52:51
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class GameRolePlayNpcWithQuestInformations : GameRolePlayNpcInformations
    {
        public const short Id = 383;
        public override short TypeId
        {
            get { return Id; }
        }
        
        public Types.GameRolePlayNpcQuestFlag questFlag;
        
        public GameRolePlayNpcWithQuestInformations()
        {
        }
        
        public GameRolePlayNpcWithQuestInformations(int contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, short npcId, bool sex, short specialArtworkId, Types.GameRolePlayNpcQuestFlag questFlag)
         : base(contextualId, look, disposition, npcId, sex, specialArtworkId)
        {
            this.questFlag = questFlag;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            questFlag.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            questFlag = new Types.GameRolePlayNpcQuestFlag();
            questFlag.Deserialize(reader);
        }
        
        public override int GetSerializationSize()
        {
            return base.GetSerializationSize() + questFlag.GetSerializationSize();
        }
        
    }
    
}