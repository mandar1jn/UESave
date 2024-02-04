using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UESave.Properties
{
    public class BoolProperty : IProperty
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Guid? id;

        public bool value;

        public BoolProperty(BinaryReader br)
        {
            br.ReadInt64();
            id = ReaderUtils.ReadOptionalGuid(br);
            value = br.ReadBoolean();
        }

        public static object Read(BinaryReader br) => new BoolProperty(br);

        public string type => "BoolProperty";
    }
}
