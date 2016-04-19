

// Generated on 04/19/2016 10:17:12
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class BasicWhoIsMessage : Message
    {
        public const uint Id = 180;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public bool self;
        public bool verbose;
        public sbyte position;
        public string accountNickname;
        public int accountId;
        public string playerName;
        public long playerId;
        public short areaId;
        public short serverId;
        public IEnumerable<Types.AbstractSocialGroupInfos> socialGroups;
        public sbyte playerState;
        
        public BasicWhoIsMessage()
        {
        }
        
        public BasicWhoIsMessage(bool self, bool verbose, sbyte position, string accountNickname, int accountId, string playerName, long playerId, short areaId, short serverId, IEnumerable<Types.AbstractSocialGroupInfos> socialGroups, sbyte playerState)
        {
            this.self = self;
            this.verbose = verbose;
            this.position = position;
            this.accountNickname = accountNickname;
            this.accountId = accountId;
            this.playerName = playerName;
            this.playerId = playerId;
            this.areaId = areaId;
            this.serverId = serverId;
            this.socialGroups = socialGroups;
            this.playerState = playerState;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            byte flag1 = 0;
            flag1 = BooleanByteWrapper.SetFlag(flag1, 0, self);
            flag1 = BooleanByteWrapper.SetFlag(flag1, 1, verbose);
            writer.WriteByte(flag1);
            writer.WriteSByte(position);
            writer.WriteUTF(accountNickname);
            writer.WriteInt(accountId);
            writer.WriteUTF(playerName);
            writer.WriteVarLong(playerId);
            writer.WriteShort(areaId);
            writer.WriteShort(serverId);
            var socialGroups_before = writer.Position;
            var socialGroups_count = 0;
            writer.WriteUShort(0);
            foreach (var entry in socialGroups)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
                 socialGroups_count++;
            }
            var socialGroups_after = writer.Position;
            writer.Seek((int)socialGroups_before);
            writer.WriteUShort((ushort)socialGroups_count);
            writer.Seek((int)socialGroups_after);

            writer.WriteSByte(playerState);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            byte flag1 = reader.ReadByte();
            self = BooleanByteWrapper.GetFlag(flag1, 0);
            verbose = BooleanByteWrapper.GetFlag(flag1, 1);
            position = reader.ReadSByte();
            accountNickname = reader.ReadUTF();
            accountId = reader.ReadInt();
            if (accountId < 0)
                throw new Exception("Forbidden value on accountId = " + accountId + ", it doesn't respect the following condition : accountId < 0");
            playerName = reader.ReadUTF();
            playerId = reader.ReadVarLong();
            if (playerId < 0 || playerId > 9007199254740990)
                throw new Exception("Forbidden value on playerId = " + playerId + ", it doesn't respect the following condition : playerId < 0 || playerId > 9007199254740990");
            areaId = reader.ReadShort();
            serverId = reader.ReadShort();
            var limit = reader.ReadUShort();
            var socialGroups_ = new Types.AbstractSocialGroupInfos[limit];
            for (int i = 0; i < limit; i++)
            {
                 socialGroups_[i] = Types.ProtocolTypeManager.GetInstance<Types.AbstractSocialGroupInfos>(reader.ReadShort());
                 socialGroups_[i].Deserialize(reader);
            }
            socialGroups = socialGroups_;
            playerState = reader.ReadSByte();
            if (playerState < 0)
                throw new Exception("Forbidden value on playerState = " + playerState + ", it doesn't respect the following condition : playerState < 0");
        }
        
    }
    
}