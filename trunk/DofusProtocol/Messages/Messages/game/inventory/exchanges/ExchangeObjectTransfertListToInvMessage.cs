using System;
using System.Collections.Generic;
using Stump.BaseCore.Framework.Utils;
using Stump.BaseCore.Framework.IO;
using Stump.DofusProtocol.Classes;
namespace Stump.DofusProtocol.Messages
{
	
	public class ExchangeObjectTransfertListToInvMessage : Message
	{
		public const uint protocolId = 6039;
		internal Boolean _isInitialized = false;
		public List<uint> ids;
		
		public ExchangeObjectTransfertListToInvMessage()
		{
			this.ids = new List<uint>();
		}
		
		public ExchangeObjectTransfertListToInvMessage(List<uint> arg1)
			: this()
		{
			initExchangeObjectTransfertListToInvMessage(arg1);
		}
		
		public override uint getMessageId()
		{
			return 6039;
		}
		
		public ExchangeObjectTransfertListToInvMessage initExchangeObjectTransfertListToInvMessage(List<uint> arg1)
		{
			this.ids = arg1;
			this._isInitialized = true;
			return this;
		}
		
		public override void reset()
		{
			this.ids = new List<uint>();
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
			this.serializeAs_ExchangeObjectTransfertListToInvMessage(arg1);
		}
		
		public void serializeAs_ExchangeObjectTransfertListToInvMessage(BigEndianWriter arg1)
		{
			arg1.WriteShort((short)this.ids.Count);
			var loc1 = 0;
			while ( loc1 < this.ids.Count )
			{
				if ( this.ids[loc1] < 0 )
				{
					throw new Exception("Forbidden value (" + this.ids[loc1] + ") on element 1 (starting at 1) of ids.");
				}
				arg1.WriteInt((int)this.ids[loc1]);
				++loc1;
			}
		}
		
		public virtual void deserialize(BigEndianReader arg1)
		{
			this.deserializeAs_ExchangeObjectTransfertListToInvMessage(arg1);
		}
		
		public void deserializeAs_ExchangeObjectTransfertListToInvMessage(BigEndianReader arg1)
		{
			var loc3 = 0;
			var loc1 = (ushort)arg1.ReadUShort();
			var loc2 = 0;
			while ( loc2 < loc1 )
			{
				if ( (loc3 = arg1.ReadInt()) < 0 )
				{
					throw new Exception("Forbidden value (" + loc3 + ") on elements of ids.");
				}
				this.ids.Add((uint)loc3);
				++loc2;
			}
		}
		
	}
}
