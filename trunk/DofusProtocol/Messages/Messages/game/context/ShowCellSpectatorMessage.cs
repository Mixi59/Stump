// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ShowCellSpectatorMessage.xml' the '26/06/2012 18:47:47'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ShowCellSpectatorMessage : ShowCellMessage
	{
		public const uint Id = 6158;
		public override uint MessageId
		{
			get
			{
				return 6158;
			}
		}
		
		public string playerName;
		
		public ShowCellSpectatorMessage()
		{
		}
		
		public ShowCellSpectatorMessage(int sourceId, short cellId, string playerName)
			 : base(sourceId, cellId)
		{
			this.playerName = playerName;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteUTF(playerName);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			playerName = reader.ReadUTF();
		}
	}
}
