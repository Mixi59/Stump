// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameRolePlayArenaRegisterMessage.xml' the '04/04/2012 14:27:26'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameRolePlayArenaRegisterMessage : Message
	{
		public const uint Id = 6280;
		public override uint MessageId
		{
			get
			{
				return 6280;
			}
		}
		
		public int battleMode;
		
		public GameRolePlayArenaRegisterMessage()
		{
		}
		
		public GameRolePlayArenaRegisterMessage(int battleMode)
		{
			this.battleMode = battleMode;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(battleMode);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			battleMode = reader.ReadInt();
			if ( battleMode < 0 )
			{
				throw new Exception("Forbidden value on battleMode = " + battleMode + ", it doesn't respect the following condition : battleMode < 0");
			}
		}
	}
}
