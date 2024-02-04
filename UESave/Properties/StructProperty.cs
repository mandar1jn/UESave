using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace UESave.Properties
{
    public class StructProperty : IProperty
    {
        public string struct_type;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? struct_override;

        public Guid struct_id;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Guid? id;

        public object value;

        public StructProperty(BinaryReader br)
        {
            long size = br.ReadInt64();

            struct_type = ReaderUtils.ReadString(br);

            struct_id = ReaderUtils.ReadGuid(br);

            id = ReaderUtils.ReadOptionalGuid(br);

            value = ReadStructValue(struct_type, br, size);
        }

        public static object ReadStructValue(string type, BinaryReader br, long size)
        {
            switch (type)
            {
                case "DateTime":
                    return br.ReadUInt64();
                case "Guid":
                    return ReaderUtils.ReadGuid(br);
                case "Vector":
                    return new Dictionary<string, double>()
                    {
                        { "x", br.ReadDouble() },
                        { "y", br.ReadDouble() },
                        { "z", br.ReadDouble() },
                    };
                case "Quat":
                    return new Dictionary<string, double>()
                    {
                        { "x", br.ReadDouble() },
                        { "y", br.ReadDouble() },
                        { "z", br.ReadDouble() },
                        { "w", br.ReadDouble() },
                    };
                case "LinearColor":
                    return new Dictionary<string, double>()
                    {
                        { "r", br.ReadSingle() },
                        { "g", br.ReadSingle() },
                        { "b", br.ReadSingle() },
                        { "a", br.ReadSingle() },
                    };
                default:
                    if (size == 16)
                    {
                        Console.WriteLine($"Unknown struct type \"{type}\". Assuming Guid.");
                        return ReaderUtils.ReadGuid(br);
                    }
                    else
                    {
                        Console.WriteLine($"Unknown struct type \"{type}\". Assuming Struct.");
                        return PropertyHelper.ReadProperties(br);
                    }
            };
        }

        public string type => "StructProperty";

        public static object Read(BinaryReader br) => new StructProperty(br);
    }
}
