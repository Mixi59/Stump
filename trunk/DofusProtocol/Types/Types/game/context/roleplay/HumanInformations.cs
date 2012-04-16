// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'HumanInformations.xml' the '04/04/2012 14:27:37'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Types
{
	public class HumanInformations
	{
		public const uint Id = 157;
		public virtual short TypeId
		{
			get
			{
				return 157;
			}
		}
		
		public IEnumerable<Types.EntityLook> followingCharactersLook;
		public sbyte emoteId;
		public double emoteStartTime;
		public Types.ActorRestrictionsInformations restrictions;
		public short titleId;
		public string titleParam;
		
		public HumanInformations()
		{
		}
		
		public HumanInformations(IEnumerable<Types.EntityLook> followingCharactersLook, sbyte emoteId, double emoteStartTime, Types.ActorRestrictionsInformations restrictions, short titleId, string titleParam)
		{
			this.followingCharactersLook = followingCharactersLook;
			this.emoteId = emoteId;
			this.emoteStartTime = emoteStartTime;
			this.restrictions = restrictions;
			this.titleId = titleId;
			this.titleParam = titleParam;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)followingCharactersLook.Count());
			foreach (var entry in followingCharactersLook)
			{
				entry.Serialize(writer);
			}
			writer.WriteSByte(emoteId);
			writer.WriteDouble(emoteStartTime);
			restrictions.Serialize(writer);
			writer.WriteShort(titleId);
			writer.WriteUTF(titleParam);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			followingCharactersLook = new Types.EntityLook[limit];
			for (int i = 0; i < limit; i++)
			{
				(followingCharactersLook as EntityLook[])[i] = new Types.EntityLook();
				(followingCharactersLook as Types.EntityLook[])[i].Deserialize(reader);
			}
			emoteId = reader.ReadSByte();
			emoteStartTime = reader.ReadDouble();
			restrictions = new Types.ActorRestrictionsInformations();
			restrictions.Deserialize(reader);
			titleId = reader.ReadShort();
			if ( titleId < 0 )
			{
				throw new Exception("Forbidden value on titleId = " + titleId + ", it doesn't respect the following condition : titleId < 0");
			}
			titleParam = reader.ReadUTF();
		}
	}
}
