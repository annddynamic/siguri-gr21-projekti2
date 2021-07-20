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
        public string clientDesIV { get; set; }
        public string clientDesKey { get; set; }
    }
}
