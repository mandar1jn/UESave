using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UESave
{
    public class UUID : IReadable
    {
        public Guid uuid;
        public int value;

        public UUID(BinaryReader br)
        {
            uuid = ReaderUtils.ReadGuid(br);
            value = br.ReadInt32();
        }

        public static object Read(BinaryReader br) => new UUID(br);
    }
}
