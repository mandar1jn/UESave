using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UESave
{
    public interface IReadable
    {

        public static abstract object Read(BinaryReader br);

    }
}
