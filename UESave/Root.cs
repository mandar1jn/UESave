using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UESave
{
    public struct Root : IReadable
    {
        public string save_game_type;
        public Dictionary<string, object> properties;

        public Root(BinaryReader br)
        {
            save_game_type = ReaderUtils.ReadString(br);

            properties = PropertyHelper.ReadProperties(br);
        }

        public static object Read(BinaryReader br) => new Root(br);
    }
}
