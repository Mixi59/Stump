

// Generated on 10/30/2016 16:20:26
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class GameFightReadyMessage : Message
    {
        public const uint Id = 708;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public bool isReady;
        
        public GameFightReadyMessage()
        {
        }
        
        public GameFightReadyMessage(bool isReady)
        {
            this.isReady = isReady;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(isReady);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            isReady = reader.ReadBoolean();
        }
        
    }
    
}