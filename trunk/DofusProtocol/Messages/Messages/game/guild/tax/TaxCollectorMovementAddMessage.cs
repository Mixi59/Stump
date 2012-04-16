// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'TaxCollectorMovementAddMessage.xml' the '04/04/2012 14:27:31'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class TaxCollectorMovementAddMessage : Message
	{
		public const uint Id = 5917;
		public override uint MessageId
		{
			get
			{
				return 5917;
			}
		}
		
		public Types.TaxCollectorInformations informations;
		
		public TaxCollectorMovementAddMessage()
		{
		}
		
		public TaxCollectorMovementAddMessage(Types.TaxCollectorInformations informations)
		{
			this.informations = informations;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(informations.TypeId);
			informations.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			informations = Types.ProtocolTypeManager.GetInstance<Types.TaxCollectorInformations>(reader.ReadShort());
			informations.Deserialize(reader);
		}
	}
}
