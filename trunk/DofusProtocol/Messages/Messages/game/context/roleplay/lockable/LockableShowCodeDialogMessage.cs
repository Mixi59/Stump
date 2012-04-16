// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'LockableShowCodeDialogMessage.xml' the '04/04/2012 14:27:27'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class LockableShowCodeDialogMessage : Message
	{
		public const uint Id = 5740;
		public override uint MessageId
		{
			get
			{
				return 5740;
			}
		}
		
		public bool changeOrUse;
		public sbyte codeSize;
		
		public LockableShowCodeDialogMessage()
		{
		}
		
		public LockableShowCodeDialogMessage(bool changeOrUse, sbyte codeSize)
		{
			this.changeOrUse = changeOrUse;
			this.codeSize = codeSize;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteBoolean(changeOrUse);
			writer.WriteSByte(codeSize);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			changeOrUse = reader.ReadBoolean();
			codeSize = reader.ReadSByte();
			if ( codeSize < 0 )
			{
				throw new Exception("Forbidden value on codeSize = " + codeSize + ", it doesn't respect the following condition : codeSize < 0");
			}
		}
	}
}
