using System;
using System.Collections.Generic;
using Stump.BaseCore.Framework.Utils;
using Stump.BaseCore.Framework.IO;
using Stump.DofusProtocol.Classes;
namespace Stump.DofusProtocol.Messages
{
	
	public class EmotePlayAbstractMessage : Message
	{
		public const uint protocolId = 5690;
		internal Boolean _isInitialized = false;
		public uint emoteId = 0;
		public uint duration = 0;
		
		public EmotePlayAbstractMessage()
		{
		}
		
		public EmotePlayAbstractMessage(uint arg1, uint arg2)
			: this()
		{
			initEmotePlayAbstractMessage(arg1, arg2);
		}
		
		public override uint getMessageId()
		{
			return 5690;
		}
		
		public EmotePlayAbstractMessage initEmotePlayAbstractMessage(uint arg1 = 0, uint arg2 = 0)
		{
			this.emoteId = arg1;
			this.duration = arg2;
			this._isInitialized = true;
			return this;
		}
		
		public override void reset()
		{
			this.emoteId = 0;
			this.duration = 0;
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
			this.serializeAs_EmotePlayAbstractMessage(arg1);
		}
		
		public void serializeAs_EmotePlayAbstractMessage(BigEndianWriter arg1)
		{
			if ( this.emoteId < 0 )
			{
				throw new Exception("Forbidden value (" + this.emoteId + ") on element emoteId.");
			}
			arg1.WriteByte((byte)this.emoteId);
			if ( this.duration < 0 || this.duration > 255 )
			{
				throw new Exception("Forbidden value (" + this.duration + ") on element duration.");
			}
			arg1.WriteByte((byte)this.duration);
		}
		
		public virtual void deserialize(BigEndianReader arg1)
		{
			this.deserializeAs_EmotePlayAbstractMessage(arg1);
		}
		
		public void deserializeAs_EmotePlayAbstractMessage(BigEndianReader arg1)
		{
			this.emoteId = (uint)arg1.ReadByte();
			if ( this.emoteId < 0 )
			{
				throw new Exception("Forbidden value (" + this.emoteId + ") on element of EmotePlayAbstractMessage.emoteId.");
			}
			this.duration = (uint)arg1.ReadByte();
			if ( this.duration < 0 || this.duration > 255 )
			{
				throw new Exception("Forbidden value (" + this.duration + ") on element of EmotePlayAbstractMessage.duration.");
			}
		}
		
	}
}
