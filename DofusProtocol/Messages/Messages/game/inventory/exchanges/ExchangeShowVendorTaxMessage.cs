

// Generated on 08/04/2015 00:37:20
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class ExchangeShowVendorTaxMessage : Message
    {
        public const uint Id = 5783;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        
        public ExchangeShowVendorTaxMessage()
        {
        }
        
        
        public override void Serialize(IDataWriter writer)
        {
        }
        
        public override void Deserialize(IDataReader reader)
        {
        }
        
    }
    
}