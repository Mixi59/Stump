// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'InventoryPresetItemUpdateMessage.xml' the '04/04/2012 14:27:35'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class InventoryPresetItemUpdateMessage : Message
	{
		public const uint Id = 6168;
		public override uint MessageId
		{
			get
			{
				return 6168;
			}
		}
		
		public sbyte presetId;
		public Types.PresetItem presetItem;
		
		public InventoryPresetItemUpdateMessage()
		{
		}
		
		public InventoryPresetItemUpdateMessage(sbyte presetId, Types.PresetItem presetItem)
		{
			this.presetId = presetId;
			this.presetItem = presetItem;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(presetId);
			presetItem.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			presetId = reader.ReadSByte();
			if ( presetId < 0 )
			{
				throw new Exception("Forbidden value on presetId = " + presetId + ", it doesn't respect the following condition : presetId < 0");
			}
			presetItem = new Types.PresetItem();
			presetItem.Deserialize(reader);
		}
	}
}
