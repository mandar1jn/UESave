using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UESave.Properties;

namespace UESave
{
    internal static class PropertyHelper
    {
        private static Type GetPropertyHandler(string  propertyName)
        {
            return propertyName switch
            {
                "IntProperty" => typeof(IntProperty),
                "StructProperty" => typeof(StructProperty),
                "NameProperty" => typeof(NameProperty),
                "EnumProperty" => typeof(EnumProperty),
                "ArrayProperty" => typeof(ArrayProperty),
                "MapProperty" => typeof(MapProperty),
                "StrProperty" => typeof(StrProperty),
                "BoolProperty" => typeof(BoolProperty),
                "Int64Property" => typeof(Int64Property),
                "FloatProperty" => typeof(FloatProperty),
                _ => throw new NotImplementedException($"Unknown property: {propertyName}")
            };
        }

        public static (string, object) ReadProperty(BinaryReader br)
        {
            string name = ReaderUtils.ReadString(br);

            if(name == "None")
            {
                return (name, new object());
            }

            string propertyName = ReaderUtils.ReadString(br);
            Type type = GetPropertyHandler(propertyName);

            object property = type.GetMethod("Read")!.Invoke(null, new object[] { br })!;

            return (name, property);
        }

        public static Dictionary<string, object> ReadProperties(BinaryReader br)
        {
            Dictionary<string, object> properties = new ();

            while(true)
            {
                ValueTuple<string, object> property = ReadProperty(br);

                if (property.Item1 == "None")
                {
                    break;
                }
                /*else if (property.Item1 == "RawData")
                {
                    List<object> bytes = ((ArrayProperty)property.Item2).value;
                    byte[] data = new byte[bytes.Count];
                    for (int i = 0; i < bytes.Count; i++)
                    {
                        data[i] = (byte)bytes[i];
                    }
                    BinaryReader tempReader = new(new MemoryStream(data));
                    while(true)
                    {
                        object prop = ReadProperty(tempReader);
                        Console.WriteLine("a");
                    }
                    Console.WriteLine("h");
                }*/
                else
                {
                    properties.Add(property.Item1, property.Item2);
                    //Console.WriteLine(property.Item1 + " - " + property.Item2);
                }
            }
            return properties;
        }


    }
}
