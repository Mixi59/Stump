

// Generated on 02/18/2015 10:46:31
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class GetPartInfoMessage : Message
    {
        public const uint Id = 1506;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public string id;
        
        public GetPartInfoMessage()
        {
        }
        
        public GetPartInfoMessage(string id)
        {
            this.id = id;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(id);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            id = reader.ReadUTF();
        }
        
    }
    
}