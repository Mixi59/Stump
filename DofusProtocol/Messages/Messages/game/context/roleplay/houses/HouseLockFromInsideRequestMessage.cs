

// Generated on 10/30/2016 16:20:31
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class HouseLockFromInsideRequestMessage : LockableChangeCodeMessage
    {
        public const uint Id = 5885;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        
        public HouseLockFromInsideRequestMessage()
        {
        }
        
        public HouseLockFromInsideRequestMessage(string code)
         : base(code)
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }
        
    }
    
}