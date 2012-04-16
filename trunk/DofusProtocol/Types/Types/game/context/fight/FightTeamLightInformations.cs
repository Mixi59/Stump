// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'FightTeamLightInformations.xml' the '04/04/2012 14:27:37'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class FightTeamLightInformations : AbstractFightTeamInformations
	{
		public const uint Id = 115;
		public override short TypeId
		{
			get
			{
				return 115;
			}
		}
		
		public sbyte teamMembersCount;
		
		public FightTeamLightInformations()
		{
		}
		
		public FightTeamLightInformations(sbyte teamId, int leaderId, sbyte teamSide, sbyte teamTypeId, sbyte teamMembersCount)
			 : base(teamId, leaderId, teamSide, teamTypeId)
		{
			this.teamMembersCount = teamMembersCount;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteSByte(teamMembersCount);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			teamMembersCount = reader.ReadSByte();
			if ( teamMembersCount < 0 )
			{
				throw new Exception("Forbidden value on teamMembersCount = " + teamMembersCount + ", it doesn't respect the following condition : teamMembersCount < 0");
			}
		}
	}
}
