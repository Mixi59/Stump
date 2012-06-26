// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'CharacterReportMessage.xml' the '26/06/2012 18:48:00'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class CharacterReportMessage : Message
	{
		public const uint Id = 6079;
		public override uint MessageId
		{
			get
			{
				return 6079;
			}
		}
		
		public uint reportedId;
		public sbyte reason;
		
		public CharacterReportMessage()
		{
		}
		
		public CharacterReportMessage(uint reportedId, sbyte reason)
		{
			this.reportedId = reportedId;
			this.reason = reason;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUInt(reportedId);
			writer.WriteSByte(reason);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			reportedId = reader.ReadUInt();
			if ( reportedId < 0 || reportedId > 4294967295 )
			{
				throw new Exception("Forbidden value on reportedId = " + reportedId + ", it doesn't respect the following condition : reportedId < 0 || reportedId > 4294967295");
			}
			reason = reader.ReadSByte();
			if ( reason < 0 )
			{
				throw new Exception("Forbidden value on reason = " + reason + ", it doesn't respect the following condition : reason < 0");
			}
		}
	}
}
