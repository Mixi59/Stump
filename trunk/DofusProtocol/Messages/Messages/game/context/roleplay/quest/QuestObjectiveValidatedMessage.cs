using System;
using System.Collections.Generic;
using Stump.BaseCore.Framework.Utils;
using Stump.BaseCore.Framework.IO;
using Stump.DofusProtocol.Classes;
namespace Stump.DofusProtocol.Messages
{
	
	public class QuestObjectiveValidatedMessage : Message
	{
		public const uint protocolId = 6098;
		internal Boolean _isInitialized = false;
		public uint questId = 0;
		public uint objectiveId = 0;
		
		public QuestObjectiveValidatedMessage()
		{
		}
		
		public QuestObjectiveValidatedMessage(uint arg1, uint arg2)
			: this()
		{
			initQuestObjectiveValidatedMessage(arg1, arg2);
		}
		
		public override uint getMessageId()
		{
			return 6098;
		}
		
		public QuestObjectiveValidatedMessage initQuestObjectiveValidatedMessage(uint arg1 = 0, uint arg2 = 0)
		{
			this.questId = arg1;
			this.@objectiveId = arg2;
			this._isInitialized = true;
			return this;
		}
		
		public override void reset()
		{
			this.questId = 0;
			this.@objectiveId = 0;
			this._isInitialized = false;
		}
		
		public override void pack(BigEndianWriter arg1)
		{
			this.serialize(arg1);
			WritePacket(arg1, this.getMessageId());
		}
		
		public override void unpack(BigEndianReader arg1, uint arg2)
		{
			this.deserialize(arg1);
		}
		
		public virtual void serialize(BigEndianWriter arg1)
		{
			this.serializeAs_QuestObjectiveValidatedMessage(arg1);
		}
		
		public void serializeAs_QuestObjectiveValidatedMessage(BigEndianWriter arg1)
		{
			if ( this.questId < 0 || this.questId > 65535 )
			{
				throw new Exception("Forbidden value (" + this.questId + ") on element questId.");
			}
			arg1.WriteShort((short)this.questId);
			if ( this.@objectiveId < 0 || this.@objectiveId > 65535 )
			{
				throw new Exception("Forbidden value (" + this.@objectiveId + ") on element objectiveId.");
			}
			arg1.WriteShort((short)this.@objectiveId);
		}
		
		public virtual void deserialize(BigEndianReader arg1)
		{
			this.deserializeAs_QuestObjectiveValidatedMessage(arg1);
		}
		
		public void deserializeAs_QuestObjectiveValidatedMessage(BigEndianReader arg1)
		{
			this.questId = (uint)arg1.ReadUShort();
			if ( this.questId < 0 || this.questId > 65535 )
			{
				throw new Exception("Forbidden value (" + this.questId + ") on element of QuestObjectiveValidatedMessage.questId.");
			}
			this.@objectiveId = (uint)arg1.ReadUShort();
			if ( this.@objectiveId < 0 || this.@objectiveId > 65535 )
			{
				throw new Exception("Forbidden value (" + this.@objectiveId + ") on element of QuestObjectiveValidatedMessage.objectiveId.");
			}
		}
		
	}
}
