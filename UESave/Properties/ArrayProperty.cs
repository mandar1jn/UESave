using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml.Linq;

namespace UESave.Properties
{
    public struct StructInfo
    {
        public string _type;
        public string name;
        public string struct_type;
        internal long _size;
        public Guid id;
    }

    public class ArrayProperty : IProperty
    {
        public string array_type;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Guid? id;

        public List<object> value;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public StructInfo? struct_info;

        public ArrayProperty(BinaryReader br)
        {
            value = new List<object>();

            br.ReadInt64();
            array_type = ReaderUtils.ReadString(br);
            id = ReaderUtils.ReadOptionalGuid(br);

            uint count = br.ReadUInt32();

            if (array_type == "StructProperty")
            {
                struct_info = new()
                {
                    _type = ReaderUtils.ReadString(br),
                    name = ReaderUtils.ReadString(br),
                    _size = br.ReadInt64(),
                    struct_type = ReaderUtils.ReadString(br),
                    id = ReaderUtils.ReadGuid(br)
                };

                br.ReadByte();
                
                for(uint i = 0; i < count; i++)
                {
                    value.Add(StructProperty.ReadStructValue(struct_info.Value.struct_type, br, struct_info.Value._size));
                }
            }
            else
            {
                for (uint i = 0; i < count; i++)
                {
                    value.Add(ReaderUtils.ReadPropertyValue(array_type, br));
                }
            }
        }

        public static object Read(BinaryReader br) => new ArrayProperty(br);

        public string type => "ArrayProperty";
    }
}
