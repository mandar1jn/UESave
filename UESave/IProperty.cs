using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UESave
{
    public interface IProperty : IReadable
    {
        public abstract string type { get; }
    }
}
