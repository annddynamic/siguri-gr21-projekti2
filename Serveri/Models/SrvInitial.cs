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
        public string response { get; set; }
        public string publicKey { get; set; }
        public byte[] clientDesIV { get; set; }
        public byte[] clientDesKey { get; set; }
    }
}
