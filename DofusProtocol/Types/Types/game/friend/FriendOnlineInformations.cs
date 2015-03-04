

// Generated on 02/19/2015 12:10:40
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class FriendOnlineInformations : FriendInformations
    {
        public const short Id = 92;
        public override short TypeId
        {
            get { return Id; }
        }
        
        public int playerId;
        public string playerName;
        public byte level;
        public sbyte alignmentSide;
        public sbyte breed;
        public bool sex;
        public Types.BasicGuildInformations guildInfo;
        public sbyte moodSmileyId;
        public Types.PlayerStatus status;
        
        public FriendOnlineInformations()
        {
        }
        
        public FriendOnlineInformations(int accountId, string accountName, sbyte playerState, short lastConnection, int achievementPoints, int playerId, string playerName, byte level, sbyte alignmentSide, sbyte breed, bool sex, Types.BasicGuildInformations guildInfo, sbyte moodSmileyId, Types.PlayerStatus status)
         : base(accountId, accountName, playerState, lastConnection, achievementPoints)
        {
            this.playerId = playerId;
            this.playerName = playerName;
            this.level = level;
            this.alignmentSide = alignmentSide;
            this.breed = breed;
            this.sex = sex;
            this.guildInfo = guildInfo;
            this.moodSmileyId = moodSmileyId;
            this.status = status;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarInt(playerId);
            writer.WriteUTF(playerName);
            writer.WriteByte(level);
            writer.WriteSByte(alignmentSide);
            writer.WriteSByte(breed);
            writer.WriteBoolean(sex);
            guildInfo.Serialize(writer);
            writer.WriteSByte(moodSmileyId);
            writer.WriteShort(status.TypeId);
            status.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            playerId = reader.ReadVarInt();
            if (playerId < 0)
                throw new Exception("Forbidden value on playerId = " + playerId + ", it doesn't respect the following condition : playerId < 0");
            playerName = reader.ReadUTF();
            level = reader.ReadByte();
            if (level < 0 || level > 200)
                throw new Exception("Forbidden value on level = " + level + ", it doesn't respect the following condition : level < 0 || level > 200");
            alignmentSide = reader.ReadSByte();
            breed = reader.ReadSByte();
            if (breed < (byte)Enums.PlayableBreedEnum.Feca || breed > (byte)Enums.PlayableBreedEnum.Eliotrope)
                throw new Exception("Forbidden value on breed = " + breed + ", it doesn't respect the following condition : breed < (byte)Enums.PlayableBreedEnum.Feca || breed > (byte)Enums.PlayableBreedEnum.Eliotrope");
            sex = reader.ReadBoolean();
            guildInfo = new Types.BasicGuildInformations();
            guildInfo.Deserialize(reader);
            moodSmileyId = reader.ReadSByte();
            status = Types.ProtocolTypeManager.GetInstance<Types.PlayerStatus>(reader.ReadShort());
            status.Deserialize(reader);
        }
        
        
    }
    
}