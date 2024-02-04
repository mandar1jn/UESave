using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UESave.Properties
{
    public struct IntProperty : IProperty
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Guid? id;
        public int value;

        public IntProperty(BinaryReader br)
        {
            br.ReadInt64();
            id = ReaderUtils.ReadOptionalGuid(br);
            value = br.ReadInt32();
        }

        public readonly string type => "IntProperty";

        public static object Read(BinaryReader br) => new IntProperty(br);
    }
}
