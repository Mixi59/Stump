using System;
using System.Collections.Generic;
using Stump.BaseCore.Framework.Utils;
using Stump.BaseCore.Framework.IO;
using Stump.DofusProtocol.Classes;
namespace Stump.DofusProtocol.Messages
{
	
	public class ExchangeObjectAddedMessage : ExchangeObjectMessage
	{
		public const uint protocolId = 5516;
		internal Boolean _isInitialized = false;
		public ObjectItem @object;
		
		public ExchangeObjectAddedMessage()
		{
			this.@object = new ObjectItem();
		}
		
		public ExchangeObjectAddedMessage(Boolean arg1, ObjectItem arg2)
			: this()
		{
			initExchangeObjectAddedMessage(arg1, arg2);
		}
		
		public override uint getMessageId()
		{
			return 5516;
		}
		
		public ExchangeObjectAddedMessage initExchangeObjectAddedMessage(Boolean arg1 = false, ObjectItem arg2 = null)
		{
			base.initExchangeObjectMessage(arg1);
			this.@object = arg2;
			this._isInitialized = true;
			return this;
		}
		
		public override void reset()
		{
			base.reset();
			this.@object = new ObjectItem();
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
			this.serializeAs_ExchangeObjectAddedMessage(arg1);
		}
		
		public void serializeAs_ExchangeObjectAddedMessage(BigEndianWriter arg1)
		{
			base.serializeAs_ExchangeObjectMessage(arg1);
			this.@object.serializeAs_ObjectItem(arg1);
		}
		
		public override void deserialize(BigEndianReader arg1)
		{
			this.deserializeAs_ExchangeObjectAddedMessage(arg1);
		}
		
		public void deserializeAs_ExchangeObjectAddedMessage(BigEndianReader arg1)
		{
			base.deserialize(arg1);
			this.@object = new ObjectItem();
			this.@object.deserialize(arg1);
		}
		
	}
}
