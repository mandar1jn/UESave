using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UESave.Properties
{
    public class NameProperty : IProperty
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Guid? id;

        public string value;

        public NameProperty(BinaryReader br)
        {
            br.ReadInt64();
            id = ReaderUtils.ReadOptionalGuid(br);
            value = ReaderUtils.ReadString(br);
        }

        public static object Read(BinaryReader br) => new NameProperty(br);

        public string type => "NameProperty";

    }
}
