// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameActionFightLifeAndShieldPointsLostMessage.xml' the '26/06/2012 18:47:44'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameActionFightLifeAndShieldPointsLostMessage : GameActionFightLifePointsLostMessage
	{
		public const uint Id = 6310;
		public override uint MessageId
		{
			get
			{
				return 6310;
			}
		}
		
		public short shieldLoss;
		
		public GameActionFightLifeAndShieldPointsLostMessage()
		{
		}
		
		public GameActionFightLifeAndShieldPointsLostMessage(short actionId, int sourceId, int targetId, short loss, short permanentDamages, short shieldLoss)
			 : base(actionId, sourceId, targetId, loss, permanentDamages)
		{
			this.shieldLoss = shieldLoss;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteShort(shieldLoss);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			shieldLoss = reader.ReadShort();
			if ( shieldLoss < 0 )
			{
				throw new Exception("Forbidden value on shieldLoss = " + shieldLoss + ", it doesn't respect the following condition : shieldLoss < 0");
			}
		}
	}
}
