

// Generated on 09/01/2014 15:51:49
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class DebugClearHighlightCellsMessage : Message
    {
        public const uint Id = 2002;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        
        public DebugClearHighlightCellsMessage()
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