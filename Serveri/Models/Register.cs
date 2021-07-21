using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveri.Models
{
    
   


    

    [Serializable]
    public class Person
    {
        public string emri { get; set; }
        public string mbiemri { get; set; }
        public string username { get; set; }
        public string salt { get; set; }
        public string fjalekalimiHashed { get; set; }

    }

}
