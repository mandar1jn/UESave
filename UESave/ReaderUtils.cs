using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UESave.Properties;

namespace UESave
{
    internal static class ReaderUtils
    {
        public static string ReadString(BinaryReader br)
        {
            uint length = br.ReadUInt32();

            if(length == 0)
            {
                return "";
            }

            byte[] bytes = br.ReadBytes((int)length);

            return Encoding.Default.GetString(bytes)[..((int)length - 1)];
        }

        public static Guid ReadGuid(BinaryReader br)
        {
            return new Guid(br.ReadBytes(16));
        }
        public static Guid? ReadOptionalGuid(BinaryReader br)
        {
            if(br.ReadByte() > 3)
            {
                return ReadGuid(br);
            }
            return null;
        }

        public static object ReadPropertyValue(string type, BinaryReader br) => type switch
        {
            "NameProperty" => ReadString(br),
            "BoolProperty" => br.ReadBoolean(),
            "IntProperty" => br.ReadInt32(),
            "EnumProperty" => ReadString(br),
            "ByteProperty" => br.ReadByte(),
            "StructProperty" => PropertyHelper.ReadProperties(br),
            "Guid" => ReadGuid(br),
            _ => throw new NotImplementedException($"Value-only read {type} not implmented"),
        };

        public static List<T> ReadList<T>(BinaryReader br)
        {
            if(!typeof(T).IsAssignableTo(typeof(IReadable)))
            {
                throw new Exception("Type should implement IReader");
            }

            uint length = br.ReadUInt32();

            List<T> list = new ();

            for(uint i = 0; i < length; i++)
            {
                IReadable res = (IReadable)typeof(T).GetMethod("Read")!.Invoke(null, new[] { br })!;

                list.Add((T)res);
            }

            return list;
            
        }
    }
}
