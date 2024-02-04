using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UESave.Properties
{
    public class MapProperty : IProperty
    {
        public string key_type;
        public string value_type;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Guid? id;

        public Dictionary<object, object> value;

        public MapProperty(BinaryReader br)
        {
            br.ReadInt64();
            key_type = ReaderUtils.ReadString(br);
            value_type = ReaderUtils.ReadString(br);
            id = ReaderUtils.ReadOptionalGuid(br);
            br.ReadInt32();
            int count = br.ReadInt32();

            value = new Dictionary<object, object>();

            for(int i = 0; i < count; i++)
            {
                // https://github.com/trumank/uesave-rs/blob/master/src/lib.rs#L2063
                if (key_type == "StructProperty")
                {
                    //try
                    //{
                        /*long pos = br.BaseStream.Position;
                        string test = ReaderUtils.ReadString(br);
                        br.BaseStream.Position = pos;*/

                        value.Add(ReaderUtils.ReadPropertyValue(key_type, br), ReaderUtils.ReadPropertyValue(value_type, br));
                    //}
                    //catch (Exception ex)
                    //{
                        //value.Add(ReaderUtils.ReadPropertyValue("Guid", br), ReaderUtils.ReadPropertyValue(value_type, br));
                    //}

                }
                else
                {
                    value.Add(ReaderUtils.ReadPropertyValue(key_type, br), ReaderUtils.ReadPropertyValue(value_type, br));
                }
            }

        }

        public static object Read(BinaryReader br) => new MapProperty(br);

        public string type => "MapProperty";
    }
}
