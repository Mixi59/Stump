using System;
using System.Collections.Generic;
using Stump.BaseCore.Framework.Utils;
using Stump.BaseCore.Framework.IO;
using Stump.DofusProtocol.Classes;
namespace Stump.DofusProtocol.Messages
{
	
	public class GameActionFightUnmarkCellsMessage : AbstractGameActionMessage
	{
		public const uint protocolId = 5570;
		internal Boolean _isInitialized = false;
		public int markId = 0;
		
		public GameActionFightUnmarkCellsMessage()
		{
		}
		
		public GameActionFightUnmarkCellsMessage(uint arg1, int arg2, int arg3)
			: this()
		{
			initGameActionFightUnmarkCellsMessage(arg1, arg2, arg3);
		}
		
		public override uint getMessageId()
		{
			return 5570;
		}
		
		public GameActionFightUnmarkCellsMessage initGameActionFightUnmarkCellsMessage(uint arg1 = 0, int arg2 = 0, int arg3 = 0)
		{
			base.initAbstractGameActionMessage(arg1, arg2);
			this.markId = arg3;
			this._isInitialized = true;
			return this;
		}
		
		public override void reset()
		{
			base.reset();
			this.markId = 0;
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
			this.serializeAs_GameActionFightUnmarkCellsMessage(arg1);
		}
		
		public void serializeAs_GameActionFightUnmarkCellsMessage(BigEndianWriter arg1)
		{
			base.serializeAs_AbstractGameActionMessage(arg1);
			arg1.WriteShort((short)this.markId);
		}
		
		public override void deserialize(BigEndianReader arg1)
		{
			this.deserializeAs_GameActionFightUnmarkCellsMessage(arg1);
		}
		
		public void deserializeAs_GameActionFightUnmarkCellsMessage(BigEndianReader arg1)
		{
			base.deserialize(arg1);
			this.markId = (int)arg1.ReadShort();
		}
		
	}
}
