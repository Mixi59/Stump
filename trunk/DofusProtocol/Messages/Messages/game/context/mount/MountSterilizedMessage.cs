// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'MountSterilizedMessage.xml' the '26/06/2012 18:47:49'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class MountSterilizedMessage : Message
	{
		public const uint Id = 5977;
		public override uint MessageId
		{
			get
			{
				return 5977;
			}
		}
		
		public double mountId;
		
		public MountSterilizedMessage()
		{
		}
		
		public MountSterilizedMessage(double mountId)
		{
			this.mountId = mountId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteDouble(mountId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			mountId = reader.ReadDouble();
		}
	}
}
