

// Generated on 03/02/2014 20:42:45
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class GuidedModeQuitRequestMessage : Message
    {
        public const uint Id = 6092;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        
        public GuidedModeQuitRequestMessage()
        {
        }
        
        
        public override void Serialize(IDataWriter writer)
        {
        }
        
        public override void Deserialize(IDataReader reader)
        {
        }
        
        public override int GetSerializationSize()
        {
            return 0;
            ;
        }
        
    }
    
}