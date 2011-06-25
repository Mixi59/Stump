// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameFightAIInformations.xml' the '24/06/2011 12:04:58'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class GameFightAIInformations : GameFightFighterInformations
	{
		public const uint Id = 151;
		public override short TypeId
		{
			get
			{
				return 151;
			}
		}
		
		
		public GameFightAIInformations()
		{
		}
		
		public GameFightAIInformations(int contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, byte teamId, bool alive, Types.GameFightMinimalStats stats)
			 : base(contextualId, look, disposition, teamId, alive, stats)
		{
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
		}
	}
}