using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveri.Models
{

    [Serializable]

    class SrvInitial
    {
        public string publicKey { get; set; }
        public byte[] desIV { get; set; }
        public byte[] desKeyEnc { get; set; }
    }
}
