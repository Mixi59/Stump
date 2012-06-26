// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'LockableStateUpdateStorageMessage.xml' the '26/06/2012 18:47:50'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class LockableStateUpdateStorageMessage : LockableStateUpdateAbstractMessage
	{
		public const uint Id = 5669;
		public override uint MessageId
		{
			get
			{
				return 5669;
			}
		}
		
		public int mapId;
		public int elementId;
		
		public LockableStateUpdateStorageMessage()
		{
		}
		
		public LockableStateUpdateStorageMessage(bool locked, int mapId, int elementId)
			 : base(locked)
		{
			this.mapId = mapId;
			this.elementId = elementId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(mapId);
			writer.WriteInt(elementId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			mapId = reader.ReadInt();
			elementId = reader.ReadInt();
			if ( elementId < 0 )
			{
				throw new Exception("Forbidden value on elementId = " + elementId + ", it doesn't respect the following condition : elementId < 0");
			}
		}
	}
}
