

// Generated on 08/11/2013 11:28:04
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class ConsoleMessage : Message
    {
        public const uint Id = 75;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public sbyte type;
        public string content;
        
        public ConsoleMessage()
        {
        }
        
        public ConsoleMessage(sbyte type, string content)
        {
            this.type = type;
            this.content = content;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(type);
            writer.WriteUTF(content);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            type = reader.ReadSByte();
            if (type < 0)
                throw new Exception("Forbidden value on type = " + type + ", it doesn't respect the following condition : type < 0");
            content = reader.ReadUTF();
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(sbyte) + sizeof(short) + Encoding.UTF8.GetByteCount(content);
        }
        
    }
    
}