using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveri.helpersSrvSide
{
    [Serializable]
    class SrvInitial  
    {
        public byte[] publicKey { get; set; }
        public string call { get; set; }
        public byte[] desIV { get; set; }
        public byte[] desKeyEnc { get; set; }
    }
}
