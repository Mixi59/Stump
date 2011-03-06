using System;
using System.Collections.Generic;
using Stump.BaseCore.Framework.Utils;
using Stump.BaseCore.Framework.IO;
namespace Stump.DofusProtocol.Classes
{
	
	public class GuildEmblem : Object
	{
		public const uint protocolId = 87;
		public int symbolShape = 0;
		public int symbolColor = 0;
		public int backgroundShape = 0;
		public int backgroundColor = 0;
		
		public GuildEmblem()
		{
		}
		
		public GuildEmblem(int arg1, int arg2, int arg3, int arg4)
			: this()
		{
			initGuildEmblem(arg1, arg2, arg3, arg4);
		}
		
		public virtual uint getTypeId()
		{
			return 87;
		}
		
		public GuildEmblem initGuildEmblem(int arg1 = 0, int arg2 = 0, int arg3 = 0, int arg4 = 0)
		{
			this.symbolShape = arg1;
			this.symbolColor = arg2;
			this.backgroundShape = arg3;
			this.backgroundColor = arg4;
			return this;
		}
		
		public virtual void reset()
		{
			this.symbolShape = 0;
			this.symbolColor = 0;
			this.backgroundShape = 0;
			this.backgroundColor = 0;
		}
		
		public virtual void serialize(BigEndianWriter arg1)
		{
			this.serializeAs_GuildEmblem(arg1);
		}
		
		public void serializeAs_GuildEmblem(BigEndianWriter arg1)
		{
			arg1.WriteShort((short)this.symbolShape);
			arg1.WriteInt((int)this.symbolColor);
			arg1.WriteShort((short)this.backgroundShape);
			arg1.WriteInt((int)this.backgroundColor);
		}
		
		public virtual void deserialize(BigEndianReader arg1)
		{
			this.deserializeAs_GuildEmblem(arg1);
		}
		
		public void deserializeAs_GuildEmblem(BigEndianReader arg1)
		{
			this.symbolShape = (int)arg1.ReadShort();
			this.symbolColor = (int)arg1.ReadInt();
			this.backgroundShape = (int)arg1.ReadShort();
			this.backgroundColor = (int)arg1.ReadInt();
		}
		
	}
}
