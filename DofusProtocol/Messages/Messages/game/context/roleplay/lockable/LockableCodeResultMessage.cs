

// Generated on 02/18/2015 10:46:15
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class LockableCodeResultMessage : Message
    {
        public const uint Id = 5672;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public sbyte result;
        
        public LockableCodeResultMessage()
        {
        }
        
        public LockableCodeResultMessage(sbyte result)
        {
            this.result = result;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(result);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            result = reader.ReadSByte();
            if (result < 0)
                throw new Exception("Forbidden value on result = " + result + ", it doesn't respect the following condition : result < 0");
        }
        
    }
    
}