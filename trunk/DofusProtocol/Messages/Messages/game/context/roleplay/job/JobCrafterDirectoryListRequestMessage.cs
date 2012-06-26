// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'JobCrafterDirectoryListRequestMessage.xml' the '26/06/2012 18:47:50'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class JobCrafterDirectoryListRequestMessage : Message
	{
		public const uint Id = 6047;
		public override uint MessageId
		{
			get
			{
				return 6047;
			}
		}
		
		public sbyte jobId;
		
		public JobCrafterDirectoryListRequestMessage()
		{
		}
		
		public JobCrafterDirectoryListRequestMessage(sbyte jobId)
		{
			this.jobId = jobId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(jobId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			jobId = reader.ReadSByte();
			if ( jobId < 0 )
			{
				throw new Exception("Forbidden value on jobId = " + jobId + ", it doesn't respect the following condition : jobId < 0");
			}
		}
	}
}
