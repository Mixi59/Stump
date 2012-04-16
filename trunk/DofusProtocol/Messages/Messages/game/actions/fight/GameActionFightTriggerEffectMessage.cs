// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameActionFightTriggerEffectMessage.xml' the '04/04/2012 14:27:20'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameActionFightTriggerEffectMessage : GameActionFightDispellEffectMessage
	{
		public const uint Id = 6147;
		public override uint MessageId
		{
			get
			{
				return 6147;
			}
		}
		
		
		public GameActionFightTriggerEffectMessage()
		{
		}
		
		public GameActionFightTriggerEffectMessage(short actionId, int sourceId, int targetId, int boostUID)
			 : base(actionId, sourceId, targetId, boostUID)
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
