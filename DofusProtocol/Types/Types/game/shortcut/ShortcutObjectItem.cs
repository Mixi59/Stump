

// Generated on 04/19/2016 10:17:54
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class ShortcutObjectItem : ShortcutObject
    {
        public const short Id = 371;
        public override short TypeId
        {
            get { return Id; }
        }
        
        public int itemUID;
        public int itemGID;
        
        public ShortcutObjectItem()
        {
        }
        
        public ShortcutObjectItem(sbyte slot, int itemUID, int itemGID)
         : base(slot)
        {
            this.itemUID = itemUID;
            this.itemGID = itemGID;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(itemUID);
            writer.WriteInt(itemGID);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            itemUID = reader.ReadInt();
            itemGID = reader.ReadInt();
        }
        
        
    }
    
}