

// Generated on 07/26/2013 22:51:04
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class ExchangeMountFreeFromPaddockMessage : Message
    {
        public const uint Id = 6055;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public string name;
        public short worldX;
        public short worldY;
        public string liberator;
        
        public ExchangeMountFreeFromPaddockMessage()
        {
        }
        
        public ExchangeMountFreeFromPaddockMessage(string name, short worldX, short worldY, string liberator)
        {
            this.name = name;
            this.worldX = worldX;
            this.worldY = worldY;
            this.liberator = liberator;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(name);
            writer.WriteShort(worldX);
            writer.WriteShort(worldY);
            writer.WriteUTF(liberator);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            name = reader.ReadUTF();
            worldX = reader.ReadShort();
            if (worldX < -255 || worldX > 255)
                throw new Exception("Forbidden value on worldX = " + worldX + ", it doesn't respect the following condition : worldX < -255 || worldX > 255");
            worldY = reader.ReadShort();
            if (worldY < -255 || worldY > 255)
                throw new Exception("Forbidden value on worldY = " + worldY + ", it doesn't respect the following condition : worldY < -255 || worldY > 255");
            liberator = reader.ReadUTF();
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(short) + name.Length + sizeof(short) + sizeof(short) + sizeof(short) + liberator.Length;
        }
        
    }
    
}