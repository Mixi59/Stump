// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PaddockAbandonnedInformations.xml' the '04/04/2012 14:27:39'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class PaddockAbandonnedInformations : PaddockBuyableInformations
	{
		public const uint Id = 133;
		public override short TypeId
		{
			get
			{
				return 133;
			}
		}
		
		public int guildId;
		
		public PaddockAbandonnedInformations()
		{
		}
		
		public PaddockAbandonnedInformations(short maxOutdoorMount, short maxItems, int price, bool locked, int guildId)
			 : base(maxOutdoorMount, maxItems, price, locked)
		{
			this.guildId = guildId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(guildId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			guildId = reader.ReadInt();
		}
	}
}
