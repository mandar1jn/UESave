using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UESave
{
    public struct CompressedHeader : IReadable
    {
        public int uncompressedLength;
        public int compressedLength;
        public string magic;
        public byte saveType;

        public CompressedHeader(BinaryReader br)
        {
            uncompressedLength = br.ReadInt32();
            compressedLength = br.ReadInt32();
            magic = Encoding.Default.GetString(br.ReadBytes(3));
            saveType = br.ReadByte();
        }

        public static object Read(BinaryReader br) => new CompressedHeader(br);
    }
}
