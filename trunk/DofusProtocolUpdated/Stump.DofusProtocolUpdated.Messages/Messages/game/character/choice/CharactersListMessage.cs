

// Generated on 03/05/2014 20:34:22
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class CharactersListMessage : BasicCharactersListMessage
    {
        public const uint Id = 151;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public bool hasStartupActions;
        
        public CharactersListMessage()
        {
        }
        
        public CharactersListMessage(IEnumerable<Types.CharacterBaseInformations> characters, bool hasStartupActions)
         : base(characters)
        {
            this.hasStartupActions = hasStartupActions;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean(hasStartupActions);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            hasStartupActions = reader.ReadBoolean();
        }
        
        public override int GetSerializationSize()
        {
            return base.GetSerializationSize() + sizeof(bool);
        }
        
    }
    
}