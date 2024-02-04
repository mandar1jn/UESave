using Newtonsoft.Json;
using System.IO.Compression;

namespace UESave
{
    public class UESave
    {
        public CompressedHeader compressedHeader;
        public Header header;
        public Root root;
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<byte> extra = new ();

        public void Load(byte[] data)
        {
            MemoryStream stream = new();

            if (System.Text.Encoding.Default.GetString(data.Skip(8).Take(3).ToArray()) == "PlZ")
            {
                BinaryReader compressedReader = new (new MemoryStream(data));

                compressedHeader = new CompressedHeader(compressedReader);

                if (compressedHeader.saveType == 0x32 || compressedHeader.saveType == 0x31)
                {

                    ZLibStream zStream = new(new MemoryStream(compressedReader.ReadBytes(compressedHeader.compressedLength)), CompressionMode.Decompress, false);

                    zStream.CopyTo(stream);

                    if(compressedHeader.saveType == 0x32)
                    {
                        stream.Seek(0, SeekOrigin.Begin);

                        zStream = new(stream, CompressionMode.Decompress, false);

                        MemoryStream inter = new();

                        zStream.CopyTo(inter);

                        stream = inter;

                    }
                }

            }

            stream.Seek(0, SeekOrigin.Begin);

            BinaryReader br = new(stream);

            header = new Header(br);
            root = new Root(br);

            for (int i = 0; i < br.BaseStream.Length - br.BaseStream.Position; i++)
            {
                extra.Add(br.ReadByte());
            }

        }

        public void Load(string filePath)
        {
            if(File.Exists(filePath))
            {
                byte[] data = File.ReadAllBytes(filePath);

                Load(data);
            }
        }
    }
}