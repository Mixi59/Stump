

// Generated on 07/26/2013 22:51:09
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class DownloadGetCurrentSpeedRequestMessage : Message
    {
        public const uint Id = 1510;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        
        public DownloadGetCurrentSpeedRequestMessage()
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