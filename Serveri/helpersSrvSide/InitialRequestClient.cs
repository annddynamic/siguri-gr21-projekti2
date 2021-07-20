using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveri.helpersSrvSide
{
    [Serializable]
    class InitialRequestClient
    {
        public string call { get; set; }
        public byte[] desIV { get; set; }
        public string desKeyEnc { get; set; }

        public string test { get; set; }
    }
}
    