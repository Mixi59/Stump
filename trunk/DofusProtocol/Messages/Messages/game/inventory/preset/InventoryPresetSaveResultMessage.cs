// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'InventoryPresetSaveResultMessage.xml' the '26/06/2012 18:47:59'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class InventoryPresetSaveResultMessage : Message
	{
		public const uint Id = 6170;
		public override uint MessageId
		{
			get
			{
				return 6170;
			}
		}
		
		public sbyte presetId;
		public sbyte code;
		
		public InventoryPresetSaveResultMessage()
		{
		}
		
		public InventoryPresetSaveResultMessage(sbyte presetId, sbyte code)
		{
			this.presetId = presetId;
			this.code = code;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(presetId);
			writer.WriteSByte(code);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			presetId = reader.ReadSByte();
			if ( presetId < 0 )
			{
				throw new Exception("Forbidden value on presetId = " + presetId + ", it doesn't respect the following condition : presetId < 0");
			}
			code = reader.ReadSByte();
			if ( code < 0 )
			{
				throw new Exception("Forbidden value on code = " + code + ", it doesn't respect the following condition : code < 0");
			}
		}
	}
}
