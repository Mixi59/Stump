using System;
using System.Collections.Generic;
using Stump.BaseCore.Framework.Utils;
using Stump.BaseCore.Framework.IO;
using Stump.DofusProtocol.Classes;
namespace Stump.DofusProtocol.Messages
{
	
	public class GameDataPaddockObjectAddMessage : Message
	{
		public const uint protocolId = 5990;
		internal Boolean _isInitialized = false;
		public PaddockItem paddockItemDescription;
		
		public GameDataPaddockObjectAddMessage()
		{
			this.paddockItemDescription = new PaddockItem();
		}
		
		public GameDataPaddockObjectAddMessage(PaddockItem arg1)
			: this()
		{
			initGameDataPaddockObjectAddMessage(arg1);
		}
		
		public override uint getMessageId()
		{
			return 5990;
		}
		
		public GameDataPaddockObjectAddMessage initGameDataPaddockObjectAddMessage(PaddockItem arg1 = null)
		{
			this.paddockItemDescription = arg1;
			this._isInitialized = true;
			return this;
		}
		
		public override void reset()
		{
			this.paddockItemDescription = new PaddockItem();
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
			this.serializeAs_GameDataPaddockObjectAddMessage(arg1);
		}
		
		public void serializeAs_GameDataPaddockObjectAddMessage(BigEndianWriter arg1)
		{
			this.paddockItemDescription.serializeAs_PaddockItem(arg1);
		}
		
		public virtual void deserialize(BigEndianReader arg1)
		{
			this.deserializeAs_GameDataPaddockObjectAddMessage(arg1);
		}
		
		public void deserializeAs_GameDataPaddockObjectAddMessage(BigEndianReader arg1)
		{
			this.paddockItemDescription = new PaddockItem();
			this.paddockItemDescription.deserialize(arg1);
		}
		
	}
}
