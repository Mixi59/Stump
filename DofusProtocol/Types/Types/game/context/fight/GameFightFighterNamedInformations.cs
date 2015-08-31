

// Generated on 08/04/2015 00:35:35
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class GameFightFighterNamedInformations : GameFightFighterInformations
    {
        public const short Id = 158;
        public override short TypeId
        {
            get { return Id; }
        }
        
        public string name;
        public Types.PlayerStatus status;
        
        public GameFightFighterNamedInformations()
        {
        }
        
        public GameFightFighterNamedInformations(int contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, sbyte teamId, sbyte wave, bool alive, Types.GameFightMinimalStats stats, IEnumerable<short> previousPositions, string name, Types.PlayerStatus status)
         : base(contextualId, look, disposition, teamId, wave, alive, stats, previousPositions)
        {
            this.name = name;
            this.status = status;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(name);
            status.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            name = reader.ReadUTF();
            status = new Types.PlayerStatus();
            status.Deserialize(reader);
        }
        
        
    }
    
}