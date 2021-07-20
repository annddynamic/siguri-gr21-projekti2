using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsFormsApp1.Models
{
    [Serializable]

    class InitialRequest
    {
        public string call { get; set; }
        public string desIV { get; set; }
        public  string desKeyEnc { get; set; }

    }
}
