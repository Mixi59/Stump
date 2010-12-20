using System;
using System.Collections.Generic;
using Stump.BaseCore.Framework.Utils;
using Stump.BaseCore.Framework.IO;
using Stump.DofusProtocol.Classes;
namespace Stump.DofusProtocol.Messages
{
	
	public class GuildInvitationStateRecruterMessage : Message
	{
		public const uint protocolId = 5563;
		internal Boolean _isInitialized = false;
		public String recrutedName = "";
		public uint invitationState = 0;
		
		public GuildInvitationStateRecruterMessage()
		{
		}
		
		public GuildInvitationStateRecruterMessage(String arg1, uint arg2)
			: this()
		{
			initGuildInvitationStateRecruterMessage(arg1, arg2);
		}
		
		public override uint getMessageId()
		{
			return 5563;
		}
		
		public GuildInvitationStateRecruterMessage initGuildInvitationStateRecruterMessage(String arg1 = "", uint arg2 = 0)
		{
			this.recrutedName = arg1;
			this.invitationState = arg2;
			this._isInitialized = true;
			return this;
		}
		
		public override void reset()
		{
			this.recrutedName = "";
			this.invitationState = 0;
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
			this.serializeAs_GuildInvitationStateRecruterMessage(arg1);
		}
		
		public void serializeAs_GuildInvitationStateRecruterMessage(BigEndianWriter arg1)
		{
			arg1.WriteUTF((string)this.recrutedName);
			arg1.WriteByte((byte)this.invitationState);
		}
		
		public virtual void deserialize(BigEndianReader arg1)
		{
			this.deserializeAs_GuildInvitationStateRecruterMessage(arg1);
		}
		
		public void deserializeAs_GuildInvitationStateRecruterMessage(BigEndianReader arg1)
		{
			this.recrutedName = (String)arg1.ReadUTF();
			this.invitationState = (uint)arg1.ReadByte();
			if ( this.invitationState < 0 )
			{
				throw new Exception("Forbidden value (" + this.invitationState + ") on element of GuildInvitationStateRecruterMessage.invitationState.");
			}
		}
		
	}
}
