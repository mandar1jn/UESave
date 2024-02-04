using Newtonsoft.Json;

namespace UESave.Properties
{
    internal class FloatProperty : IProperty
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Guid? id;

        public float value;

        public FloatProperty(BinaryReader br)
        {
            br.ReadInt64();
            id = ReaderUtils.ReadOptionalGuid(br);
            value = br.ReadSingle();
        }

        public static object Read(BinaryReader br) => new FloatProperty(br);

        public string type => "FloatProperty";
    }
}
