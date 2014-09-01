

// Generated on 09/01/2014 15:52:52
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class IgnoredOnlineInformations : IgnoredInformations
    {
        public const short Id = 105;
        public override short TypeId
        {
            get { return Id; }
        }
        
        public int playerId;
        public string playerName;
        public sbyte breed;
        public bool sex;
        
        public IgnoredOnlineInformations()
        {
        }
        
        public IgnoredOnlineInformations(int accountId, string accountName, int playerId, string playerName, sbyte breed, bool sex)
         : base(accountId, accountName)
        {
            this.playerId = playerId;
            this.playerName = playerName;
            this.breed = breed;
            this.sex = sex;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(playerId);
            writer.WriteUTF(playerName);
            writer.WriteSByte(breed);
            writer.WriteBoolean(sex);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            playerId = reader.ReadInt();
            if (playerId < 0)
                throw new Exception("Forbidden value on playerId = " + playerId + ", it doesn't respect the following condition : playerId < 0");
            playerName = reader.ReadUTF();
            breed = reader.ReadSByte();
            if (breed < (byte)Enums.PlayableBreedEnum.Feca || breed > (byte)Enums.PlayableBreedEnum.Steamer)
                throw new Exception("Forbidden value on breed = " + breed + ", it doesn't respect the following condition : breed < (byte)Enums.PlayableBreedEnum.Feca || breed > (byte)Enums.PlayableBreedEnum.Steamer");
            sex = reader.ReadBoolean();
        }
        
        public override int GetSerializationSize()
        {
            return base.GetSerializationSize() + sizeof(int) + sizeof(short) + Encoding.UTF8.GetByteCount(playerName) + sizeof(sbyte) + sizeof(bool);
        }
        
    }
    
}