// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameActionFightCloseCombatMessage.xml' the '26/06/2012 18:47:44'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameActionFightCloseCombatMessage : AbstractGameActionFightTargetedAbilityMessage
	{
		public const uint Id = 6116;
		public override uint MessageId
		{
			get
			{
				return 6116;
			}
		}
		
		public int weaponGenericId;
		
		public GameActionFightCloseCombatMessage()
		{
		}
		
		public GameActionFightCloseCombatMessage(short actionId, int sourceId, int targetId, short destinationCellId, sbyte critical, bool silentCast, int weaponGenericId)
			 : base(actionId, sourceId, targetId, destinationCellId, critical, silentCast)
		{
			this.weaponGenericId = weaponGenericId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(weaponGenericId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			weaponGenericId = reader.ReadInt();
			if ( weaponGenericId < 0 )
			{
				throw new Exception("Forbidden value on weaponGenericId = " + weaponGenericId + ", it doesn't respect the following condition : weaponGenericId < 0");
			}
		}
	}
}
