// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'IdentificationMessage.xml' the '04/04/2012 14:27:19'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class IdentificationMessage : Message
	{
		public const uint Id = 4;
		public override uint MessageId
		{
			get
			{
				return 4;
			}
		}
		
		public bool autoconnect;
		public bool useCertificate;
		public bool useLoginToken;
		public Types.Version version;
		public string lang;
		public string login;
		public IEnumerable<sbyte> credentials;
		public short serverId;
		
		public IdentificationMessage()
		{
		}
		
		public IdentificationMessage(bool autoconnect, bool useCertificate, bool useLoginToken, Types.Version version, string lang, string login, IEnumerable<sbyte> credentials, short serverId)
		{
			this.autoconnect = autoconnect;
			this.useCertificate = useCertificate;
			this.useLoginToken = useLoginToken;
			this.version = version;
			this.lang = lang;
			this.login = login;
			this.credentials = credentials;
			this.serverId = serverId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			byte flag1 = 0;
			flag1 = BooleanByteWrapper.SetFlag(flag1, 0, autoconnect);
			flag1 = BooleanByteWrapper.SetFlag(flag1, 1, useCertificate);
			flag1 = BooleanByteWrapper.SetFlag(flag1, 2, useLoginToken);
			writer.WriteByte(flag1);
			version.Serialize(writer);
			writer.WriteUTF(lang);
			writer.WriteUTF(login);
			writer.WriteUShort((ushort)credentials.Count());
			foreach (var entry in credentials)
			{
				writer.WriteSByte(entry);
			}
			writer.WriteShort(serverId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			byte flag1 = reader.ReadByte();
			autoconnect = BooleanByteWrapper.GetFlag(flag1, 0);
			useCertificate = BooleanByteWrapper.GetFlag(flag1, 1);
			useLoginToken = BooleanByteWrapper.GetFlag(flag1, 2);
			version = new Types.Version();
			version.Deserialize(reader);
			lang = reader.ReadUTF();
			login = reader.ReadUTF();
			int limit = reader.ReadUShort();
			credentials = new sbyte[limit];
			for (int i = 0; i < limit; i++)
			{
				(credentials as sbyte[])[i] = reader.ReadSByte();
			}
			serverId = reader.ReadShort();
		}
	}
}
