// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameRolePlayCharacterInformations.xml' the '04/04/2012 14:27:37'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class GameRolePlayCharacterInformations : GameRolePlayHumanoidInformations
	{
		public const uint Id = 36;
		public override short TypeId
		{
			get
			{
				return 36;
			}
		}
		
		public Types.ActorAlignmentInformations alignmentInfos;
		
		public GameRolePlayCharacterInformations()
		{
		}
		
		public GameRolePlayCharacterInformations(int contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, string name, Types.HumanInformations humanoidInfo, Types.ActorAlignmentInformations alignmentInfos)
			 : base(contextualId, look, disposition, name, humanoidInfo)
		{
			this.alignmentInfos = alignmentInfos;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			alignmentInfos.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			alignmentInfos = new Types.ActorAlignmentInformations();
			alignmentInfos.Deserialize(reader);
		}
	}
}
