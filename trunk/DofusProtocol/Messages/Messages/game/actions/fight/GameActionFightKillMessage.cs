using System;
using System.Collections.Generic;
using Stump.BaseCore.Framework.Utils;
using Stump.BaseCore.Framework.IO;
using Stump.DofusProtocol.Classes;
namespace Stump.DofusProtocol.Messages
{
	
	public class GameActionFightKillMessage : AbstractGameActionMessage
	{
		public const uint protocolId = 5571;
		internal Boolean _isInitialized = false;
		public int targetId = 0;
		
		public GameActionFightKillMessage()
		{
		}
		
		public GameActionFightKillMessage(uint arg1, int arg2, int arg3)
			: this()
		{
			initGameActionFightKillMessage(arg1, arg2, arg3);
		}
		
		public override uint getMessageId()
		{
			return 5571;
		}
		
		public GameActionFightKillMessage initGameActionFightKillMessage(uint arg1 = 0, int arg2 = 0, int arg3 = 0)
		{
			base.initAbstractGameActionMessage(arg1, arg2);
			this.targetId = arg3;
			this._isInitialized = true;
			return this;
		}
		
		public override void reset()
		{
			base.reset();
			this.targetId = 0;
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
		
		public override void serialize(BigEndianWriter arg1)
		{
			this.serializeAs_GameActionFightKillMessage(arg1);
		}
		
		public void serializeAs_GameActionFightKillMessage(BigEndianWriter arg1)
		{
			base.serializeAs_AbstractGameActionMessage(arg1);
			arg1.WriteInt((int)this.targetId);
		}
		
		public override void deserialize(BigEndianReader arg1)
		{
			this.deserializeAs_GameActionFightKillMessage(arg1);
		}
		
		public void deserializeAs_GameActionFightKillMessage(BigEndianReader arg1)
		{
			base.deserialize(arg1);
			this.targetId = (int)arg1.ReadInt();
		}
		
	}
}
