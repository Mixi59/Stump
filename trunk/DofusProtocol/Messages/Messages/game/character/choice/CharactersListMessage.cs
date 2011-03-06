using System;
using System.Collections.Generic;
using Stump.BaseCore.Framework.Utils;
using Stump.BaseCore.Framework.IO;
using Stump.DofusProtocol.Classes;
namespace Stump.DofusProtocol.Messages
{
	
	public class CharactersListMessage : Message
	{
		public const uint protocolId = 151;
		internal Boolean _isInitialized = false;
		public Boolean hasStartupActions = false;
		public List<CharacterBaseInformations> characters;
		
		public CharactersListMessage()
		{
			this.characters = new List<CharacterBaseInformations>();
		}
		
		public CharactersListMessage(Boolean arg1, List<CharacterBaseInformations> arg2)
			: this()
		{
			initCharactersListMessage(arg1, arg2);
		}
		
		public override uint getMessageId()
		{
			return 151;
		}
		
		public CharactersListMessage initCharactersListMessage(Boolean arg1 = false, List<CharacterBaseInformations> arg2 = null)
		{
			this.hasStartupActions = arg1;
			this.characters = arg2;
			this._isInitialized = true;
			return this;
		}
		
		public override void reset()
		{
			this.hasStartupActions = false;
			this.characters = new List<CharacterBaseInformations>();
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
			this.serializeAs_CharactersListMessage(arg1);
		}
		
		public void serializeAs_CharactersListMessage(BigEndianWriter arg1)
		{
			arg1.WriteBoolean(this.hasStartupActions);
			arg1.WriteShort((short)this.characters.Count);
			var loc1 = 0;
			while ( loc1 < this.characters.Count )
			{
				arg1.WriteShort((short)this.characters[loc1].getTypeId());
				this.characters[loc1].serialize(arg1);
				++loc1;
			}
		}
		
		public virtual void deserialize(BigEndianReader arg1)
		{
			this.deserializeAs_CharactersListMessage(arg1);
		}
		
		public void deserializeAs_CharactersListMessage(BigEndianReader arg1)
		{
			var loc3 = 0;
			object loc4 = null;
			this.hasStartupActions = (Boolean)arg1.ReadBoolean();
			var loc1 = (ushort)arg1.ReadUShort();
			var loc2 = 0;
			while ( loc2 < loc1 )
			{
				loc3 = (ushort)arg1.ReadUShort();
				(( loc4 = ProtocolTypeManager.GetInstance<CharacterBaseInformations>((uint)loc3)) as CharacterBaseInformations).deserialize(arg1);
				this.characters.Add((CharacterBaseInformations)loc4);
				++loc2;
			}
		}
		
	}
}
