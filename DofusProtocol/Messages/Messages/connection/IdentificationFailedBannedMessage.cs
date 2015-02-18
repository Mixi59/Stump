

// Generated on 02/18/2015 10:46:03
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class IdentificationFailedBannedMessage : IdentificationFailedMessage
    {
        public const uint Id = 6174;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public double banEndDate;
        
        public IdentificationFailedBannedMessage()
        {
        }
        
        public IdentificationFailedBannedMessage(sbyte reason, double banEndDate)
         : base(reason)
        {
            this.banEndDate = banEndDate;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(banEndDate);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            banEndDate = reader.ReadDouble();
            if (banEndDate < 0 || banEndDate > 9.007199254740992E15)
                throw new Exception("Forbidden value on banEndDate = " + banEndDate + ", it doesn't respect the following condition : banEndDate < 0 || banEndDate > 9.007199254740992E15");
        }
        
    }
    
}