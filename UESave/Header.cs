using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UESave
{

    public struct EngineVersion
    {
        public ushort major;
        public ushort minor;
        public ushort patch;
        public uint build;
        public string version;
    }

    public struct Header : IReadable
    {
        public string magic;
        public uint saveGameVersion;
        public ulong packageVersion;
        public EngineVersion engineVersion;
        public uint customFormatVersion;
        public List<UUID> customFormat;

        public Header(BinaryReader br)
        {
            magic = Encoding.Default.GetString(br.ReadBytes(4));
            saveGameVersion = br.ReadUInt32();

            if(saveGameVersion < 3)
            {
                packageVersion = br.ReadUInt32();
            }
            else
            {
                packageVersion = br.ReadUInt64();
            }

            engineVersion = new()
            {
                major = br.ReadUInt16(),
                minor = br.ReadUInt16(),
                patch = br.ReadUInt16(),
                build = br.ReadUInt32(),
                version = ReaderUtils.ReadString(br)
            };

            customFormatVersion = br.ReadUInt32();

            customFormat = ReaderUtils.ReadList<UUID>(br);

        }

        public static object Read(BinaryReader br) => new Header(br);
    }
}
