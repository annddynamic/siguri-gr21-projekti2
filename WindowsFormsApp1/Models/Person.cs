using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{  
    [Serializable]
    class Person
    {
        public string emri { get; set; }
        public string mbiemri { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string fjalekalimi { get; set; }

    }
}
