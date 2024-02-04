using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UESave.Properties
{
    public class EnumProperty : IProperty
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Guid? id;

        public string enum_type;

        public string value;

        public EnumProperty(BinaryReader br)
        {
            br.ReadInt64();
            enum_type = ReaderUtils.ReadString(br);
            id = ReaderUtils.ReadOptionalGuid(br);
            value = ReaderUtils.ReadString(br);
        }

        public static object Read(BinaryReader br) => new EnumProperty(br);

        public string type => "EnumProperty";
    }
}
