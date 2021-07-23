using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    [Serializable]

    public class faturaRequest
    {
        public string call { get; set; }
        public Fatura fatura;

    }

    [Serializable]
    public class Fatura
    {
        public string lloji { get; set; }
        public string viti { get; set; }
        public string muaji { get; set; }
        public string vleraEuro { get; set; }
        public string vleraPaTVSH { get; set; }

    }
}
